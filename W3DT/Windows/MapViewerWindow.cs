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
using W3DT.Helpers;
using SereniaBLPLib;

namespace W3DT
{
    public partial class MapViewerWindow : Form
    {
        private Explorer explorer;
        private Dictionary<string, List<CASCFile>> maps;

        private int tileTotal = 0;
        private int tileDone = 0;

        private RunnerBuildMinimap buildRunner;
        private BitmapCanvas canvas;

        private int drawOffsetX = 0;
        private int drawOffsetY = 0;
        private int lastOffsetX = 0;
        private int lastOffsetY = 0;

        private string selectedMapName;
        private RunnerMapExport exportRunner;
        private LoadingWindow loadingWindow;
        private Action exportCancelCallback;

        // Mouse input
        private int mouseStartX;
        private int mouseStartY;
        private bool isMovingMap = false;

        public MapViewerWindow()
        {
            InitializeComponent();

            maps = new Dictionary<string, List<CASCFile>>();

            explorer = new Explorer(this, "^World\\Minimaps\\", null, UI_FilterTimer, null, null, new string[] { "blp" }, "MVT_N_{0}", true);
            explorer.ExploreHitCallback = OnExploreHit;
            explorer.ExploreDoneCallback = OnExploreDone;

            EventManager.MapExportDone += OnMapExportDone;
            EventManager.CASCLoadStart += OnCASCLoadStart;
            EventManager.MinimapTileDone += OnMinimapTileDone;
            explorer.Initialize();

            exportCancelCallback = CancelExport;
        }

        private void OnFileExploreHit(object sender, EventArgs e)
        {
            FileExploreHitArgs args = (FileExploreHitArgs)e;
        }

        private void TerminateRunners()
        {
            if (buildRunner != null)
            {
                buildRunner.Kill();
                buildRunner = null;
            }
        }

        private void OnExploreHit(CASCFile file)
        {
            string[] parts = file.FullName.Split(new char[] { '/', '\\' });
            string mapName = parts[2];

            if (!mapName.ToLower().Equals("wmo"))
            {
                if (!maps.ContainsKey(mapName))
                {
                    UI_FileList.Nodes.Add(mapName);
                    maps.Add(mapName, new List<CASCFile>());
                }

                maps[mapName].Add(file);
                UpdateSearchState(Constants.SEARCH_STATE_SEARCHING);
            }
        }

        private void OnExploreDone()
        {
            UpdateSearchState(Constants.SEARCH_STATE_DONE);
        }

        private void UpdateSearchState(string state)
        {
            UI_FileCount_Label.Text = string.Format(Constants.MAP_SEARCH_STATE, maps.Count, state);
        }

        private void OnCASCLoadStart(object sender, EventArgs e)
        {
            Close();
        }

        private void UI_FileList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = UI_FileList.SelectedNode;

            if (selected != null)
            {
                // Clean up previous excursions.
                TerminateRunners(); // Terminate runners that be running.

                if (canvas != null)
                    canvas.Dispose();

                canvas = null;

                UI_PreviewStatus.Hide();
                UI_Map.Invalidate();
                UI_ExportButton.Show();

                drawOffsetX = lastOffsetX = 0;
                drawOffsetY = lastOffsetY = 0;

                // Detatch mouse control (this shouldn't ever be an issue, really).
                isMovingMap = false;

                string mapName = selected.Text;
                selectedMapName = mapName;

                tileTotal = maps[mapName].Count;
                tileDone = 0;
                UI_TileDisplay.Text = string.Format(Constants.MAP_VIEWER_TILE_STATUS, 0, 0);
                UI_TileDisplay.Show();

                buildRunner = new RunnerBuildMinimap(maps[mapName].ToArray());
                buildRunner.Begin();
            }
        }

        private void OnMinimapTileDone(object sender, EventArgs e)
        {
            MinimapTileReadyArgs args = (MinimapTileReadyArgs)e;

            if (canvas == null)
            {
                int sizeX = (args.Bounds.HighX - args.Bounds.LowX) + 1;
                int sizeY = (args.Bounds.HighY - args.Bounds.LowY) + 1;

                canvas = new BitmapCanvas(sizeX, sizeY);
            }

            int posX = (args.Position.X - args.Bounds.LowX) * 256;
            int posY = (args.Position.Y - args.Bounds.LowY) * 256;

            canvas.DrawToCanvas(args.Image, posX, posY);
            UI_Map.Invalidate();

            tileDone++;
            if (tileTotal > tileDone)
                UI_TileDisplay.Text = string.Format(Constants.MAP_VIEWER_TILE_STATUS, tileDone, tileTotal);
            else
                UI_TileDisplay.Hide();
        }

        private void MapViewerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unregister events.
            EventManager.CASCLoadStart -= OnCASCLoadStart;
            EventManager.MinimapTileDone -= OnMinimapTileDone;

            CancelExport();

            TerminateRunners();
            explorer.Dispose();
        }

        private void MapViewerWindow_ResizeEnd(object sender, EventArgs e)
        {
            UI_Map.Invalidate();
        }

        private void UI_Map_MouseDown(object sender, MouseEventArgs e)
        {
            isMovingMap = true;
            mouseStartX = e.X;
            mouseStartY = e.Y;
        }

        private void UI_Map_MouseUp(object sender, MouseEventArgs e)
        {
            isMovingMap = false;

            lastOffsetX = drawOffsetX;
            lastOffsetY = drawOffsetY;
        }

        private void UI_Map_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMovingMap)
            {
                drawOffsetX = lastOffsetX + (e.X - mouseStartX);
                drawOffsetY = lastOffsetY + (e.Y - mouseStartY);

                // ToDo: Prevent scrolling out of bounds here.

                UI_Map.Invalidate();
            }
        }

        private void UI_Map_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(UI_Map.BackColor);

            if (canvas != null)
                canvas.Draw(e.Graphics, drawOffsetX, drawOffsetY, UI_Map.Width, UI_Map.Height);
        }

        private void UI_ExportButton_Click(object sender, EventArgs e)
        {
            // Ensure we actually have a map selected.
            if (selectedMapName == null)
            {
                UI_ExportButton.Hide();
                return;
            }

            UI_SaveDialog.FileName = selectedMapName + ".obj";
            if (UI_SaveDialog.ShowDialog() == DialogResult.OK)
                BeginMapExport(UI_SaveDialog.FileName);
        }

        private void BeginMapExport(string fileName)
        {
            exportRunner = new RunnerMapExport(selectedMapName, fileName);
            exportRunner.Begin();

            loadingWindow = new LoadingWindow(string.Format("Exporting {0}...", selectedMapName), "Depending on map size, this may take a while.", true, exportCancelCallback);
            loadingWindow.ShowDialog();
        }

        private void OnMapExportDone(object sender, EventArgs e)
        {
            MapExportDoneArgs args = (MapExportDoneArgs)e;

            CloseLoadingWindow();

            if (args.Success)
                Alert.Show(string.Format("{0} successfully extracted!", selectedMapName), true);
            else
                Alert.Show(string.Format("Unable to extract {0}!", selectedMapName));
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

        private void CancelExport()
        {
            CloseLoadingWindow();

            if (exportRunner != null)
            {
                exportRunner.Kill();
                exportRunner = null;
            }
        }
    }
}
