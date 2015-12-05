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

            EventManager.Trigger_FileExtractComplete(new FileExtractCompleteArgs(file, success));
        }
    }
}
