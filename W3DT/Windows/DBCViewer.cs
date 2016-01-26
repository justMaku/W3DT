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

namespace W3DT
{
    public partial class DBCViewer : Form
    {
        private static readonly string RUNNER_ID = "DBC_SCAN_{0}";
        private static readonly string[] EXTENSIONS = new string[] { "dbc" };

        private string currentScanID = null;
        private string saveTargetPath = null;
        private CASCFile selectedFile = null;
        private int scanIndex = 0;
        private int found = 0;
        private RunnerBase runner;
        private RunnerExtractItem extractRunner;

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

        private void OnFileExtractComplete(object sender, EventArgs rawArgs)
        {
            FileExtractCompleteArgs args = (FileExtractCompleteArgs)rawArgs;

            if (args.Success)
                File.Copy(Path.Combine(Constants.TEMP_DIRECTORY, args.File.FullName), saveTargetPath);
            else
                MessageBox.Show("Unable to export file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            extractRunner = null;
            saveTargetPath = null;
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
            if (UI_FileList.SelectedNode != null && UI_FileList.SelectedNode.Tag is CASCFile)
            {
                CASCFile file = (CASCFile)UI_FileList.SelectedNode.Tag;
                UI_SaveDialog.FileName = file.Name;
                saveTargetPath = null;

                if (extractRunner != null)
                {
                    extractRunner.Kill();
                    extractRunner = null;
                }

                if (UI_SaveDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(UI_SaveDialog.FileName);

                    if (extension.EndsWith("dbc"))
                    {
                        string fullPath = Path.Combine(Constants.TEMP_DIRECTORY, file.FullName);
                        if (File.Exists(fullPath))
                        {
                            File.Copy(fullPath, UI_SaveDialog.FileName);
                        }
                        else
                        {
                            saveTargetPath = UI_SaveDialog.FileName;
                            extractRunner = new RunnerExtractItem(file);
                            extractRunner.Begin();
                        }
                    }
                }
            }
        }
    }
}
