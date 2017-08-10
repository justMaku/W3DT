using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace W3DT
{
    public partial class MainForm : Form
    {
        private Dictionary<string, Form> SubWindows;
        private Brush tooltipBrush;
        private bool tipActive = false;

        public MainForm()
        {
            InitializeComponent();
            Focus();

            SubWindows = new Dictionary<string, Form>();
            tooltipBrush = new SolidBrush(Color.FromArgb((80 * 255) / 100, Color.Black));
            DoubleBuffered = true;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (MainButton btn in Constants.MAIN_FORM_BUTTONS)
            {
                if (btn.Bounds.Contains(e.Location))
                {
                    Invalidate();
                    tipActive = true;
                    Cursor.Current = Cursors.Hand;
                    return;
                }
            }

            if (tipActive)
            {
                Invalidate();
                tipActive = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            Point mouse = PointToClient(Cursor.Position);
            foreach (MainButton btn in Constants.MAIN_FORM_BUTTONS)
            {
                if (btn.Bounds.Contains(mouse))
                {
                    if (btn.WindowType != null)
                        ShowWindow(btn.WindowType, btn.WindowType.Equals(typeof(SettingsForm)));

                    break;
                }
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            Point mouse = PointToClient(Cursor.Position);

            // Latest changes panel
            gfx.DrawImage(Properties.Resources.latest_text, 12, 87, 431, 230);
            gfx.DrawImage(Properties.Resources.cat_boat, Width - 356, Height - 371, 356, 371);

            // Top buttons
            MainButton tBtn = null;
            for (int i = 0; i < Constants.MAIN_FORM_BUTTONS.Length; i++)
            {
                MainButton btn = Constants.MAIN_FORM_BUTTONS[i];

                gfx.DrawImage(btn.Image, btn.Bounds.X, btn.Bounds.Y, btn.Bounds.Width, btn.Bounds.Height);
                gfx.DrawImage(W3DT.Properties.Resources.icon_border, btn.Bounds.X - 6, btn.Bounds.Y - 6, 68, 68);

                // Flag tooltip to be rendered.
                if (btn.Bounds.Contains(mouse))
                    tBtn = btn;
            }

            if (tBtn != null)
            {
                int tooltipHeight = 25;
                int tooltipWidth = 270;

                int xOffset = tBtn.Bounds.X + (tBtn.Bounds.Width / 2);
                int yOffset = tBtn.Bounds.Y + (tBtn.Bounds.Height / 2);

                if (xOffset + tooltipWidth + 20 >= Width)
                    xOffset -= tooltipWidth;

                // Calculate text stuff.
                int bodyWidth = tooltipWidth - 20;
                int bodyHeight = (int)gfx.MeasureString(tBtn.Description, Font, bodyWidth, StringFormat.GenericTypographic).Height + 12;
                tooltipHeight += bodyHeight;

                gfx.FillRectangle(tooltipBrush, new Rectangle(xOffset, yOffset, tooltipWidth, tooltipHeight));

                gfx.DrawImage(Properties.Resources.UI_Tooltip_TL, xOffset - 8, yOffset - 8);
                gfx.DrawImage(Properties.Resources.UI_Tooltip_TR, xOffset + tooltipWidth, yOffset - 8);
                gfx.DrawImage(Properties.Resources.UI_Tooltip_BR, xOffset + tooltipWidth, yOffset + tooltipHeight);
                gfx.DrawImage(Properties.Resources.UI_Tooltip_BL, xOffset - 8, yOffset + tooltipHeight);

                Image top = Properties.Resources.UI_Tooltip_T;
                Image bottom = Properties.Resources.UI_Tooltip_B;
                Image right = Properties.Resources.UI_Tooltip_R;
                Image left = Properties.Resources.UI_Tooltip_L;

                int x = 0;
                int y = 0;

                while (x < tooltipWidth)
                {
                    int diff = (x + 8) - tooltipWidth;
                    if (diff > 0)
                    {
                        int ofs = 8 - diff;
                        gfx.DrawImage(top, xOffset + x, yOffset - 8, ofs, 8);
                        gfx.DrawImage(bottom, xOffset + x, yOffset + tooltipHeight, ofs, 8);
                    }
                    else
                    {
                        gfx.DrawImage(top, xOffset + x, yOffset - 8);
                        gfx.DrawImage(bottom, xOffset + x, yOffset + tooltipHeight);
                    }

                    x += 8;
                }

                while (y < tooltipHeight)
                {
                    int diff = (y + 8) - tooltipHeight;
                    if (diff > 0)
                    {
                        int ofs = 8 - diff;
                        gfx.DrawImage(left, xOffset - 8, yOffset + y, 8, ofs);
                        gfx.DrawImage(right, xOffset + tooltipWidth, yOffset + y, 8, ofs);
                    }
                    else
                    {
                        gfx.DrawImage(left, xOffset - 8, yOffset + y);
                        gfx.DrawImage(right, xOffset + tooltipWidth, yOffset + y);
                    }

                    y += 8;
                }

                gfx.DrawString(tBtn.Text, Font, Brushes.Gold, xOffset + 8, yOffset + 8);
                gfx.DrawString(tBtn.Description, Font, Brushes.White, new Rectangle(xOffset + 8, yOffset + 16 + (int)Font.GetHeight(), bodyWidth, bodyHeight));
            }
        }

        private void ShowWindow(Type windowType, bool dialog = false)
        {
            Form newForm = null;
            string windowClassName = windowType.Name;

            if (SubWindows.ContainsKey(windowClassName))
            {
                Form form = SubWindows[windowClassName];
                if (form != null && !form.IsDisposed)
                    newForm = form;
            }

            if (newForm == null)
            {
                newForm = (Form)Activator.CreateInstance(windowType);
                SubWindows[windowClassName] = newForm;
            }

            if (dialog)
                newForm.ShowDialog();
            else
                newForm.Show();

            newForm.Focus();
        }
    }
}
