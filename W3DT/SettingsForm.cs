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

            if (!sourceScreen.Visible)
                sourceScreen.Show();

            sourceScreen.Focus();
        }

        public void OnSourceSelectionDone()
        {
            // stub
        }
    }
}
