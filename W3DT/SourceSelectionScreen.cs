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
    public partial class SourceSelectionScreen : Form
    {
        private bool selectionDone = false;
        private SplashScreen splash;

        public SourceSelectionScreen(SplashScreen splash)
        {
            InitializeComponent();
            this.splash = splash;
        }

        private void UI_SourceOptions_Done_Click(object sender, EventArgs e)
        {
            selectionDone = true;

            Program.Settings.UseRemote = UI_SourceOption_Remote.Checked;
            Program.Settings.ShowSourceSelector = false;
            Program.Settings.Persist();

            splash.OnSourceSelectionDone();
            Close();
        }

        private void SourceSelectionScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            // If this window is closed without a selection, just terminate.
            if (!selectionDone)
                Application.Exit();
        }
    }
}
