using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
