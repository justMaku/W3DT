using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Events
{
    class ExportBLPtoPNGArgs : EventArgs
    {
        public bool Success { get; private set; }

        public ExportBLPtoPNGArgs(bool success)
        {
            Success = success;
        }
    }
}
