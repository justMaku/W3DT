using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace W3DT.Controls
{
    public partial class AwesomeTooltip : Control
    {
        public string HeaderText { get; set; }
        public string BodyText { get; set; }

        public AwesomeTooltip(string header, string body)
        {
            HeaderText = header;
            BodyText = body;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            BackColor = Color.Black;
            Size = new Size(270, 50);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // ToDo: Make this less flickery.

            Graphics g = e.Graphics;
            Rectangle bounds = new Rectangle(8, 8, this.Width - 16, this.Height - 16);

            Color frmColor = this.Parent.BackColor;
            Brush bckColor = default(Brush);

            bckColor = new SolidBrush(Color.FromArgb((80 * 255) / 100, this.BackColor));
            if (this.BackColor != Color.Transparent)
                g.FillRectangle(bckColor, bounds);

            int yOffset = Height - 8;
            int xOffset = Width - 8;

            g.DrawImage(global::W3DT.Properties.Resources.UI_Tooltip_TL, 0, 0);
            g.DrawImage(global::W3DT.Properties.Resources.UI_Tooltip_TR, xOffset, 0);
            g.DrawImage(global::W3DT.Properties.Resources.UI_Tooltip_BR, xOffset, yOffset);
            g.DrawImage(global::W3DT.Properties.Resources.UI_Tooltip_BL, 0, yOffset);

            Image top = global::W3DT.Properties.Resources.UI_Tooltip_T;
            Image bottom = global::W3DT.Properties.Resources.UI_Tooltip_B;
            Image right = global::W3DT.Properties.Resources.UI_Tooltip_R;
            Image left = global::W3DT.Properties.Resources.UI_Tooltip_L;

            int x = 8;
            int y = 8;

            while (x < xOffset)
            {
                int diff = (x + 8) - xOffset;
                if (diff > 0)
                {
                    int ofs = 8 - diff;
                    g.DrawImage(top, x, 0, ofs, 8);
                    g.DrawImage(bottom, x, yOffset, ofs, 8);
                }
                else
                {
                    g.DrawImage(top, x, 0);
                    g.DrawImage(bottom, x, yOffset);
                }

                x += 8;
            }

            while (y < yOffset)
            {
                int diff = (y + 8) - yOffset;
                if (diff > 0)
                {
                    int ofs = 8 - diff;
                    g.DrawImage(left, 0, y, 8, ofs);
                    g.DrawImage(right, xOffset, y, 8, ofs);
                }
                else
                {
                    g.DrawImage(left, 0, y);
                    g.DrawImage(right, xOffset, y);
                }

                y += 8;
            }

            g.DrawString(HeaderText, Font, Brushes.Gold, 16, 16);

            int bodyWidth = Width - 40;
            int bodyHeight = (int)g.MeasureString(BodyText, Font, bodyWidth, StringFormat.GenericTypographic).Height + 8;
            g.DrawString(BodyText, Font, Brushes.White, new Rectangle(16, 32 + (int) Font.GetHeight(), bodyWidth, bodyHeight));

            Height = 50 + bodyHeight;

            bckColor.Dispose();
            g.Dispose();
            base.OnPaint(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (this.Parent != null)
                Parent.Invalidate(this.Bounds, true);

            base.OnBackColorChanged(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnParentBackColorChanged(e);
        }
    }
}