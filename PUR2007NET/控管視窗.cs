using GonGinLibrary;
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
    public partial class 控管視窗 : Form
    {
        private DataTable Dt_date = new DataTable();
        private SqlConnection _QueryConn = null;   // 共用
        private SqlCommand _QueryComm = null;      // 共用
        private SqlDataReader _QueryDr = null;     // 共用
        private string 開啟項目 = string.Empty;
        private string 部門字串 = string.Empty;

        public 控管視窗()
        {
            InitializeComponent();

            _QueryConn = new SqlConnection(GonGinVariable.SqlConnectString);
            _QueryComm = _QueryConn.CreateCommand();

            if (_QueryConn.State == ConnectionState.Closed) _QueryConn.Open();           
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            //Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Dt_date.AcceptChanges();

            #region
            // 多筆部門串起，distinct後在串起
            //DataTable AA = new DataTable();
            //AA.Columns.Add("部門");

            //int k = 0;
            //foreach (DataRow item in Dt_date.Select("SEL='True'"))
            //{
            //    string[] sArray = item["預算部門"].ToString().Split(';');
            //    foreach (string i in sArray)
            //    {
            //        if (!string.IsNullOrEmpty(i.Trim()))
            //        {
            //            AA.Rows.Add();
            //            AA.Rows[k]["部門"] = i;
            //            k++;
            //        }
            //    }
            //}
            //AA = AA.DefaultView.ToTable(true, "部門");
            #endregion


            int p = 0;
            string 狀態 = string.Empty;
            string 部門字串 = string.Empty;
            // 把多筆的部門串起來，p判斷預算申請是否有不一樣
            foreach (DataRow item in Dt_date.Select("SEL='True'"))
            {
                狀態 = Dt_date.Select("SEL='True'")[0]["預算申請"].ToString();
                if (狀態 != item["預算申請"].ToString())
                    p++;

                部門字串 += item["預算部門"].ToString();
            }

            // 大於0表示有兩筆以上不同的預算申請，直接帶空值
            if (p > 0)
                狀態 = "";

            部門選項 pop = new 部門選項(狀態, 部門字串);
            if (pop.ShowDialog() == DialogResult.Yes)
            {
                foreach (DataRow oRow in Dt_date.Select("SEL='True'"))
                {
                    oRow["預算申請"] = pop.開啟項目;
                    oRow["預算部門"] = pop.部門字串;
                }
            }
            naviDataGridView1.DataSource = Dt_date.DefaultView;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Validate();
            Dt_date.AcceptChanges();

            string OPEN = "";
            if (checkBox1.Checked)
                OPEN = "Y";
            

            SqlTransaction transaction;
            transaction = _QueryConn.BeginTransaction("Transaction");
            _QueryComm.Transaction = transaction;

            foreach (DataRow oRow in Dt_date.Rows)  // 逐筆處理明細檔資料
            {
                _QueryComm.CommandText = " UPDATE PEN007 SET 預算申請=@預算申請,預算部門=@預算部門 WHERE FULLDATE = @FULLDATE ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@預算申請", SqlDbType.VarChar).Value = oRow["預算申請"].ToString();
                _QueryComm.Parameters.Add("@預算部門", SqlDbType.VarChar).Value = oRow["預算部門"].ToString();
                _QueryComm.Parameters.Add("@FULLDATE", SqlDbType.VarChar).Value = oRow["FULLDATE"].ToString();
                _QueryComm.ExecuteNonQuery();
            }

            // 更改AMDCONFIG，開啟或關閉
            _QueryComm.CommandText = " UPDATE AMDCONFIG SET FTYPE = @FTYPE WHERE FID = '574' ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@FTYPE", SqlDbType.VarChar).Value = OPEN;
            _QueryComm.ExecuteNonQuery();

            // 更改AMDCONFIG，緊急公告內容
            _QueryComm.CommandText = " UPDATE AMDCONFIG SET FVALUE = @FVALUE WHERE FID = '574' ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@FVALUE", SqlDbType.VarChar).Value = naviTextBox1.Text;
            _QueryComm.ExecuteNonQuery();


            transaction.Commit();
            MessageBox.Show("開啟成功!! ", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        private void naviDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            switch (Dgv.Columns[e.ColumnIndex].Name)
            {
                case "部門鈕":
                    {
                        Dt_date.AcceptChanges();

                        部門選項 pop = new 部門選項(Dgv.Rows[e.RowIndex].Cells["預算申請"].Value.ToString(), Dgv.Rows[e.RowIndex].Cells["預算部門"].Value.ToString());
                        if (pop.ShowDialog() == DialogResult.Yes)
                        {
                            Dgv.CurrentRow.Cells["預算申請"].Value = pop.開啟項目;
                            Dgv.CurrentRow.Cells["預算部門"].Value = pop.部門字串;
                        }
                        naviDataGridView1.DataSource = Dt_date.DefaultView;
                    }
                    break;
            }
        }

        private void naviDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            switch (Dgv.Columns[e.ColumnIndex].Name)
            {
                case "部門鈕":
                    {
                        Dgv.Rows[e.RowIndex].Cells["部門鈕"].Value = "修改";
                        Dgv.Rows[e.RowIndex].Cells["部門鈕"].Style.BackColor = Color.FromArgb(102, 205, 170);
                    }
                    break;
            }
        }

        private void 資料更新_Click(object sender, EventArgs e)
        {
            控管視窗_Load(null,null);
        }

        private void 控管視窗_Load(object sender, EventArgs e)
        {
            SqlDataAdapter Ad = new SqlDataAdapter(" SELECT SEL=CAST(0 AS BIT),FULLDATE=CONVERT(varchar(100), FULLDATE, 111),預算申請,預算部門 FROM PEN007 " + 
                                                   " WHERE PEN007001 = '1' AND PEN007006 = '0' AND FULLDATE BETWEEN CONVERT(varchar(100), GETDATE(), 111) AND CONVERT(varchar(100), GETDATE() + 14, 111) " + 
                                                   " ORDER BY FULLDATE  ", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.Text;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.CommandTimeout = 600;
            Dt_date.Clear();
            Ad.Fill(Dt_date);

            GonGinCheckOfDataDuplication 公告 = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString, "AMDCONFIG", "FTYPE", "FVALUE", "FVALUE", " FID = '574' ");
            if (string.IsNullOrEmpty(公告.傳回值.Trim()))
                checkBox1.Checked = false;
            else
                checkBox1.Checked = true;

            naviTextBox1.Text = 公告.傳回值二;
            
            naviDataGridView1.DataSource = Dt_date.DefaultView;
        }
    }
}
