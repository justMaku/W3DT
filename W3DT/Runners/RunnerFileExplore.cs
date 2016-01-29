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
        private string id;
        private string filter;
        private string[] extensions;

        public RunnerFileExplore(string id, string[] extensions, string filter = null)
        {
            this.id = id;
            this.filter = filter;
            this.extensions = extensions;
        }

        public override void Work()
        {
            Thread.Sleep(500);
            Explore(Program.Root);
            EventManager.Trigger_FileExploreDone(new FileExploreDoneArgs(id));
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
                    if ((filter == null || file.Name.ToLower().Contains(filter)) && IsValidExtension(file))
                        EventManager.Trigger_FileExploreHit(new FileExploreHitArgs(id, file));
                }
            }
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
