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
using W3DT.Events;
using W3DT.Runners;
using libZPlay;

namespace W3DT
{
    public partial class SoundPlayer : Form
    {
        private ZPlay player;
        private bool ready = false;
        private bool paused = false;
        private CASCFile file;
        private string localPath;

        public SoundPlayer(CASCFile file)
        {
            InitializeComponent();

            this.file = file;
            localPath = Path.Combine(Constants.TEMP_DIRECTORY, file.FullName);

            player = new ZPlay();
            if (!File.Exists(localPath))
            {
                EventManager.FileExtractComplete += OnFileExtractComplete;
                new RunnerExtractItem(file).Begin();
                SetState("Extracting file...");
            }
            else
            {
                ready = true;
                Play();
            }

            UI_TrackTitle.Text = file.Name;
            Text = file.Name + " - W3DT";

            UI_VolumeBar.Value = Program.Settings.SoundPlayerVolume;
            player.SetPlayerVolume(UI_VolumeBar.Value, UI_VolumeBar.Value);
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
                    Play();
                }
                else
                {
                    Log.Write("File extract failed for {0}!", localPath);
                    SetState("Unable to load file!");
                }
            }
        }

        private void Play()
        {
            if (File.Exists(localPath))
            {
                SetState("Playing...");
                player.OpenFile(localPath, TStreamFormat.sfAutodetect);
                player.StartPlayback();
            }
            else
            {
                Log.Write("Unable to play {0}, local file does not exist?", localPath);
                SetState("Error!");
            }
        }

        private void SetState(string state)
        {
            UI_StateLabel.Text = string.Format("Currently: {0}", state);
        }

        private void SoundPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.StopPlayback();
            player.Close();
        }

        private void UI_PlayButton_Click(object sender, EventArgs e)
        {
            if (ready)
            {
                if (paused)
                {
                    player.ResumePlayback();
                    paused = false;
                }
                else
                {
                    player.StartPlayback();
                }
            }
        }

        private void UI_PauseButton_Click(object sender, EventArgs e)
        {
            if (ready)
            {
                player.PausePlayback();
                paused = true;
            }
        }

        private void UI_StopButton_Click(object sender, EventArgs e)
        {
            if (ready)
            {
                player.StopPlayback();
                paused = false;
            }
        }

        private void UI_VolumeBar_Scroll(object sender, EventArgs e)
        {
            player.SetPlayerVolume(UI_VolumeBar.Value, UI_VolumeBar.Value);
            Program.Settings.SoundPlayerVolume = UI_VolumeBar.Value;
            Program.Settings.Persist();
        }

        private void UI_SaveButton_Click(object sender, EventArgs e)
        {
            Dialog_Save.Filter = string.Format("{0} file|*{0}", Path.GetExtension(file.Name));
            Dialog_Save.FileName = file.Name;

            if (Dialog_Save.ShowDialog() == DialogResult.OK)
                File.Copy(localPath, Dialog_Save.FileName);
        }
    }
}
