using Kiosk.Class;
using Kiosk.Popup;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kiosk
{
    public partial class InputMobileNo_Add : Form
    {
        private int hypen;

        public InputMobileNo_Add()
        {
            InitializeComponent();
            textBox1.AutoSize = false;
            textBox1.Height += 10;
        }

        private void InputMobileNo_Add_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1080, 1920);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            //if (Main.form1.tbCheck.Text == "1")
            this.lbFirst.Text = "생년월일 또는 핸드폰번호를 입력해 주세요."; // + Environment.NewLine + "입력";
            this.lbFirst.AutoEllipsis = true;
            this.lbFirst.AutoSize = true;

            this.lbFirst.Font = new Font("Noto Sans KR", 24, FontStyle.Bold);
            this.lbSecond.Text = "입력";
            this.lbSecond.Font = new Font("Noto Sans KR", 24, FontStyle.Bold);
            this.textBox1.Text = lbFirst.Text;

            this.btnPreview.Visible = true;
            this.btnNext.Location = new Point(337, 0);
            this.btnNext.Visible = true;
            //numberKeyboard1.Visible = false;

            this.textBox1.Size = new System.Drawing.Size(494, 80);
            this.textBox1.TextAlign = HorizontalAlignment.Center;

            this.lbFirst.AutoSize = false;
            this.lbFirst.TextAlign = ContentAlignment.MiddleCenter;
            this.lbFirst.Dock = DockStyle.Fill;

            this.lbSecond.AutoSize = false;
            this.lbSecond.TextAlign = ContentAlignment.MiddleCenter;
            this.lbSecond.Dock = DockStyle.Fill;

            textBox1.MaxLength = 13;
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
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button2":                  //2
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button3":                  //3
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button6":                  //4
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button5":                  //5
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button4":                  //6
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button9":                  //7
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button8":                  //8
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button7":                  //9
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    break;
                case "button11":                  //10
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
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
                        if (textBox1.Text.Length >= 14)
                        {
                            return;
                        }
                        textBox1.Text = Temp;
                        hypen = 0;
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

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Common.PageMove("Sub_NewPtnt", this.Name, "1");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.AppendLine($" SELECT PAT_NO , PAT_NM , PAT_BTH , MOBILE_NO ");
            sb.AppendLine($" FROM PTNT_INFO ");
            sb.AppendLine($" WHERE PAT_NM LIKE '%{Common.Name}%'");
            if (textBox1.Text.Length == 6)
            {
                sb.AppendLine($" AND SUBSTR(PAT_JNO2,1,6) = '{textBox1.Text}'");
            }
            else if (textBox1.Text.Length == 4 || textBox1.Text.Length >= 13)
            {
                sb.AppendLine($" AND MOBILE_NO  like '%{textBox1.Text}%'");
            }

            DataTable Ptnt_Dt = DBCommon.SelectData(sb.ToString());
            if (Ptnt_Dt.Rows.Count > 0)
            {
                Receipt.ReceiptContract(Common.Name, Common.PersonalNO, "", Common.MobileNO, "", "", "", "", "");
            }
            else
            {
                PopupMessageQuestion popupMessage = new PopupMessageQuestion();
                popupMessage.panel4.BackColor = Color.White;
                popupMessage.Names = "";
                popupMessage.messages = "고객정보가 존재하지 않습니다.";
                popupMessage.result = "신규접수 하시겠습니까?";
                DialogResult dr = popupMessage.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Common.check = 1;
                    Common.PageMove("Sub_NewPtnt", this.Name, "1"); //고객 정보가 없을시 이름넣는곳으로 전환
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("010"))
            {
                if (textBox1.Text.Length > 13)
                {
                    return;
                }
            }
            else
            {
                if (textBox1.Text.Length > 14)
                {
                    return;
                }
            }
            /*
            else if (textBox1.Text.Length == 0)
            {
                textBox1.Text = "휴대폰 번호를 입력해 주세요.";
            }
            */
            bool chk = Regex.IsMatch(this.textBox1.Text, @"[ㄱ-ㅎ가-힣]");

            if (chk)
            {
                textBox1.Text = string.Empty;
            }

            if (hypen == 0)
            {
                if (textBox1.Text.Length > 2)
                {
                    if (textBox1.Text.Contains("010"))
                    {
                        if (textBox1.Text.Length == 3 || textBox1.Text.Length == 8)
                        {
                            textBox1.Text += "-";
                            textBox1.SelectionStart = textBox1.Text.Length;
                        }
                    }
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
        }
    }
}