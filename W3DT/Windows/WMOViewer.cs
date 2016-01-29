using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using SharpGL;
using W3DT._3D;
using W3DT.Runners;
using W3DT.Events;
using W3DT.CASC;
using W3DT.Formats;

namespace W3DT
{
    public partial class WMOViewer : Form
    {
        private Explorer explorer;
        private Regex ignoreFilter = new Regex(@"(.*)_[0-9]{3}\.wmo$");
        private LoadingWindow loadingWindow;
        private Dictionary<string, List<CASCFile>> groupFiles;
        private List<ExtractState> requiredFiles;
        private WMOFile loadedFile = null;

        public WMOViewer()
        {
            InitializeComponent();
            groupFiles = new Dictionary<string, List<CASCFile>>();

            EventManager.FileExtractComplete += OnFileExtractComplete;

            explorer = new Explorer(this, UI_FilterField, UI_FilterOverlay, UI_FilterTime, UI_FileCount_Label, UI_FileList, new string[] { "wmo" }, "WMO_V_{0}", true);
            explorer.IgnoreFilter = ignoreFilter;
            explorer.ExploreHitCallback = OnExploreHit;
            explorer.Initialize();
        }

        private void LoadWMOFile()
        {
            WMOFile root = null;
            foreach (ExtractState state in requiredFiles)
            {
                string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, state.File.FullName);
                if (root == null)
                    root = new WMOFile(tempPath, true);
                else
                    root.addGroupFile(new WMOFile(tempPath, false));
            }

            try
            {
                root.parse();
                loadedFile = root;
            }
            catch (WMOException e)
            {
                Log.Write("ERROR: Exception was caught while opening WMO file!");
                Log.Write("ERROR: " + e.Message);

                MessageBox.Show("Sorry, that WMO file cannot be opened!", "Errk!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnExploreHit(CASCFile file)
        {
            Match match = ignoreFilter.Match(file.Name);

            if (match.Success)
            {
                string nameBase = match.Groups[1].ToString();
                if (!groupFiles.ContainsKey(nameBase))
                    groupFiles.Add(nameBase, new List<CASCFile>());

                groupFiles[nameBase].Add(file);
            }
        }

        private void WMOViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            explorer.Dispose();
        }

        private void UI_FileList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (UI_FileList.SelectedNode != null && UI_FileList.SelectedNode.Tag is CASCFile)
            {
                // Dispose current loaded file.
                if (loadedFile != null)
                {
                    loadedFile.flush();
                    loadedFile = null;
                }

                CASCFile entry = (CASCFile)UI_FileList.SelectedNode.Tag;
                string rootBase = Path.GetFileNameWithoutExtension(entry.FullName);

                requiredFiles = new List<ExtractState>();
                requiredFiles.Add(new ExtractState(entry));

                // Collect group files for extraction.
                if (groupFiles.ContainsKey(rootBase))
                    foreach (CASCFile groupFile in groupFiles[rootBase])
                        requiredFiles.Add(new ExtractState(groupFile));


                foreach (ExtractState target in requiredFiles)
                {
                    string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, target.File.FullName);

                    if (!File.Exists(tempPath))
                    {
                        RunnerExtractItem extract = new RunnerExtractItem(target.File);
                        target.TrackerID = extract.runnerID;
                        extract.Begin();
                    }
                    else
                    {
                        target.State = true;
                    }
                }

                loadingWindow = new LoadingWindow("Loading WMO file: " + entry.Name, "Notice: No peons were harmed in the making of this software.");
                loadingWindow.ShowDialog();
            }
        }

        private void OnFileExtractComplete(object sender, EventArgs e)
        {
            FileExtractCompleteArgs args = (FileExtractCompleteArgs)e;

            ExtractState match = requiredFiles.FirstOrDefault(f => f.TrackerID == args.RunnerID);
            if (match != null)
            {
                if (!args.Success)
                    throw new Exception("Unable to extract WMO file -> " + args.File.FullName);

                match.State = true;

                if (!requiredFiles.Any(f => !f.State))
                {
                    loadingWindow.Close();
                    loadingWindow = null;

                    LoadWMOFile();
                }
            }
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
            cube.Draw(gl);

            //  Nudge the rotation.
            rotation += 3.0f;
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            cube = new Cube(1F, 0F, 0F);

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(-2, 2, -2, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private float rotation = 0.0f;
        private Cube cube;
    }
}
