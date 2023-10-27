using Kiosk.Class;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Kiosk
{
    public partial class SurgeryKind : Form
    {
        StringBuilder sb = new StringBuilder();
        string _surgerykind = string.Empty;
        string _input = string.Empty;

        public SurgeryKind()
        {
            InitializeComponent();
        }

        private void SurgeryKind_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1080, 1920); this.lbFirst.Font = new Font("맑은 고딕", 24, FontStyle.Bold);
            this.lbFirst.ForeColor = Color.Black;
            this.lbFirst.Text = "시술구분·내원경로 선택 ";

            this.lbFirst.AutoSize = false;
            this.lbFirst.TextAlign = ContentAlignment.MiddleCenter;
            this.lbFirst.Dock = DockStyle.Fill;

            this.lbSecond.AutoSize = false;
            this.lbSecond.TextAlign = ContentAlignment.MiddleCenter;
            this.lbSecond.Dock = DockStyle.Fill;

            this.btnNext.Location = new Point(337, 0);
            this.btnNext.Visible = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.radGridView1.Font = new Font("NotoSansKR-Regular", 10, FontStyle.Regular);
            this.radGridView2.Font = new Font("NotoSansKR-Regular", 10, FontStyle.Regular);

            try
            {
                radGridView1.CellFormatting += new CellFormattingEventHandler(radGridView1_CellFormatting);
                radGridView2.CellFormatting += new CellFormattingEventHandler(RadGridView2_CellFormatting);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Common.SetLog(ex.Message, 3);
            }

            for (int i = 0; i < radGridView1.Columns.Count; i++)
            {
                radGridView1.Columns[i].WrapText = true;
            }

            for (int i = 0; i < radGridView2.Columns.Count; i++)
            {
                radGridView2.Columns[i].WrapText = true;
            }

            sb.AppendLine($" with Surgery as (");
            sb.AppendLine($" SELECT DETL.BSE_CD AS CODE, DETL.BSE_CD_NM AS NAME, DETL.SEQ");
            sb.AppendLine($" FROM CODE_MASTER MSTR");
            sb.AppendLine($" , CODE_DETAIL DETL");
            sb.AppendLine($" WHERE MSTR.YKIHO = '22222222'");
            sb.AppendLine($" AND MSTR.USE_YN = TRUE");
            sb.AppendLine($" AND DETL.YKIHO = MSTR.YKIHO");
            sb.AppendLine($" AND DETL.COM_CD = MSTR.COM_CD");
            sb.AppendLine($" AND DETL.USE_YN = TRUE");
            sb.AppendLine($" AND MSTR.COM_CD_NM = '시술구분'");
            sb.AppendLine($" ORDER BY DETL.SEQ");
            sb.AppendLine($" )");
            sb.AppendLine($" select code");
            sb.AppendLine($" , '시술구분' as name");
            sb.AppendLine($" , max(case when seq=1 then name else null end) code_2");
            sb.AppendLine($" , max(case when seq=2 then name else null end) code_3");
            sb.AppendLine($" , max(case when seq=3 then name else null end) code_4");
            sb.AppendLine($" , max(case when seq=4 then name else null end) code_5");
            sb.AppendLine($" , max(case when seq=5 then name else null end) code_6");
            sb.AppendLine($" , max(case when seq=6 then name else null end) code_7");
            sb.AppendLine($" , max(case when seq=7 then name else null end) code_8");
            sb.AppendLine($" from Surgery");
            sb.AppendLine($" union all");
            sb.AppendLine($" select code");
            sb.AppendLine($" , '시술구분' as name");
            sb.AppendLine($" , max(case when seq=8 then name else null end) code_9");
            sb.AppendLine($" , max(case when seq=9 then name else null end) code_10");
            sb.AppendLine($" , max(case when seq=10 then name else null end) code_11");
            sb.AppendLine($" , max(case when seq=11 then name else null end) code_12");
            sb.AppendLine($" , max(case when seq=12 then name else null end) code_13");
            sb.AppendLine($" , max(case when seq=13 then name else null end) code_14");
            sb.AppendLine($" , max(case when seq=14 then name else null end) code_15");
            sb.AppendLine($" from Surgery");
            sb.AppendLine($" union all");
            sb.AppendLine($" select code");
            sb.AppendLine($" , '시술구분' as name");
            sb.AppendLine($" , '미지정' as code_16");
            sb.AppendLine($" , '' as code_17");
            sb.AppendLine($" , '' as code_18");
            sb.AppendLine($" , '' as code_19");
            sb.AppendLine($" , '' as code_20");
            sb.AppendLine($" , '' as code_21");
            sb.AppendLine($" , '' as code_22");
            sb.AppendLine($" from Surgery");
            sb.AppendLine($" where code = 'C01'");
            var dt10 = DBCommon.SelectData(sb.ToString());
            if (dt10 != null)
            {
                radGridView1.DataSource = dt10;
            }

            sb.Clear();
            sb.AppendLine($"  with Surgery as (");
            sb.AppendLine($"  SELECT DETL.BSE_CD AS CODE, DETL.BSE_CD_NM AS NAME, DETL.SEQ");
            sb.AppendLine($"  FROM CODE_MASTER MSTR");
            sb.AppendLine($"  , CODE_DETAIL DETL");
            sb.AppendLine($"  WHERE MSTR.YKIHO = '22222222'");
            sb.AppendLine($"  AND MSTR.USE_YN = TRUE");
            sb.AppendLine($"  AND DETL.YKIHO = MSTR.YKIHO");
            sb.AppendLine($"  AND DETL.COM_CD = MSTR.COM_CD");
            sb.AppendLine($"  AND DETL.USE_YN = TRUE");
            sb.AppendLine($"  AND MSTR.COM_CD_NM = '내원경로'");
            sb.AppendLine($"  ORDER BY CODE, SEQ");
            sb.AppendLine($"  )");
            sb.AppendLine($"  select code");
            sb.AppendLine($"  , '진료분야' as name");
            sb.AppendLine($"  , max(case when code='C00' then name else null end) code_2");
            sb.AppendLine($"  , max(case when code='C01' then name else null end) code_3");
            sb.AppendLine($"  , max(case when code='C02' then name else null end) code_4");
            sb.AppendLine($"  , max(case when code='C03' then name else null end) code_5");
            sb.AppendLine($"  , max(case when code='C04' then name else null end) code_6");
            sb.AppendLine($"  , max(case when code='C05' then name else null end) code_7");
            sb.AppendLine($"  , max(case when code='C06' then name else null end) code_8");
            sb.AppendLine($"  from Surgery");
            sb.AppendLine($"  union all");
            sb.AppendLine($"  select code");
            sb.AppendLine($"  , '진료분야' as name");
            sb.AppendLine($"  , max(case when code='C07' then name else null end) code_9");
            sb.AppendLine($"  , max(case when code='C08' then name else null end) code_10");
            sb.AppendLine($"  , max(case when code='C09' then name else null end) code_11");
            sb.AppendLine($"  , max(case when code='C10' then name else null end) code_12");
            sb.AppendLine($"  , max(case when code='C11' then name else null end) code_13");
            sb.AppendLine($"  , max(case when code='C12' then name else null end) code_14");
            sb.AppendLine($"  , max(case when code='C13' then name else null end) code_15");
            sb.AppendLine($"  from Surgery");
            sb.AppendLine($"  union all");
            sb.AppendLine($"  select code");
            sb.AppendLine($"  , '진료분야' as name");
            sb.AppendLine($"  , '미지정' as code_16");
            sb.AppendLine($"  , '' as code_17");
            sb.AppendLine($"  , '' as code_18");
            sb.AppendLine($"  , '' as code_19");
            sb.AppendLine($"  , '' as code_20");
            sb.AppendLine($"  , '' as code_21");
            sb.AppendLine($"  , '' as code_22");
            sb.AppendLine($"  from Surgery");
            sb.AppendLine($"  where code = 'C01'");
            var dt20 = DBCommon.SelectData(sb.ToString());
            if (dt20 != null)
            {
                radGridView2.DataSource = dt20;
            }

            radGridView1.Rows[0].Height = 60;
            radGridView1.Rows[1].Height = 60;
            radGridView1.Rows[2].Height = 60;

            radGridView2.Rows[0].Height = 60;
            radGridView2.Rows[1].Height = 60;
            radGridView2.Rows[2].Height = 60;

            radGridView1.Rows[0].Cells["name"].Value = "";
            radGridView1.Rows[2].Cells["name"].Value = "";

            radGridView2.Rows[0].Cells["name"].Value = "";
            radGridView2.Rows[2].Cells["name"].Value = "";

            for (int i = 0; i < radGridView1.ColumnCount; i++)
            {
                radGridView1.Columns[i].ReadOnly = true;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Common.PageMove("InputAddress", this.Name, "1");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //접수정보
            if (_surgerykind != "")
            {
                Common.SurgeryKind = _surgerykind;
                Common.input = _input;
            }

            Common.PageMove("ReceiptInfo", this.Name, "1");
        }

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (radGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].IsSelected)
            {
                if (e.ColumnIndex > 0)
                {
                    _surgerykind = radGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    radGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(99, 114, 171);
                    radGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                }
            }
            else
            {
                if (e.ColumnIndex > 0)
                {
                    _input = radGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    radGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                    radGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
                }
            }

            textBox1.Text = _surgerykind;
        }

        private void radGridView2_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (radGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].IsSelected)
            {
                if (e.ColumnIndex > 0)
                {
                    _input = radGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    radGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(99, 114, 171);
                    radGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                }
            }
            else
            {
                if (e.ColumnIndex > 0)
                {
                    _input = radGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    radGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                    radGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
                }
            }
            textBox2.Text = _input;

            if (_input !="" && _surgerykind !="")
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

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "name") // 폰트를 변경하고 싶은 컬럼 이름으로 변경
            {
                e.CellElement.Font = new Font("NotoSansKR", 12, FontStyle.Bold); // 원하는 폰트 설정
                //e.Column.RowSpan = 3;
                e.CellElement.BackColor = Color.FromArgb(99, 114, 171);
                e.CellElement.ForeColor = Color.White;
            }
            else
            {
                e.CellElement.Font = new Font("NotoSansKR", 12, FontStyle.Regular); // 원하는 폰트 설정
                e.CellElement.ForeColor = Color.Black;
            }
        }

        private void RadGridView2_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "name") // 폰트를 변경하고 싶은 컬럼 이름으로 변경
            {
                e.CellElement.Font = new Font("NotoSansKR-Medium", 12, FontStyle.Bold); // 원하는 폰트 설정
                //e.Column.RowSpan = 3;
                e.CellElement.BackColor = Color.FromArgb(99, 114, 171);
                e.CellElement.ForeColor = Color.White;
            }
            else
            {
                e.CellElement.Font = new Font("NotoSansKR-Regular", 12, FontStyle.Regular); // 원하는 폰트 설정
                e.CellElement.ForeColor = Color.Black;
            }
        }
    }
}