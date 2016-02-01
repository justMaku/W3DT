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
    public partial class MapViewerWindow : Form
    {
        private Explorer explorer;
        private Dictionary<string, List<string>> maps;

        public MapViewerWindow()
        {
            InitializeComponent();

            maps = new Dictionary<string, List<string>>();

            explorer = new Explorer(this, "^World\\Minimaps\\", null, UI_FilterTimer, null, null, new string[] { "blp" }, "MVT_N_{0}", true);
            explorer.ExploreHitCallback = OnExploreHit;
            explorer.ExploreDoneCallback = OnExploreDone;

            EventManager.CASCLoadStart += OnCASCLoadStart;
            explorer.Initialize();
        }

        private void OnExploreHit(CASCFile file)
        {
            string[] parts = file.FullName.Split(new char[] { '/', '\\' });
            string mapName = parts[parts.Length - 2];

            if (!maps.ContainsKey(mapName))
            {
                UI_FileList.Nodes.Add(mapName);
                maps.Add(mapName, new List<string>());
            }

            maps[mapName].Add(file.Name);
            UpdateSearchState(Constants.SEARCH_STATE_SEARCHING);
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

        private void MapViewerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            explorer.Dispose();
        }

        private void UI_FileList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = UI_FileList.SelectedNode;

            if (selected != null && selected.Tag is CASCFile)
            {
                // ToDo: Load the map for the selected map.
            }
        }

        private void ArtExplorerWindow_ResizeEnd(object sender, EventArgs e)
        {
            /*if (currentImage != null)
            {
                  Graphics gfx = UI_ImagePreview.CreateGraphics();
                  gfx.Clear(UI_ImagePreview.BackColor);
                  gfx.DrawImage(currentImage, 0, 0);
            }*/
        }
    }
}
