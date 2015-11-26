using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using W3DT.Runners;
using W3DT.Events;

namespace W3DT
{
    public partial class CDNSearchWindow : Form
    {
        public CDNSearchWindow()
        {
            InitializeComponent();
            new RunnerCDNCheck().Begin();
        }
    }
}
