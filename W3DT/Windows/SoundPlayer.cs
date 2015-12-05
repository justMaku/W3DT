using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using W3DT.CASC;
using W3DT.Runners;
using W3DT.Events;
using libZPlay;

namespace W3DT.Windows
{
    public partial class SoundPlayer : Form
    {
        private ZPlay player;
        private TStreamTime position = new TStreamTime();
        private bool ready = false;
        private CASCFile file;
        private string localPath;

        public SoundPlayer(CASCFile file)
        {
            this.file = file;
            localPath = Path.Combine(Constants.TEMP_DIRECTORY, file.FullName);

            if (!File.Exists(localPath))
            {
                EventManager.FileExtractComplete += OnFileExtractComplete;
                new RunnerExtractItem(file).Begin();
            }

            InitializeComponent();
            UI_TrackTitle.Text = file.Name;
            Text = file.Name + " - W3DT";
            player = new ZPlay();
        }

        private void OnFileExtractComplete(object sender, EventArgs args)
        {
            FileExtractCompleteArgs extractArgs = (FileExtractCompleteArgs)args;
            if (extractArgs.File.Equals(file))
            {
                EventManager.FileExtractComplete -= OnFileExtractComplete;
                if (extractArgs.Success)
                {
                    ready = true;
                    SetState("Playing...");
                    player.OpenFile(localPath, TStreamFormat.sfAutodetect);
                    player.StartPlayback();
                }
                else
                {
                    SetState("Unable to load file!");
                }
            }
        }

        private void SetState(string state)
        {
            UI_StateLabel.Text = string.Format("Currently: {0}", state);
        }
    }
}
