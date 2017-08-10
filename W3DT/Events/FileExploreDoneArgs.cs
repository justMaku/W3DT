using System;

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
