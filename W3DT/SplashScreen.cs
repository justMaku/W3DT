using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using W3DT.Events;
using W3DT.Runners;
using W3DT.JSONContainers;

namespace W3DT
{
    public partial class SplashScreen : Form
    {
        private bool isDoneLoading = false;
        public static string UPDATE_PACKAGE_NAME = "update.zip";

        public SplashScreen()
        {
            InitializeComponent();
            EventManager.H_UpdateCheckComplete += OnUpdateCheckComplete;

            // Check for and remove leftover update package.
            try
            {
                if (File.Exists(UPDATE_PACKAGE_NAME))
                    File.Delete(UPDATE_PACKAGE_NAME);
            }
            catch (Exception ex)
            {
                // File locked, or something. Brazenly continue onwards.
                Debug.WriteLine("Unable to delete leftover package file (" + ex.GetType().Name + ")");
                Debug.WriteLine("Exception info: " + ex.Message);
            }

            new RunnerUpdateCheck().Begin();
        }

        public void OnUpdateCheckComplete(LatestReleaseData data)
        {
            // Check our update data.
            if (data.message != null)
            {
                // If a message is set, it was some kind of error.
                Debug.WriteLine("Not updating, GitHub gave us an error: " + data.message);
            }
            else
            {
                Debug.WriteLine("Remote latest version: " + data.tag_name);
                
                // ToDo:
                // - Compare data.tag_name with current version (define this somewhere).
                // - If remote version is newer, download package async.
                // - Once download complete event procs, abort app load and launch updater.
                // - Updater will wait (and try to engage) the closing of this app before unpacking.
            }

            EventManager.H_UpdateCheckComplete -= OnUpdateCheckComplete; // Unregister event.
            isDoneLoading = true;
        }

        private void Timer_SplashClose_Tick(object sender, EventArgs e)
        {
            // Speed up the timer after the first pass.
            if (Timer_SplashClose.Interval == 4000)
                Timer_SplashClose.Interval = 100;

            // Check if we're done loading.
            if (isDoneLoading)
            {
                Timer_SplashClose.Enabled = false; // Disable timer.
                this.Close(); // Close the splash screen.
            }
        }
    }
}
