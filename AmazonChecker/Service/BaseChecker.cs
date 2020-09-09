using System.Collections.Generic;
using System.Drawing;

namespace AmazonChecker.Service
{
    abstract public class BaseChecker
    {
        public List<List<object>> FirstSheetDatas { get; set; }
        public abstract List<List<object>> GetDatasChecker(string resourceString);
        public abstract void UpdateCells(int x, int y, object value);
        public abstract void SetColor(int x, int y, Color color);
        public abstract bool Save();
    }
}
