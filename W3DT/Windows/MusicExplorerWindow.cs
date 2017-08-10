using System;
using System.Collections.Generic;
using System.Windows.Forms;
using W3DT.CASC;

namespace W3DT
{
    public partial class MusicExplorerWindow : Form
    {
        private List<SoundPlayer> players = new List<SoundPlayer>();
        private Explorer explorer;

        public MusicExplorerWindow()
        {
            InitializeComponent();
            explorer = new Explorer(this, UI_FilterField, UI_FilterOverlay, UI_FilterCheckTimer, UI_FileCount_Label, UI_FileList, new string[] { "ogg", "mp3" }, "MEW_N_{0}", true);
            explorer.Initialize();

            UI_MultiWindows_Field.Checked = Program.Settings.AllowMultipleSoundPlayers;
        }

        private void MusicExplorerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            explorer.Dispose();
        }

        private void UI_FileList_DoubleClick(object sender, EventArgs e)
        {
            if (UI_FileList.SelectedNode != null && UI_FileList.SelectedNode.Tag != null)
            {
                if (!Program.Settings.AllowMultipleSoundPlayers)
                {
                    foreach (SoundPlayer player in players)
                        player.Close();

                    players.Clear();
                }

                SoundPlayer newPlayer = new SoundPlayer((CASCFile)UI_FileList.SelectedNode.Tag);
                newPlayer.Show();
                players.Add(newPlayer);
            }
        }

        private void UI_MultiWindows_Field_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AllowMultipleSoundPlayers = UI_MultiWindows_Field.Checked;
            Program.Settings.Persist();
        }
    }
}
