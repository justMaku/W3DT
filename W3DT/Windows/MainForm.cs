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
    public partial class MainForm : Form
    {
        private SettingsForm W_Settings;

        public MainForm()
        {
            InitializeComponent();
            Focus();
        }

        private void UI_SecBtn_Settings_Click(object sender, EventArgs e)
        {
            if (W_Settings == null || W_Settings.IsDisposed)
                W_Settings = new SettingsForm();

            W_Settings.ShowDialog();
        }
    }
}
