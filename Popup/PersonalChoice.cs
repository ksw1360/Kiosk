using System.Windows.Forms;

namespace Kiosk.Popup
{
    public partial class PersonalChoice : Form
    {
        public PersonalChoice()
        {
            InitializeComponent();
        }

        private void PersonalChoice_Load(object sender, System.EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
