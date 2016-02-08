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

namespace W3DT
{
    public partial class MainForm : Form
    {
        private Dictionary<string, Form> SubWindows;
        private Point PanelPoint = new System.Drawing.Point(12, 96);

        public MainForm()
        {
            InitializeComponent();
            Focus();

            SubWindows = new Dictionary<string, Form>();
        }

        private void OnMainButtonMouseLeave(object sender, EventArgs e)
        {
            UI_SectionLabel.Text = (string)UI_SectionLabel.Tag;
        }

        private void OnMainButtonMouseEnter(object sender, EventArgs e)
        {
            UI_SectionLabel.Text = (string) ((MainFormButton)sender).Tag;
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
