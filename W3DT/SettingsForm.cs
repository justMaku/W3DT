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
    public partial class SettingsForm : Form, ISourceSelectionParent
    {
        private SourceSelectionScreen sourceScreen;

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            UI_Info_CurrVersion.Text = string.Format(Constants.CURRENT_VERSION_STRING, Program.Version);

            string host = Program.Settings.UseRemote ? Program.Settings.RemoteHost : "N/A";
            UI_Info_RemoteHost.Text = string.Format(Constants.CDN_HOST_STRING, host);
        }

        private void LoadSettings()
        {
            UI_AutomaticUpdates.Checked = Program.Settings.AutomaticUpdates;
        }

        private void UI_SaveButton_Click(object sender, EventArgs e)
        {
            Program.Settings.AutomaticUpdates = UI_AutomaticUpdates.Checked;
            Program.Settings.Persist();
            Close();
        }

        private void UI_DiscardButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UI_DataSourceButton_Click(object sender, EventArgs e)
        {
            if (sourceScreen == null || sourceScreen.IsDisposed)
                sourceScreen = new SourceSelectionScreen(this);

            sourceScreen.ShowDialog();
        }

        public void OnSourceSelectionDone()
        {
            UpdateInfo();
        }
    }
}
