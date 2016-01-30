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
using W3DT.Formats.WMO;

namespace W3DT
{
    public partial class WMOViewer : Form
    {
        private Explorer explorer;
        private Regex ignoreFilter = new Regex(@"(.*)_[0-9]{3}\.wmo$");
        private LoadingWindow loadingWindow;
        private Dictionary<string, List<CASCFile>> groupFiles;
        private List<ExtractState> requiredFiles;
        private List<RunnerExtractItem> runners;
        private WMOFile loadedFile = null;
        private Action cancelCallback;

        // 3D View
        private float rotation = 0.0f;
        private Mesh mesh = null;

        public WMOViewer()
        {
            InitializeComponent();
            groupFiles = new Dictionary<string, List<CASCFile>>();
            runners = new List<RunnerExtractItem>();

            EventManager.FileExtractComplete += OnFileExtractComplete;

            explorer = new Explorer(this, UI_FilterField, UI_FilterOverlay, UI_FilterTime, UI_FileCount_Label, UI_FileList, new string[] { "wmo" }, "WMO_V_{0}", true);
            explorer.IgnoreFilter = ignoreFilter;
            explorer.ExploreHitCallback = OnExploreHit;
            explorer.Initialize();

            cancelCallback = TerminateRunners;
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

                CreateWMOMesh();
            }
            catch (WMOException e)
            {
                Log.Write("ERROR: Exception was caught while opening WMO file!");
                Log.Write("ERROR: " + e.Message);

                MessageBox.Show("Sorry, that WMO file cannot be opened!", "Errk!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateWMOMesh()
        {
            if (loadedFile == null)
                return;

            Log.Write("CreateWMOMesh: Creating new mesh from WMO data...");

            mesh = new Mesh();
            Chunk_MOGP firstGroup = (Chunk_MOGP)loadedFile.getChunksByID(Chunk_MOGP.Magic).FirstOrDefault();
            // ToDo: Confirm we actually have this.

            if (firstGroup != null)
            {
                // Verts.
                Chunk_MOVT vertChunk = (Chunk_MOVT)firstGroup.getChunks().Where(c => c.ChunkID == Chunk_MOVT.Magic).FirstOrDefault();
                foreach (Position vertPos in vertChunk.vertices)
                    mesh.addVert(vertPos);

                Log.Write("CreateWMOMesh: {0} vertices added to mesh index", mesh.VertCount);

                // Faces.
                Chunk_MOVI faceChunk = (Chunk_MOVI)firstGroup.getChunks().Where(c => c.ChunkID == Chunk_MOVI.Magic).FirstOrDefault();
                foreach (FacePosition facePos in faceChunk.positions)
                    mesh.addFace(facePos.point1, facePos.point2, facePos.point3);

                Log.Write("CreateWMOMesh: {0} faces added to mesh", mesh.FaceCount);
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
            TerminateRunners();
            explorer.Dispose();
        }

        private void TerminateRunners()
        {
            if (runners != null)
                foreach (RunnerExtractItem runner in runners)
                    runner.Kill();

            runners.Clear();
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

                TerminateRunners();

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
                        runners.Add(extract);
                    }
                    else
                    {
                        target.State = true;
                    }
                }

                loadingWindow = new LoadingWindow("Loading WMO file: " + entry.Name, "No peons were harmed in the making of this software.", true, cancelCallback);
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

        private void upateCamera()
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            float distance = cameraDist * -1;
            gl.LookAt(distance, 20, distance, 0, 0, 0, 0, 1, 0);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            if (mesh != null)
                mesh.Draw(gl);

            //  Nudge the rotation.
            rotation += 3.0f;
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(1, 1, 1, 1);
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 900.0);
            gl.LookAt(50, 20, 50, 0, 0, 0, 0, 1, 0);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
    }
}
