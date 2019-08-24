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
    public partial class 特別科目 : Form
    {
        private SqlConnection _QueryConn = null;   // 共用
        private SqlCommand _QueryComm = null;      // 共用
        private SqlDataReader _QueryDr = null;     // 共用

        private SqlDataAdapter Ad = null;
        private DataSet Ds = new DataSet();
        private DataTable Dt = new DataTable();
        private string BGYM = "";

        public 特別科目()
        {
            InitializeComponent();

            _QueryConn = new SqlConnection(GonGinLibrary.GonGinVariable.SqlConnectString);
            _QueryComm = _QueryConn.CreateCommand();

            if (_QueryConn.State == ConnectionState.Closed) _QueryConn.Open();
        }

        private void Query()
        {
            Ad = new SqlDataAdapter("PUR2007NET_特別科目", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = BGYM;
            Ds.Clear();
            Ad.Fill(Ds);

            // 產生總合，並計算總合
            int totle = 0;
            foreach (DataRow oRow in Ds.Tables[1].Rows)
            {
                totle += Convert.ToInt32(oRow[2].ToString());
            }

            Ds.Tables[1].Rows.Add();

            foreach (DataRow oRow in Ds.Tables[1].Rows)
            {
                if (string.IsNullOrEmpty(oRow["BUGNO"].ToString()))
                {
                    oRow["BUGNO"] = "總合";
                    oRow[2] = totle;
                }
            }

            Ds.Tables[1].AcceptChanges();

            naviDataGridView1.DataSource = Ds.Tables[1].DefaultView;
            naviDataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //foreach (DataRow item in Ds.Tables[0].Rows)
            //{
            //    naviDataGridView1.Columns[item["部門"].ToString()].DefaultCellStyle.Format = "N0";
            //}
            //naviDataGridView1.DefaultCellStyle.Format = "N0";


            // 用數字來判斷開放的位置，可填的就變色
            foreach (DataGridViewRow oRow in naviDataGridView1.Rows)
            {
                if (oRow.Cells["Gv1BUGNO"].Value.ToString() == "總合")
                    oRow.DefaultCellStyle.BackColor = Color.FromArgb(238, 174, 238);
                else
                {
                    foreach (DataRow oRows in Ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(oRow.Cells[oRows["部門"].ToString()].Value.ToString().Trim()))
                            oRow.Cells[oRows["部門"].ToString()].Style.BackColor = Color.FromArgb(180, 238, 180);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            naviDataGridView1.ReadOnly = false;
            toolStripButton1.Visible = false;
            存檔.Visible = true;

            comboBox1.Enabled = false;

            // 用數字來判斷開放的位置，可填的就變色
            foreach (DataGridViewRow oRow in naviDataGridView1.Rows)
            {
                foreach (DataRow oRows in Ds.Tables[0].Rows)
                {
                    if (string.IsNullOrEmpty(oRow.Cells[oRows["部門"].ToString()].Value.ToString().Trim()))
                    {
                        oRow.Cells[oRows["部門"].ToString()].ReadOnly = true;
                    }
                    else
                    {
                        if (oRow.Cells["Gv1BUGNO"].Value.ToString() != "總合")
                            oRow.Cells[oRows["部門"].ToString()].ReadOnly = false;
                        else
                            oRow.Cells[oRows["部門"].ToString()].ReadOnly = true;
                    }
                }
            }
        }

        int YY = 0;
        private void naviDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            switch (Dgv.Columns[e.ColumnIndex].Name)
            {
                case "Gv1BUGNO":
                case "Gv1BUGNA":
                    {
                        Dgv.Rows[e.RowIndex].Cells["Gv1BUGNO"].ReadOnly = true;
                        Dgv.Rows[e.RowIndex].Cells["Gv1BUGNA"].ReadOnly = true;
                    }
                    break;

                default:
                    // 浮動欄位，設定金錢格式
                    e.CellStyle.Format = "N0";
                    //e.CellStyle.Format = "###,###,###.####";
                    break;
            }
        }

        private void 特別科目_Load(object sender, EventArgs e)
        {
            下拉();
            comboBox1.SelectedItem = 1;

            naviDataGridView1.ReadOnly = true;
        }

        private void 下拉()
        {
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
            //comboBox1.SelectedValue = "BGYM";
        }

        private void 存檔_Click(object sender, EventArgs e)
        {
            this.Validate();

            SqlTransaction transaction;
            transaction = _QueryConn.BeginTransaction("Transaction");
            _QueryComm.Transaction = transaction;

            _QueryComm.CommandText = " DELETE BUGDA_固定科目 WHERE BGYM=@BGYM AND KIND='S' ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = BGYM;
            _QueryComm.ExecuteNonQuery();

            //string DEPT = "";
            foreach (DataRow oRow in Ds.Tables[1].Rows)
            {
                foreach (DataColumn OColumn in oRow.Table.Columns)
                {
                    if (OColumn.ToString() != "BUGNO" && OColumn.ToString() != "BUGNA")
                    {
                        string[] sArray = OColumn.ToString().Split('_');

                        if (!string.IsNullOrEmpty(oRow[OColumn.ToString()].ToString().Trim()) && Convert.ToInt32(oRow[OColumn.ToString()].ToString()) > 0)
                        {
                            _QueryComm.CommandText = " INSERT INTO BUGDA_固定科目 (BGDEP,BGYM,BGNO,BGAMT1,CRUSER,CRDATE,KIND) " +
                                                     " VALUES (@BGDEP,@BGYM,@BGNO,@BGAMT1,@CRUSER,GETDATE(),'S') ";
                            _QueryComm.CommandType = CommandType.Text;
                            _QueryComm.Parameters.Clear();
                            _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = sArray[1].ToString();
                            _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = BGYM;
                            _QueryComm.Parameters.Add("@BGNO", SqlDbType.VarChar).Value = oRow["BUGNO"].ToString();
                            _QueryComm.Parameters.Add("@BGAMT1", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow[OColumn.ToString()].ToString());
                            _QueryComm.Parameters.Add("@CRUSER", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                            _QueryComm.ExecuteNonQuery();
                        }
                    }
                }
            }
            transaction.Commit();

            // 計算總合
            int totle = 0;
            foreach (DataRow oRow in Ds.Tables[1].Rows)
            {
                if (oRow["BUGNO"].ToString() != "總合")
                    totle += Convert.ToInt32(oRow[2].ToString());
            }

            foreach (DataRow oRow in Ds.Tables[1].Rows)
            {
                if (oRow["BUGNO"].ToString() == "總合")
                    oRow[2] = totle;
            }


            toolStripButton1.Visible = true;
            存檔.Visible = false;
            naviDataGridView1.ReadOnly = true;
            MessageBox.Show("存檔成功！！");
            comboBox1.Enabled = true;

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "System.Data.DataRowView")
            {
                naviTextBox3.Text = comboBox1.Text;
                BGYM = naviTextBox3.Text;
                Query();
                label1.Text = "預算月份：" + BGYM;
            }
        }

        int X = 1;
        private void naviDataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;



            //Dgv.Rows[e.RowIndex].Cells["Gv1BUGNO"]

            //    i.ToString("#,0");

            //this.Text = (X + 1).ToString();
        }
    }
}
