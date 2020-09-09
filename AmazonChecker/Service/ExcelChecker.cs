using AmazonChecker.Service;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AmazonChecker.CommonHelper
{
    public class ExcelChecker : BaseChecker
    {
        private string pathFileExcel { get; set; }

        public ExcelChecker(string pathFileExcel) : base()
        {
            this.pathFileExcel = pathFileExcel;
            this.FirstSheetDatas = GetDatasChecker(pathFileExcel);
        }

        public override List<List<object>> GetDatasChecker(string resourceString)
        {
            var result = new List<List<object>>();
            if (string.IsNullOrEmpty(resourceString) || !File.Exists(resourceString))
            {
                return result;
            }

            FileInfo file = new FileInfo(resourceString);

            using (var excelPakage = new ExcelPackage(file))
            {
                var rowIndexStart = excelPakage.Workbook.Worksheets.First().Dimension.Start.Row;
                var rowIndexEnd = excelPakage.Workbook.Worksheets.First().Dimension.End.Row;
                
                for (int i = rowIndexStart; i <= rowIndexEnd; i++)
                {
                    var columns = new List<object>();
                    var columnIndexStart = excelPakage.Workbook.Worksheets.First().Dimension.Start.Column;
                    var columnIndexEnd = excelPakage.Workbook.Worksheets.First().Dimension.End.Column;

                    for (int j = columnIndexStart; j <= columnIndexEnd; j++)
                    {
                        columns.Add(excelPakage.Workbook.Worksheets.First().Cells[i, j].Value);
                    }

                    result.Add(columns);
                }
            }

            return result;
        }

        public override bool Save()
        {
            try
            {
                FileInfo file = new FileInfo(this.pathFileExcel);
                using (var excelPakage = new ExcelPackage(file))
                {
                    var rowIndexStart = excelPakage.Workbook.Worksheets.First().Dimension.Start.Row;
                    for (int i = 0; i < this.FirstSheetDatas.Count; i++)
                    {
                        var columnIndexStart = excelPakage.Workbook.Worksheets.First().Dimension.Start.Column;
                        for (int j = 0; j < this.FirstSheetDatas[i].Count; j++)
                        {
                            excelPakage.Workbook.Worksheets.First().Cells[i + rowIndexStart, j + columnIndexStart].Value = this.FirstSheetDatas[i][j];
                        }
                    }
                    excelPakage.Save();
                }

                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }
    }
}
