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
        private RunnerExtractItem extractRunner;
        private string currentImageName;
        private Bitmap currentImage;

        private Explorer explorer;

        public ArtExplorerWindow()
        {
            InitializeComponent();
            explorer = new Explorer(this, UI_FilterField, UI_FilterOverlay, UI_FilterTimer, UI_FileCount_Label, UI_FileList, new string[] { "blp" }, "AEW_N_{0}", true);

            UI_AutoLoadPreview_Field.Checked = Program.Settings.AutoShowArtworkPreview;
            EventManager.FileExtractComplete += OnFileExtractComplete;

            explorer.Initialize();
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

        private void ArtExplorerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            explorer.Dispose();
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
