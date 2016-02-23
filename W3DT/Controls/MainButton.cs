using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace W3DT
{
    class MainButton
    {
        private static int index = 0;

        public Bitmap Image { get; private set; }
        public string Text { get; private set; }
        public string Description { get; private set; }
        public int Index { get; private set; }
        public Rectangle Bounds { get; private set; }
        public Type WindowType { get; private set; }

        public MainButton(Bitmap image, Type type, string text, string desc)
        {
            Image = image;
            Text = text;
            Description = desc;
            Index = index;
            WindowType = type;
            Bounds = new Rectangle((Index * 68) + ((Index + 1) * 6) + 12, 12, 56, 56);

            index++;
        }
    }
}
