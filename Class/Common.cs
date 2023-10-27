using Kiosk.Popup;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

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
    }
}