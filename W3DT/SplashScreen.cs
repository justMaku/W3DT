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
using System.Text.RegularExpressions;
using W3DT.Events;
using W3DT.Runners;
using W3DT.JSONContainers;
using W3DT.CASC;

namespace W3DT
{
    public partial class SplashScreen : Form, ISourceSelectionParent
    {
        private bool isDoneLoading = false;
        private bool isUpdateCheckDone = false;
        private bool hasShownSourceScreen = false;

        private bool isCASCDone = false;
        private bool isCASCStarted = false;

        private string[] loadFlavor;
        private Random random = new Random();

        public SplashScreen()
        {
            InitializeComponent();
            EventManager.LoadStepDone += OnLoadBarStepDone;
            EventManager.UpdateCheckDone += OnUpdateCheckComplete;
            EventManager.UpdateDownloadDone += OnUpdateDownloadComplete;

            try
            {
                loadFlavor = File.ReadAllLines(Constants.LOAD_FLAVOR_FILE);
                loadBar.Tag = loadFlavor[random.Next(loadFlavor.Length)];
            }
            catch
            {
                loadFlavor = null;
                loadBar.Tag = Constants.LOADING_DEFAULT;
            }

            // Check for and remove leftover update package.
            try
            {
                if (File.Exists(Constants.UPDATE_PACKAGE_FILE))
                    File.Delete(Constants.UPDATE_PACKAGE_FILE);
            }
            catch (Exception ex)
            {
                // File locked, or something. Brazenly continue onwards.
                Log.Write("Unable to delete leftover package file (" + ex.GetType().Name + ")");
                Log.Write("Exception info: " + ex.Message);
            }

            if (Program.DO_UPDATE)
            {
                new RunnerUpdateCheck().Begin();
            }
            else
            {
                OnLoadBarStepDone(null, null);

                if (!Program.Settings.ShowSourceSelector)
                    isDoneLoading = true;

                isUpdateCheckDone = true;
            }
        }

        public void OnLoadBarStepDone(object sender, EventArgs args)
        {
            loadBar.PerformStep();

            if (loadFlavor != null)
                loadBar.Tag = loadFlavor[random.Next(loadFlavor.Length)];
        }

        public void OnUpdateCheckComplete(object sender, EventArgs args)
        {
            LatestReleaseData data = ((UpdateCheckDoneArgs)args).Data;
            bool isUpdating = false;

            // Check our update data.
            if (data.message != null)
            {
                // If a message is set, it was some kind of error.
                Log.Write("NOT UPDATING: GitHub gave us an error -> " + data.message);
            }
            else
            {
                // Ensure our remote version number is not malformed.
                data.tag_name = data.tag_name.Trim(); // Trim any whitespace that might have slipped in.
                Regex versionCheck = new Regex(@"^\d+\.\d+\.\d+\.\d+$", RegexOptions.IgnoreCase);
                if (versionCheck.Match(data.tag_name).Success)
                {
                    Log.Write("Remote latest version: " + data.tag_name);

                    Version localVersion = new Version(Program.Version);
                    Version remoteVersion = new Version(data.tag_name);

                    if (remoteVersion.CompareTo(localVersion) > 0)
                    {
                        if (data.assets.Length > 0)
                        {
                            // Local version is out-of-date, lets fix that.
                            isUpdating = true;
                            new RunnerDownloadUpdate(data.assets[0].browser_download_url).Begin();
                        }
                        else
                        {
                            // Missing assets. Human error (most likely).
                            Log.Write("NOT UPDATING: Remote version has no assets attached!");
                        }
                    }
                    else
                    {
                        // Local version is equal to remote (or somehow newer).
                        Log.Write("NOT UPDATING: Local version is newer or equal to remote version.");
                    }
                }
                else
                {
                    // Version number is not valid, generally caused by human error.
                    Log.Write("NOT UPDATING: Remote version number is malformed -> " + data.tag_name);
                }
            }

            // There shouldn't be more than one event fired, but unregister anyway.
            EventManager.UpdateCheckDone -= OnUpdateCheckComplete;

            if (!isUpdating)
            {
                EventManager.Trigger_LoadStepDone();

                if (!Program.Settings.ShowSourceSelector)
                    isDoneLoading = true;

                isUpdateCheckDone = true;
            }
        }

        private void ShowSourceSelectionScreen()
        {
            SourceSelectionScreen sourceScreen = new SourceSelectionScreen(this, true);
            sourceScreen.Show();
            sourceScreen.Focus();
        }

        public void OnSourceSelectionDone()
        {
            isDoneLoading = true;
        }

        public void OnUpdateDownloadComplete(object sender, EventArgs args)
        {
            EventManager.Trigger_LoadStepDone();

            bool success = ((UpdateDownloadDoneArgs)args).Success;
            if (success)
            {
                Process.Start("W3DT_Updater.exe");
                Program.STOP_LOAD = true;
                Close();
            }
            else
            {
                // Mark loading as done to continue with app launch.
                isUpdateCheckDone = true;
                isDoneLoading = true;
            }

            // There shouldn't be more than one event fired, but unregister anyway.
            EventManager.UpdateDownloadDone -= OnUpdateDownloadComplete;
        }

        private void OnCASCLoadDone(object sender, EventArgs args)
        {
            EventManager.CASCLoadDone -= OnCASCLoadDone;
            isCASCDone = true;
        }

        private void Timer_SplashClose_Tick(object sender, EventArgs e)
        {
            // Speed up the timer after the first pass.
            if (Timer_SplashClose.Interval > 100)
            {
                OnLoadBarStepDone(null, null);
                Timer_SplashClose.Interval = 100;
            }

            if (!hasShownSourceScreen && Program.Settings.ShowSourceSelector && isUpdateCheckDone)
            {
                hasShownSourceScreen = true;
                ShowSourceSelectionScreen();
            }

            // Check if we're done loading.
            if (isDoneLoading && !isCASCStarted)
            {
                isCASCStarted = true;
                EventManager.CASCLoadDone += OnCASCLoadDone;
                new RunnerInitializeCASC().Begin();
            }

            // Check if we're really done loading.
            if (isCASCDone)
            {
                Timer_SplashClose.Enabled = false; // Disable timer.
                this.Close(); // Close the splash screen.
            }
        }
    }
}
