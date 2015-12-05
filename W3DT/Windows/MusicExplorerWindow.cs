﻿using System;
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
        private int currentScan = 0;
        private string currentID = null;

        private RunnerBase runner;
        private int maxHit;
        private int currentHit;
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

            List<string> data = FileNameCache.GetFilesWithExtension("ogg");
            maxHit = data.Count;
            currentHit = 0;

            UI_FileCount_Label.Text = string.Format(Constants.MUSIC_WINDOW_SEARCH_PROGRESS, 0, 0);

            currentScan++;
            currentID = string.Format(RUNNER_ID, currentScan);

            runner = new RunnerFileExplore(currentID, data, GetFilter());
            runner.Begin();
        }

        private void OnFileExploreHit(object sender, EventArgs args)
        {
            FileExploreHitArgs fileArgs = (FileExploreHitArgs)args;

            if (fileArgs.ID.Equals(currentID))
            {
                currentHit++;
                UI_FileList.Items.Add(fileArgs.File);

                decimal pct = ((decimal) currentHit / maxHit) * 100;
                UI_FileCount_Label.Text = string.Format(Constants.MUSIC_WINDOW_SEARCH_PROGRESS, UI_FileList.Items.Count, (int) pct);
            }
        }

        private void OnFileExploreDone(object sender, EventArgs args)
        {
            if (((FileExploreDoneArgs)args).ID.Equals(currentID))
            {
                EventManager.FileExploreDone -= OnFileExploreDone;
                EventManager.FileExploreHit -= OnFileExploreHit;
                runner = null;

                UI_FileCount_Label.Text = string.Format(Constants.MUSIC_WINDOW_SEARCH_DONE, UI_FileList.Items.Count);
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
            if (value.Length == 0 || value.Equals(Constants.FILTER_DEFAULT))
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
        }
    }
}
