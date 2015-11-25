using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace W3DT.Controls
{
    public partial class LoadingEye : Panel
    {
        private int currentStep = -1;

        public LoadingEye()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = (50);
            timer.Tick += new EventHandler(UpdateEye);
            timer.Start();
        }

        private void UpdateEye(object sender, EventArgs e)
        {
            currentStep++;
            if (currentStep == 32)
                currentStep = 0;

            image.Location = new Point(-(64 * (currentStep % 8)), -(64 * (currentStep / 8)));
        }
    }
}
