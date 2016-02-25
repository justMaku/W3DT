using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace W3DT
{
    public partial class ModelViewer : Form
    {
        private Explorer explorer;

        public ModelViewer()
        {
            InitializeComponent();

            explorer = new Explorer(this, UI_FilterField, UI_FilterOverlay, UI_FilterTime, UI_FileCount_Label, UI_FileList, new string[] { "m2" }, "M2_V_{0}", true);
            explorer.rootFolders = new string[] { "character", "creature" };
            explorer.Initialize();
        }
    }
}
