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
        private CASCFile file;

        public RunnerExtractItem(CASCFile file)
        {
            this.file = file;
        }

        public override void Work()
        {
            Log.Write("Extracting CASC item: " + file.FullName);
            if (!Program.IsCASCReady())
                return;

            Program.CASCEngine.SaveFileTo(file.FullName, Constants.TEMP_DIRECTORY);

            //RootEntry entry = Program.CASCEngine.RootHandler.RootData[file.Hash].FirstOrDefault();
            //Program.CASCEngine.SaveFileTo(file.Hash, Constants.TEMP_DIRECTORY, file.Value);
            //Program.CASCEngine.ExtractFile(entry.MD5, Constants.TEMP_DIRECTORY, file.Value);

            EventManager.Trigger_FileExtractComplete(new FileExtractCompleteArgs(file));
        }
    }
}
