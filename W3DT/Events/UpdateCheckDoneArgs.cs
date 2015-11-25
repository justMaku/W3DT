using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.JSONContainers;

namespace W3DT.Events
{
    class UpdateCheckDoneArgs : EventArgs
    {
        public LatestReleaseData Data { get; private set; }

        public UpdateCheckDoneArgs(LatestReleaseData data)
        {
            Data = data;
        }
    }
}
