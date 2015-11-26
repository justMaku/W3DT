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
        private SourceSelectionScreen parent;

        public CDNSearchWindow(SourceSelectionScreen parent)
        {
            InitializeComponent();
            this.parent = parent;

            new RunnerCDNCheck().Begin();
        }
    }
}
