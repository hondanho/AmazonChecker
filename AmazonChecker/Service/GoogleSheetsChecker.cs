using AmazonChecker.Model;
using AmazonChecker.Service;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.GetRequest;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.UpdateRequest;

namespace AmazonChecker.CommonHelper
{
    public class GoogleSheetsChecker: BaseChecker
    {
        private static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string APPLICATION_NAME = "Quickstart";
        private string SHEETS_NAME = "";
        private string SPREADSHEET_ID = "";
        private static string CREDENTIAL_PATH = AppDomain.CurrentDomain.BaseDirectory + "credentials.json";
        private static string TOKEN_PATH = AppDomain.CurrentDomain.BaseDirectory + "token.json";
        private SheetsService _service;

        public GoogleSheetsChecker(string linkGoogleSheet)
        {
            if (!File.Exists(CREDENTIAL_PATH))
            {
                throw new Exception($"File credentials.json không tồn tại. Hãy copy file  credentials.json vào folder: \"{AppDomain.CurrentDomain.BaseDirectory}\"");
            }
            try
            {
                //UserCredential credential;
                //using (var stream = new FileStream(CREDENTIAL_PATH, FileMode.Open, FileAccess.Read))
                //{
                //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //        GoogleClientSecrets.Load(stream).Secrets,
                //        Scopes,
                //        "user",
                //        CancellationToken.None,
                //        new FileDataStore(TOKEN_PATH, true)).Result;
                //}

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                var json = File.ReadAllText(CREDENTIAL_PATH);
                var cr = JsonConvert.DeserializeObject<PersonalServiceAccountCred>(json, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });

                // Create an explicit ServiceAccountCredential credential
                var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(cr.ClientEmail)
                {
                    Scopes = Scopes
                }.FromPrivateKey(cr.PrivateKey));

                this._service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred,
                    ApplicationName = APPLICATION_NAME,
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"File credentials.json không hợp lệ. Hãy thay file credentials.json vào folder: \"{AppDomain.CurrentDomain.BaseDirectory}\"", ex);
            }

            this.FirstSheetDatas = GetDatasChecker(linkGoogleSheet);
        }

        public override List<List<object>> GetDatasChecker(string linkGoogleSheet)
        {
            var result = new List<List<object>>();
            try
            {
                this.SPREADSHEET_ID = Regex.Match(linkGoogleSheet, @"(?<=https:\/\/docs\.google\.com\/spreadsheets\/d\/)(.*)(?=\/edit)").Value;
                if (string.IsNullOrEmpty(linkGoogleSheet) || string.IsNullOrEmpty(this.SPREADSHEET_ID))
                {
                    return result;
                }

                var requestSheetName = this._service.Spreadsheets.Get(this.SPREADSHEET_ID);
                var resSheetName = requestSheetName.Execute();
                if (resSheetName != null)
                {
                    this.SHEETS_NAME = resSheetName.Sheets[0].Properties.Title;
                    var request = this._service.Spreadsheets.Values.Get(this.SPREADSHEET_ID, SHEETS_NAME);
                    request.MajorDimension = MajorDimensionEnum.ROWS;

                    ValueRange response = request.Execute();
                    foreach (var res in response.Values)
                    {
                        result.Add(new List<object>(res));
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is GoogleApiException)
                {
                    var e = (GoogleApiException)ex;
                    if (e.Error.Code == (int)HttpStatusCode.Forbidden)
                    {
                        throw new Exception("Không có quyền đọc ghi vào file google sheets này", e);
                    }
                }

                throw new Exception(ex.Message);
            }

            return result;
        }

        public override bool Save()
        {
            try
            {
                var requestBody = new ValueRange();
                requestBody.Values = new List<IList<object>>();
                foreach (var data in this.FirstSheetDatas)
                {
                    requestBody.Values.Add(data);
                }
                var request = this._service.Spreadsheets.Values.Update(requestBody, this.SPREADSHEET_ID, SHEETS_NAME);
                request.ValueInputOption = ValueInputOptionEnum.RAW;

                var response = request.Execute();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Lưu data vào google sheet bị lỗi, hãy thử lại sau.", ex);
            }
        }
    }
}
