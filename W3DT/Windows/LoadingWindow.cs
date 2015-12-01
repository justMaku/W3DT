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
        public LoadingWindow(string firstLine, string secondLine)
        {
            InitializeComponent();
            UI_TextLine1.Text = firstLine;
            UI_TextLine2.Text = secondLine;
        }
    }
}
