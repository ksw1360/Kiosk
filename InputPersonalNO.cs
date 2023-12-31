﻿using Kiosk.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kiosk
{
    public partial class InputPersonalNO : Form
    {
        List<string> list = new List<string>();
        int chk = 0;
        private int hypen;
        string juminful = string.Empty;

        public InputPersonalNO()
        {
            InitializeComponent();
            textBox1.AutoSize = false;
            textBox1.Height += 10;
        }

        private void InputPersonalNO_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1080, 1920);
            this.lbFirst.Text = "건강보험 조회 시에만 사용";
            if (Common.check == 1)
            {
                this.lbSecond.Text = "주민등록번호를 입력해 주세요.";
                this.button10.Text = "초기화";
            }
            else
            {
                this.lbSecond.Text = "생년월일(6자리) 또는 핸드폰번호 입력";
                this.button10.Text = "OK";
            }

            this.btnPreview.Visible = true;
            this.btnNext.Location = new Point(337, 0);

            this.lbFirst.AutoSize = false;
            this.lbFirst.TextAlign = ContentAlignment.MiddleCenter;
            this.lbFirst.Dock = DockStyle.Fill;
            this.lbFirst.Font = new Font("NotoSansKR-Regular", 24, FontStyle.Regular);
            this.lbFirst.ForeColor = Color.FromArgb(102, 102, 102);

            this.lbSecond.AutoSize = false;
            this.lbSecond.TextAlign = ContentAlignment.MiddleCenter;
            this.lbSecond.Dock = DockStyle.Fill;
            this.lbSecond.Font = new Font("NotoSansKR-Bold", 60, FontStyle.Bold);
            this.lbSecond.ForeColor = Color.FromArgb(34, 34, 34);

            //numberKeyboard1.Visible = false;
            this.textBox1.Text = "주민등록번호를 입력해 주세요.";

            
            textBox1.Size = new System.Drawing.Size(494, 80);
            /*
            textBox1.Font = new Font("Roboto-Medium", 37, FontStyle.Regular);
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Left = button13.Left;
            textBox1.MaxLength = 1;
            */

            //textBox1.Multiline = true;
            //textBox1.TextAlign = HorizontalAlignment.Left;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Padding = new Padding(0, 20, 0, 0); // 상단 여백 조절

            textBox1.MaxLength = 14;
            textBox2.MaxLength = 14;


            try
            {
                if (Common.PersonalNO != null)
                {
                    this.textBox1.Text = Common.PersonalNO;
                }
            }
            catch (Exception ex)
            {
                Common.SetLog(ex.Message, 3);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (juminful.Length < 13)
            {
                Common.PersonalNO = this.textBox2.Text;
            }
            else
            {
                Common.PersonalNO = juminful;
            }
            Common.PageMove("inputAddress", this.Name, "1");
            /*
            InputAddress inputAddress = new InputAddress();
            inputAddress.StartPosition = FormStartPosition.CenterScreen;
            this.Hide();
            inputAddress.ShowDialog();
            */
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
        }

        private void NumberKeyboard1_DataRequest(object sender, EventArgs e)
        {
            this.textBox1.Text = sender.ToString();
        }

        private void numberKeyboard1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (Common.check == 1)
            {
                Common.PageMove("InputMobileNo", this.Name, "1");
            }
            else
            {
                Common.PageMove("InputMobileNo_Add", this.Name, "1");
            }
        }

        private void button10_Click(object sender, EventArgs e)
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
                    textBox2.Text += message;
                    break;
                case "button2":                  //2
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    textBox2.Text += message;
                    break;
                case "button3":                  //3
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    textBox2.Text += message;
                    break;
                case "button6":                  //4
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    textBox2.Text += message;
                    break;
                case "button5":                  //5
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    textBox2.Text += message;
                    break;
                case "button4":                  //6
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    textBox2.Text += message;
                    break;
                case "button9":                  //7
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    textBox2.Text += message;
                    break;
                case "button8":                  //8
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    textBox2.Text += message;
                    break;
                case "button7":                  //9
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    textBox2.Text += message;
                    break;
                case "button11":                  //10
                    //textBox1.Text += button.Text;
                    message += button.Text;
                    if (textBox1.Text.Length >= 14)
                    {
                        return;
                    }
                    textBox1.Text += message;
                    textBox2.Text += message;
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
                        textBox2.Text = Temp;
                        // = message;
                    }
                    break;
                case "button10":
                    hypen = 0;
                    message = string.Empty;
                    textBox1.Text = message;
                    textBox2.Text = message;
                    break;
            }
            if (button.Name == "button10")
            {
                textBox1.Text = message;
                textBox2.Text = message;
            }
            else if (button.Name == "button12")
            {

            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*
            if (textBox1.Text.Length >= 14)
            {
                return;
            }
            */
            /*
            else if (textBox1.Text.Length == 0)
            {
                textBox1.Text = "주민등록번호를 입력해 주세요.";
            }
            */
            if (textBox1.Text.Length == 6)
            {
                textBox1.Text = textBox1.Text + "-";
            }
            if (textBox1.Text.Length > 7)
            {
                if (textBox1.Text.Length == 9)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 8) + "*";
                }
                else if (textBox1.Text.Length == 10)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 9) + "*";
                }
                else if (textBox1.Text.Length == 11)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 10) + "*";
                }
                else if (textBox1.Text.Length == 12)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 11) + "*";
                }
                else if (textBox1.Text.Length == 13)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 12) + "*";
                }
                else if (textBox1.Text.Length == 14)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 13) + "*";
                }
                else if (textBox1.Text.Length == 15)
                {
                    textBox1.Text = textBox1.Text.Substring(0, 14) + "*";
                }
            }

            if (textBox1.Text.Length >= 14)
            {
                bool chk = Regex.IsMatch(this.textBox1.Text, @"[ㄱ-ㅎ가-힣]");
                if (chk == false)
                {
                    juminful = textBox2.Text;
                    this.btnNext.Enabled = true;
                    btnNext.BackgroundImage = Properties.Resources.상단_다음버튼_색반전;
                    return;
                }
            }
            else
            {
                this.btnNext.Enabled = false;
                btnNext.BackgroundImage = Properties.Resources.그룹_117;
                btnNext.BackColor = Color.FromArgb(249, 249, 249);
                btnNext.ForeColor = Color.FromArgb(99, 114, 171);
            }
        }

        private void textBox1_Click_1(object sender, EventArgs e)
        {
            bool chk = Regex.IsMatch(this.textBox1.Text, @"[ㄱ-ㅎ가-힣]");

            if (chk)
            {
                textBox1.Text = string.Empty;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 6)
            {
                textBox1.Text = textBox1.Text + "-";
            }
        }
    }
}