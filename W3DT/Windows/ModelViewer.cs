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
using System.IO;
using W3DT.CASC;
using W3DT.Runners;
using W3DT.Events;
using W3DT.Formats;

namespace W3DT
{
    public partial class ModelViewer : Form
    {
        private Explorer explorer;

        private RunnerExtractItem runner;
        private LoadingWindow loadingWindow;
        private Action cancelCallback;

        private string selectedFileName;

        public ModelViewer()
        {
            InitializeComponent();

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
            TerminateRunners();
        }
    }
}
