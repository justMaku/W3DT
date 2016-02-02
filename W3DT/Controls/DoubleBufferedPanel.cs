using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace W3DT.Controls
{
    public partial class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
    }
}
