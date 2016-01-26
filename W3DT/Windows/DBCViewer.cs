using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using W3DT.Events;
using W3DT.Runners;

namespace W3DT
{
    public partial class DBCViewer : Form
    {
        private static readonly string RUNNER_ID = "DBC_SCAN_{0}";
        private static readonly string[] EXTENSIONS = new string[] { "dbc" };

        private string currentScanID = null;
        private int scanIndex = 0;
        private int found = 0;
        private RunnerBase runner;

        public DBCViewer()
        {
            InitializeComponent();
            InitializeDBCList();
        }

        private void InitializeDBCList()
        {
            UI_FileList.Nodes.Clear();

            if (!Program.IsCASCReady())
                return;

            found = 0;

            EventManager.FileExploreHit += OnFileExploreHit;
            EventManager.FileExploreDone += OnFileExploreDone;

            UI_FilesFound.Text = string.Format(Constants.GENERIC_WINDOW_SEARCH_STATE, 0, "Preparing...");

            scanIndex++;
            currentScanID = string.Format(RUNNER_ID, scanIndex);

            runner = new RunnerFileExplore(currentScanID, EXTENSIONS, null);
            runner.Begin();
        }

        private void OnFileExploreDone(object sender, EventArgs args)
        {
            if (((FileExploreDoneArgs)args).ID.Equals(currentScanID))
            {
                EventManager.FileExploreDone -= OnFileExploreDone;
                EventManager.FileExploreHit -= OnFileExploreHit;
                runner = null;

                UI_FilesFound.Text = string.Format(Constants.GENERIC_WINDOW_SEARCH_STATE, found, "Done");
            }
        }

        private void OnFileExploreHit(object sender, EventArgs args)
        {
            FileExploreHitArgs fileArgs = (FileExploreHitArgs)args;

            if (fileArgs.ID.Equals(currentScanID))
            {
                found++;

                TreeNode newNode = new TreeNode(fileArgs.Entry.Name);
                newNode.Tag = fileArgs.Entry;
                UI_FileList.Nodes.Add(newNode);

                UI_FilesFound.Text = string.Format(Constants.GENERIC_WINDOW_SEARCH_STATE, found, "Searching...");
            }
        }
    }
}
