using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonChecker.Model
{
    public class SheetModel
    {
        public object Value { get; set; }
        public Color Color { get; set; }

        public SheetModel(object value, Color color)
        {
            Value = value;
            Color = color;
        }

        public SheetModel(object value)
        {
            Value = value;
        }
    }
}
