using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.GetRequest;

namespace AmazonChecker.CommonHelper
{
    public class GoogleSheetsChecker
    {
        private static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string APPLICATION_NAME = "AmazonChecker";
        private const string SHEETS_NAME = "Products";

        private const string SPREADSHEET_ID = "1dMZ-uHTSq8Q9hZRz5pyAfw6T8JxaMHpA7eqbvRpQYgQ";
        
        private static string CREDENTIAL_PATH = AppDomain.CurrentDomain.BaseDirectory + "credentials.json";
        private static string TOKEN_PATH = AppDomain.CurrentDomain.BaseDirectory + "token.json";

        public static void Execute()
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

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME,
            });

            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SPREADSHEET_ID, SHEETS_NAME);
            request.MajorDimension = MajorDimensionEnum.ROWS;


            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Name, Major");
                foreach (var row in values)
                {
                    Console.WriteLine("{0}, {1}", row[0], row[4]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }
    }
}
