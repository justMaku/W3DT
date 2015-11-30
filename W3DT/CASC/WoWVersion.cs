using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.CASC
{
    class WoWVersion
    {
        public string UrlTag { get; set; }
        public string Title { get; set; }

        public WoWVersion(string urlTag, string title)
        {
            UrlTag = urlTag;
            Title = title;
        }

        public WoWVersion() { }

        public override string ToString()
        {
            return Title;
        }
    }
}
