using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using W3DT.Controls;

using W3DT.Events;
using W3DT.Runners;

namespace W3DT
{
    public partial class MainForm : Form
    {
        private Dictionary<string, Form> SubWindows;

        private AwesomeTooltip tip;
        private Rectangle tipBound;
        private bool tipValid = false;

        public MainForm()
        {
            InitializeComponent();
            Focus();

            SubWindows = new Dictionary<string, Form>();
        }

        private void UpdateTooltip(object sender, MouseEventArgs e)
        {
            if (tip != null && !tipBound.Contains(PointToClient(Cursor.Position)))
            {
                tip.Hide();
                tipValid = false;
            }
        }

        private void ShowTooltip(object sender, EventArgs args)
        {
            if (tipValid)
                return;

            Control control = (Control)sender;
            string[] parts = ((string)control.Tag).Split('|');
            if (tip == null)
            {
                tip = new AwesomeTooltip(parts[0], parts[1]);
                tip.MouseMove += UpdateTooltip;
                Controls.Add(tip);
            }
            else
            {
                tip.HeaderText = parts[0];
                tip.BodyText = parts[1];
            }

            int drawX = control.Left + (control.Width / 2);

            if (drawX + tip.Width > Width)
                drawX -= tip.Width;

            tip.Location = new Point(drawX, control.Top + (control.Height / 2));
            tipBound = new Rectangle(control.Left, control.Top, control.Width, control.Height);
            tip.BringToFront();
            tip.Show();
            tipValid = true;
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

        private void UI_Button_WMO_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(WMOViewer));
        }

        private void UI_Button_MapViewer_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(MapViewerWindow));
        }

        private void UI_Button_SoundPlayer_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(MusicExplorerWindow));
        }

        private void UI_Button_Artwork_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(ArtExplorerWindow));
        }

        private void UI_Button_DBC_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(DBCViewer));
        }

        private void UI_Button_Settings_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(SettingsForm), true);
        }
    }
}
