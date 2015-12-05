using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Events
{
    class FileExploreDoneArgs : EventArgs
    {
        public string ID { get; private set; }

        public FileExploreDoneArgs(string id)
        {
            ID = id;
        }
    }
}
