using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using W3DT.Events;
using W3DT.CASC;

namespace W3DT.Runners
{
    class RunnerFileExplore : RunnerBase
    {
        private enum FilterType
        {
            CONTAINS,
            STARTS_WITH,
            ENDS_WITH,
            EQUAL
        };

        private string id;
        private string filter;
        private FilterType filterType = FilterType.CONTAINS;
        private string[] extensions;

        public RunnerFileExplore(string id, string[] extensions, string filter = null)
        {
            this.id = id;
            this.filter = filter;
            this.extensions = extensions;

            // Pre-compile the filter parameters.
            if (filter != null)
            {
                char first = filter.Substring(0, 1)[0];
                if (first == '=')
                    filterType = FilterType.EQUAL;
                else if (first == '^')
                    filterType = FilterType.STARTS_WITH;
                else if (first == '$')
                    filterType = FilterType.ENDS_WITH;

                if (filterType != FilterType.CONTAINS)
                    this.filter = filter.Substring(1, filter.Length - 1);

                Log.Write("RunnerFileExplore: Using filter {0} [{1}] for search.", filter, filterType.ToString());
            }
        }

        public override void Work()
        {
            Thread.Sleep(500);
            Explore(Program.Root);
            EventManager.Trigger_FileExploreDone(id);
        }

        private void Explore(CASCFolder folder)
        {
            foreach (KeyValuePair<string, ICASCEntry> node in folder.Entries)
            {
                ICASCEntry entry = node.Value;

                if (entry is CASCFolder)
                {
                    Explore((CASCFolder)entry);
                }
                else if (entry is CASCFile)
                {
                    CASCFile file = (CASCFile)entry;

                    // Check we have a valid extension.
                    if (!IsValidExtension(file))
                        continue;

                    // Ensure the file matches the filter.
                    if (!MatchesFilter(file.FullName))
                        continue;

                    EventManager.Trigger_FileExploreHit(id, file);
                }
            }
        }

        private bool MatchesFilter(string file)
        {
            if (filter == null)
                return true;

            file = file.ToLower();
            if (filterType == FilterType.CONTAINS)
                return file.Contains(filter);

            if (filterType == FilterType.STARTS_WITH)
                return file.StartsWith(filter);

            if (filterType == FilterType.ENDS_WITH)
                return file.EndsWith(filter);

            // Default to FilterType.EQUALS
            return file == filter;
        }

        private bool IsValidExtension(CASCFile file)
        {
            foreach (string extension in extensions)
                if (file.Name.EndsWith(extension))
                    return true;

            return false;
        }
    }
}
