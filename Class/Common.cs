using Kiosk.Popup;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;

namespace Kiosk.Class
{
    public class Common
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string Name { get; internal set; }
        public static string MobileNO { get; internal set; }
        public static string PersonalNO { get; internal set; }
        public static string Address { get; internal set; }
        public static string YKIHO { get; internal set; }
        public static int check { get; internal set; }
        public static string USER_ID { get; internal set; }
        public static string USER_NM { get; internal set; }
        public static bool ADMIN_YN { get; internal set; }
        public static object USER_BTH { get; internal set; }
        public static string DR_LCS_NO { get; internal set; }
        public static string SurgeryKind { get; internal set; }
        public static string Pat_No { get; internal set; }
        public static string VIST_SN { get; internal set; }
        public static string SMS_YN { get; internal set; }
        public static string Rules { get; internal set; }
        public static string Ptntinfo { get; internal set; }
        public static string Eventarl { get; internal set; }

        public static string userid = "App";

        public static string Mobile_Message = "휴대폰 번호를 입력해 주세요.";
        internal static string input;

        public class SendPatientModel
        {
            private string m_PatNo;
            public string PatNo
            {
                get { return m_PatNo; }
                set { this.m_PatNo = value; }
            }

            private string m_MobileNo;
            public string MobileNo
            {
                get { return m_MobileNo; }
                set { this.m_MobileNo = value; }
            }

            private string m_PatName;
            public string PatName
            {
                get { return m_PatName; }
                set { this.m_PatName = value; }
            }

            private string m_VisitSn;
            public string VisitSn
            {
                get { return m_VisitSn; }
                set { this.m_VisitSn = value; }
            }
            //고객 내원일(예약일)
            private string m_ReserveDate;
            public string ReserveDate
            {
                get { return m_ReserveDate; }
                set { this.m_ReserveDate = value; }
            }
            //고객 내원시간(예약시간)
            private string m_ReserveTime;
            public string ReserveTime
            {
                get { return m_ReserveTime; }
                set { this.m_ReserveTime = value; }
            }

            //수동발송 기능과 환자별 특정시간을 지정해야 할때 사용
            private string m_SendDateTime;
            public string SendDateTime
            {
                get { return m_SendDateTime; }
                set { this.m_SendDateTime = value; }
            }
        }

