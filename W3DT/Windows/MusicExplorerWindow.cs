using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using W3DT.CASC;

namespace W3DT
{
    public partial class MusicExplorerWindow : Form
    {
        public MusicExplorerWindow()
        {
            InitializeComponent();
            InitializeMusicList();
        }

        private void InitializeMusicList()
        {
            UI_FileList.Items.Clear();

            if (!Program.IsCASCReady())
                return;

            foreach (string file in FileNameCache.GetFilesWithExtension("ogg"))
            {
                UI_FileList.Items.Add(file);
            }
        }
    }
}
