using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.CASC;

namespace W3DT.Events
{
    class FileExtractArgs : EventArgs
    {
        public bool Success { get; private set; }
        public int RunnerID { get; private set; }

        public FileExtractArgs(bool success, int runnerID)
        {
            Success = success;
            RunnerID = runnerID;
        }
    }
}
