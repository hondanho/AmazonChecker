using AmazonChecker.Model;
using AmazonChecker.Service;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.GetRequest;

namespace AmazonChecker.CommonHelper
{
    public class ExcelChecker : BaseChecker
    {
        public override List<List<SheetModel>> GetDatasChecker(string resourceString)
        {
            var result = new List<List<SheetModel>>();
            if (string.IsNullOrEmpty(resourceString) || !File.Exists(resourceString))
            {
                Notify.Warning("File không tồn tại hoặc không hợp lệ!");
                return result;
            }

            FileInfo file = new FileInfo(resourceString);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.First();

                for (int i = 1; i <= excelWorksheet.Dimension.End.Row; i++)
                {
                    var columns = new List<SheetModel>();
                    for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        columns.Add(new SheetModel(excelWorksheet.Cells[i, 4].Value));
                    }

                    result.Add(columns);
                }
            }

            return result;
        }
        public override void UpdateSheet(int i, int j, object value)
        {
            this.ListDataChecker[i][j].Value = value;
        }
        public override void SetColor(int i, int j, System.Drawing.Color color)
        {
            this.ListDataChecker[i][j].Color = color;
        }

        public override bool Save()
        {
        }
    }
}
