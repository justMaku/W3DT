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

        public FileExtractCompleteArgs(CASCFile file)
        {
            File = file;
        }
    }
}
