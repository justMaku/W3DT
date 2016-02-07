using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace W3DT
{
    public class Alert
    {
        public static void Show(string message, bool success = false)
        {
            MessageBox.Show(message, success ? "Success" : "Error", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
    }
}
