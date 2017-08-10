using System;

namespace W3DT.Events
{
    public class LoadingPromptArgs : EventArgs
    {
        public string Message { get; private set; }

        public LoadingPromptArgs(string message)
        {
            this.Message = message;
        }
    }
}
