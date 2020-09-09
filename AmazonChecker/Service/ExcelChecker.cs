using AmazonChecker.Service;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AmazonChecker.CommonHelper
{
    public class ExcelChecker : BaseChecker
    {
        public ExcelPackage _excelPackage { get; set; }
        public ExcelWorksheet _excelWorkSheet
        {
            get
            {
                if (this._excelPackage != null)
                {
                    return _excelPackage.Workbook.Worksheets.First();
                }
                else return null;
            }
            set {
            }
        }

        public ExcelChecker(string pathFileExcel) : base()
        {
            this.FirstSheetDatas = GetDatasChecker(pathFileExcel);
        }

        public override List<List<object>> GetDatasChecker(string resourceString)
        {
            var result = new List<List<object>>();
            if (string.IsNullOrEmpty(resourceString) || !File.Exists(resourceString))
            {
                Notify.Warning("File không tồn tại hoặc không hợp lệ!");
                return result;
            }

            FileInfo file = new FileInfo(resourceString);

            this._excelPackage = new ExcelPackage(file);
            for (int i = 1; i <= this._excelWorkSheet.Dimension.End.Row; i++)
            {
                var columns = new List<object>();
                for (int j = this._excelWorkSheet.Dimension.Start.Column; j <= this._excelWorkSheet.Dimension.End.Column; j++)
                {
                    columns.Add(this._excelWorkSheet.Cells[i, j].Value);
                }

                result.Add(columns);
            }

            return result;
        }
        public override void UpdateCells(int i, int j, object value)
        {
            if (this._excelWorkSheet != null)
            {
                this._excelWorkSheet.Cells[i, j].Value = value;
            }
        }

        public override void SetColor(int i, int j, System.Drawing.Color color)
        {
            if (this._excelWorkSheet != null)
            {
                this._excelWorkSheet.Cells[i, j].Style.Font.Color.SetColor(color);
            }
        }

        public override bool Save()
        {
            try
            {
                this._excelPackage.Save();
                return true;
            }
            catch
            {
            }

            return false;
        }
    }
}
