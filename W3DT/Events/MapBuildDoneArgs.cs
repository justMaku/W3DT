using System;
using System.Drawing;

namespace W3DT.Events
{
    public class MapBuildDoneArgs : EventArgs
    {
        public Bitmap Data { get; private set; }

        public MapBuildDoneArgs(Bitmap data)
        {
            Data = data;
        }
    }
}
