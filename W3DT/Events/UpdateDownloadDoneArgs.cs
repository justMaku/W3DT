using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Events
{
    class UpdateDownloadDoneArgs : EventArgs
    {
        public bool Success { get; private set; }

        public UpdateDownloadDoneArgs(bool success)
        {
            Success = success;
        }
    }
}
