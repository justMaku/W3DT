using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.JSONContainers
{
    public class LatestReleaseData
    {
        public string message { get; set; }
        public string tag_name { get; set; }
        public ReleaseAsset[] assets { get; set; }
    }
}
