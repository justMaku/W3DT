using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using W3DT.CASC;
using W3DT.Events;
using W3DT.Runners;
using SereniaBLPLib;

namespace W3DT
{
    public partial class ArtExplorerWindow : Form
    {
        private static readonly string RUNNER_ID = "AEW_N_{0}";
        private static readonly string[] extensions = new string[] { "blp" };
        private int currentScan = 0;
        private string currentID = null;
        private int found = 0;

        private RunnerBase runner;
        private bool filterHasChanged = false;
        private RunnerExtractItem extractRunner;

        private string currentImageName;
        private Bitmap currentImage;

        public ArtExplorerWindow()
        {
            InitializeComponent();
            InitializeArtList();

            UI_AutoLoadPreview_Field.Checked = Program.Settings.AutoShowArtworkPreview;
            EventManager.FileExtractComplete += OnFileExtractComplete;
        }

        private void InitializeArtList()
        {
            UI_FileList.Nodes.Clear();

            if (!Program.IsCASCReady())
                return;

            found = 0;

            EventManager.FileExploreHit += OnFileExploreHit;
            EventManager.FileExploreDone += OnFileExploreDone;

            UI_FileCount_Label.Text = string.Format(Constants.GENERIC_WINDOW_SEARCH_STATE, 0, "Preparing...");

            currentScan++;
            currentID = string.Format(RUNNER_ID, currentScan);

            runner = new RunnerFileExplore(currentID, extensions, GetFilter());
            runner.Begin();
        }

        private void OnFileExtractComplete(object sender, EventArgs rawArgs)
        {
            FileExtractCompleteArgs args = (FileExtractCompleteArgs)rawArgs;
            currentImageName = null;

            UI_ExportButton.Hide();

            if (args.Success)
            {
                displayImage(Path.Combine(Constants.TEMP_DIRECTORY, args.File.FullName));
            }
            else
            {
                UI_PreviewStatus.Text = "Error loading image!";
                UI_PreviewStatus.Show();
            }

            extractRunner = null;
        }

        private void displayImage(string file)
        {
            currentImage = null;
            UI_PreviewStatus.Hide();

            using (var blp = new BlpFile(File.OpenRead(file)))
                currentImage = blp.GetBitmap(0);

            Graphics gfx = UI_ImagePreview.CreateGraphics();
            gfx.Clear(UI_ImagePreview.BackColor);
            gfx.DrawImage(currentImage, 0, 0);
            UI_ExportButton.Show();

            currentImageName = file;
        }

        private void OnFileExploreHit(object sender, EventArgs args)
        {
            FileExploreHitArgs fileArgs = (FileExploreHitArgs)args;

            if (fileArgs.ID.Equals(currentID))
            {
                found++;
                List<string> pathParts = fileArgs.Entry.FullName.Split('\\').ToList();

                int index = 1;
                TreeNode currentNode = null;
                foreach (string pathPart in pathParts)
                {
                    if (currentNode != null)
                    {
                        currentNode = TreeNodeHelper.FindOrCreateSubNode(currentNode, pathPart);

                        if (index == pathParts.Count)
                            currentNode.Tag = fileArgs.Entry;
                    }
                    else
                    {
                        currentNode = TreeNodeHelper.FindOrCreateSubNode(UI_FileList, pathPart);
                    }
                    index++;
                }

                UI_FileCount_Label.Text = string.Format(Constants.GENERIC_WINDOW_SEARCH_STATE, found, "Searching...");
            }
        }

        private void OnFileExploreDone(object sender, EventArgs args)
        {
            if (((FileExploreDoneArgs)args).ID.Equals(currentID))
            {
                EventManager.FileExploreDone -= OnFileExploreDone;
                EventManager.FileExploreHit -= OnFileExploreHit;
                runner = null;

                UI_FileCount_Label.Text = string.Format(Constants.GENERIC_WINDOW_SEARCH_STATE, found, "Done");
            }
        }

        private void ArtExplorerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            EventManager.FileExploreDone -= OnFileExploreDone;
            EventManager.FileExploreHit -= OnFileExploreHit;

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
                InitializeArtList();
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

        private void UI_AutoLoadPreview_Field_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AutoShowArtworkPreview = UI_AutoLoadPreview_Field.Checked;
            Program.Settings.Persist();
        }

        private void LoadSelectedImage()
        {
            TreeNode selected = UI_FileList.SelectedNode;

            if (selected.Tag != null && selected.Tag is CASCFile)
            {
                CASCFile file = (CASCFile)selected.Tag;
                if (extractRunner != null)
                {
                    extractRunner.Kill();
                    extractRunner = null;
                }

                ClearImagePreview();

                UI_PreviewStatus.Text = "Loading...";
                UI_PreviewStatus.Show();

                string fullPath = Path.Combine(Constants.TEMP_DIRECTORY, file.FullName);
                if (File.Exists(fullPath))
                {
                    displayImage(fullPath);
                }
                else
                {
                    extractRunner = new RunnerExtractItem(file);
                    extractRunner.Begin();
                }
            }
        }

        private void UI_FileList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = UI_FileList.SelectedNode;

            if (selected != null && selected.Tag is CASCFile)
            {
                if (Program.Settings.AutoShowArtworkPreview)
                {
                    LoadSelectedImage();
                }
                else
                {
                    if (extractRunner != null)
                    {
                        extractRunner.Kill();
                        extractRunner = null;
                    }

                    ClearImagePreview();

                    UI_PreviewStatus.Text = "Click to load preview...";
                    UI_PreviewStatus.Show();
                }
            }
        }

        private void ClearImagePreview()
        {
            Graphics gfx = UI_ImagePreview.CreateGraphics();
            gfx.Clear(UI_ImagePreview.BackColor);
            currentImage = null;
            currentImageName = null;
            UI_ExportButton.Hide();
        }

        private void UI_PreviewStatus_Click(object sender, EventArgs e)
        {
            if (!Program.Settings.AutoShowArtworkPreview && extractRunner == null)
                LoadSelectedImage();
        }

        private void UI_ExportButton_Click(object sender, EventArgs e)
        {
            UI_DialogSave.FileName = Path.GetFileNameWithoutExtension(currentImageName);

            if (UI_DialogSave.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format = ImageFormat.Jpeg;
                string extension = Path.GetExtension(UI_DialogSave.FileName);

                if (extension.EndsWith("png"))
                    format = ImageFormat.Png;
                else if (extension.EndsWith("bmp"))
                    format = ImageFormat.Bmp;

                currentImage.Save(UI_DialogSave.FileName, format);
            }
        }

        private void ArtExplorerWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (currentImage != null)
            {
                Graphics gfx = UI_ImagePreview.CreateGraphics();
                gfx.Clear(UI_ImagePreview.BackColor);
                gfx.DrawImage(currentImage, 0, 0);
            }
        }
    }
}
