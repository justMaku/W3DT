﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using W3DT.CASC;
using W3DT.Runners;
using W3DT.Events;
using W3DT.Formats;
using W3DT._3D;
using SharpGL;

namespace W3DT
{
    public partial class ModelViewer : Form
    {
        private Explorer explorer;

        private RunnerExtractItem runner;
        private LoadingWindow loadingWindow;
        private Action cancelCallback;

        private string selectedFileName;

        // 3D View
        private float rotationY = 0.0f;
        private float prevRotY = 0.0f;
        private float zoom = 1.0f;
        private bool autoRotate = true;
        private List<Mesh> meshes;

        // Mouse position cache for 3D rotation
        private int mouseStartX = 0;
        private int mouseStartY = 0;
        private bool isMouseRotating = false;

        public ModelViewer()
        {
            InitializeComponent();

            meshes = new List<Mesh>();

            explorer = new Explorer(this, UI_FilterField, UI_FilterOverlay, UI_FilterTime, UI_FileCount_Label, UI_FileList, new string[] { "m2", "mdx" }, "M2_V_{0}", true);
            explorer.rootFolders = new string[] { "character", "creature" };
            explorer.Initialize();

            cancelCallback = CancelLoad;
            EventManager.FileExtractComplete += EventManager_FileExtractComplete;
            EventManager.CASCLoadStart += EventManager_CASCLoadStart;
        }

        private void TerminateRunners()
        {
            if (runner != null)
            {
                runner.Kill();
                runner = null;
            }
        }

        private void CloseLoadingWindow()
        {
            if (loadingWindow != null)
            {
                if (!loadingWindow.IsDisposed && loadingWindow.Visible)
                    loadingWindow.Close();

                loadingWindow = null;
            }
        }

        private void CancelLoad()
        {
            TerminateRunners();
            CloseLoadingWindow();
        }

        private void EventManager_FileExtractComplete(object sender, EventArgs e)
        {
            FileExtractCompleteArgs args = (FileExtractCompleteArgs)e;

            if (runner != null && args.RunnerID == runner.runnerID)
            {
                string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, args.File.FullName);

                try
                {
                    if (File.Exists(tempPath))
                    {
                        M2File model = new M2File(tempPath);
                        model.parse();

                        Log.Write("ModelViewer: Loaded {0} M2 data.", model.Name);

                        // ToDo: Download textures.
                        // ToDo: Compile M2 data into a mesh and render it.
                    }
                    else
                    {
                        throw new M2Exception("Extracted file does not exist: " + tempPath);
                    }
                }
                catch (M2Exception ex)
                {
                    Alert.Show(string.Format("Sorry, an error prevented {0} from being opened!", selectedFileName));
                    Log.Write("Unable to extract M2 file: " + ex.Message);
                    Log.Write(ex.StackTrace);
                }
            }
        }

        private void EventManager_CASCLoadStart(object sender, EventArgs e)
        {
            // CASC is being reloaded, abandon ship.
            Close();
        }

        private void UI_FileList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = UI_FileList.SelectedNode;
            if (node != null && node.Tag is CASCFile)
            {
                CASCFile file = (CASCFile)node.Tag;
                TerminateRunners();

                loadingWindow = new LoadingWindow(string.Format("Loading {0} model...", selectedFileName), "Extracting from data source...", true, cancelCallback);
                loadingWindow.ShowDialog();

                selectedFileName = file.Name;
                runner = new RunnerExtractItem(file);
                runner.Begin();
            }
        }

        private void ModelViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelLoad();
            EventManager.FileExtractComplete -= EventManager_FileExtractComplete;
            EventManager.CASCLoadStart -= EventManager_CASCLoadStart;
            TerminateRunners();
        }

        private void updateViewerBackground(Color backColour)
        {
            float r = (float)backColour.R / 255f;
            float g = (float)backColour.G / 255f;
            float b = (float)backColour.B / 255f;

            UI_3DView.OpenGL.ClearColor(r, g, b, 1);
        }

        private void updateCamera()
        {
            OpenGL gl = UI_3DView.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Perspective(60.0f * zoom, (double)UI_3DView.Width / (double)UI_3DView.Height, 0.01, 900.0);
            gl.LookAt(50, 20, 50, 0, 0, 0, 0, 1, 0);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void UI_3DView_OpenGLInitialized(object sender, EventArgs e)
        {
            updateCamera();
            OpenGL gl = UI_3DView.OpenGL;

            gl.DepthFunc(OpenGL.GL_LESS);
            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            updateViewerBackground(Color.FromArgb(Program.Settings.ModelViewerBackgroundColour));
        }

        private void UI_3DView_Resized(object sender, EventArgs e)
        {
            updateCamera();
        }

        private void UI_3DView_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = UI_3DView.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            // Auto-correct rotation values to keep them sane.
            if (rotationY > 360f) rotationY -= 360f; else if (rotationY < -360f) rotationY += 360f;

            gl.Rotate(rotationY, 0.0f, 1.0f, 0.0f); // Rotate Y

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            foreach (Mesh mesh in meshes)
                if (mesh.ShouldRender)
                    mesh.Draw(gl);

            gl.Disable(OpenGL.GL_TEXTURE_2D);

            if (autoRotate)
                rotationY += 3.0f;
        }

        private void UI_3DView_MouseDown(object sender, MouseEventArgs e)
        {
            mouseStartX = e.X;
            mouseStartY = e.Y;
            isMouseRotating = true;
        }

        private void UI_3DView_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseRotating = false;
            prevRotY = rotationY;
        }

        private void UI_3DView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseRotating)
            {
                int diffX = e.X - mouseStartX;
                int diffY = e.Y - mouseStartY;

                rotationY = prevRotY + (diffX * 0.25f);

                autoRotate = false;
            }
        }

        private void UI_3DView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (autoRotate)
                autoRotate = false;

            zoom += e.Delta >= 0 ? -0.15f : 0.15f;
            if (zoom < 0.01f)
                zoom = 0.01f;

            updateCamera();
        }

        private void UI_ColourChangeButton_Click(object sender, EventArgs e)
        {
            UI_ColourDialog.Color = Color.FromArgb(Program.Settings.ModelViewerBackgroundColour);
            if (UI_ColourDialog.ShowDialog() == DialogResult.OK)
            {
                updateViewerBackground(UI_ColourDialog.Color);
                Program.Settings.ModelViewerBackgroundColour = UI_ColourDialog.Color.ToArgb();
                Program.Settings.Persist();
            }
        }
    }
}