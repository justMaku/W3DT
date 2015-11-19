using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.JSONContainers;

namespace W3DT.Events
{
    static class EventManager
    {
        public delegate void E_UpdateCheckComplete(LatestReleaseData data);
        public static event E_UpdateCheckComplete H_UpdateCheckComplete;

        public delegate void E_UpdateDownloadComplete(bool success);
        public static event E_UpdateDownloadComplete H_UpdateDownloadComplete;

        public static void T_UpdateCheckComplete(LatestReleaseData data)
        {
            if (H_UpdateCheckComplete != null)
                H_UpdateCheckComplete(data);
        }

        public static void T_UpdateDownloadComplete(bool success)
        {
            if (H_UpdateDownloadComplete != null)
                H_UpdateDownloadComplete(success);
        }
    }
}
