using AmazonChecker.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonChecker.Service
{
    abstract public class BaseChecker
    {
        public List<List<SheetModel>> ListDataChecker { get; set; }

        public abstract List<List<SheetModel>> GetDatasChecker(string resourceString);
        public abstract void UpdateSheet(int x, int y, object value);
        public abstract void SetColor(int x, int y, Color color);
        public abstract bool Save();
    }
}
