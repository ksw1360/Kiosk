using Kiosk.Popup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kiosk.Class
{
    internal class Receipt
    {
        internal static StringBuilder sb = new StringBuilder();
        internal static void ReceiptContract(string name, string pat_jno2, string surgery, string mobile, string address, string kind, string SMS, string personalInfo, string rcvEventMsg)
        {
            //구환 접수
            //1. 환자 정보 조회
            //2. 접수
            try
            {
                sb.Clear();
                sb.AppendLine($" SELECT PAT_NO , PAT_NM , PAT_BTH");
                sb.AppendLine($" , CONCAT(SUBSTRING(MOBILE_NO,1,3),'-',REPLACE(SUBSTRING(MOBILE_NO,5,4),SUBSTRING(MOBILE_NO,5,4),'****'),'-',SUBSTRING(MOBILE_NO,10,4)) as MOBILE_NO ");
                sb.AppendLine($" FROM PTNT_INFO ");
                sb.AppendLine($" WHERE PAT_NM LIKE '%{name}%'");
                if (string.IsNullOrEmpty(pat_jno2) == false)
                {
                    sb.AppendLine($" AND SUBSTR(PAT_JNO2,1,6) = '{pat_jno2}'");
                }
                else if (string.IsNullOrEmpty(mobile) == false)
                {
                    sb.AppendLine($" AND MOBILE_NO  like '%{mobile}%'");
                }

                DataTable Ptnt_Dt = DBCommon.SelectData(sb.ToString());

                //if (textBox1.Text.Length >= 4 && textBox1.Text.Length <= 13)
                if (Ptnt_Dt.Rows.Count > 0) //구환
                {
                    if (Ptnt_Dt.Rows.Count > 1)
                    {
                        using (var ptnt_List_Popup = new Ptnt_List_Popup())
                        {
                            ptnt_List_Popup.dt = Ptnt_Dt;
                            var dr = ptnt_List_Popup.ShowDialog();
                            if (dr == DialogResult.OK)
                            {
                                var Ptnt_List = ptnt_List_Popup.List;
                                //접수
                                bool chk = SetReceipt(Ptnt_List[0], Ptnt_List[1], Ptnt_List[2], Ptnt_List[3]);

                                if (chk)
                                {
                                    //접수 완료 메세지창
                                    if (Ptnt_List != null) // <- 수정예정
                                    {
                                        PopupMessage popupMessage = new PopupMessage();
                                        popupMessage.Names = name;
                                        popupMessage.message = "접수되었습니다.";
                                        popupMessage.result = "대기해 주시면 순차적으로 안내 도와드리겠습니다.";
                                        popupMessage.StartPosition = FormStartPosition.CenterScreen;
                                        popupMessage.ShowDialog();
                                    }
                                }
                                else
                                {
                                    PopupMessage popupMessage = new PopupMessage();
                                    popupMessage.Names = Ptnt_List[0];
                                    popupMessage.message = "접수중 오류가 발생하였습니다.";
                                    popupMessage.StartPosition = FormStartPosition.CenterScreen;
                                    popupMessage.ShowDialog();

                                }
                            }
                        }
                    }
                    else if (Ptnt_Dt.Rows.Count < 2)
                    {
                        string modifiedText = string.Empty;
                        PopupMessageQuestion popup = new PopupMessageQuestion();
                        popup.panel4.Visible = true;
                        popup.Names = "입력하신 정보가 존재합니다.";
                        string _names = Ptnt_Dt.Rows[0]["PAT_NM"].ToString();
                        if (_names.Length >= 3)
                        {
                            modifiedText = _names.Substring(0, 1) + "*" + _names.Substring(2, 1);
                        }
                        else if (_names.Length <= 3)
                        {
                            modifiedText = _names.Substring(0, 1) + "*";
                        }
                        popup.messages = modifiedText + " " + Ptnt_Dt.Rows[0]["MOBILE_NO"].ToString();
                        popup.result = "으로 접수하시겠습니까?";
                        DialogResult dr = popup.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            bool chk = SetReceipt(Ptnt_Dt.Rows[0]["PAT_NM"].ToString()
                                     , Ptnt_Dt.Rows[0]["PAT_BTH"].ToString()
                                     , Ptnt_Dt.Rows[0]["MOBILE_NO"].ToString()
                                     , Ptnt_Dt.Rows[0]["PAT_NO"].ToString());
                            if (chk)
                            {
                                if (Ptnt_Dt != null)
                                {
                                    PopupMessage popupMessage = new PopupMessage();
                                    popupMessage.Names = name;
                                    popupMessage.message = "접수되었습니다.";
                                    popupMessage.result = "대기해 주시면 순차적으로 안내 도와드리겠습니다.";
                                    popupMessage.StartPosition = FormStartPosition.CenterScreen;
                                    popupMessage.ShowDialog();
                                }
                            }
                            else
                            {
                                PopupMessage popupMessage = new PopupMessage();
                                popupMessage.Names = Ptnt_Dt.Rows[0]["PAT_NO"].ToString();
                                popupMessage.message = "접수중 오류가 발생하였습니다.";
                                popupMessage.StartPosition = FormStartPosition.CenterScreen;
                                popupMessage.ShowDialog();
                            }
                        }
                        else
                        {
                            //MessageBox.Show("취소되었습니다.");
                        }
                    }
                }
                else
                {
                    //신환 접수
                    //1. 신환 등록
                    //2. 신환 접수
                    //Common.PageMove("InputPersonalNO", "InputMobileNo", "1");

                    if (!SetPtntInfo(name, pat_jno2, surgery, mobile, address, kind, SMS, personalInfo, rcvEventMsg)) //신환 등록
                    {
                        PopupMessage popupMessage = new PopupMessage();
                        popupMessage.Names = Ptnt_Dt.Rows[0]["PAT_NM"].ToString();
                        popupMessage.message = "신규 등록중 오류가 발생하였습니다.";
                        popupMessage.StartPosition = FormStartPosition.CenterScreen;
                        popupMessage.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Common.SetLog(ex.Message, 3);
            }
        }

        private static bool SetPtntInfo(string name, string pat_jno2, string surgery, string mobile, string address, string kind, string sMS, string personalInfo, string rcvEventMsg)
        {
            bool chk = false;

            try
            {
                //수진자번호(PAT_NO) 생성하기
                sb.Clear();
                sb.AppendLine($" SELECT  LPAD(IFNULL(MAX(PAT_NO) + 1, 1), 10, 0) AS PAT_NO ");
                sb.AppendLine($" FROM PTNT_INFO ");
                sb.AppendLine($" WHERE YKIHO = '22222222' ");
                sb.AppendLine($" AND SUBSTRING(PAT_NO, 1, 1) <> '6' ");
                DataTable dt = DBCommon.SelectData(sb.ToString());
                string pat_no = dt.Rows[0]["PAT_NO"].ToString();
                if (pat_no != "")
                {
                    Common.Pat_No = pat_no;
                }
                else
                {
                    MessageBox.Show("수진자번호 생성을 실패하였습니다.");
                    chk = false;
                }

                //신규차트번호
                sb.Clear();
                sb.AppendLine($" SELECT LPAD(IFNULL(MAX(RCNT_CHART_NO + 1), 0), 10, 0) AS CHART_NO FROM HOSP_INFO WHERE YKIHO = '{Common.YKIHO}' ");
                DataTable dt20 = new DataTable();
                dt20 = DBCommon.SelectData(sb.ToString());
                string cahrt_no = string.Join(Environment.NewLine, dt20.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
                //신환 등록
                sb.Clear();
                sb.AppendLine($" INSERT INTO PTNT_INFO (YKIHO, PAT_NO, PAT_NM, CHART_NO, PAT_JNO2, MOBILE_NO, ADDR, SMS_AGR_YN, AD_SMS_AGR_YN, PRSN_INFO_AGR_YN, FRST_REG_ID, FRST_REG_DT, LAST_MOD_ID, LAST_MOD_DT)");
                sb.AppendLine($" VALUES (");
                sb.AppendLine($"   '{Common.YKIHO}'");
                sb.AppendLine($" , '{Common.Pat_No}'");
                sb.AppendLine($" , '{name}'");
                sb.AppendLine($" , '{cahrt_no}'");
                sb.AppendLine($" , '{pat_jno2}'");
                sb.AppendLine($" , '{mobile}'");
                sb.AppendLine($" , '{address}'");
                sb.AppendLine($" , '{sMS}'");
                sb.AppendLine($" , '{personalInfo}'");
                sb.AppendLine($" , '{rcvEventMsg}'");
                sb.AppendLine($" , '{Common.USER_ID}'");
                sb.AppendLine($" , Now()");
                sb.AppendLine($" , '{Common.USER_ID}'");
                sb.AppendLine($" , Now()");
                sb.AppendLine($" ) ON DUPLICATE KEY UPDATE LAST_MOD_ID = '{Common.USER_ID}'");
                sb.AppendLine($" , LAST_MOD_DT = NOW()");
                int rowAffected = DBCommon.InsertIF(sb.ToString());
                if (rowAffected > 0)
                {
                    //신환 등록후 재조회
                    sb.Clear();
                    sb.AppendLine($" SELECT PAT_NO , PAT_NM , PAT_BTH , MOBILE_NO ");
                    sb.AppendLine($" FROM PTNT_INFO ");
                    sb.AppendLine($" WHERE MOBILE_NO  like '%{mobile}%'");
                    DataTable Ptnt_Dt = DBCommon.SelectData(sb.ToString());

                    if (Ptnt_Dt.Rows.Count > 0) //정상등록시
                    {
                        chk = SetReceipt(name, pat_jno2, mobile, Common.Pat_No);
                        if (chk)
                        {
                            PopupMessage popupMessage = new PopupMessage();
                            popupMessage.Names = name + " " + mobile;
                            popupMessage.message = "신규 환자 등록되었습니다.";
                            popupMessage.result = "대기해 주시면 순차적으로 안내 도와드리겠습니다.";
                            popupMessage.StartPosition = FormStartPosition.CenterScreen;
                            popupMessage.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.SetLog(ex.Message, 3);
                chk = false;
            }

            return chk;
        }

        /// <summary>
        /// 접수하기
        /// </summary>
        private static bool SetReceipt(string name, string birthday, string mobile, string PAT_NO)
        {
            bool chk = false;

            int ret = 0;
            string today = DateTime.Now.ToString("yyyyMMdd");
            sb.AppendLine($"  SELECT PAT_NO ");
            sb.AppendLine($"  FROM PTNT_DETL ");
            sb.AppendLine($"  WHERE YKIHO = '{Common.YKIHO}' ");
            sb.AppendLine($"    AND PAT_NO = '{PAT_NO}' ");
            sb.AppendLine($"    AND ACPT_DD = '{today}' ");
            sb.AppendLine($"    AND PRGR_STAT_CD NOT IN('A', 'F') ");
            DataTable dt10 = DBCommon.SelectData(sb.ToString());

            if (dt10 != null)
            {
                if (MessageBox.Show("해당 일자에 접수된 기록이 있습니다.", "접수하시겠습니까?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //보험처리
                    //접수
                    if ((ret = SaveAcpt(Common.YKIHO, PAT_NO)) != 0)
                    {
                        chk = true;
                    }
                }
            }
            else
            {
                //접수
                if ((ret = SaveAcpt(Common.YKIHO, PAT_NO)) != 0)
                {
                    chk = true;
                }
            }
            return chk;
        }

        private static int SaveAcpt(string yKIHO, string pAT_NO)
        {
            int ret = 0;
            int val = 0;
            string today = DateTime.Now.ToString("yyyyMMdd");
            string currenttime = DateTime.Now.ToString("HH:mm");
            List<string> lstStrSQL = new List<string>();
            DataTable table = new DataTable();
            bool late_ = false; // 지연설정 
            //string strSQL = "";
            string insu_Knd_Cd = string.Empty;
            string rcntVstDd = string.Empty;
            string diagTpCd = "C01";
            string vistsn = string.Empty;

            try
            {
                insu_Knd_Cd = "B01";
                sb.AppendLine($" SELECT MAX(ACPT_DD) AS MAX_ACPT_DD, PAT_NO");
                sb.AppendLine($"   FROM PTNT_DETL");
                sb.AppendLine($"  WHERE YKIHO = '{yKIHO}'");
                sb.AppendLine($"    AND PAT_NO = '{pAT_NO}'");
                sb.AppendLine($"    AND PRGR_STAT_CD NOT IN ('A', 'F')");
                sb.AppendLine($"    AND RSRV_CNCL_YN = FALSE");
                sb.AppendLine($"    AND DEL_YN = FALSE");
                var dt10 = DBCommon.SelectData(sb.ToString());

                if (dt10 != null)
                {
                    string MAX_ACPT_DD = dt10.Rows[0]["MAX_ACPT_DD"].ToString();
                    //int res = Convert.ToInt32();

                    sb.Clear();
                    sb.AppendLine($" SELECT LPAD(IFNULL(MAX(VIST_SN) + 1, 1), 5, 0) FROM PTNT_DETL WHERE YKIHO='{yKIHO}' AND PAT_NO = '{pAT_NO}'");

                    rcntVstDd = dt10.Rows[0]["MAX_ACPT_DD"].ToString();
                    //vistsn = dt10.Rows[0]["VIST_SN"].ToString();

                    if (vistsn == "")
                    {
                        vistsn = DBCommon.ChkVISTNum(yKIHO, pAT_NO);
                    }

                    rcntVstDd = today;
                    //string.Join(Environment.NewLine, dt10.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));

                    /*
                    if (!string.IsNullOrEmpty(rcntVstDd))
                    {
                        diagTpCd = "C02";
                        if (DateTime.Compare(Convert.ToDateTime(today), DateTime.ParseExact(rcntVstDd, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None)) > 0)
                        {
                            rcntVstDd = today;
                        }
                    }
                    */

                    #region PTNT_DETL(INSERT, UPDATE)
                    sb.Clear();
                    sb.AppendLine($" UPDATE PTNT_DETL ");
                    sb.AppendLine($" SET ");
                    sb.AppendLine($"    PRGR_STAT_CD = 'B' ");
                    //if (String.IsNullOrEmpty(dt10.Rows[0]["ACPT_DD"].ToString()))
                    if (string.IsNullOrEmpty(rcntVstDd))
                    {
                        sb.AppendLine($" ,  ACPT_DD = '{today}'");        //접수일자 
                        sb.AppendLine($" ,  ACPT_TM = '{currenttime}'");  //접수시간
                        late_ = true;
                    }

                    sb.AppendLine($" , RSRV_CNCL_YN=0");                  //예약취소여부(0:false, 1:true
                    sb.AppendLine($" , ACPT_CNCL_YN=0");                  //접수취소여부(0:false, 1:true)
                    sb.AppendLine($" , INSU_KND_CD= '{insu_Knd_Cd}'");    //보험종류코드
                    sb.AppendLine($" , STATUS_BOARD_CD ='A'");
                    sb.AppendLine($" , STATUS_TIME= NOW()");
                    sb.AppendLine($" , LAST_MOD_ID='{Common.userid}'");
                    sb.AppendLine($" , LAST_MOD_DT=NOW()");
                    sb.AppendLine($" WHERE YKIHO='{yKIHO}'");
                    sb.AppendLine($"   AND PAT_NO='{pAT_NO}'");
                    sb.AppendLine($"   AND VIST_SN='{vistsn}'");
                    val = DBCommon.UpdateQuery2(sb.ToString());

                    sb.Clear();
                    sb.AppendLine($" UPDATE PTNT_INFO SET");
                    sb.AppendLine($"    INSU_KND_CD='BO1'");              //보험종류코드
                    sb.AppendLine($"  , RCNT_VST_DD='{rcntVstDd}'");      //최근내원일자
                    sb.AppendLine($"  , LAST_MOD_ID='{Common.userid}'"); //최종수정자ID
                    sb.AppendLine($"  , LAST_MOD_DT=NOW() ");
                    sb.AppendLine($" WHERE YKIHO='{yKIHO}'");
                    sb.AppendLine($"   AND PAT_NO='{pAT_NO}'");           //최종수정일
                    val = val + DBCommon.UpdateQuery2(sb.ToString());

                    if (val == 2)
                    {
                        //SMS 전송 부분 키오스크에서 접수시 문자 수신 여부 
                        //기획에게 문의후 작업 예정
                        string message_ = "[접수안내]\r\n접수 완료 되었습니다.\r\n[CRM안내]\r\n전송된 결과가 없습니다.";
                        string AC_Me = "";
                        string LA_Me = "";
                        /*
                        if (late_)
                        {
                            //string strSQL = "";
                            strSQL += Environment.NewLine + "SELECT IFNULL(SMS_AGR_YN, '0') AS SMS_AGR_YN, MOBILE_NO";
                            strSQL += Environment.NewLine + "  FROM PTNT_INFO";
                            strSQL += Environment.NewLine + $" WHERE PAT_NO = '{pAT_NO}'";
                            strSQL += Environment.NewLine + $" AND YKIHO = '{yKIHO}'";
                            var dt20 = DBCommon.SelectData(sb.ToString());
                            string MobileNo = dt20.Rows[0]["MOBILE_NO"].ToString();
                        }
                        */
                    }

                    ret = 1;
                    #endregion
                }
                else
                {
                    if (string.IsNullOrEmpty(vistsn))
                    {
                        vistsn = "00000";
                    }
                    sb.Clear();
                    sb.AppendLine($" INSERT INTO Motion_DB.PTNT_DETL(YKIHO, PAT_NO, VIST_SN, PRGR_STAT_CD, ACPT_DD, RSRV_DD, RSRV_TM, RSRV_CNCL_YN, FRST_REG_ID, FRST_REG_DT, LAST_MOD_ID, LAST_MOD_DT)");
                    sb.AppendLine($" VALUES(");
                    sb.AppendLine($"   '{yKIHO}'");
                    sb.AppendLine($" , '{pAT_NO}'");
                    sb.AppendLine($" , '{vistsn}'");
                    sb.AppendLine($" , 'B'");
                    sb.AppendLine($" , '{today}'");
                    sb.AppendLine($" , '{today}'");
                    sb.AppendLine($" , '{currenttime.Replace(":", "")}'");
                    sb.AppendLine($" , '0'");
                    sb.AppendLine($" , 'App'");
                    sb.AppendLine($" , NOW()");
                    sb.AppendLine($" , 'App'");
                    sb.AppendLine($" , NOW()");
                    //sb.AppendLine($" , '{}'");
                    sb.AppendLine($" ) ON DUPLICATE KEY UPDATE ");
                    sb.AppendLine($"   PRGR_STAT_CD='B'");
                    sb.AppendLine($" , ACPT_DD = '{today}'");
                    sb.AppendLine($" , ACPT_DD = '{currenttime.Replace(":", "")}'");
                    sb.AppendLine($" , ACPT_CNCL_YN = '0' ");
                    //sb.AppendLine($"   FRST_REG_ID = 'App'");
                    //sb.AppendLine($"   FRST_REG_DT = NOW()");
                    sb.AppendLine($" , LAST_MOD_ID = 'App'");
                    sb.AppendLine($" , LAST_MOD_DT = NOW()");
                    ret = DBCommon.InsertIF(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Common.SetLog(ex.Message, 3);
                ret = 0;
            }

            return ret;
        }
    }
}