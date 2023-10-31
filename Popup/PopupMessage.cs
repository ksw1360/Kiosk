using Kiosk.Class;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Kiosk.Popup
{
    public partial class PopupMessage : Form
    {
        public PopupMessage()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        public string Names { get; internal set; }
        public string message { get; internal set; }
        public string result { get; internal set; }

        private void PopupMessage_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            this.lbFirst.Text = Names;
            this.lbFirst.Font = new Font("Noto Sans KR Bold", 37, FontStyle.Bold);

            this.lbFirst.AutoSize = false;
            this.lbFirst.TextAlign = ContentAlignment.MiddleCenter;
            this.lbFirst.Dock = DockStyle.Fill;

            this.lbSecond.Text = message;
            this.lbSecond.AutoSize = false;
            this.lbSecond.Font = new Font("Noto Sans KR Bold", 33, FontStyle.Bold);
            this.lbSecond.TextAlign = ContentAlignment.MiddleCenter;
            this.lbSecond.Dock = DockStyle.Fill;

            this.lbThird.Text = result;
            this.lbThird.AutoSize = false;
            this.lbThird.Font = new Font("Noto Sans KR Regular", 24, FontStyle.Regular);
            this.lbThird.TextAlign = ContentAlignment.MiddleCenter;
            this.lbThird.Dock = DockStyle.Fill;

            this.TopMost = true;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //Common Init
                Common.Init();

                Form specificForm = Application.OpenForms.OfType<Main>().FirstOrDefault();

                if (specificForm != null)
                {
                    //specificForm.Show();
                    specificForm.Visible = true;
                }

                // 다른 모든 폼을 닫습니다.
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i] != specificForm)
                    {
                        Application.OpenForms[i].Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.SetLog(ex.Message, 3);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}