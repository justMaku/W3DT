using System;

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
