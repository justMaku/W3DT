using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using W3DT.Runners;
using W3DT.Events;
using W3DT.CASC;

namespace W3DT
{
    public class Explorer
    {
        private TextBox searchField;
        private Label searchOverlay;
        private Label status;
        private TreeView fileList;

        private Timer FilterTimer;
        private RunnerBase runner;

        public Regex IgnoreFilter;
        public Action<CASCFile> ExploreHitCallback;
        public Action ExploreDoneCallback;

        private string[] extensions;
        private string runnerID;
        private string currentScanID;
        private string hardFilter = null;

        public string[] rootFolders;

        private bool splitIntoDirectories;
        private bool filterHasChanged;

        private int found = 0;
        private int currentScan = 0;

        public ISynchronizeInvoke target { get; private set; }

        public Explorer(ISynchronizeInvoke sync, object searchField, Label searchOverlay, Timer timer, Label status, TreeView fileList, string[] extensions, string runnerID, bool splitIntoDirectories)
        {
            this.target = sync;
            this.searchOverlay = searchOverlay;
            this.status = status;
            this.fileList = fileList;
            this.extensions = extensions;
            this.runnerID = runnerID;
            this.splitIntoDirectories = splitIntoDirectories;
            this.FilterTimer = timer;

            // If we have a search field, monitor it for changes.
            if (searchField != null)
            {
                if (searchField is TextBox)
                {
                    if (timer == null)
                        throw new Exception("Explorer tasked without a timer! Bad Hannah.");

                    this.searchField = (TextBox)searchField;

                    timer.Tick += OnFilterTimerTick;
                    this.searchField.TextChanged += OnFilterChanged;

                    if (searchOverlay != null)
                        searchOverlay.MouseUp += OnOverlayMouseUp;
                }
                else
                {
                    hardFilter = searchField.ToString().ToLower();
                }
            }
        }

        public void Initialize()
        {
            if (fileList != null)
                fileList.Nodes.Clear(); // Clear existing nodes.

            // Do not continue without the CASC engine.
            if (!Program.IsCASCReady)
                return;

            found = 0; // Reset found counter.

            // File exploration hooks!
            EventManager.FileExploreHit += OnFileExploreHit;
            EventManager.FileExploreDone += OnFileExploreDone;

            if (status != null)
                status.Text = string.Format(Constants.GENERIC_WINDOW_SEARCH_STATE, 0, "Preparing...");

            currentScan++;
            currentScanID = string.Format(runnerID, currentScan);

            string filter = hardFilter;
            if (searchField != null)
            {
                string fieldText = searchField.Text.Trim();
                if (fieldText.Length > 0)
                    filter = fieldText.ToLower();
            }

            runner = new RunnerFileExplore(currentScanID, extensions, filter, rootFolders);
            runner.Begin();
        }

        public void Dispose()
        {
            // Search field exists? Unhook event and stop timer.
            if (searchField != null)
            {
                searchField.TextChanged -= OnFilterChanged;
                FilterTimer.Stop();
            }

            UnregisterHooks();

            // Don't leave the runner running!
            if (runner != null)
                runner.Kill();
        }

        private void UnregisterHooks()
        {
            EventManager.FileExploreDone -= OnFileExploreDone;
            EventManager.FileExploreHit -= OnFileExploreHit;
        }

        private void OnFilterTimerTick(object sender, EventArgs e)
        {
            if (filterHasChanged)
            {
                // Kill existing runner.
                if (runner != null)
                    runner.Kill();

                UnregisterHooks();

                filterHasChanged = false;
                FilterTimer.Stop();
                Initialize();
            }
        }

        private void OnFilterChanged(object sender, EventArgs e)
        {
            filterHasChanged = true;

            FilterTimer.Stop();
            FilterTimer.Start();

            if (searchOverlay != null)
                searchOverlay.Visible = searchField.Text.Length == 0;
        }

        private void OnOverlayMouseUp(object sender, MouseEventArgs e)
        {
            // When the user clicks on the overlay, pass focus through to the field below.
            searchField.Focus();
        }

        private void OnFileExploreDone(object sender, EventArgs e)
        {
            if (((FileExploreDoneArgs)e).ID.Equals(currentScanID))
            {
                UnregisterHooks();
                runner = null;

                if (status != null)
                    status.Text = string.Format(Constants.GENERIC_WINDOW_SEARCH_STATE, found, "Done");

                if (ExploreDoneCallback != null)
                    ExploreDoneCallback();
            }
        }

        private void OnFileExploreHit(object sender, EventArgs e)
        {
            FileExploreHitArgs args = (FileExploreHitArgs)e;

            if (args.ID.Equals(currentScanID))
            {
                // Use callback if set.
                // This is purposely above the ignore filter check.
                // Results filtered by the primary filter do not make it here.
                // IgnoreFilter is secondary and does not exclude from the callback.
                if (ExploreHitCallback != null)
                    ExploreHitCallback(args.Entry);

                // Ignore filter check.
                if (IgnoreFilter != null && IgnoreFilter.IsMatch(args.Entry.Name))
                    return;

                found++;

                if (fileList != null)
                {
                    if (splitIntoDirectories)
                    {
                        List<string> pathParts = args.Entry.FullName.Split('\\').ToList();

                        int index = 1;
                        TreeNode currentNode = null;
                        foreach (string pathPart in pathParts)
                        {
                            if (currentNode != null)
                            {
                                currentNode = TreeNodeHelper.FindOrCreateSubNode(currentNode, pathPart);

                                if (index == pathParts.Count)
                                    currentNode.Tag = args.Entry;
                            }
                            else
                            {
                                currentNode = TreeNodeHelper.FindOrCreateSubNode(fileList, pathPart);
                            }
                            index++;
                        }
                    }
                    else
                    {
                        TreeNode newNode = new TreeNode(args.Entry.Name);
                        newNode.Tag = args.Entry;
                        fileList.Nodes.Add(newNode);
                    }
                }

                if (status != null)
                    status.Text = string.Format(Constants.GENERIC_WINDOW_SEARCH_STATE, found, Constants.SEARCH_STATE_SEARCHING);
            }
        }
    }
}
