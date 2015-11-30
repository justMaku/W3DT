using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Events
{
    class CASCLoadDoneArgs : EventArgs
    {
        public bool Success { get; private set; }

        public CASCLoadDoneArgs(bool success)
        {
            Success = success;
        }
    }
}
