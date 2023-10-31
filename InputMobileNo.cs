using Kiosk.Class;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kiosk
{
    public partial class InputMobileNo : Form
    {
        int hypen = 0;
        public InputMobileNo()
        {
            InitializeComponent();
            textBox1.AutoSize = false;
            textBox1.Height += 10;
        }

        private void InputMobileNo_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            this.Size = new Size(1080, 1920);

            this.lbFirst.Text = "본원에서 메세지를 발송해 드립니다.";
            this.lbSecond.Text = "휴대폰 번호 입력";
            this.textBox1.Text = Common.Mobile_Message;

            this.lbFirst.Text = "본원에서 메세지를 발송해 드립니다.";
            this.lbFirst.AutoEllipsis = true;
            this.lbFirst.AutoSize = true;
            this.lbFirst.Font = new Font("맑은 고딕", 24, FontStyle.Bold);

            this.lbSecond.Text = "휴대폰 번호 입력";
            this.lbSecond.AutoEllipsis = true;
            this.lbSecond.AutoSize = true;
            this.lbSecond.Font = new Font("맑은 고딕", 24, FontStyle.Bold);

            this.btnPreview.Visible = true;
            this.btnNext.Location = new Point(337, 0);
            this.btnNext.Visible = true;

            this.textBox1.Size = new System.Drawing.Size(494, 80);
            this.textBox1.Font = new Font("NotoSansKR-Regular", 30, FontStyle.Regular);
            this.textBox1.TextAlign = HorizontalAlignment.Center;

            this.lbFirst.AutoSize = false;
            this.lbFirst.TextAlign = ContentAlignment.MiddleCenter;
            this.lbFirst.Dock = DockStyle.Fill;

            this.lbSecond.AutoSize = false;
            this.lbSecond.TextAlign = ContentAlignment.MiddleCenter;
            this.lbSecond.Dock = DockStyle.Fill;

            textBox1.MaxLength = 13;

            try
            {
                if (Common.MobileNO != null)
                {
                    this.textBox1.Text = Common.MobileNO;
                }
            }
            catch (Exception ex)
            {
                Common.SetLog(ex.Message, 3);
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
        }

        private void NumberKeyboard1_DataRequest(object sender, EventArgs e)
        {
            textBox1.Text = sender.ToString();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Common.PageMove("Sub_NewPtnt", this.Name, "1");
        }

        /// <summary>
        /// 다음버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            Common.MobileNO = this.textBox1.Text;
            //this.Hide();
            if (textBox1.Text.Length < 13)
            {
                MessageBox.Show("핸드폰 번호를 확인바랍니다.");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.AppendLine($" SELECT COUNT(*) AS CNT ");
            sb.AppendLine($" FROM PTNT_INFO ");
            sb.AppendLine($" WHERE MOBILE_NO  like '%{textBox1.Text}%'");
            sb.AppendLine($"   AND YKIHO = '{Common.YKIHO}'");
            DataTable Ptnt_Dt = DBCommon.SelectData(sb.ToString());

            int rows = Convert.ToInt32(Ptnt_Dt.Rows[0]["CNT"].ToString());

            if (rows > 0)
            {
                //접수하기
                //구환
                //전화번호로 검색된 구환
                Receipt.ReceiptContract(Common.Name, Common.PersonalNO, Common.SurgeryKind, Common.MobileNO, Common.Address, "", "0", "0", "0");
            }
            else
            {
                Common.PageMove("InputPersonalNO", this.Name, "1");
            }
        }

        private void numberKeyboard1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
            //numberKeyboard1.Visible = true;
            //numberKeyboard1.DataRequest2 += NumberKeyboard1_DataRequest;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 13)
            {
                return;
            }
            if (textBox1.Text.Length == 3)
            {
                textBox1.Text = textBox1.Text + "-";
            }

            if (textBox1.Text.Length >= 8)
            {
                textBox1.Text = textBox1.Text + "-";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string message = string.Empty;

            bool chk = Regex.IsMatch(this.textBox1.Text, @"[ㄱ-ㅎ가-힣]");

            if (chk)
            {
                textBox1.Text = string.Empty;
            }

            Button button = (Button)sender;
            switch (button.Name)
            {
                case "button13":                  //1
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 13)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button2":                  //2
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 13)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button3":                  //3
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 13)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button6":                  //4
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 13)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button5":                  //5
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    textBox1.Text += message;
                    break;
                case "button4":                  //6
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 13)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button9":                  //7
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    textBox1.Text += message;
                    break;
                case "button8":                  //8
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 13)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button7":                  //9
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 13)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button11":                  //10
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 13)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button12":
                    if (textBox1.Text.Length > 0)
                    {
                        hypen = 1;
                        string Temp = string.Empty;
                        bool chk2 = false;
                        int startIndex = textBox1.Text.Length - 1;
                        if (textBox1.Text.Substring(textBox1.Text.Length - 1, 1) == "-")
                        {
                            chk2 = false;
                        }
                        else
                        {
                            chk2 = true;
                        }
                        //bool chk2 = Regex.IsMatch(this.textBox1.Text, @"[ㄱ-ㅎ가-힣]");
                        if (chk2)
                        {
                            Temp = textBox1.Text.Remove(startIndex);
                        }
                        else
                        {
                            Temp = textBox1.Text.TrimEnd('-');
                            //RemoveSpecialCharacter(textBox1.Text, startIndex);
                        }
                        if (textBox1.Text.Length >= 13)
                        {
                            return;
                        }
                        textBox1.Text = Temp;
                        // = message;
                    }
                    break;
                case "button10":
                    hypen = 0;
                    message = button.Text + "-";
                    textBox1.Text = message;
                    break;
            }
            if (button.Name == "button10")
            {
                textBox1.Text = message;
            }
            else if (button.Name == "button12")
            {

            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            SetDelText(textBox1.Text);
        }

        private void SetDelText(string text)
        {
            if (textBox1.Text.Length > 13)
            {
                return;
            }
            /*
            else if (textBox1.Text.Length == 0)
            {
                textBox1.Text = "휴대폰 번호를 입력해 주세요.";
            }
            */
            if (hypen == 0)
            {
                if (textBox1.Text.Length == 3 || textBox1.Text.Length == 8)
                {
                    textBox1.Text += "-";
                    textBox1.SelectionStart = textBox1.Text.Length;
                }
            }
            else
            {
                if (textBox1.Text.EndsWith("-"))
                {
                    //Temp = textBox1.Text.TrimEnd('-');
                    textBox1.Text = textBox1.Text.TrimEnd('-');
                    //RemoveSpecialCharacter(textBox1.Text, textBox1.Text.Length - 1);
                }
            }

            if (textBox1.Text.Length > 0)
            {
                this.btnNext.Enabled = true;
                btnNext.BackgroundImage = Properties.Resources.상단_다음버튼_색반전;
            }
            else
            {
                this.btnNext.Enabled = false;
                btnNext.BackgroundImage = Properties.Resources.그룹_117;
                btnNext.BackColor = Color.FromArgb(249, 249, 249);
                btnNext.ForeColor = Color.FromArgb(99, 114, 171);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool chk = Regex.IsMatch(this.textBox1.Text, @"[ㄱㅎ가힣]");

            if (chk)
            {
                textBox1.Text = string.Empty;
            }
            else if (textBox1.Text.Length == 13)
            {
                btnNext.PerformClick();
            }
        }
    }
}