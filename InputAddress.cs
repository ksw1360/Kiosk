using Kiosk.Class;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Kiosk
{
    public partial class InputAddress : Form
    {
        /// <summary>
        /// 키보드 이벤트 발생시키기
        /// </summary>
        /// <param name="virtualKey">가상 키</param>
        /// <param name="scanCode">스캔 코드</param>
        /// <param name="flag">플래그</param>
        /// <param name="extraInformation">추가 정보</param>
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte virtualKey, byte scanCode, uint flag, int extraInformation);

        #region 후킹 초기화 하기 - InitHook(controlHandle)

        /// <summary>
        /// 후킹 초기화 하기
        /// </summary>
        /// <param name="controlHandle">컨트롤 핸들</param>
        [DllImport("vkb.dll", CharSet = CharSet.Auto)]
        private static extern void InitHook(IntPtr controlHandle);

        #endregion
        #region 후킹 설치하기 - InstallHook()

        /// <summary>
        /// 후킹 설치하기
        /// </summary>
        [DllImport("vkb.dll", CharSet = CharSet.Auto)]
        private static extern void InstallHook();

        #endregion

        #region Field

        /// <summary>
        /// 사운드 재생 여부
        /// </summary>
        //private bool playSound = true;

        /// <summary>
        /// Caps Lock 버튼 눌림 여부
        /// </summary>
        private bool isPressedCAPSLOCK = false;

        /// <summary>
        /// Shift 버튼 눌림 여부
        /// </summary>
        private bool isPressedSHIFT = false;

        /// <summary>
        /// 한글 모드 여부
        /// </summary>
        private bool isHANGULMode = false;
        //private int idx;

        #endregion
        
        private string query;
        private string jsonStr;

        public InputAddress()
        {
            InitializeComponent();
            textBox1.AutoSize = false;
            textBox1.Height += 10;

            textBox2.AutoSize = false;
            textBox2.Height += 10;

            textBox3.AutoSize = false;
            textBox3.Height += 10;
        }

        private void InputAddress_Load(object sender, EventArgs e)
        {
            Initialize();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1080, 1920);
            this.lbFirst.Text = "주소입력";
            this.lbSecond.Text = "지번(동/읍/면/리) 또는 도로명을 검색하여 상세 주소를 입력해 주세요.";
            this.textBox1.Text = "지번 / 도로명";
            this.textBox2.Text = "주소";
            this.textBox3.Text = "상세주소";
            this.btnPreview.Visible = true;
            this.btnNext.Location = new Point(337, 0);
            this.btnNext.Visible = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.lbFirst.AutoSize = false;
            this.lbFirst.TextAlign = ContentAlignment.MiddleCenter;
            this.lbFirst.Dock = DockStyle.Fill;

            this.lbSecond.AutoSize = false;
            this.lbSecond.TextAlign = ContentAlignment.MiddleCenter;
            this.lbSecond.Dock = DockStyle.Fill;

            textBox1.Size = new System.Drawing.Size(799, 94);
            textBox1.Font = new Font("Noto Sans KR Regular", 26, FontStyle.Regular);
            textBox1.TextAlign = HorizontalAlignment.Center;

            textBox2.Size = new System.Drawing.Size(799, 94);
            textBox2.Font = new Font("Noto Sans KR Regular", 26, FontStyle.Regular);
            textBox2.TextAlign = HorizontalAlignment.Center;

            textBox3.Size = new System.Drawing.Size(799, 94);
            textBox3.Font = new Font("Noto Sans KR Regular", 26, FontStyle.Regular);
            textBox3.TextAlign = HorizontalAlignment.Center;

            KeyboardImageSetup();

            try
            {
                if (Common.Address != null)
                {
                    var list = Common.Address.Split('|');

                    textBox1.Text = list[0]; //우편번호
                    textBox2.Text = list[1]; //주소(도로명)
                    textBox3.Text = list[2]; //상세주소
                }
            }
            catch (Exception ex)
            {
                Common.SetLog(ex.Message, 3);
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string RestAPI = "a03fea2af8d12a4d76d131aa3cb4c217";
            query = textBox1.Text;
            // 카카오 주소 검색 API 요청 URL 생성
            string apiUrl = $"https://dapi.kakao.com/v2/local/search/address.json?query={WebUtility.UrlEncode(query)}";

            // HTTP 요청 생성
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "GET";
            request.Headers.Add("Authorization", "KakaoAK " + RestAPI);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(responseStream))
                    {
                        // JSON 응답 파싱
                        string responseJson = reader.ReadToEnd();
                        dynamic data = JsonConvert.DeserializeObject(responseJson);

                        foreach (var address in data.documents)
                        {
                            textBox1.Text = address.road_address.zone_no.Value;
                            textBox2.Text = address.road_address.address_name.Value;
                            textBox3.Text = string.Empty;
                            textBox3.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Common.SetLog(ex.Message, 3);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Common.Address = textBox1.Text + "|" + textBox2.Text + "|" + textBox3.Text;
            Common.PageMove("SurgeryKind", this.Name, "1");
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Common.PageMove("InputPersonalNO", this.Name, "1");
        }

        private void k0101PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox keyPictureBox = sender as PictureBox;

            if (keyPictureBox == null)
            {
                return;
            }

            #region 1행

            if (keyPictureBox == this.k0101PictureBox) // "~"
            {
                keybd_event((byte)Keys.Oem3, 0, 0, 0);
                keybd_event((byte)Keys.Oem3, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0102PictureBox) // "1"
            {
                keybd_event((byte)Keys.D1, 0, 0, 0);
                keybd_event((byte)Keys.D1, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0103PictureBox) // "2"
            {
                keybd_event((byte)Keys.D2, 0, 0, 0);
                keybd_event((byte)Keys.D2, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0104PictureBox) // "3"
            {
                keybd_event((byte)Keys.D3, 0, 0, 0);
                keybd_event((byte)Keys.D3, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0105PictureBox) // "4"
            {
                keybd_event((byte)Keys.D4, 0, 0, 0);
                keybd_event((byte)Keys.D4, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0106PictureBox) // "5"
            {
                keybd_event((byte)Keys.D5, 0, 0, 0);
                keybd_event((byte)Keys.D5, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0107PictureBox) // "6"
            {
                keybd_event((byte)Keys.D6, 0, 0, 0);
                keybd_event((byte)Keys.D6, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0108PictureBox) // "7"
            {
                keybd_event((byte)Keys.D7, 0, 0, 0);
                keybd_event((byte)Keys.D7, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0109PictureBox) // "8"
            {
                keybd_event((byte)Keys.D8, 0, 0, 0);
                keybd_event((byte)Keys.D8, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0110PictureBox) // "9"
            {
                keybd_event((byte)Keys.D9, 0, 0, 0);
                keybd_event((byte)Keys.D9, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0111PictureBox) // "0"
            {
                keybd_event((byte)Keys.D0, 0, 0, 0);
                keybd_event((byte)Keys.D0, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0112PictureBox) // "-"
            {
                keybd_event((byte)Keys.OemMinus, 0, 0, 0);
                keybd_event((byte)Keys.OemMinus, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0113PictureBox) // "+"
            {
                keybd_event((byte)Keys.Oemplus, 0, 0, 0);
                keybd_event((byte)Keys.Oemplus, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0114PictureBox) // "Backspace"
            {
                keybd_event((byte)Keys.Back, 0, 0, 0);
                keybd_event((byte)Keys.Back, 0, 0x02, 0);
            }
            /*
            if (keyPictureBox == this.k0115PictureBox) // "Insert"
            {
                keybd_event((byte)Keys.Insert, 0, 0, 0);
                keybd_event((byte)Keys.Insert, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0116PictureBox) // "Home"
            {
                keybd_event((byte)Keys.Home, 0, 0, 0);
                keybd_event((byte)Keys.Home, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0117PictureBox) // "PgUp"
            {
                keybd_event((byte)Keys.PageUp, 0, 0, 0);
                keybd_event((byte)Keys.PageUp, 0, 0x02, 0);
            }
            */

            #endregion
            #region 2행

            if (keyPictureBox == this.k0201PictureBox) // "Tab"
            {
                keybd_event((byte)Keys.Tab, 0, 0, 0);
                keybd_event((byte)Keys.Tab, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0202PictureBox) // "Q"
            {
                keybd_event((byte)Keys.Q, 0, 0, 0);
                keybd_event((byte)Keys.Q, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0203PictureBox) // "W"
            {
                keybd_event((byte)Keys.W, 0, 0, 0);
                keybd_event((byte)Keys.W, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0204PictureBox) // "E"
            {
                keybd_event((byte)Keys.E, 0, 0, 0);
                keybd_event((byte)Keys.E, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0205PictureBox) // "R"
            {
                keybd_event((byte)Keys.R, 0, 0, 0);
                keybd_event((byte)Keys.R, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0206PictureBox) // "T"
            {
                keybd_event((byte)Keys.T, 0, 0, 0);
                keybd_event((byte)Keys.T, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0207PictureBox) // "Y"
            {
                keybd_event((byte)Keys.Y, 0, 0, 0);
                keybd_event((byte)Keys.Y, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0208PictureBox) // "U"
            {
                keybd_event((byte)Keys.U, 0, 0, 0);
                keybd_event((byte)Keys.U, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0209PictureBox) // "I"
            {
                keybd_event((byte)Keys.I, 0, 0, 0);
                keybd_event((byte)Keys.I, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0210PictureBox) // "O"
            {
                keybd_event((byte)Keys.O, 0, 0, 0);
                keybd_event((byte)Keys.O, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0211PictureBox) // "P"
            {
                keybd_event((byte)Keys.P, 0, 0, 0);
                keybd_event((byte)Keys.P, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0212PictureBox) // "["
            {
                keybd_event((byte)Keys.Oem4, 0, 0, 0);
                keybd_event((byte)Keys.Oem4, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0213PictureBox) // "]"
            {
                keybd_event((byte)Keys.Oem6, 0, 0, 0);
                keybd_event((byte)Keys.Oem6, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0214PictureBox) // "\"
            {
                keybd_event((byte)Keys.Oem5, 0, 0, 0);
                keybd_event((byte)Keys.Oem5, 0, 0x02, 0);
            }
            /*
            if (keyPictureBox == this.k0215PictureBox) // "Delete"
            {
                keybd_event((byte)Keys.Delete, 0, 0, 0);
                keybd_event((byte)Keys.Delete, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0216PictureBox) // "End"
            {
                keybd_event((byte)Keys.End, 0, 0, 0);
                keybd_event((byte)Keys.End, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0217PictureBox) // "PgDn"
            {
                keybd_event((byte)Keys.PageDown, 0, 0, 0);
                keybd_event((byte)Keys.PageDown, 0, 0x02, 0);
            }
            */
            #endregion
            #region 3행

            if (keyPictureBox == this.k0301PictureBox) // "Caps Lock"
            {
                Image image;

                if (this.isPressedCAPSLOCK)
                {
                    image = Properties.Resources.K0301;
                }
                else
                {
                    image = Properties.Resources.K0301ON;
                }

                this.k0301PictureBox.Image = image;

                this.isPressedCAPSLOCK = this.isPressedCAPSLOCK ^ true;

                keybd_event((byte)Keys.CapsLock, 0, 0, 0);
                keybd_event((byte)Keys.CapsLock, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0302PictureBox) // "A"
            {
                keybd_event((byte)Keys.A, 0, 0, 0);
                keybd_event((byte)Keys.A, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0303PictureBox) // "S"
            {
                keybd_event((byte)Keys.S, 0, 0, 0);
                keybd_event((byte)Keys.S, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0304PictureBox) // "D"
            {
                keybd_event((byte)Keys.D, 0, 0, 0);
                keybd_event((byte)Keys.D, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0305PictureBox) // "F"
            {
                keybd_event((byte)Keys.F, 0, 0, 0);
                keybd_event((byte)Keys.F, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0306PictureBox) // "G"
            {
                keybd_event((byte)Keys.G, 0, 0, 0);
                keybd_event((byte)Keys.G, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0307PictureBox) // "H"
            {
                keybd_event((byte)Keys.H, 0, 0, 0);
                keybd_event((byte)Keys.H, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0308PictureBox) // "J"
            {
                keybd_event((byte)Keys.J, 0, 0, 0);
                keybd_event((byte)Keys.J, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0309PictureBox) // "K"
            {
                keybd_event((byte)Keys.K, 0, 0, 0);
                keybd_event((byte)Keys.K, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0310PictureBox) // "L"
            {
                keybd_event((byte)Keys.L, 0, 0, 0);
                keybd_event((byte)Keys.L, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0311PictureBox) // ";"
            {
                keybd_event((byte)Keys.OemSemicolon, 0, 0, 0);
                keybd_event((byte)Keys.OemSemicolon, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0312PictureBox) // "'"
            {
                keybd_event((byte)Keys.OemQuotes, 0, 0, 0);
                keybd_event((byte)Keys.OemQuotes, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0313PictureBox) // "Enter"
            {
                keybd_event((byte)Keys.Enter, 0, 0, 0);
                keybd_event((byte)Keys.Enter, 0, 0x02, 0);
            }

            #endregion
            #region 4행

            if (keyPictureBox == this.k0401PictureBox) // "Left Shift"
            {
                if (this.isPressedSHIFT == false)
                {
                    this.isPressedSHIFT = true;

                    Image leftSHIFTImage = Properties.Resources.K0401ON;
                    Image rightSHIFTImage = Properties.Resources.K0412ON;

                    this.k0401PictureBox.Image = leftSHIFTImage;
                    this.k0412PictureBox.Image = rightSHIFTImage;

                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                }
                else
                {
                    this.isPressedSHIFT = false;

                    Image leftSHIFTImage = Properties.Resources.K0401;
                    Image rightSHIFTImage = Properties.Resources.K0412;

                    this.k0401PictureBox.Image = leftSHIFTImage;
                    this.k0412PictureBox.Image = rightSHIFTImage;

                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                }
            }

            if (keyPictureBox == this.k0402PictureBox) // "Z"
            {
                keybd_event((byte)Keys.Z, 0, 0, 0);
                keybd_event((byte)Keys.Z, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0403PictureBox) // "X"
            {
                keybd_event((byte)Keys.X, 0, 0, 0);
                keybd_event((byte)Keys.X, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0404PictureBox) // "C"
            {
                keybd_event((byte)Keys.C, 0, 0, 0);
                keybd_event((byte)Keys.C, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0405PictureBox) // "V"
            {
                keybd_event((byte)Keys.V, 0, 0, 0);
                keybd_event((byte)Keys.V, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0406PictureBox) // "B"
            {
                keybd_event((byte)Keys.B, 0, 0, 0);
                keybd_event((byte)Keys.B, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0407PictureBox) // "N"
            {
                keybd_event((byte)Keys.N, 0, 0, 0);
                keybd_event((byte)Keys.N, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0408PictureBox) // "M"
            {
                keybd_event((byte)Keys.M, 0, 0, 0);
                keybd_event((byte)Keys.M, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0409PictureBox) // "<"
            {
                keybd_event((byte)Keys.Oemcomma, 0, 0, 0);
                keybd_event((byte)Keys.Oemcomma, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0410PictureBox) // ">"
            {
                keybd_event((byte)Keys.OemPeriod, 0, 0, 0);
                keybd_event((byte)Keys.OemPeriod, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0411PictureBox) // "?"
            {
                keybd_event((byte)Keys.OemQuestion, 0, 0, 0);
                keybd_event((byte)Keys.OemQuestion, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0412PictureBox) // "Right Shift"
            {
                if (this.isPressedSHIFT == false)
                {
                    this.isPressedSHIFT = true;

                    Image leftSHIFTImage = Properties.Resources.K0401ON;
                    Image rightSHIFTImage = Properties.Resources.K0412ON;

                    this.k0401PictureBox.Image = leftSHIFTImage;
                    this.k0412PictureBox.Image = rightSHIFTImage;

                    keybd_event((byte)Keys.RShiftKey, 0, 0, 0);
                }
                else
                {
                    this.isPressedSHIFT = false;

                    Image leftSHIFTImage = Properties.Resources.K0401;
                    Image rightSHIFTImage = Properties.Resources.K0412;

                    this.k0401PictureBox.Image = leftSHIFTImage;
                    this.k0412PictureBox.Image = rightSHIFTImage;

                    keybd_event((byte)Keys.RShiftKey, 0, 0x02, 0);
                }
            }

            /*
            if (keyPictureBox == this.k0413PictureBox) // "↑"
            {
                keybd_event((byte)Keys.Up, 0, 0, 0);
                keybd_event((byte)Keys.Up, 0, 0x02, 0);
            }
            */
            #endregion
            #region 5행

            if (keyPictureBox == this.k0501PictureBox) // "Left Ctrl"
            {
                keybd_event((byte)Keys.LControlKey, 0, 0, 0);
                keybd_event((byte)Keys.LControlKey, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0502PictureBox) // "Left Alt"
            {
                keybd_event((byte)Keys.LMenu, 0, 0, 0);
                keybd_event((byte)Keys.LMenu, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0503PictureBox) // "한자"
            {
                keybd_event((byte)Keys.HanjaMode, 0, 0, 0);
                keybd_event((byte)Keys.HanjaMode, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0504PictureBox) // "Space"
            {
                keybd_event((byte)Keys.Space, 0, 0, 0);
                keybd_event((byte)Keys.Space, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0505PictureBox) // "한글/영문"
            {
                if (this.isHANGULMode == false)
                {
                    this.isHANGULMode = true;

                    Image image = Properties.Resources.K0505HANGUL;

                    this.k0505PictureBox.Image = image;
                }
                else
                {
                    this.isHANGULMode = false;

                    Image image = Properties.Resources.K0505ENGLISH;

                    this.k0505PictureBox.Image = image;
                }

                keybd_event((byte)Keys.HangulMode, 0, 0, 0);
            }

            if (keyPictureBox == this.k0506PictureBox) // "Right Alt"
            {
                keybd_event((byte)Keys.RMenu, 0, 0, 0);
                keybd_event((byte)Keys.RMenu, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0507PictureBox) // "Print"
            {
                keybd_event((byte)Keys.Apps, 0, 0, 0);
                keybd_event((byte)Keys.Apps, 0, 0x0002, 0);
            }

            if (keyPictureBox == this.k0508PictureBox) // "Right Ctrl"
            {
                keybd_event((byte)Keys.RControlKey, 0, 0, 0);
                keybd_event((byte)Keys.RControlKey, 0, 0x02, 0);
            }
            /*
            if (keyPictureBox == this.k0509PictureBox) // "←"
            {
                keybd_event((byte)Keys.Left, 0, 0, 0);
                keybd_event((byte)Keys.Left, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0510PictureBox) // "↓"
            {
                keybd_event((byte)Keys.Down, 0, 0, 0);
                keybd_event((byte)Keys.Down, 0, 0x02, 0);
            }

            if (keyPictureBox == this.k0511PictureBox) // "→"
            {
                keybd_event((byte)Keys.Right, 0, 0, 0);
                keybd_event((byte)Keys.Right, 0, 0x02, 0);
            }
            */
            #endregion
        }

        private void KeyboardImageSetup()
        {
            #region 1열
            k0101PictureBox.Image = Properties.Resources.K0101;
            k0102PictureBox.Image = Properties.Resources.K0102;
            k0103PictureBox.Image = Properties.Resources.K0103;
            k0104PictureBox.Image = Properties.Resources.K0104;
            k0105PictureBox.Image = Properties.Resources.K0105;
            k0106PictureBox.Image = Properties.Resources.K0106;
            k0107PictureBox.Image = Properties.Resources.K0107;
            k0108PictureBox.Image = Properties.Resources.K0108;
            k0109PictureBox.Image = Properties.Resources.K0109;
            k0110PictureBox.Image = Properties.Resources.K0110;
            //k0111PictureBox.Image = Properties.Resources.K0111;
            //k0111PictureBox.Image.
            k0112PictureBox.Image = Properties.Resources.K0112;
            k0113PictureBox.Image = Properties.Resources.K0113;
            k0114PictureBox.Image = Properties.Resources.K0114;
            #endregion

            #region 2열
            k0201PictureBox.Image = Properties.Resources.K0201;
            k0202PictureBox.Image = Properties.Resources.K0202;
            k0203PictureBox.Image = Properties.Resources.K0203;
            k0204PictureBox.Image = Properties.Resources.K0204;
            k0205PictureBox.Image = Properties.Resources.K0205;
            k0206PictureBox.Image = Properties.Resources.K0206;
            k0207PictureBox.Image = Properties.Resources.K0207;
            k0208PictureBox.Image = Properties.Resources.K0208;
            k0209PictureBox.Image = Properties.Resources.K0209;
            k0210PictureBox.Image = Properties.Resources.K0210;
            k0211PictureBox.Image = Properties.Resources.K0211;
            k0212PictureBox.Image = Properties.Resources.K0212;
            k0213PictureBox.Image = Properties.Resources.K0213;
            k0214PictureBox.Image = Properties.Resources.K0214;
            #endregion

            #region 3열
            k0301PictureBox.Image = Properties.Resources.K0301;
            k0302PictureBox.Image = Properties.Resources.K0302;
            k0303PictureBox.Image = Properties.Resources.K0303;
            k0304PictureBox.Image = Properties.Resources.K0304;
            k0305PictureBox.Image = Properties.Resources.K0305;
            k0306PictureBox.Image = Properties.Resources.K0306;
            k0307PictureBox.Image = Properties.Resources.K0307;
            k0308PictureBox.Image = Properties.Resources.K0308;
            k0309PictureBox.Image = Properties.Resources.K0309;
            k0310PictureBox.Image = Properties.Resources.K0310;
            k0311PictureBox.Image = Properties.Resources.K0311;
            k0312PictureBox.Image = Properties.Resources.K0312;
            k0313PictureBox.Image = Properties.Resources.K0313;
            #endregion

            #region 4열
            k0401PictureBox.Image = Properties.Resources.K0401;
            k0402PictureBox.Image = Properties.Resources.K0402;
            k0403PictureBox.Image = Properties.Resources.K0403;
            k0404PictureBox.Image = Properties.Resources.K0404;
            k0405PictureBox.Image = Properties.Resources.K0405;
            k0406PictureBox.Image = Properties.Resources.K0406;
            k0407PictureBox.Image = Properties.Resources.K0407;
            k0408PictureBox.Image = Properties.Resources.K0408;
            k0409PictureBox.Image = Properties.Resources.K0409;
            k0410PictureBox.Image = Properties.Resources.K0410;
            k0411PictureBox.Image = Properties.Resources.K0411;
            k0412PictureBox.Image = Properties.Resources.K0412;
            //k0413PictureBox.Image = Properties.Resources.K0413;
            #endregion

            #region 5열
            k0501PictureBox.Image = Properties.Resources.K0501;
            k0502PictureBox.Image = Properties.Resources.K0502;
            k0503PictureBox.Image = Properties.Resources.K0503;
            k0504PictureBox.Image = Properties.Resources.K0504;
            k0505PictureBox.Image = Properties.Resources.K0505;
            k0506PictureBox.Image = Properties.Resources.K0506;
            k0507PictureBox.Image = Properties.Resources.K0507;
            k0508PictureBox.Image = Properties.Resources.K0508;
            //k0509PictureBox.Image = Properties.Resources.K0509;
            //k0510PictureBox.Image = Properties.Resources.K0510;
            //k0511PictureBox.Image = Properties.Resources.K0511;
            //k0512PictureBox.Image = Properties.Resources.K0512;
            //k0513PictureBox.Image = Properties.Resources.K0513;
            //k0514PictureBox.Image = Properties.Resources.K0514;
            #endregion
        }

        #region 초기화 하기 - Initialize()

        /// <summary>
        /// 초기화 하기
        /// </summary>
        private void Initialize()
        {
            InitHook(this.virtualKeyboardPanel.Handle);

            InstallHook();
        }
        #endregion
    }
}