        #region Label Drawing Region 
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]            //Dll임포트
        private static extern IntPtr CreateRoundRectRgn                            //파라미터
    (
        int nLeftRect,      // x-coordinate of upper-left corner
        int nTopRect,       // y-coordinate of upper-left corner
        int nRightRect,     // x-coordinate of lower-right corner
        int nBottomRect,    // y-coordinate of lower-right corner
        int nWidthEllipse,  // height of ellipse
        int nHeightEllipse  // width of ellipse
    );

        internal static void ReadingQRcode(string text)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($" select qi.SERIAL_NO ");
                sb.AppendLine($"     , qi.YKIHO ");
                sb.AppendLine($"     , qi.PAT_NM ");
                sb.AppendLine($"     , qi.MOBILE_NO ");
                sb.AppendLine($"     , qi.QR_IMG ");
                sb.AppendLine($"     , qi.REG_DT ");
                sb.AppendLine($"from QR_INFO qi ");
                sb.AppendLine($"where SERIAL_NO = '{text}'");
                DataTable qrDT = DBCommon2.SelectData(sb.ToString());

                if (qrDT.Rows.Count > 0)
                {
                    Name = qrDT.Rows[0]["PAT_NM"].ToString();
                    MobileNO = qrDT.Rows[0]["MOBILE_NO"].ToString();

                    string hpno = MobileNO.Substring(0, 3) + "-" + MobileNO.Substring(3, 4) + "-" + MobileNO.Substring(7, 4);

                    sb.Clear();
                    sb.AppendLine($" select *");
                    sb.AppendLine($" from PTNT_INFO pi2");
                    sb.AppendLine($" where YKIHO = '{YKIHO}'");
                    sb.AppendLine($"   and PAT_NM like '%{Name}%'");
                    sb.AppendLine($"   and MOBILE_NO = '{hpno}'");
                    DataTable PtCntDT = DBCommon.SelectData(sb.ToString());

                    if (PtCntDT.Rows.Count > 0)
                    {
                        Ptnt_List_Popup ptnt_List_Popup = new Ptnt_List_Popup();
                        ptnt_List_Popup.dt = PtCntDT;
                        var dr = ptnt_List_Popup.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            bool chk = Receipt.SetReceipt(PtCntDT.Rows[0]["PAT_NM"].ToString()
                                     , PtCntDT.Rows[0]["PAT_BTH"].ToString()
                                     , PtCntDT.Rows[0]["MOBILE_NO"].ToString()
                                     , PtCntDT.Rows[0]["PAT_NO"].ToString());
                            if (chk)
                            {
                                if (PtCntDT != null)
                                {
                                    //SendSMS(chk, Common.Pat_No, Common.YKIHO);
                                    PopupMessage popupMessage = new PopupMessage();
                                    popupMessage.Names = Name;
                                    popupMessage.message = "접수되었습니다.";
                                    popupMessage.result = "대기해 주시면 순차적으로 안내 도와드리겠습니다.";
                                    popupMessage.StartPosition = FormStartPosition.CenterScreen;
                                    popupMessage.ShowDialog();
                                }
                            }
                            else
                            {
                                PopupMessage popupMessage = new PopupMessage();
                                popupMessage.Names = PtCntDT.Rows[0]["PAT_NO"].ToString();
                                popupMessage.message = "접수중 오류가 발생하였습니다.";
                                popupMessage.StartPosition = FormStartPosition.CenterScreen;
                                popupMessage.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetLog(ex.Message, 4);
            }
        }

        #endregion

        public static void Init()
        {
            //Common Init
            Name = string.Empty;
            PersonalNO = string.Empty;
            SurgeryKind = string.Empty;
            MobileNO = string.Empty;
            Address = string.Empty;
            input = string.Empty;
            Rules = "0";
            Ptntinfo = "0";
            Eventarl = "0";
        }

        public class Road_Address
        {
            public string ZipCode { get; set; }
            public string RoadAddress { get; set; }
        }

        public class JsonAddress
        {
            public Road_Address road_address { get; set; }
            public List<Road_Address> _Address_List { get; set; }
        }

        public static void PageMove(string form, string PreviewForm, string chk)
        {
            try
            {
                Form fc = Application.OpenForms[form]; //열린폼을 찾기
                                                       //[form]; // as Main;
                Form preform1 = Application.OpenForms[PreviewForm]; //현재 작업중인 폼
                if (preform1 != null)
                {
                    preform1.Hide(); //현재 작업중인 폼을 감춘다
                }
                if (fc != null) //호출해야 할 폼이 생성되어 있는지 확인
                {
                    //fc.ShowInTaskbar = true;
                    fc.Visible = true; //호출해야 할 폼을 표출한다
                    fc.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    switch (form) //폼 생성
                    {
                        case "InputAddress":
                            fc = new InputAddress();
                            break;
                        case "inputAddress":
                            fc = new InputAddress();
                            break;
                        case "InputMobileNo":
                            fc = new InputMobileNo();
                            break;
                        case "InputPersonalNO":
                            fc = new InputPersonalNO();
                            break;
                        case "Main":
                            fc = new Main();
                            break;
                        case "Sub_NewPtnt":
                            fc = new Sub_NewPtnt();
                            break;
                        case "SurgeryKind":
                            fc = new SurgeryKind();
                            break;
                        case "PopupAddressApi":
                            fc = new PopupAddressApi();
                            break;
                        case "ReceiptInfo":
                            fc = new ReceiptInfo();
                            break;
                        case "LogIn":
                            fc = new LogIn();
                            break;
                        case "InputMobileNo_Add":
                            fc = new InputMobileNo_Add();
                            break;
                    }
                }
                fc.StartPosition = FormStartPosition.CenterScreen;
                fc.ShowDialog();

                #region 사용안함
                /*
                if (chk == "0")
                {
                    foreach (Form forms in Application.OpenForms)
                    {
                        if (forms.Name != "Main")
                        {
                            Application.OpenForms[form].Show();
                            forms.Hide();
                            forms.Close();
                            break;
                        }
                    }
                }
                if (fc != null)
                {
                    preform1.Hide();
                    Application.OpenForms[form].Show();
                    preform1.Close();
                }
                else
                {
                    //fc = new Form();

                    }
                }
                */
                #endregion

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                SetLog(ex.Message, 3);
            }
        }

        internal static void SetLog(string message, int Lv)
        {
            if (Lv == 0)
            {
                log.Debug("Debug " + message);
            }
            else if (Lv == 1)
            {
                log.Info("Info " + message);
            }
            else if (Lv == 2)
            {
                log.Warn("Warn " + message);
            }
            else if (Lv == 3)
            {
                log.Error("Error " + message);
            }
            else if (Lv == 4)
            {
                log.Fatal("Fatal " + message);
            }
        }

        internal static void RoundButtonCorners(Button button, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90); // 왼쪽 위 모서리
            path.AddArc(button.Width - radius, 0, radius, radius, 270, 90); // 오른쪽 위 모서리
            path.AddArc(button.Width - radius, button.Height - radius, radius, radius, 0, 90); // 오른쪽 아래 모서리
            path.AddArc(0, button.Height - radius, radius, radius, 90, 90); // 왼쪽 아래 모서리
            path.CloseFigure();

            button.Region = new Region(path);
        }

        internal static bool send_SMS(bool v, string mobileNo, string pat_no, string vist_sn, string yKIHO)
        {
            try
            {
                bool ret = v;

                List<SendPatientModel> sendPatientList = new List<SendPatientModel>();
                SendPatientModel sendPatient = new SendPatientModel();

                sendPatient.MobileNo = mobileNo;
                sendPatient.PatName = Common.USER_NM;
                sendPatient.PatNo = Common.Pat_No;
                sendPatient.VisitSn = VIST_SN;
                sendPatient.ReserveDate = DateTime.Now.ToString("yyyyMMdd");
                sendPatient.ReserveTime = DateTime.Now.ToString("HHmm");

                sendPatientList.Add(sendPatient);
                Common commonSms = new CommonSms();

                string retMsg = commonSms.SendMsg(ret, sendPatientList);

                ret = retMsg == "1" ? true : false;
                return ret;
            }
            catch
            {
                return false;
            }
        }

        private string SendMsg(bool kinds_, List<SendPatientModel> sendPatientList)
        {
            int ret = 0;
            string Plan_CD = string.Empty;
            if (kinds_)
            {
                Plan_CD = "Accept";
            }

            CreatorSmsFactory creator = new CreatorSmsFactory();
            SendSms sendMsg = creator.CreateInstance(Plan_CD, sendPatientList); //작업중

            if (sendMsg != null)
                ret = sendMsg.Send();

            if (ret == 0)
                return "1";
            else
                return "-1";
        }
    }
}