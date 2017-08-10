using System;

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
