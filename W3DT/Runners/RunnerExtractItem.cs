using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Events;
using W3DT.CASC;

namespace W3DT.Runners
{
    class RunnerExtractItem : RunnerBase
    {
        private StringHashPair file;

        public RunnerExtractItem(StringHashPair file)
        {
            this.file = file;
        }

        public override void Work()
        {
            Log.Write("Extracting CASC item: " + file.Value);
            if (!Program.IsCASCReady())
                return;

            Program.CASCEngine.SaveFileTo(file.Hash, Constants.TEMP_DIRECTORY, file.Value);
            EventManager.Trigger_FileExtractComplete(new FileExtractCompleteArgs(file));
        }
    }
}
