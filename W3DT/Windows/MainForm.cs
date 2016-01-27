using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace W3DT
{
    public partial class MainForm : Form
    {
        private Dictionary<string, Form> SubWindows;
        private SettingsForm W_Settings;

        public MainForm()
        {
            InitializeComponent();
            Focus();

            SubWindows = new Dictionary<string, Form>();
        }

        private void ShowWindow(Type windowType)
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

            newForm.Show();
            newForm.Focus();
        }

        private void UI_SecBtn_Settings_Click(object sender, EventArgs e)
        {
            if (W_Settings == null || W_Settings.IsDisposed)
                W_Settings = new SettingsForm();

            W_Settings.ShowDialog();
        }

        private void UI_SecBtn_Sound_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(MusicExplorerWindow));
        }

        private void UI_SecBtn_Artwork_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(ArtExplorerWindow));
        }

        private void UI_SponserBlock_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox)
                Process.Start((string)((PictureBox)sender).Tag);
        }

        private void UI_SecBtn_DBC_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(DBCViewer));
        }

        private void UI_SecBtn_WMO_Click(object sender, EventArgs e)
        {
            ShowWindow(typeof(WMOViewer));
        }
    }
}
