using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using libZPlay;

namespace W3DT.Windows
{
    public partial class SoundPlayer : Form
    {
        private ZPlay player;
        private TStreamTime position = new TStreamTime();
        private string gameFilePath;
        private bool ready = false;

        public SoundPlayer(string file)
        {
            gameFilePath = file;

            InitializeComponent();

            UI_TrackTitle.Text = Path.GetFileName(gameFilePath);
        }

        private void SetState(string state)
        {
            UI_StateLabel.Text = string.Format("Currently: {0}", state);
        }
    }
}
