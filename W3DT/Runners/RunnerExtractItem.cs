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
        private static int E_RUNNER_ID = 1;

        private CASCFile file;
        public int runnerID { get; private set; }

        public RunnerExtractItem(CASCFile file)
        {
            this.file = file;

            runnerID = E_RUNNER_ID;
            E_RUNNER_ID++;
        }

        public override void Work()
        {
            bool success = false;
            Log.Write("Extracting CASC item: " + file.FullName);

            if (Program.IsCASCReady())
            {
                try
                {
                    Program.CASCEngine.SaveFileTo(file.FullName, Constants.TEMP_DIRECTORY);
                    success = true;
                }
                catch
                {
                    Log.Write("Unable to extract item: " + file.FullName);
                }
            }

            EventManager.Trigger_FileExtractComplete(new FileExtractCompleteArgs(file, success, runnerID));
        }
    }
}
