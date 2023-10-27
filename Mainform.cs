using formsApp;
using Kiosk.Class;
using Kiosk.Temp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kiosk
{
    public partial class Main : Form
    {
        public static Main form1;
        public static int check = 0;

        public Main()
        {
            InitializeComponent();
            form1 = this;
            this.CenterToScreen();
            this.Size = new Size(420, 570);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1080, 1920);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = new Point(0, 0);
            this.FormBorderStyle = FormBorderStyle.None; 

            this.lbSecond.Text = "안녕하세요";
            this.lbSecond.AutoSize = false;
            this.lbSecond.TextAlign = ContentAlignment.MiddleCenter;
            this.lbSecond.Dock = DockStyle.Fill;
            this.lbSecond.Font = new Font("Arial", 30, FontStyle.Regular);

            this.lbFirst.Text = "내원 여부 선택";
            this.lbFirst.Font = new Font("Arial", 40, FontStyle.Bold);
            this.lbFirst.AutoSize = false;
            this.lbFirst.TextAlign = ContentAlignment.MiddleCenter;
            this.lbFirst.Dock = DockStyle.Fill;
        }

        private void btnNewPtnt_Click(object sender, EventArgs e)
        {
            Common.check = 1;
            Common.PageMove("Sub_NewPtnt", this.Name, "1");
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            VKeyboard.hideKeyboard();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Common.PageMove("Sub_NewPtnt", this.Name, "1");
        }

        private void btnPrePtnt_Click(object sender, EventArgs e)
        {
            Common.check = 2;
            Common.PageMove("Sub_NewPtnt", this.Name, "1");
        }

        private void btnPreview_Click_1(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputPersonalNO inputPersonalNO = new InputPersonalNO();
            this.Hide();
            inputPersonalNO.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Common.SetLog("Program End", 1);
            Application.DoEvents();
            this.Close();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //InputAddress input = new InputAddress();
            //input.ShowDialog();
            Design_Test _Test = new Design_Test();
            this.Hide();
            _Test.ShowDialog();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
        }

        private void btnPreview_Click_2(object sender, EventArgs e)
        {
            Common.PageMove("LogIn", this.Name, "1");
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Visible = true;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}