using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.CASC;

namespace W3DT.Events
{
    class FileExtractCompleteArgs : EventArgs
    {
        //public string GamePath { get; private set; }
        //public string LocalPath { get; private set; }
        public StringHashPair File { get; private set; }

        public FileExtractCompleteArgs(StringHashPair file)
        {
            File = file;
        }
    }
}
