using Kiosk.Class;
using Kiosk.Popup;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kiosk
{
    public partial class ReceiptInfo : Form
    {
        TextBox[] textBox = new TextBox[] { };
        Label[] label = new Label[] { };

        public ReceiptInfo()
        {
            InitializeComponent();
            txtname.AutoSize = false;
            txtname.Height += 10;

            txtpersonalno.AutoSize = false;
            txtpersonalno.Height += 10;

            txtsurgery.AutoSize = false;
            txtsurgery.Height += 10;

            txtmobile.AutoSize = false;
            txtmobile.Height += 10;

            txtaddress.AutoSize = false;
            txtaddress.Height += 10;

            txtkind.AutoSize = false;
            txtkind.Height += 10;

        }

        private void ReceiptInfo_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Size = new Size(1080, 1920);
            this.lbFirst.Visible = false;

            this.lbThird.Font = new Font("NotoSansKR-Medium", 23, FontStyle.Regular);
            this.lbThird.Text = "입력한 정보 확인 후" + Environment.NewLine
                               + "개인정보 활용 및 수집 동의 체크";
            this.lbThird.AutoSize = false;
            this.lbThird.TextAlign = ContentAlignment.MiddleCenter;
            this.lbThird.Dock = DockStyle.Fill;
            this.lbThird.ForeColor = Color.FromArgb(153, 153, 153);
            this.lbThird.BackColor = Color.FromArgb(249, 249, 249);

            this.lbSecond.Font = new Font("NotoSansKR-Bold", 42, FontStyle.Bold);
            this.lbSecond.Text = "접수 정보";

            this.lbFirst.AutoSize = false;
            this.lbFirst.TextAlign = ContentAlignment.MiddleCenter;
            this.lbFirst.Dock = DockStyle.Fill;

            this.lbSecond.AutoSize = false;
            this.lbSecond.TextAlign = ContentAlignment.MiddleCenter;
            this.lbSecond.Dock = DockStyle.Fill;

            this.btnPreview.Visible = true;
            this.btnNext.Visible = false;

            //라벨 폰트 설정
            this.label1.Font = new Font("NotoSansKR-Medium", 18, FontStyle.Regular);
            this.label1.ForeColor = Color.FromArgb(34, 34, 34);
            this.label1.Text = "이름";

            this.label2.Font = new Font("NotoSansKR-Medium", 18, FontStyle.Regular);
            this.label2.Text = "주민등록번호";

            this.label3.Font = new Font("NotoSansKR-Medium", 18, FontStyle.Regular);
            this.label3.Text = "진료분야";

            this.label4.Font = new Font("NotoSansKR-Medium", 18, FontStyle.Regular);
            this.label4.Text = "휴대폰번호";

            this.label5.Font = new Font("NotoSansKR-Medium", 18, FontStyle.Regular);
            this.label5.Text = "주소";

            this.label6.Font = new Font("NotoSansKR-Medium", 18, FontStyle.Regular);
            this.label6.Text = "내원경로";

            this.label7.Font = new Font("NotoSansKR-Medium", 18, FontStyle.Regular);
            this.label7.Text = "문자수신";

            this.label8.Font = new Font("NotoSansKR-Medium", 18, FontStyle.Regular);
            this.label8.Text = "이용약관동의";

            this.btnReceipt.Font = new Font("NotoSansCJKKR-Medium", 25, FontStyle.Regular);

            this.radioButton1.Font = new Font("NotoSansCJKKR-Medium", 19, FontStyle.Regular);
            this.radioButton2.Font = new Font("NotoSansCJKKR-Medium", 19, FontStyle.Regular);

            this.checkBox1.Font = new Font("NotoSansKR-Regular", 16, FontStyle.Regular);
            this.checkBox1.ForeColor = Color.FromArgb(136, 136, 136);

            for (int idx = 0; idx < textBox.Length; idx++)
            {
                if (textBox[idx] != null)
                {
                    textBox[idx].Size = new System.Drawing.Size(477, 47);
                    textBox[idx].ForeColor = Color.White;
                    //Noto Sans KR Light, 17.9999981pt
                    textBox[idx].Font = new Font("Noto Sans KR", 17, FontStyle.Regular);
                    textBox[idx].TextAlign = HorizontalAlignment.Center;
                    textBox[idx].MaxLength = 1;
                }
            }

            for (int i = 0; i < label.Length; i++)
            {
                if (label[i] != null)
                {
                    label[i].BackColor = Color.FromArgb(249, 249, 249);
                    label[i].ForeColor = Color.FromArgb(34, 34, 34);
                }
            }

            this.radioButton1.Checked = true;

            Init();
            SetData();
        }

        private void Init()
        {
            txtname.Text = string.Empty;
            txtpersonalno.Text = string.Empty;
            txtsurgery.Text = string.Empty;
            txtmobile.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtkind.Text = string.Empty;
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
        }

        private void SetData()
        {
            if (Common.Name != null && Common.Name != "")
            {
                txtname.Text = Common.Name;
                txtpersonalno.Text = Common.PersonalNO;
                txtsurgery.Text = Common.SurgeryKind;
                txtmobile.Text = Common.MobileNO;
                txtaddress.Text = Common.Address;
                txtkind.Text = Common.input;
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Common.PageMove("SurgeryKind", this.Name, "1");
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.ForeColor = Color.FromArgb(100, 114, 171); //Checked
            }
            else
            {
                this.ForeColor = Color.FromArgb(187, 187, 187); //Checked
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.ForeColor = Color.FromArgb(100, 114, 171); //Checked
            }
            else
            {
                this.ForeColor = Color.FromArgb(187, 187, 187); //Checked
            }
        }

        private void btnPreview_Click_1(object sender, EventArgs e)
        {
            Common.PageMove("InputAddress", this.Name, "1");
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                //문자수신 여부
                if (radioButton1.Checked)
                {
                    Common.SMS_YN = "1";
                }
                else if (radioButton2.Checked == false)
                {
                    Common.SMS_YN = "0";
                }

                //이용약관 동의
                if (checkBox1.Checked) //전체동의
                {
                    Common.Rules = "1";
                    Common.Ptntinfo = "1";
                    Common.Eventarl = "1";
                }
                else if (checkBox1.Checked==false)
                {
                    Common.Rules = "1";

                    //개인정보 활용 동의
                    if (checkBox2.Checked)
                    {
                        Common.Ptntinfo = "1";
                    }
                    else if (checkBox2.Checked == false)
                    {
                        Common.Ptntinfo = "0";
                    }

                    //이벤트 및 광고 SMS 수신 동의
                    if (checkBox3.Checked)
                    {
                        Common.Eventarl = "1";
                    }
                    else if (checkBox3.Checked == false)
                    {
                        Common.Eventarl = "0";
                    }
                }

                //신한 접수하기
                Receipt.ReceiptContract(txtname.Text       //이름
                                      , txtpersonalno.Text //주민등록번호
                                      , txtsurgery.Text    //진료분야
                                      , txtmobile.Text     //휴대폰번호
                                      , txtaddress.Text    //주소
                                      , txtkind.Text       //내원경로
                                      , Common.SMS_YN      //문자수신여부
                                      , Common.Ptntinfo    //개인정보 활용 동의서 동의 여부
                                      , Common.Eventarl    //이벤트 수신 동의 여부
                                      ); //신환
                //접수후 초기화

            }
            catch (Exception ex)
            {
                Common.SetLog(ex.Message, 3);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PersonInfoRule rule = new PersonInfoRule();
            rule.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                checkBox2.Checked = true;
                checkBox3.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;

            }
        }
    }
}