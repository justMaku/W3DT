using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
