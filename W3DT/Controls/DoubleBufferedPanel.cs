using System.Windows.Forms;

namespace W3DT.Controls
{
    public partial class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
    }
}
