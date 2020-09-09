using AmazonChecker.Service;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.GetRequest;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.UpdateRequest;

namespace AmazonChecker.CommonHelper
{
    public class GoogleSheetsChecker: BaseChecker
    {
        private static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string APPLICATION_NAME = "AmazonChecker";
        private const string SHEETS_NAME = "Products";
        private string SPREADSHEET_ID = "";
        private static string CREDENTIAL_PATH = AppDomain.CurrentDomain.BaseDirectory + "credentials.json";
        private static string TOKEN_PATH = AppDomain.CurrentDomain.BaseDirectory + "token.json";
        private SheetsService _service;

        public GoogleSheetsChecker(string linkGoogleSheet)
        {
            UserCredential credential;
            using (var stream = new FileStream(CREDENTIAL_PATH, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(TOKEN_PATH, true)).Result;
            }

            this._service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME,
            });

            this.FirstSheetDatas = GetDatasChecker(linkGoogleSheet);
        }

        public override List<List<object>> GetDatasChecker(string linkGoogleSheet)
        {
            var result = new List<List<object>>();
            if (string.IsNullOrEmpty(linkGoogleSheet))
            {
                return result;
            }
            this.SPREADSHEET_ID = Regex.Match(linkGoogleSheet, @"(?<=https:\/\/docs\.google\.com\/spreadsheets\/d\/)(.*)(?=\/edit)").Value;
            var request = this._service.Spreadsheets.Values.Get(this.SPREADSHEET_ID, SHEETS_NAME);
            request.MajorDimension = MajorDimensionEnum.ROWS;
            request = this._service.Spreadsheets.Values.Get(this.SPREADSHEET_ID, SHEETS_NAME);
            ValueRange response = request.Execute();
            foreach (var res in response.Values)
            {
                result.Add(new List<object>(res));
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
            }
            return false;
        }
    }
}
