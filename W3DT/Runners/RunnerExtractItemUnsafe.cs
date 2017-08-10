using System.IO;
using W3DT.Events;

namespace W3DT.Runners
{
    class RunnerExtractItemUnsafe : RunnerBase
    {
        private static int E_RUNNER_ID = 1;

        private string[] files;
        public int runnerID { get; private set; }
        public bool CacheCheck { get; set; }

        public RunnerExtractItemUnsafe(params string[] files)
        {
            this.files = files;
            CacheCheck = false;

            runnerID = E_RUNNER_ID;
            E_RUNNER_ID++;
        }

        public override void Work()
        {
            bool ready = Program.IsCASCReady;

            foreach (string file in files)
            {
                bool success = false;
                Log.Write("Extracting CASC item: " + file);

                if (Program.IsCASCReady)
                {
                    try
                    {
                        if (CacheCheck)
                        {
                            string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, file);
                            if (!File.Exists(tempPath))
                                Program.CASCEngine.SaveFileTo(file, Constants.TEMP_DIRECTORY);
                        }
                        else
                        {
                            Program.CASCEngine.SaveFileTo(file, Constants.TEMP_DIRECTORY);
                        }

                        success = true;
                    }
                    catch
                    {
                        Log.Write("Unable to extract item: " + file);
                    }
                }

                EventManager.Trigger_FileExtractUnsafeComplete(file, success, runnerID);
            }
        }

        public new void Kill()
        {
            foreach (string file in files)
            {
                string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, file);

                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }

            if (thread != null && thread.IsAlive)
                thread.Abort();
        }
    }
}
