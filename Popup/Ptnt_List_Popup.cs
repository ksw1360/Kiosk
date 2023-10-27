using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Kiosk.Popup
{
    public partial class Ptnt_List_Popup : Form
    {
        internal DataTable dt;
        internal List<string> List = new List<string>();

        public Ptnt_List_Popup()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Ptnt_List_Popup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            if (dt != null)
            {
                radGridView1.DataSource = dt;
            }

            this.radGridView1.Font = new Font("굴림", 22, FontStyle.Regular);
        }

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                /*
                List.Add(radGridView1.Rows[e.RowIndex].Cells["PAT_NM"].Value.ToString());
                List.Add(radGridView1.Rows[e.RowIndex].Cells["PAT_BTH"].Value.ToString());
                List.Add(radGridView1.Rows[e.RowIndex].Cells["MOBILE_NO"].Value.ToString());
                List.Add(radGridView1.Rows[e.RowIndex].Cells["PAT_NO"].Value.ToString());
                */
                GetGridSelectData(e.RowIndex);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void GetGridSelectData(int rowIndex)
        {
            List.Add(radGridView1.Rows[rowIndex].Cells["PAT_NM"].Value.ToString());
            List.Add(radGridView1.Rows[rowIndex].Cells["PAT_BTH"].Value.ToString());
            List.Add(radGridView1.Rows[rowIndex].Cells["MOBILE_NO"].Value.ToString());
            List.Add(radGridView1.Rows[rowIndex].Cells["PAT_NO"].Value.ToString());
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var grid = (RadGridView)sender;
            var row = radGridView1.SelectedRows.FirstOrDefault(); // 선택한 첫 번째 행 가져오기
            if (row != null)
            {
                var rows = row.Index;
                GetGridSelectData(rows);

                DialogResult = DialogResult.OK;
            }
        }
    }
}
