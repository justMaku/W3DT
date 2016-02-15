using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using W3DT.Events;
using W3DT.CASC;

namespace W3DT.Runners
{
    class RunnerExtractItem : RunnerBase
    {
        private static int E_RUNNER_ID = 1;

        private CASCFile[] files;
        public int runnerID { get; private set; }

        public RunnerExtractItem(params CASCFile[] files)
        {
            this.files = files;

            runnerID = E_RUNNER_ID;
            ThreadName += string.Format(" ({0})", runnerID);
            E_RUNNER_ID++;
        }

        public override void Work()
        {
            bool ready = Program.IsCASCReady;

            foreach (CASCFile file in files)
            {
                bool success = false;
                Log.Write("Extracting CASC item: " + file.FullName);

                if (ready)
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

                EventManager.Trigger_FileExtractComplete(file, success, runnerID);
            }
        }

        public new void Kill()
        {
            foreach (CASCFile file in files)
            {
                string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, file.FullName);

                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }

            if (thread != null && thread.IsAlive)
                thread.Abort();
        }
    }
}
