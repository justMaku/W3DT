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
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void Timer_SplashClose_Tick(object sender, EventArgs e)
        {
            // ToDo: Hold up closing for actual resource loading.

            Timer_SplashClose.Enabled = false; // Disable timer.
            this.Close(); // Close the splash screen.
        }
    }
}
