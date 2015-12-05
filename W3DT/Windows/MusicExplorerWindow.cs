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
    public partial class MusicExplorerWindow : Form
    {
        private static readonly string RUNNER_ID = "MEW_N_{0}";
        private static readonly string[] extensions = new string[] { "ogg", "mp3" };
        private int currentScan = 0;
        private string currentID = null;

        private RunnerBase runner;
        private bool filterHasChanged = false;

        public MusicExplorerWindow()
        {
            InitializeComponent();
            InitializeMusicList();
        }

        private void InitializeMusicList()
        {
            UI_FileList.Items.Clear();

            if (!Program.IsCASCReady())
                return;

            EventManager.FileExploreHit += OnFileExploreHit;
            EventManager.FileExploreDone += OnFileExploreDone;

            UI_FileCount_Label.Text = string.Format(Constants.MUSIC_WINDOW_SEARCH_STATE, 0, "Preparing...");

            currentScan++;
            currentID = string.Format(RUNNER_ID, currentScan);

            runner = new RunnerFileExplore(currentID, extensions, GetFilter());
            runner.Begin();
        }

        private void OnFileExploreHit(object sender, EventArgs args)
        {
            FileExploreHitArgs fileArgs = (FileExploreHitArgs)args;

            if (fileArgs.ID.Equals(currentID))
            {
                UI_FileList.Items.Add(fileArgs.Entry);

                UI_FileCount_Label.Text = string.Format(Constants.MUSIC_WINDOW_SEARCH_STATE, UI_FileList.Items.Count, "Searching...");
            }
        }

        private void OnFileExploreDone(object sender, EventArgs args)
        {
            if (((FileExploreDoneArgs)args).ID.Equals(currentID))
            {
                EventManager.FileExploreDone -= OnFileExploreDone;
                EventManager.FileExploreHit -= OnFileExploreHit;
                runner = null;

                UI_FileCount_Label.Text = string.Format(Constants.MUSIC_WINDOW_SEARCH_STATE, UI_FileList.Items.Count, "Done");
            }
        }

        private void MusicExplorerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (runner != null)
                runner.Kill();
        }

        private string GetFilter()
        {
            string value = UI_FilterField.Text.Trim();
            if (value.Length == 0)
                return null;

            return value;
        }

        private void UI_FilterCheckTimer_Tick(object sender, EventArgs e)
        {
            if (filterHasChanged)
            {
                if (runner != null)
                    runner.Kill();

                EventManager.FileExploreDone -= OnFileExploreDone;
                EventManager.FileExploreHit -= OnFileExploreHit;

                filterHasChanged = false;
                UI_FilterCheckTimer.Enabled = false;
                InitializeMusicList();
            }
        }

        private void UI_FilterField_TextChanged(object sender, EventArgs e)
        {
            filterHasChanged = true;
            UI_FilterCheckTimer.Enabled = false;
            UI_FilterCheckTimer.Enabled = true;
            UI_FilterOverlay.Visible = UI_FilterField.Text.Length == 0;
        }

        private void UI_FilterOverlay_MouseUp(object sender, MouseEventArgs e)
        {
            UI_FilterField.Focus();
        }

        private void UI_FileList_DoubleClick(object sender, EventArgs e)
        {
            if (UI_FileList.SelectedItem != null)
                new RunnerExtractItem((CASCFile)UI_FileList.SelectedItem).Begin();
        }
    }
}
