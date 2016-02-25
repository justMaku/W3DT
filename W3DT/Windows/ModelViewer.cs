using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using W3DT.CASC;
using W3DT.Runners;
using W3DT.Events;

namespace W3DT
{
    public partial class ModelViewer : Form
    {
        private Explorer explorer;
        private RunnerExtractItem runner;

        public ModelViewer()
        {
            InitializeComponent();

            explorer = new Explorer(this, UI_FilterField, UI_FilterOverlay, UI_FilterTime, UI_FileCount_Label, UI_FileList, new string[] { "m2", "mdx" }, "M2_V_{0}", true);
            explorer.rootFolders = new string[] { "character", "creature" };
            explorer.Initialize();

            EventManager.FileExtractComplete += EventManager_FileExtractComplete;
        }

        private void TerminateRunners()
        {
            if (runner != null)
            {
                runner.Kill();
                runner = null;
            }
        }

        private void EventManager_FileExtractComplete(object sender, EventArgs e)
        {
            FileExtractCompleteArgs args = (FileExtractCompleteArgs)e;

            if (runner != null && args.RunnerID == runner.runnerID)
            {
                // ToDo: Load model.
            }
        }

        private void UI_FileList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = UI_FileList.SelectedNode;
            if (node != null && node.Tag is CASCFile)
            {
                CASCFile file = (CASCFile)node.Tag;
                TerminateRunners();

                runner = new RunnerExtractItem(file);
                runner.Begin();
            }
        }

        private void ModelViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            EventManager.FileExtractComplete -= EventManager_FileExtractComplete;
            TerminateRunners();
        }
    }
}
