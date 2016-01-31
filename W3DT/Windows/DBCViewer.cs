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
        private CASCFile selectedFile = null;
        private DBCFile selectedDbcFile = null;

        private RunnerExtractItem extractRunner;
        private LoadingWindow loadingWindow;
        private Action cancelCallback;
        private int runnerID = -1;

        private Explorer explorer;

        public DBCViewer()
        {
            InitializeComponent();
            cancelCallback = CancelCallback;
            explorer = new Explorer(this, null, null, null, UI_FilesFound, UI_FileList, new string[] { "dbc" }, "DBC_SCAN_{0}", false);

            EventManager.FileExtractComplete += OnFileExtractComplete;

            explorer.Initialize();
        }

        private void CancelCallback()
        {
            if (extractRunner != null)
            {
                extractRunner.Kill();
                extractRunner = null;
            }
        }

        private void ShowDBCFile(string path)
        {
            selectedDbcFile = new DBCFile(path);

            DataGridView view = UI_DBCTable;
            DBCFile file = selectedDbcFile;
            DBCTable table = file.Table;

            // Reset
            view.Columns.Clear();

            for (int i = 0; i < table.getColumnCount(); i++)
                view.Columns.Add(table.getColumnName(i), table.getColumnName(i));

            foreach (DBCRecord record in table.getRecords())
                view.Rows.Add(record.getValues());
        }

        private void OnFileExtractComplete(object sender, EventArgs rawArgs)
        {
            FileExtractCompleteArgs args = (FileExtractCompleteArgs)rawArgs;

            if (args.RunnerID == runnerID)
            {
                extractRunner = null;

                loadingWindow.Close();
                loadingWindow = null;

                if (args.Success)
                    ShowDBCFile(Path.Combine(Constants.TEMP_DIRECTORY, args.File.FullName));
                else
                    throw new Exception("Unable to extract DBC file -> " + args.File.FullName);
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
                    runnerID = extractRunner.runnerID;
                    extractRunner.Begin();

                    loadingWindow = new LoadingWindow("Loading DBC file: " + cascFile.Name, "Life advice: Avoid dragon's breath.", true, cancelCallback);
                    loadingWindow.ShowDialog();
                }
                else
                {
                    ShowDBCFile(tempPath);
                }
            }
        }

        private void DBCViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (loadingWindow != null)
            {
                if (!loadingWindow.IsDisposed && loadingWindow.Visible)
                    loadingWindow.Close();

                loadingWindow = null;
            }

            CancelCallback();
            EventManager.FileExtractComplete -= OnFileExtractComplete;
            explorer.Dispose();
        }
    }
}
