using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace W3DT.Controls
{
    public class LoadBar : ProgressBar
    {
        public LoadBar()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;
            Graphics g = e.Graphics;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(Brushes.Orange, 2, 2, rec.Width, rec.Height);

            string text = (string)Tag;

            using (Font f = new Font(FontFamily.GenericSansSerif, 9f))
            {
                SizeF len = g.MeasureString(text, f);
                Point location = new Point(Convert.ToInt32((Width / 2) - len.Width / 2), Convert.ToInt32((Height / 2) - len.Height / 2));
                g.DrawString(text, f, Brushes.Black, location);
            }
        }
    }
}
