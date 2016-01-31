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

        // Texture prep
        private List<ExtractState> requiredTex;
        private List<RunnerExtractItemUnsafe> texRunners;
        private TextureManager texManager;

        // 3D View
        private float rotation = 0.0f;
        private List<Mesh> meshes;

        public WMOViewer()
        {
            InitializeComponent();
            groupFiles = new Dictionary<string, List<CASCFile>>();

            runners = new List<RunnerExtractItem>();
            texRunners = new List<RunnerExtractItemUnsafe>();
            meshes = new List<Mesh>();

            EventManager.CASCLoadStart += OnCASCLoadStart;
            EventManager.FileExtractComplete += OnFileExtractComplete;

            explorer = new Explorer(this, UI_FilterField, UI_FilterOverlay, UI_FilterTime, UI_FileCount_Label, UI_FileList, new string[] { "wmo" }, "WMO_V_{0}", true);
            explorer.IgnoreFilter = ignoreFilter;
            explorer.ExploreHitCallback = OnExploreHit;
            explorer.Initialize();

            cancelCallback = CancelExtraction;

            texManager = new TextureManager(openGLControl.OpenGL);
        }

        private void OnCASCLoadStart(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelExtraction()
        {
            TerminateRunners();

            if (loadingWindow != null)
            {
                if (!loadingWindow.IsDisposed && loadingWindow.Visible)
                    loadingWindow.Close();

                loadingWindow = null;
            }
        }

        private void ErrorMessage(string message)
        {
            MessageBox.Show(message, "WMO Extraction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoadWMOFile()
        {
            loadingWindow.SetSecondLine("Almost done.. hang tight!");

            WMOFile root = null;
            foreach (ExtractState state in requiredFiles)
            {
                string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, ((CASCFile)state.File).FullName);
                if (root == null)
                    root = new WMOFile(tempPath, true);
                else
                    root.addGroupFile(new WMOFile(tempPath, false));
            }

            try
            {
                root.parse();
                loadedFile = root;

                PrepareTextureFiles();
            }
            catch (WMOException e)
            {
                OnWMOException(e);
            }
        }

        private void OnWMOException(WMOException e)
        {
            CancelExtraction();

            Log.Write("ERROR: Exception was caught while opening WMO file!");
            Log.Write("ERROR: " + e.Message);

            ErrorMessage("Sorry, that WMO file cannot be opened!");
        }

        private void PrepareTextureFiles()
        {
            loadingWindow.SetFirstLine("Extracting WMO textures...");

            Chunk_MOTX texChunk = (Chunk_MOTX)loadedFile.getChunk(Chunk_MOTX.Magic);
            requiredTex = new List<ExtractState>(texChunk.textures.count());
            foreach (string tex in texChunk.textures.all())
                requiredTex.Add(new ExtractState(tex));

            UpdateTexturePrepStatus();

            foreach (ExtractState state in requiredTex)
            {
                string file = (string)state.File;
                string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, file);

                if (!File.Exists(tempPath))
                {
                    RunnerExtractItemUnsafe runner = new RunnerExtractItemUnsafe(file);
                    state.TrackerID = runner.runnerID;
                    texRunners.Add(runner);
                    runner.Begin();
                }
                else
                {
                    state.State = true;
                }
            }
        }

        private void UpdateTexturePrepStatus()
        {
            loadingWindow.SetSecondLine(string.Format("{0} / {1} extracted!", requiredTex.Count(e => e.State), requiredTex.Count));
        }

        private void CreateWMOMesh()
        {
            loadingWindow.Close();
            loadingWindow = null;

            if (loadedFile == null)
                return;

            Log.Write("CreateWMOMesh: Created new meshes from WMO data...");

            texManager.clear(); // Clear any existing textures from the GL.
            meshes.Clear(); // Clear existing meshes.
            UI_MeshList.Items.Clear();

            // Load all textures into the texture manager.
            Chunk_MOTX texChunk = (Chunk_MOTX)loadedFile.getChunk(Chunk_MOTX.Magic);
            foreach (KeyValuePair<int, string> tex in texChunk.textures.raw())
                texManager.addTexture(tex.Key, Path.Combine(Constants.TEMP_DIRECTORY, tex.Value));

            Chunk_MOGN nameChunk = (Chunk_MOGN)loadedFile.getChunk(Chunk_MOGN.Magic);
            
            // Material register.
            Chunk_MOMT matChunk = (Chunk_MOMT)loadedFile.getChunk(Chunk_MOMT.Magic);

            foreach (Chunk_Base rawChunk in loadedFile.getChunksByID(Chunk_MOGP.Magic))
            {
                Chunk_MOGP chunk = (Chunk_MOGP)rawChunk;
                string meshName = nameChunk.data.get((int)chunk.groupNameIndex);

                // Skip antiportals.
                if (meshName.ToLower().Equals("antiportal"))
                    continue;

                Mesh mesh = new Mesh(meshName);

                // Populate mesh with vertices.
                Chunk_MOVT vertChunk = (Chunk_MOVT)chunk.getChunk(Chunk_MOVT.Magic);
                foreach (Position vertPos in vertChunk.vertices)
                    mesh.addVert(vertPos);

                // Populate mesh with UVs.
                Chunk_MOTV uvChunk = (Chunk_MOTV)chunk.getChunk(Chunk_MOTV.Magic);
                foreach (UV uv in uvChunk.uvData)
                    mesh.addUV(uv);

                // Populate mesh with triangles (faces).
                Chunk_MOVI faceChunk = (Chunk_MOVI)chunk.getChunk(Chunk_MOVI.Magic);
                Chunk_MOPY faceMatChunk = (Chunk_MOPY)chunk.getChunk(Chunk_MOPY.Magic);

                for (int i = 0; i < faceChunk.positions.Length; i++)
                {
                    FacePosition position = faceChunk.positions[i];
                    FaceInfo info = faceMatChunk.faceInfo[i];

                    if (info.materialID != 0xFF) // 0xFF (255) identifies a collision face.
                    {
                        Material mat = matChunk.materials[info.materialID];
                        uint texID = texManager.getTexture((int)mat.texture1.offset);

                        mesh.addFace(texID, mat.texture2.colour, position.point1, position.point2, position.point3);
                    }
                }

                Log.Write("CreateWMOMesh: " + mesh.ToAdvancedString());
                meshes.Add(mesh);
                UI_MeshList.Items.Add(mesh);
            }

            for (int i = 0; i < UI_MeshList.Items.Count; i++)
                UI_MeshList.SetItemChecked(i, true);
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
            EventManager.FileExtractComplete -= OnFileExtractComplete;
            EventManager.CASCLoadStart -= OnCASCLoadStart;

            CancelExtraction();
            explorer.Dispose();
        }

        private void TerminateRunners()
        {
            foreach (RunnerExtractItem runner in runners)
                runner.Kill();

            foreach (RunnerExtractItemUnsafe runner in texRunners)
                runner.Kill();

            runners.Clear();
            texRunners.Clear();
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
                    string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, ((CASCFile) target.File).FullName);

                    if (!File.Exists(tempPath))
                    {
                        RunnerExtractItem extract = new RunnerExtractItem(((CASCFile)target.File));
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
            if (e is FileExtractCompleteArgs)
            {
                FileExtractCompleteArgs args = (FileExtractCompleteArgs)e;
                ExtractState match = requiredFiles.FirstOrDefault(f => f.TrackerID == args.RunnerID);

                if (match != null)
                {
                    if (!args.Success)
                    {
                        CancelExtraction();
                        ErrorMessage(string.Format("Unable to extract WMO file '{0}'.", args.File.FullName));
                    }

                    match.State = true;

                    if (!requiredFiles.Any(f => !f.State))
                        LoadWMOFile();
                }
            }
            else if (e is FileExtractCompleteUnsafeArgs)
            {
                FileExtractCompleteUnsafeArgs args = (FileExtractCompleteUnsafeArgs)e;
                ExtractState texMatch = requiredTex.FirstOrDefault(f => f.TrackerID == args.RunnerID);

                if (texMatch != null)
                {
                    if (!args.Success)
                    {
                        CancelExtraction();
                        ErrorMessage(string.Format("Unable to extract WMO texture '{0}'.", args.File));
                    }

                    texMatch.State = true;

                    if (!requiredTex.Any(f => !f.State))
                    {
                        try
                        {
                            CreateWMOMesh();
                        }
                        catch (WMOException ex)
                        {
                            OnWMOException(ex);
                        }
                    }
                    else
                    {
                        UpdateTexturePrepStatus();
                    }
                }
            }
        }

        private void UI_MeshList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ((Mesh)UI_MeshList.Items[e.Index]).ShouldRender = e.NewValue == CheckState.Checked;
        }

        private void UI_ExportObjButton_Click(object sender, EventArgs e)
        {
            UI_ExportSaveDialog.FileName = Path.GetFileNameWithoutExtension(loadedFile.baseName) + ".obj";
            if (UI_ExportSaveDialog.ShowDialog() == DialogResult.OK)
            {
                EventManager.ExportBLPtoPNGComplete += OnExportBLPtoPNGComplete;

                WaveFrontWriter writer = new WaveFrontWriter(UI_ExportSaveDialog.FileName, texManager);
                foreach (Mesh mesh in meshes)
                    if (mesh.ShouldRender)
                        writer.addMesh(mesh);

                writer.Write();
                writer.Close();

                loadingWindow = new LoadingWindow("Exporting WMO as WaveFront OBJ...", "*Loud disconcerting grinding of cogs*");
                loadingWindow.ShowDialog();
            }
        }

        private void OnExportBLPtoPNGComplete(object sender, EventArgs e)
        {
            ExportBLPtoPNGArgs args = (ExportBLPtoPNGArgs)e;
            EventManager.ExportBLPtoPNGComplete -= OnExportBLPtoPNGComplete;

            if (!args.Success)
                ErrorMessage("Unable to export textures!");

            loadingWindow.Close();
            loadingWindow = null;
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            foreach (Mesh mesh in meshes)
                if (mesh.ShouldRender)
                    mesh.Draw(gl);

            gl.Disable(OpenGL.GL_TEXTURE_2D);

            //  Nudge the rotation.
            rotation += 3.0f;
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            gl.DepthFunc(OpenGL.GL_LESS);
            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            //  Set the clear color.
            gl.ClearColor(1, 1, 1, 1);
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Perspective(60.0f, openGLControl.Width / openGLControl.Height, 0.01, 900.0);
            gl.LookAt(50, 20, 50, 0, 0, 0, 0, 1, 0);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
    }
}
