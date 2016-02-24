using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using W3DT.CASC;
using W3DT.Events;
using W3DT.Runners;
using W3DT.MapViewer;
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
        private MapCanvas canvas;
        private uint buildRunnerIndex;

        private int drawOffsetX = 0;
        private int drawOffsetY = 0;
        private int lastOffsetX = 0;
        private int lastOffsetY = 0;

        private string selectedMapName;
        private RunnerMapExport exportRunner;
        private LoadingWindow loadingWindow;
        private Action exportCancelCallback;
        
        // 2D Export
        private RunnerExport2DMap imageExportRunner;
        private Action imageExportCancelCallback;

        private Regex mapTilePattern = new Regex(@"(\d+)_(\d+)\.blp$");
        private Dictionary<string, Point> mapStartPoints;

        // Mouse input
        private int mouseStartX;
        private int mouseStartY;
        private bool isMovingMap = false;

        private Overlay overlay;

        public MapViewerWindow()
        {
            InitializeComponent();

            maps = new Dictionary<string, List<CASCFile>>();
            mapStartPoints = new Dictionary<string, Point>();
            overlay = new Overlay(256, 256);

            explorer = new Explorer(this, "^World\\Minimaps\\", null, UI_FilterTimer, null, null, new string[] { "blp" }, "MVT_N_{0}", true);
            explorer.ExploreHitCallback = OnExploreHit;
            explorer.ExploreDoneCallback = OnExploreDone;

            EventManager.MapExportDone += OnMapExportDone;
            EventManager.MapExportDone2D += OnMapExportDone2D;
            EventManager.CASCLoadStart += OnCASCLoadStart;
            EventManager.MinimapTileDone += OnMinimapTileDone;
            explorer.Initialize();

            exportCancelCallback = CancelExport;
            imageExportCancelCallback = Cancel2DExport;
        }

        private void OnMapExportDone2D(object sender, EventArgs e)
        {
            Cancel2DExport();
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

            // Ignore noLiquid tiles.
            string fileName = parts[parts.Length - 1];
            if (fileName.StartsWith("noLiquid"))
                return;

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

                // "Cheap" way to find the map entry point without
                // reading DBC files. Used by exporter for non-full exports.
                Point point;

                if (!mapStartPoints.ContainsKey(mapName))
                {
                    point = new Point(63, 63);
                    mapStartPoints.Add(mapName, point);
                }
                else
                {
                    point = mapStartPoints[mapName];
                }

                Match match = mapTilePattern.Match(fileName);
                if (match.Success)
                {
                    int lowX = int.Parse(match.Groups[1].Value);
                    int lowY = int.Parse(match.Groups[2].Value);

                    if (lowX < point.X) point.X = lowX;
                    if (lowY < point.Y) point.Y = lowY;

                    mapStartPoints[mapName] = point;
                }
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
                overlay.ClearPoints();

                UI_PreviewStatus.Hide();
                UI_Map.Invalidate();

                UI_ExportButton.Enabled = true;
                UI_ExportImageButton.Enabled = true;

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
                UI_ExportTip.Hide();

                buildRunner = new RunnerBuildMinimap(maps[mapName].ToArray());
                buildRunnerIndex = buildRunner.Index;
                buildRunner.Begin();
            }
        }

        private void OnMinimapTileDone(object sender, EventArgs e)
        {
            MinimapTileReadyArgs args = (MinimapTileReadyArgs)e;

            // Ensure we're using the correct runner.
            if (args.RunnerIndex != buildRunnerIndex)
                return;

            if (canvas == null)
            {
                int sizeX = (args.Bounds.HighX - args.Bounds.LowX) + 1;
                int sizeY = (args.Bounds.HighY - args.Bounds.LowY) + 1;

                canvas = new MapCanvas(sizeX, sizeY);
            }

            int posX = (args.Position.X - args.Bounds.LowX) * 256;
            int posY = (args.Position.Y - args.Bounds.LowY) * 256;

            canvas.DrawToCanvas(args.Image, posX, posY);
            UI_Map.Invalidate();

            tileDone++;
            if (tileTotal > tileDone)
            {
                UI_TileDisplay.Text = string.Format(Constants.MAP_VIEWER_TILE_STATUS, tileDone, tileTotal);
            }
            else
            {
                UI_TileDisplay.Hide();
                UI_ExportTip.Show();
            }
        }

        private void MapViewerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unregister events.
            EventManager.CASCLoadStart -= OnCASCLoadStart;
            EventManager.MapExportDone -= OnMapExportDone;
            EventManager.MinimapTileDone -= OnMinimapTileDone;
            EventManager.MapExportDone2D -= OnMapExportDone2D;

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
            if (Control.ModifierKeys.HasFlag(Keys.Shift))
            {
                int pointX = e.Location.X - drawOffsetX;
                int pointY = e.Location.Y - drawOffsetY;

                pointX -= pointX % 256;
                pointY -= pointY % 256;

                overlay.ToggleOverlay(new Point(pointX, pointY));
                UI_Map.Invalidate();
            }
            else
            {
                isMovingMap = true;
                mouseStartX = e.X;
                mouseStartY = e.Y;
            }
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
                canvas.Draw(e.Graphics, drawOffsetX, drawOffsetY, UI_Map.Width, UI_Map.Height, overlay);
        }

        private void UI_ExportButton_Click(object sender, EventArgs e)
        {
            // Ensure we actually have a map selected.
            if (selectedMapName == null)
            {
                UI_ExportButton.Enabled = false;
                return;
            }

            int exportSize = overlay.Points.Count > 0 ? overlay.Points.Count : maps[selectedMapName].Count;
            //bool confirm = true;
            string message = null;

            if (exportSize >= 100)
                message = Constants.MAP_VIEWER_WARNING_INSANE;
            else if (exportSize >= 10)
                message = Constants.MAP_VIEWER_WARNING_LARGE;
            else if (exportSize >= 4)
                message = Constants.MAP_VIEWER_WARNING;

            if (message == null || MessageBox.Show(message, Constants.MAP_VIEWER_WARNING_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                UI_SaveDialog.FileName = selectedMapName + ".obj";
                UI_SaveDialog.Filter = "WaveFront OBJ (*.obj)|*.obj";
                if (UI_SaveDialog.ShowDialog() == DialogResult.OK)
                    BeginMapExport(UI_SaveDialog.FileName);
            }
        }

        private void BeginMapExport(string fileName)
        {
            // Calculate which tiles we want (map viewer -> ADT)
            List<Point> points = new List<Point>();
            Point mapPoint = mapStartPoints[selectedMapName];
            foreach (Point point in overlay.Points)
                points.Add(new Point(mapPoint.X + (point.X / 256), mapPoint.Y + (point.Y / 256)));

            exportRunner = new RunnerMapExport(selectedMapName, fileName, points);
            exportRunner.Begin();

            loadingWindow = new LoadingWindow(string.Format("Exporting {0}...", selectedMapName), "Depending on map size, this may take a while.", true, exportCancelCallback);
            loadingWindow.ShowDialog();
        }

        private void OnMapExportDone(object sender, EventArgs e)
        {
            MapExportDoneArgs args = (MapExportDoneArgs)e;

            CloseLoadingWindow();

            if (args.Success)
            {
                Alert.Show(string.Format("{0} successfully extracted!", selectedMapName), true);
            }
            else
            {
                Alert.Show(string.Format("Unable to extract {0}!", selectedMapName));
                Log.Write("Unable to extract {0} -> {1}", selectedMapName, args.Message);
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

        private void CancelExport()
        {
            CloseLoadingWindow();

            if (exportRunner != null)
            {
                exportRunner.Kill();
                exportRunner = null;
            }
        }

        private void UI_ExportImageButton_Click(object sender, EventArgs e)
        {
            if (selectedMapName == null)
            {
                UI_ExportImageButton.Enabled = false;
                return;
            }

            UI_SaveDialog.Filter = "Portal Network Graphics (*.png)|*.png";
            UI_SaveDialog.FileName = selectedMapName + ".png";

            if (UI_SaveDialog.ShowDialog() == DialogResult.OK)
            {
                imageExportRunner = new RunnerExport2DMap(UI_SaveDialog.FileName, canvas);
                imageExportRunner.Begin();

                loadingWindow = new LoadingWindow(string.Format("Exporting {0} as a 2D image...", selectedMapName), "This probably won't take too long!", true, imageExportCancelCallback);
                loadingWindow.ShowDialog();
            }
        }

        private void Cancel2DExport()
        {
            CloseLoadingWindow();

            if (imageExportRunner != null)
            {
                imageExportRunner.Kill();
                imageExportRunner = null;
            }
        }
    }
}
