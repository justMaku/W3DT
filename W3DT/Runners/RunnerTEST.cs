using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using W3DT.Events;
using W3DT.CASC;

namespace W3DT.Runners
{
    class RunnerTEST : RunnerBase
    {
        public override void Work()
        {
            Thread.Sleep(500);
            Explore(Program.Root);
            //EventManager.Trigger_FileExploreDone(new FileExploreDoneArgs(id));
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
                    if (file.Name.ToLower().Contains("illidan2"))
                    {
                        new RunnerExtractItem(file).Begin();
                        return;
                    }
                }
            }
        }
    }
}
