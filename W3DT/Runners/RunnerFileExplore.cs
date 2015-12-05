using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using W3DT.Events;

namespace W3DT.Runners
{
    class RunnerFileExplore : RunnerBase
    {
        private string id;
        private string filter;
        private string[] files;

        public RunnerFileExplore(string id, List<string> files, string filter = null)
        {
            this.id = id;
            this.files = new string[files.Count];
            this.filter = filter;

            files.CopyTo(this.files);
        }

        public override void Work()
        {
            foreach (string file in files)
            {
                if (filter == null || file.Contains(filter))
                    EventManager.Trigger_FileExploreHit(new FileExploreHitArgs(id, file));

                Thread.Sleep(5);
            }

            EventManager.Trigger_FileExploreDone(new FileExploreDoneArgs(id));
        }
    }
}
