using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Events
{
    class CDNScanDoneArgs : EventArgs
    {
        public string BestHost { get; private set; }

        public CDNScanDoneArgs(string bestHost)
        {
            BestHost = bestHost;
        }
    }
}
