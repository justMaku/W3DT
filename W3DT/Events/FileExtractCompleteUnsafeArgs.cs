using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.CASC;

namespace W3DT.Events
{
    class FileExtractCompleteUnsafeArgs : FileExtractArgs
    {
        public string File { get; private set; }

        public FileExtractCompleteUnsafeArgs(string file, bool success, int runnerID) : base(success, runnerID)
        {
            File = file;
        }
    }
}
