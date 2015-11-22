using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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

            UI_DirectoryField.Text = Constants.DIRECTORY_PLACEHOLDER;
        }

        private void UI_SourceOptions_Done_Click(object sender, EventArgs e)
        {
            string selectedDirectory = UI_DirectoryField.Text;

            // Ensure the user didn't leave the field "blank".
            if (selectedDirectory == string.Empty || selectedDirectory == Constants.DIRECTORY_PLACEHOLDER || selectedDirectory == null)
            {
                ShowBalloon("There must always be, a Lich K-- .. I mean, this can't be blank!", UI_DirectorySelectButton);
                return;
            }

            // Ensure the given directory actually exists.
            if (!Directory.Exists(selectedDirectory))
            {
                ShowBalloon("The directory you selected could not be found, even by Brann Bronzebeard!", UI_DirectorySelectButton);
                return;
            }

            // Rough check to see if we picked a valid installation.
            string buildFile = Path.Combine(selectedDirectory, Constants.WOW_BUILD_FILE);
            if (!File.Exists(buildFile))
            {
                ShowBalloon("That does not appear to be a valid World of Warcraft installation..", UI_DirectorySelectButton);
                return;
            }

            selectionDone = true;

            Program.Settings.UseRemote = UI_SourceOption_Remote.Checked;
            Program.Settings.ShowSourceSelector = false;
            Program.Settings.WoWDirectory = selectedDirectory;
            Program.Settings.Persist();

            splash.OnSourceSelectionDone();
            Close();
        }

        private void ShowBalloon(string text, Control control)
        {
            FileErrorTooltip.Show(string.Empty, control, 0); // Call this to fix positioning.
            FileErrorTooltip.Show(text, control, 10000);
        }

        private void SourceSelectionScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            // If this window is closed without a selection, just terminate.
            if (!selectionDone)
                Application.Exit();
        }

        private void OnSourceOptionRadioChanged(object sender, EventArgs e)
        {
            UI_DirectoryField.Enabled = UI_SourceOption_Local.Checked;
        }

        private void UI_DirectorySelectButton_Click(object sender, EventArgs e)
        {
            if (FileBrowserDialog.ShowDialog() == DialogResult.OK)
                UI_DirectoryField.Text = FileBrowserDialog.SelectedPath;
        }
    }
}
