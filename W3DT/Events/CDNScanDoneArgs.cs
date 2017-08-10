using System;

namespace W3DT.Events
{
    class CDNScanDoneArgs : EventArgs
    {
        public string BestHost { get; private set; }
        public string HostPath { get; private set; }

        public CDNScanDoneArgs(string bestHost, string hostPath)
        {
            BestHost = bestHost;
            HostPath = hostPath;
        }
    }
}
