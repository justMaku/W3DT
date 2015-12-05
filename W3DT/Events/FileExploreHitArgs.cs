using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Events
{
    class FileExploreHitArgs : EventArgs
    {
        public string ID { get; private set; }
        public string File { get; private set; }

        public FileExploreHitArgs(string identifier, string file)
        {
            ID = identifier;
            File = file;
        }
    }
}
