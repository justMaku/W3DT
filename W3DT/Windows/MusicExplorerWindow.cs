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
        private static readonly string RUNNER_ID = "MEW_N";
        private RunnerBase runner;

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

            runner = new RunnerFileExplore(RUNNER_ID, FileNameCache.GetFilesWithExtension("ogg"));
            runner.Begin();
        }

        private void OnFileExploreHit(object sender, EventArgs args)
        {
            FileExploreHitArgs fileArgs = (FileExploreHitArgs)args;

            if (fileArgs.ID.Equals(RUNNER_ID))
            {
                UI_FileList.Items.Add(fileArgs.File);
                UI_FileCount_Label.Text = UI_FileList.Items.Count + " Files Found";
            }
        }

        private void OnFileExploreDone(object sender, EventArgs args)
        {
            if (((FileExploreDoneArgs)args).ID.Equals(RUNNER_ID))
            {
                EventManager.FileExploreDone -= OnFileExploreDone;
                EventManager.FileExploreHit -= OnFileExploreHit;
                runner = null;
            }
        }

        private void MusicExplorerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (runner != null)
                runner.Kill();
        }
    }
}
