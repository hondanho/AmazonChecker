using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmazonChecker.CommonHelper
{
    public static class Notify
    {
        static public void Warning(string s)
        {
            MessageBox.Show(s, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static public void Info(string s)
        {
            MessageBox.Show(s, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
