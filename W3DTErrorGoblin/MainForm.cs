using System.Windows.Forms;
using System.IO;

namespace W3DTErrorGoblin
{
    public partial class MainForm : Form
    {
        public MainForm(string file)
        {
            InitializeComponent();

            if (file != null)
                UI_LogBox.Text = File.ReadAllText(file);
        }
    }
}
