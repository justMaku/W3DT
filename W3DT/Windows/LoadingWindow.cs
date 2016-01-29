using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace W3DT
{
    public partial class LoadingWindow : Form
    {
        private Action callback;

        public LoadingWindow(string firstLine, string secondLine, bool cancelButton = false, Action cancelCallback = null)
        {
            InitializeComponent();
            UI_TextLine1.Text = firstLine;
            UI_TextLine2.Text = secondLine;

            UI_CancelButton.Visible = cancelButton;
            if (cancelButton)
                callback = cancelCallback;
        }

        private void UI_CancelButton_Click(object sender, EventArgs e)
        {
            if (callback != null)
                callback();

            Close();
        }
    }
}
