using System;
using System.Drawing;

namespace W3DT.Events
{
    public class ModelViewerBackgroundChangedArgs : EventArgs
    {
        public Color Colour { get; private set; }

        public ModelViewerBackgroundChangedArgs(Color colour)
        {
            Colour = colour;
        }
    }
}
