using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using W3DT.CASC;
using W3DT.Events;
using W3DT.Runners;

namespace W3DT
{
    public partial class SettingsForm : Form, ISourceSelectionParent
    {
        private SourceSelectionScreen sourceScreen;
        private LoadingWindow loadingWindow;
        public bool ShouldRebootCASC = false;
        private bool loaded = false;

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
            UpdateInfo();
            PopulateLocaleList();

            loaded = true;
        }

        private void UpdateInfo()
        {
            UI_Info_CurrVersion.Text = string.Format(Constants.CURRENT_VERSION_STRING, Program.Version);

            if (Program.Settings.UseRemote)
                UI_Info_DataSource.Text = string.Format(Constants.CDN_HOST_STRING, Program.Settings.RemoteHost);
            else
                UI_Info_DataSource.Text = string.Format(Constants.WOW_DIRECTORY_STRING, Program.Settings.WoWDirectory);
        }

        private void PopulateLocaleList()
        {
            Locale userLocale = Locale.GetUserLocale();
            foreach (Locale locale in Locale.Values)
            {
                UI_FileLocale_Field.Items.Add(locale);
                if (locale.ID.Equals(userLocale.ID))
                    UI_FileLocale_Field.SelectedIndex = UI_FileLocale_Field.Items.IndexOf(locale);
            }
        }

        private void LoadSettings()
        {
            UI_AutomaticUpdates.Checked = Program.Settings.AutomaticUpdates;

            if (Program.Settings.UseRemote)
            {
                ComboBox field = UI_RemoteVersion_Field;
                field.Enabled = true;
                field.Items.Clear();

                foreach (WoWVersion version in Constants.WOW_VERSIONS)
                {
                    field.Items.Add(version);
                    WoWVersion defaultVersion = Program.Settings.RemoteClientVersion;

                    if (defaultVersion != null && defaultVersion.Equals(version))
                        field.SelectedIndex = field.Items.IndexOf(version);
                }                
            }
            else
            {
                UI_RemoteVersion_Field.Enabled = false;
            }
        }

        private void UI_SaveButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UI_DataSourceButton_Click(object sender, EventArgs e)
        {
            ShouldRebootCASC = false;
            if (sourceScreen == null || sourceScreen.IsDisposed)
                sourceScreen = new SourceSelectionScreen(this, false);

            sourceScreen.ShowDialog();
        }

        public void OnSourceSelectionDone()
        {
            UpdateInfo();

            if (ShouldRebootCASC)
                RebootCASC();
        }

        private void RebootCASC()
        {
            // Delete compiled listfile cache.
            if (File.Exists(Constants.LIST_FILE_BIN))
                File.Delete(Constants.LIST_FILE_BIN);

            ShouldRebootCASC = false;
            EventManager.CASCLoadDone += OnCASCLoadDone;
            new RunnerInitializeCASC().Begin();
            loadingWindow = new LoadingWindow("Reinitializing CASC file engine...", "Non-interesting fact: C'Thun does not like cherry ice cream.");
            loadingWindow.ShowDialog();
        }

        private void OnCASCLoadDone(object sender, EventArgs e)
        {
            EventManager.CASCLoadDone -= OnCASCLoadDone;
            if (loadingWindow != null)
            {
                loadingWindow.Close();
                loadingWindow = null;
            }

            if (!((CASCLoadDoneArgs)e).Success)
                throw new Exception("CASC engine blew up.");
        }

        private void UI_AutomaticUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AutomaticUpdates = UI_AutomaticUpdates.Checked;
            Program.Settings.Persist();
        }

        private void UI_RemoteVersion_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selected = UI_RemoteVersion_Field.SelectedItem;

            if (selected != null && selected is WoWVersion)
            {
                Program.Settings.RemoteClientVersion = (WoWVersion)selected;
                Program.Settings.Persist();

                if (loaded)
                    RebootCASC();
            }
        }

        private void UI_FileLocale_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selected = UI_FileLocale_Field.SelectedItem;

            if (selected != null && selected is Locale)
            {
                Program.Settings.FileLocale = ((Locale)selected).ID;
                Program.Settings.Persist();

                if (loaded)
                    RebootCASC();
            }
        }
    }
}
