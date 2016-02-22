using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace W3DT.MapViewer
{
    public class Overlay
    {
        public Bitmap Image { get; private set; }
        public List<Point> Points { get; private set; }

        public Overlay(int width, int height)
        {
            Points = new List<Point>();
            Image = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(Image))
            using (Brush brsh = new SolidBrush(Color.FromArgb(100, 144, 195, 212)))
                gfx.FillRectangle(brsh, 0, 0, width, height);
        }

        public bool ShouldDrawOverlay(Point point)
        {
            return Points.Contains(point);
        }

        public void ToggleOverlay(Point point)
        {
            if (Points.Contains(point))
                Points.Remove(point);
            else
                Points.Add(point);
        }

        public void ClearPoints()
        {
            Points.Clear();
        }
    }
}
