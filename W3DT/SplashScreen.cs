using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using W3DT.Events;
using W3DT.Runners;
using W3DT.JSONContainers;

namespace W3DT
{
    public partial class SplashScreen : Form
    {
        private bool isDoneLoading = false;

        public SplashScreen()
        {
            InitializeComponent();
            EventManager.H_UpdateCheckComplete += OnUpdateCheckComplete;

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
                // ToDo: Compare versions to our own and initiate update.
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
