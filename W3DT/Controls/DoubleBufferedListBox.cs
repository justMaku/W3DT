using System.Windows.Forms;

namespace W3DT.Controls
{
    public partial class DoubleBufferedListBox : ListBox
    {
        public DoubleBufferedListBox()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
    }
}
