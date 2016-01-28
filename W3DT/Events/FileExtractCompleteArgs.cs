using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.CASC;

namespace W3DT.Events
{
    class FileExtractCompleteArgs : EventArgs
    {
        public CASCFile File { get; private set; }
        public bool Success { get; private set; }
        public int RunnerID { get; private set; }

        public FileExtractCompleteArgs(CASCFile file, bool success, int runnerID)
        {
            File = file;
            Success = success;
            RunnerID = runnerID;
        }
    }
}
