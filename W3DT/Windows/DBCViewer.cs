using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using W3DT.Events;
using W3DT.Runners;
using W3DT.CASC;
using W3DT.Formats;

namespace W3DT
{
    public partial class DBCViewer : Form
    {
        private static readonly string RUNNER_ID = "DBC_SCAN_{0}";
        private static readonly string[] EXTENSIONS = new string[] { "dbc" };

        private string currentScanID = null;
        private CASCFile selectedFile = null;
        private DBCFile selectedDbcFile = null;
        private int scanIndex = 0;
        private int found = 0;

        private RunnerBase runner;
        private RunnerExtractItem extractRunner;
        private LoadingWindow loadingWindow;

        public DBCViewer()
        {
            InitializeComponent();
            InitializeDBCList();

            EventManager.FileExtractComplete += OnFileExtractComplete;
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

        private void ShowDBCFile(string path)
        {
            selectedDbcFile = new DBCFile(path);
        }

        private void OnFileExtractComplete(object sender, EventArgs rawArgs)
        {
            FileExtractCompleteArgs args = (FileExtractCompleteArgs)rawArgs;
            extractRunner = null;

            loadingWindow.Close();
            loadingWindow = null;

            if (args.Success)
                ShowDBCFile(Path.Combine(Constants.TEMP_DIRECTORY, args.File.FullName));
            else
                throw new Exception("Unable to extract DBC file -> " + args.File.FullName);
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

        private void UI_ExportButton_Click(object sender, EventArgs e)
        {
            if (selectedDbcFile != null)
            {
                UI_SaveDialog.FileName = selectedFile.Name;
                if (UI_SaveDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(UI_SaveDialog.FileName);

                    if (extension.EndsWith("dbc"))
                        selectedDbcFile.writeToFile(UI_SaveDialog.FileName);
                    else
                        MessageBox.Show("Unable to save, unsupported format!", "No can do, Captain!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UI_FileList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (UI_FileList.SelectedNode != null && UI_FileList.SelectedNode.Tag is CASCFile)
            {
                selectedFile = null;
                selectedDbcFile = null;

                CASCFile cascFile = (CASCFile)UI_FileList.SelectedNode.Tag;
                string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, cascFile.FullName);
                selectedFile = cascFile;

                if (!File.Exists(tempPath))
                {
                    extractRunner = new RunnerExtractItem(cascFile);
                    extractRunner.Begin();

                    loadingWindow = new LoadingWindow("Loading DBC file: " + cascFile.Name, "Life advice: Avoid dragon's breath.");
                    loadingWindow.ShowDialog();
                }
                else
                {
                    ShowDBCFile(tempPath);
                }
            }
        }
    }
}
