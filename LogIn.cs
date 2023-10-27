using Kiosk.Class;
using Kiosk.Popup;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Kiosk
{
    public partial class LogIn : Form
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
        private bool playSound = true;

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
        private int idx;

        #endregion

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder reVal, int size, string filepath);

        // 윈도우 찾기
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);

        // 윈도우 표시하기/숨기기
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int windowHandle, int command);
        
        DataRow drUser;

        SHA256 sha256 = new SHA256Managed();

        public LogIn()
        {
            InitializeComponent();
            this.CenterToScreen();
            txtUser.AutoSize = false;
            txtUser.Height += 10;
            txtUserPW.AutoSize = false;
            txtUserPW.Height += 10;
            //this.TopMost = true;
        }

        // 필요한 위치에서 다음 프로시저를 호출
        private void Taskbar(string sts)
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            if (sts == "Hide")
                ShowWindow(hwnd, 0);
            if (sts == "Show")
                ShowWindow(hwnd, 1);
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            Common.SetLog("Program Start", 1);
            Initialize();
            //Taskbar("Hide");
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1080, 1920);
            //this.StartPosition = FormStartPosition.CenterScreen;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20));

            this.txtUser.Size = new System.Drawing.Size(800, 80);
            this.txtUserPW.Size = new System.Drawing.Size(800, 80);
            
            this.txtUser.Font = new Font("NotoSansKR-Regular", 36, FontStyle.Regular);
            this.txtUserPW.Font = new Font("NotoSansKR-Regular", 36, FontStyle.Regular);
            this.txtUser.TextAlign = HorizontalAlignment.Left;
            this.txtUserPW.TextAlign = HorizontalAlignment.Left;
            this.txtUser.Text = "ID";
            this.txtUserPW.Text = "PASSWORD";

            string path = Application.StartupPath + @"\LogIn.ini";
            if (File.Exists(path))
            {
                StringBuilder _id = new StringBuilder();
                StringBuilder _pw = new StringBuilder();
                StringBuilder _chk = new StringBuilder();
                GetPrivateProfileString("LogIn", "ID", "", _id, _id.Capacity, path);
                GetPrivateProfileString("LogIn", "PASSWORD", "", _pw, _pw.Capacity, path);
                GetPrivateProfileString("LogIn", "Checked", "", _chk, _chk.Capacity, path);

                txtUser.Text = _id.ToString();
                txtUserPW.Text = _pw.ToString();
                //label1.Text = _id.ToString();
                if(_chk.ToString() !=""|| _chk!=null)
                {
                    if(_chk.ToString() == "Checked")
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }
            }            

            KeyboardImageSetup();

            Common.RoundButtonCorners(button1, 20);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Taskbar("Show");
            CloseAllForms();
            this.Close();
        }

        private void CloseAllForms()
        {
            // 모든 열린 폼을 확인하고 닫습니다.
            foreach (Form form in Application.OpenForms)
            {
                if (form != this)
                {
                    form.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ykihoInput ykihoInput = new ykihoInput();
            ykihoInput.ShowDialog();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            txtUser.Text = string.Empty;
            txtUser.Focus();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            txtUserPW.Text = string.Empty;
            txtUserPW.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\LogIn.ini";
            if (checkBox1.Checked)
            {
                WritePrivateProfileString("LogIn", "ID", txtUser.Text, path);
                WritePrivateProfileString("LogIn", "PASSWORD", txtUserPW.Text, path);
                WritePrivateProfileString("LogIn", "Checked", "Checked", path);
            }

            if (File.Exists(Application.StartupPath + @"\YKIHO.ini"))
            {
                StringBuilder sb = new StringBuilder();
                //WritePrivateProfileString("YKIHO", "ykiho", ykiho, path);
                GetPrivateProfileString("YKIHO", "ykiho", "", sb, sb.Capacity, Application.StartupPath + @"\YKIHO.ini");
                Common.YKIHO = sb.ToString();
            }

            if (Common.YKIHO == null || string.IsNullOrEmpty(Common.YKIHO))
            {
                MessageBox.Show("요양기관번호를 입력해주세요.");
                ykihoInput ykihoInput = new ykihoInput();
                ykihoInput.ShowDialog();
            }

            if (CheckUserInfo())
            {
                Common.PageMove("Main", this.Name, "1");
            }
        }

        private DataTable CheckUser()
        {
            DataTable dtUser = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"/* queryID : ChkUser - 사용자 체크 */");
                sb.AppendLine($" SELECT USER_ID-- 사용자ID");
                sb.AppendLine($"     , USER_NM       -- 사용자명");
                sb.AppendLine($"     , USER_PW       -- 비밀번호");
                sb.AppendLine($"     , PW_ERR_CNT    -- 비밀번호오류횟수");
                sb.AppendLine($"     , PW_INIT_YN    -- 비밀번호초기화여부");
                sb.AppendLine($"     , ADMIN_YN      -- 관리자여부");
                sb.AppendLine($"     , PW_CHG_DT     -- 비밀번호수정일시");
                sb.AppendLine($"     , USER_JNO2     -- 사용자주민등록번호(공개용)");
                sb.AppendLine($"     , LOGIN_STAT_CD -- 로그인상태코드");
                sb.AppendLine($"     , LAST_LOGIN_IP -- 최종로그인IP");
                sb.AppendLine($"     , DR_LCS_NO     -- 의사번호");
                sb.AppendLine($"  FROM USER_INFO");
                sb.AppendLine($" WHERE YKIHO   = '{Common.YKIHO}'");
                sb.AppendLine($"   AND USER_ID = '{txtUser.Text}'");
                sb.AppendLine($"   AND USE_YN  = TRUE");
                dtUser = DBCommon.SelectData(sb.ToString());
                if (dtUser.Rows.Count == 0)
                {
                    MessageBox.Show("존재하지 않는 사용자입니다.");
                    return null;
                }
                else if (dtUser.Rows.Count > 1)
                {
                    MessageBox.Show("사용자 정보가 2건 이상 조회되었습니다. 관리자에게 문의하세요");
                    return null;
                }

                DataRow drUser = dtUser.Rows[0];

                string UserData = drUser["USER_JNO2"].ToString();
                if (!string.IsNullOrWhiteSpace(UserData))
                {
                    UserData = UserData.Substring(0, 6);
                }

                Common.USER_ID = drUser["USER_ID"].ToString();
                Common.USER_NM = drUser["USER_NM"].ToString();
                Common.ADMIN_YN = (bool)drUser["ADMIN_YN"];
                Common.USER_BTH = UserData;
                Common.DR_LCS_NO = drUser["DR_LCS_NO"].ToString();
            }
            catch (Exception ex)
            {
                Common.SetLog(ex.Message, 3);
                //MessageBox.Show(ex.Message);

                return null;
            }

            return dtUser;
        }

        private bool CheckUserInfo()
        {
            bool chk = true;
            //# validation 체크
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show(this, "아이디를 입력하세요.", "", MessageBoxButtons.OK);
                chk = false;
            }

            if (string.IsNullOrWhiteSpace(txtUserPW.Text))
            {
                MessageBox.Show(this, "비밀번호를 입력하세요.", "", MessageBoxButtons.OK);
                chk = false;
            }

            DataTable dtUser = CheckUser();
            if (dtUser == null)
            {
                chk = false;
            }

            if (dtUser.Rows.Count > 0)
            {
                drUser = dtUser.Rows[0];
            }
            //# 비밀번호 오류횟수 체크
            if (Convert.ToInt32(drUser["PW_ERR_CNT"]) >= 5)
            {
                MessageBox.Show(this, "비밀번호를 5회 잘못입력하였습니다. 관리자에게 문의하세요.", "", MessageBoxButtons.OK);
                ///2021.09.03 pec 로그인로그
                //ComFunc.LoginAccs(this, false, "비밀번호를 5회 잘못입력하였습니다");

                chk = false;
            }

            //string encPW = ComLib.SHA256(txtUserPW.Text);
            //string encPW = txtUserPW.Text;
            byte[] hash = sha256.ComputeHash(Encoding.ASCII.GetBytes(txtUserPW.Text));
            StringBuilder sb = new StringBuilder();

            foreach (byte b in hash)
            {
                sb.AppendFormat("{0:x02}", b);
            }

            string encPW = sb.ToString();

            //# 비밀번호 초기화 여부 체크
            //비밀번호 암호화 부분 - 수정예정
            //if ((bool)drUser["PW_INIT_YN"] == true && encPW == ComLib.SHA256(ComVar.INIT_PW))
            if ((bool)drUser["PW_INIT_YN"] == true)
            {
                MessageBox.Show(this, "초기화된 비밀번호입니다.\r\n비밀번호를 변경해주세요.", "", MessageBoxButtons.OK);
                PopChangePW frmChangePW = new PopChangePW();
                frmChangePW.old_password = encPW;
                if (DialogResult.OK == frmChangePW.ShowDialog())
                {
                    txtUserPW.Focus();
                    txtUserPW.SelectAll();
                }

                chk = false;
            }

            return chk;
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

        #region keyboard Key Event

        private void k0102PictureBox_Click(object sender, EventArgs e)
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

        #endregion
    }
}