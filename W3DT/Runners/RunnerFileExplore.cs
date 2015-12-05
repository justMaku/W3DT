using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using W3DT.Events;
using W3DT.CASC;

namespace W3DT.Runners
{
    class RunnerFileExplore : RunnerBase
    {
        private string id;
        private string filter;
        private StringHashPair[] files;

        public RunnerFileExplore(string id, List<StringHashPair> files, string filter = null)
        {
            this.id = id;
            this.files = new StringHashPair[files.Count];
            this.filter = filter;

            files.CopyTo(this.files);
        }

        public override void Work()
        {
            Thread.Sleep(500);

            foreach (StringHashPair file in files)
            {
                if (filter == null || file.Value.Contains(filter))
                    EventManager.Trigger_FileExploreHit(new FileExploreHitArgs(id, file));
            }

            EventManager.Trigger_FileExploreDone(new FileExploreDoneArgs(id));
        }
    }
}
