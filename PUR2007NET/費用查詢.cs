using GonGinLibrary;
using PUR2007NET.費用查詢報表;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PUR2007NET
{
    public partial class 費用查詢 : Form
    {
        private SqlDataAdapter Ad = null;
        private DataSet Ds_預算使用統計 = new DataSet();
        private DataTable Dt頭檔 = new DataTable();
        private DataSet Ds明細 = new DataSet();
        private DataTable Dt明細_S = new DataTable();
        private DataTable Dt明細_D = new DataTable();

        private DataTable Dt明細_費用 = new DataTable();

        private string 科目 = "";
        private string 科目名 = "";
        public 費用查詢()
        {
            InitializeComponent();
        }

        private void 費用查詢_Load(object sender, EventArgs e)
        {
            if (GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "93")
                toolStripButton1.Visible = true;

            DataTable 下拉 = new DataTable();

            SqlDataAdapter Ad = new SqlDataAdapter(" SELECT DISTINCT BGYM FROM BUGDA_簽核明細 WHERE BGYM > 201809 ORDER BY BGYM DESC ", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.Text;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.CommandTimeout = 600;
            下拉.Clear();
            Ad.Fill(下拉);

            comboBox1.DataSource = 下拉;
            comboBox1.ValueMember = "BGYM";
            comboBox1.DisplayMember = "BGYM";
            月份起.Text = comboBox1.SelectedValue.ToString();

            Query();



        }

        private void Query()
        {
            // 前一個月
            //string 前一個月 = string.Empty;
            //if (月份起.Text.Substring(4, 2) == "01")
            //    前一個月 = (Convert.ToInt32(月份起.Text.Substring(0, 4)) - 1).ToString() + "12";

            //else
            //    前一個月 = 月份起.Text.Substring(0, 4) + (Convert.ToInt32(月份起.Text.Substring(4, 2)) - 1).ToString();

            //if (前一個月.Length == 5)
            //    前一個月 = 月份起.Text.Substring(0, 4) + "0" + (Convert.ToInt32(月份起.Text.Substring(4, 2)) - 1).ToString();


            // 實際報表
            Ad = new SqlDataAdapter("PUR_預算使用統計", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = 月份起.Text;
            Ad.SelectCommand.Parameters.Add("@OPTION", SqlDbType.VarChar).Value = "";
            Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = "Y";
            Ds_預算使用統計.Clear();
            Ad.Fill(Ds_預算使用統計);

            Dt頭檔.Clear();
            Dt頭檔 = Ds_預算使用統計.Tables[0].Copy();
            Dt頭檔.Columns.Add("S", typeof(String));


            // 實際明細
            Ad = new SqlDataAdapter("PUR2007NET_費用查詢", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = 月份起.Text;
            Ad.SelectCommand.Parameters.Add("@OPTION", SqlDbType.VarChar).Value = "";
            Ds明細.Clear();
            Ad.Fill(Ds明細);



            foreach (DataRow oRow in Dt頭檔.Rows)
            {
                if (Ds明細.Tables[0].Select("BUGNO = '" + oRow["BUGNO"].ToString() + "'").Count() > 0)
                    oRow["S"] = 'Y';
            }
            Dt頭檔.AcceptChanges();

            naviDataGridView1.DataSource = Dt頭檔.DefaultView;
        }

        private void naviDataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            //Dt明細_S = new DataTable();

            Dt明細_S.Clear();

            if (Ds明細.Tables[0].Select("BUGNO = '" + Dgv.Rows[e.RowIndex].Cells["Gv1BUGNO"].Value.ToString() + "'").Count() > 0)
                Dt明細_S = Ds明細.Tables[0].Select("BUGNO = '" + Dgv.Rows[e.RowIndex].Cells["Gv1BUGNO"].Value.ToString() + "'").CopyToDataTable();

            naviDataGridView2.DataSource = Dt明細_S.DefaultView;

            科目 = Dgv.Rows[e.RowIndex].Cells["Gv1BUGNO"].Value.ToString();
            科目名 = Dgv.Rows[e.RowIndex].Cells["Gv1BUGNA"].Value.ToString();

            if (Ds明細.Tables[2].Select("BUGNO = '" + Dgv.Rows[e.RowIndex].Cells["Gv1BUGNO"].Value.ToString() + "'").Count() > 0)
                Dt明細_費用 = Ds明細.Tables[2].Select("BUGNO = '" + Dgv.Rows[e.RowIndex].Cells["Gv1BUGNO"].Value.ToString() + "'").CopyToDataTable();
        }

        private void naviDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            if (Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv1合計"].Value) > 0 && !Dgv.Rows[e.RowIndex].Cells["Gv1BUGNO"].Value.ToString().Equals("999") && Dgv.Rows[e.RowIndex].Cells["Gv1S"].Value.ToString().Equals("Y"))
                Dgv.Rows[e.RowIndex].Cells["Gv1合計"].Style.BackColor = Color.FromArgb(180, 238, 180);

            if (Dgv.Rows[e.RowIndex].Cells["Gv1BUGNO"].Value.ToString().Equals("999"))
                Dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(135, 206, 250);
        }

        private void naviDataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            if (Dgv.Rows[e.RowIndex].Cells["Gv2FLAG"].Value.ToString().Equals("合計"))
            {
                Dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(135, 206, 250);
            }

            Dgv.Rows[e.RowIndex].Cells["Gv2INVAMT"].Style.BackColor = Color.FromArgb(180, 238, 180);

        }

        private void 查詢_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            月份起.Text = comboBox1.Text;
        }

        private void naviDataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (naviDataGridView2.SelectedCells.Count > 1)
                naviStatusStrip1.StatusOfSum = naviDataGridView2.儲存格合計.ToString();
        }

        private void naviDataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (naviDataGridView1.SelectedCells.Count > 1)
                naviStatusStrip1.StatusOfSum = naviDataGridView1.儲存格合計.ToString();
        }

        private void naviDataGridView3_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (naviDataGridView3.SelectedCells.Count > 1)
                naviStatusStrip1.StatusOfSum = naviDataGridView3.儲存格合計.ToString();
        }

        private void naviDataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            Dt明細_D.Clear();

            Ds明細.Tables[1].CaseSensitive = true;

            if (Ds明細.Tables[1].Select("FLAG = '" + Dgv.Rows[e.RowIndex].Cells["Gv2FLAG"].Value.ToString() +
                                                   "' AND BUGNO = '" + Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString() +
                                                   "' AND FTNA = '" + Dgv.Rows[e.RowIndex].Cells["Gv2FTNA"].Value.ToString() +
                                                   "' AND FMNA = '" + Dgv.Rows[e.RowIndex].Cells["Gv2FMNA"].Value.ToString() +
                                                   "' AND INCDS2 = '" + Dgv.Rows[e.RowIndex].Cells["Gv2INCDS2"].Value.ToString() +
                                                   "' AND DEPTNAME = '" + Dgv.Rows[e.RowIndex].Cells["Gv2DEPTNAME"].Value.ToString() + "'").Count() > 0)
                Dt明細_D = Ds明細.Tables[1].Select("FLAG = '" + Dgv.Rows[e.RowIndex].Cells["Gv2FLAG"].Value.ToString() +
                                                   "' AND BUGNO = '" + Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString() +
                                                   "' AND FTNA = '" + Dgv.Rows[e.RowIndex].Cells["Gv2FTNA"].Value.ToString() +
                                                   "' AND FMNA = '" + Dgv.Rows[e.RowIndex].Cells["Gv2FMNA"].Value.ToString() +
                                                   "' AND INCDS2 = '" + Dgv.Rows[e.RowIndex].Cells["Gv2INCDS2"].Value.ToString() +
                                                   "' AND DEPTNAME = '" + Dgv.Rows[e.RowIndex].Cells["Gv2DEPTNAME"].Value.ToString() + "'").CopyToDataTable();

            naviDataGridView3.DataSource = Dt明細_D.DefaultView;

        }

        private void naviDataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            Dgv.Rows[e.RowIndex].Cells["Gv3INVAMT"].Style.BackColor = Color.FromArgb(173, 216, 230);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Dt明細_S.Rows.Count == 0)
                return;

            string 項目 = 月份起.Text.Substring(0,4) +"年"+ 月份起.Text.Substring(4, 2) +"月" + " " + 科目 + " " + 科目名 + "_品項總表";

            費用查詢01 Reporter1 = new 費用查詢01();
            Reporter1.Database.Tables[0].SetDataSource(Dt明細_費用);

            Form ReportForm = new Form();
            CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            CryReport.ReportSource = Reporter1;
            CryReport.Dock = DockStyle.Fill;
            Reporter1.ParameterFields["項目"].CurrentValues.AddValue(項目);
            Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
            Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
            ReportForm.Controls.Add(CryReport);
            ReportForm.WindowState = FormWindowState.Maximized;
            ReportForm.Show();
        }
    }
}
