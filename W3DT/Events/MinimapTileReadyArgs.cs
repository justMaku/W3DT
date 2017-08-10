using System;
using W3DT.Runners;

namespace W3DT.Events
{
    public class MinimapTileReadyArgs : EventArgs
    {
        public string Image { get; private set; }
        public MapTileXY Position { get; private set; }
        public MapTileBounds Bounds { get; private set; }
        public uint RunnerIndex { get; private set; }

        public MinimapTileReadyArgs(MapTileXY position, MapTileBounds bounds, string image, uint index)
        {
            Position = position;
            Bounds = bounds;
            Image = image;
            RunnerIndex = index;
        }
    }
}
