using GonGinLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUR2007NET
{
    public partial class BUGDA_修改 : Form
    {
        private SqlDataAdapter Ad = null;
        private SqlDataAdapter Ad1 = null;
        private SqlDataAdapter Ad_D = null;
        private DataTable _明細 = new DataTable();
        private DataTable Dt_品項 = new DataTable();
        private DataTable 更新_品項 = new DataTable();
        private SqlConnection _QueryConn = null;   // 共用
        private SqlCommand _QueryComm = null;      // 共用
        private SqlDataReader _QueryDr = null;     // 共用
        private string _項目 = string.Empty;
        private string _單據單號 = string.Empty;
        private string _日期 = string.Empty;
        private string _部門 = string.Empty;
        private string _急件 = string.Empty;

        public BUGDA_修改(DataTable Dt_明細, string 項目, string 單據單號, string 日期, string 部門, string 急要件)
        {
            _明細 = Dt_明細;
            _項目 = 項目;
            _單據單號 = 單據單號;
            _日期 = 日期;
            _部門 = 部門;
            _急件 = 急要件;
            InitializeComponent();

            _QueryConn = new SqlConnection(GonGinVariable.SqlConnectString);
            _QueryComm = _QueryConn.CreateCommand();

            if (_QueryConn.State == ConnectionState.Closed) _QueryConn.Open();

            naviDataGridView1.DataSource = _明細.DefaultView;
            //_明細.Columns.Add("判斷", typeof(String));
            label1.Text = "申請項目：" + "(" + 項目 + ")";

            if (_急件 == "Y")
                急件.Checked = true;
            else
                一般.Checked = true;

            // 新增計算合計明細的Row
            if (_明細.Rows.Count > 0)
            {
                _明細.Rows.Add();
                int 預算總和 = 0;
                int 追加總和 = 0;
                int 追加後預算總和 = 0;

                foreach (DataRow oRow in _明細.Rows)
                {
                    if (!string.IsNullOrEmpty(oRow["UID"].ToString().Trim()))
                    {
                        預算總和 += Convert.ToInt32(oRow["BGAMT1"].ToString());
                        追加總和 += Convert.ToInt32(oRow["BGBMT2"].ToString());
                        追加後預算總和 += Convert.ToInt32(oRow["追加後預算"].ToString());
                    }
                    else
                    {
                        oRow["BGDEPNAME"] = "合計";
                        oRow["BGAMT1"] = 預算總和;
                        oRow["BGBMT2"] = 追加總和;
                        oRow["追加後預算"] = 追加後預算總和;
                    }
                }
            }
        }

        private void 存檔_Click(object sender, EventArgs e)
        {
            this.Validate();
            Dt_品項.AcceptChanges();
            SqlTransaction transaction;
            transaction = _QueryConn.BeginTransaction("Transaction");
            _QueryComm.Transaction = transaction;

            toolStrip1.Focus();

            if (一般.Checked)
                _急件 = "";
            else if (急件.Checked)
                _急件 = "Y";

            string 步驟 = string.Empty;
            步驟 = "修改";


            foreach (DataRow item in _明細.Rows)
            {
                if (!string.IsNullOrEmpty(item["UID"].ToString()))
                    item["追加後預算"] = (Convert.ToInt32(item["BGAMT1"].ToString()) + Convert.ToInt32(item["BGBMT2"].ToString())).ToString();
            }
            _明細.AcceptChanges();

            //try
            //{
            // 頭檔
            _QueryComm.CommandText = " UPDATE BUGDA_簽核 SET 急件=@急件,AMDUSR=@AMDUSR,AMDDAY=GETDATE() WHERE 單據單號=@單據單號";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = _單據單號;
            _QueryComm.Parameters.Add("@AMDUSR", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            _QueryComm.Parameters.Add("@急件", SqlDbType.VarChar).Value = _急件;
            _QueryComm.ExecuteNonQuery();

            foreach (DataRow oRow in _明細.Rows)  // 逐筆處理明細檔資料
            {
                //if (oRow.RowState == DataRowState.Modified)
                //{
                _QueryComm.CommandText = " UPDATE BUGDA_簽核明細 SET BGAMT1=@BGAMT1,BGBMT2=@BGBMT2,BGREM=@BGREM,AMDUSR=@AMDUSR,AMDDAY=GETDATE(),追加後預算 = @追加後預算,BGTYPE=@BGTYPE " +
                                         " WHERE 單據單號=@單據單號 AND BGDEP=@BGDEP AND BUGNO=@BUGNO AND BGYM=@BGYM";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = _單據單號;
                _QueryComm.Parameters.Add("@BUGNO", SqlDbType.VarChar).Value = oRow["BUGNO"].ToString();
                _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = oRow["BGYM"].ToString();
                _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = oRow["BGDEP"].ToString();
                //_QueryComm.Parameters.Add("@SHOWTP", SqlDbType.VarChar).Value = oRow["SHOWTP"].ToString();
                if (string.IsNullOrEmpty(oRow["BGAMT1"].ToString().Trim()))
                    _QueryComm.Parameters.Add("@BGAMT1", SqlDbType.Decimal).Value = DBNull.Value;
                else
                    _QueryComm.Parameters.Add("@BGAMT1", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["BGAMT1"].ToString());
                if (string.IsNullOrEmpty(oRow["BGBMT2"].ToString().Trim()))
                    _QueryComm.Parameters.Add("@BGBMT2", SqlDbType.Decimal).Value = DBNull.Value;
                else
                    _QueryComm.Parameters.Add("@BGBMT2", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["BGBMT2"].ToString());
                //if (string.IsNullOrEmpty(oRow["BGCMT3"].ToString().Trim()))
                //    _QueryComm.Parameters.Add("@BGCMT3", SqlDbType.Decimal).Value = DBNull.Value;
                //else
                //    _QueryComm.Parameters.Add("@BGCMT3", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["BGCMT3"].ToString());
                _QueryComm.Parameters.Add("@AMDUSR", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                _QueryComm.Parameters.Add("@BGREM", SqlDbType.VarChar).Value = oRow["BGREM"].ToString();
                if (string.IsNullOrEmpty(oRow["追加後預算"].ToString().Trim()))
                    _QueryComm.Parameters.Add("@追加後預算", SqlDbType.Decimal).Value = DBNull.Value;
                else
                    _QueryComm.Parameters.Add("@追加後預算", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["追加後預算"].ToString());
                _QueryComm.Parameters.Add("@BGTYPE", SqlDbType.VarChar).Value = _項目;
                _QueryComm.ExecuteNonQuery();
            }
            //}

            _QueryComm.CommandText = " DELETE BUGDA_簽核品項 WHERE 單據單號=@單據單號 ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = _單據單號;
            _QueryComm.ExecuteNonQuery();

            foreach (DataRow oRow in Dt_品項.Rows)
            {
                _QueryComm.CommandText = " INSERT INTO BUGDA_簽核品項 (UID,FDWG,FCDS,BUGNO,數量,金額,CRUSER,CRDATE,FSIZ,FSMT,REMARK,單據單號,NUM) VALUES " +
                                         " (@UID,@FDWG,N'" + oRow["FCDS"].ToString() + "',@BUGNO,@數量,@金額,@CRUSER,GETDATE(),@FSIZ,@FSMT,@REMARK,@單據單號,@NUM) ";
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(oRow["UID"].ToString());
                _QueryComm.Parameters.Add("@CRUSER", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                _QueryComm.Parameters.Add("@FDWG", SqlDbType.VarChar).Value = oRow["FDWG"].ToString();
                //_QueryComm.Parameters.Add("@FCDS", SqlDbType.VarChar).Value = oRow["FCDS"].ToString();
                //_QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = oRow["BGDEP"].ToString();
                //_QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = oRow["BGYM"].ToString();
                _QueryComm.Parameters.Add("@BUGNO", SqlDbType.VarChar).Value = oRow["BUGNO"].ToString();
                _QueryComm.Parameters.Add("@數量", SqlDbType.Int).Value = Convert.ToInt32(oRow["數量"].ToString());
                _QueryComm.Parameters.Add("@金額", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["金額"].ToString());
                //_QueryComm.Parameters.Add("@BGTYPE", SqlDbType.VarChar).Value = _項目;
                _QueryComm.Parameters.Add("@FSIZ", SqlDbType.VarChar).Value = oRow["FSIZ"].ToString();
                _QueryComm.Parameters.Add("@FSMT", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["FSMT"].ToString());
                _QueryComm.Parameters.Add("@REMARK", SqlDbType.VarChar).Value = oRow["REMARK"].ToString();
                _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = _單據單號;
                _QueryComm.Parameters.Add("@NUM", SqlDbType.Int).Value = Convert.ToInt32(oRow["NUM"].ToString());
                _QueryComm.ExecuteNonQuery();
            }

            transaction.Commit();
            this.DialogResult = DialogResult.Yes;
            MessageBox.Show("存檔成功!!");
            Close();
            //}
            //catch (Exception ex)
            //{
            //    transaction.Rollback();
            //    MessageBox.Show(步驟 + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("已確定存檔過，是否關閉 ?", "核准訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Close();
        }

        private void BUGDA_修改_Load(object sender, EventArgs e)
        {
            string 上階部門 = string.Empty;
            // 上階部門
            _QueryComm.CommandText = " SELECT GRDEPT1 = (SELECT GRDEPT1 FROM AMDDEPT B WHERE A.UPDEPT = B.DEPTNO) FROM AMDDEPT A WHERE DEPTNO = @DEPTNO ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUserDeptNo;
            _QueryDr = _QueryComm.ExecuteReader();

            if (_QueryDr.HasRows)
            {
                _QueryDr.Read();
                上階部門 = _QueryDr["GRDEPT1"].ToString();
            }
            _QueryDr.Close();

            naviDataGridView1.Columns["Gv2BGDEP"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BGYM"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BUGNO"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BUGNA"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BGACNO"].ReadOnly = true;
            //naviDataGridView1.Columns["Gv2BGREM"].ReadOnly = true;
            //naviDataGridView1.Columns["Gv2BGCMT1"].ReadOnly = true;

            //if (_項目 == "預算")
            naviDataGridView1.Columns["Gv2BGBMT2"].ReadOnly = true;
            //else if (_項目 == "追加")
            naviDataGridView1.Columns["Gv2BGAMT1"].ReadOnly = true;
            //if (_項目 == "挪用")
            naviDataGridView1.Columns["Gv2BGCMT3"].Visible = true;


            DataTable Dt_部門 = new DataTable();
            Ad = new SqlDataAdapter("SELECT 部門,BUGNO FROM BUGNA_部門 WHERE 部門 = @部門", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.Text;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.Parameters.Add("@部門", SqlDbType.VarChar).Value = 上階部門;
            Dt_部門.Clear();
            Ad.Fill(Dt_部門);

            // 判斷品項Item
            //DataTable Dt_品項 = new DataTable();
            SqlDataAdapter Ad_D = new SqlDataAdapter(" SELECT BUGNO FROM BUGDA_ITEM GROUP BY BUGNO ", GonGinVariable.SqlConnectString);
            Ad_D.SelectCommand.CommandType = CommandType.Text;
            Ad_D.SelectCommand.Parameters.Clear();
            Ad_D.SelectCommand.CommandTimeout = 600;
            Dt_品項.Clear();
            Ad_D.Fill(Dt_品項);


            string 特殊部門判斷 = string.Empty;
            // 上階部門
            _QueryComm.CommandText = " SELECT GRDEPT1 = (SELECT GRDEPT1 FROM AMDDEPT B WHERE A.UPDEPT = B.DEPTNO) FROM AMDDEPT A WHERE DEPTNO = @DEPTNO ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = _部門;
            _QueryDr = _QueryComm.ExecuteReader();

            if (_QueryDr.HasRows)
            {
                _QueryDr.Read();
                特殊部門判斷 = _QueryDr["GRDEPT1"].ToString();
            }
            _QueryDr.Close();


            // 由於財務帶過來是整張表，所以用Dt_部門來判斷該可以填的科目
            foreach (DataGridViewRow oRow in naviDataGridView1.Rows)
            {
                if (!string.IsNullOrEmpty(oRow.Cells["Gv2UID"].Value.ToString().Trim()))
                {
                    oRow.ReadOnly = true;
                    oRow.Cells["品項"].Style.BackColor = Color.FromArgb(100, 149, 237);
                    oRow.Cells["品項"].Value = "品項";
                    oRow.Cells["品項"].ReadOnly = false;
                }
                else
                    oRow.Cells["品項"].Value = "";

                // 欄位變色
                if (_項目 == "預算")
                    oRow.Cells["Gv2BGAMT1"].Style.BackColor = Color.FromArgb(180, 238, 180);
                else if (_項目 == "追加")
                    oRow.Cells["Gv2BGBMT2"].Style.BackColor = Color.FromArgb(180, 238, 180);


                // 瓊華、美鋒、陳美淑、李蓉姍
                //oRow.Cells["Gv2BGDEP"].Value.ToString()



                #region 不知道的規則
                //foreach (DataRow item in Dt_品項.Rows)
                //{
                //    // 有在部門&品項裡面
                //    if (
                //        Dt_部門.Select("BUGNO = '" + oRow.Cells["Gv2BUGNO"].Value.ToString() + "'").Count() > 0 &&
                //        !string.IsNullOrEmpty(oRow.Cells["Gv2UID"].Value.ToString()))
                //    {
                //        if (_項目 == "預算")
                //            oRow.Cells["Gv2BGAMT1"].Style.BackColor = Color.FromArgb(180, 238, 180);
                //        else if (_項目 == "追加")
                //            oRow.Cells["Gv2BGBMT2"].Style.BackColor = Color.FromArgb(180, 238, 180);

                //        oRow.Cells["品項"].Style.BackColor = Color.FromArgb(100, 149, 237);
                //        oRow.Cells["品項"].Value = "品項";
                //        oRow.Cells["品項"].ReadOnly = false;
                //    }
                //    // 有在部門沒在品項
                //    else if (
                //             Dt_部門.Select("BUGNO = '" + oRow.Cells["Gv2BUGNO"].Value.ToString() + "'").Count() > 0 &&
                //             !string.IsNullOrEmpty(oRow.Cells["Gv2UID"].Value.ToString()))
                //    {
                //        // 例外科目25A1、31A1
                //        if (oRow.Cells["Gv2BUGNO"].Value.ToString() == "25A1" || oRow.Cells["Gv2BUGNO"].Value.ToString() == "31A1")
                //        {
                //            if (_項目 == "預算")
                //                oRow.Cells["Gv2BGAMT1"].Style.BackColor = Color.FromArgb(180, 238, 180);
                //            else if (_項目 == "追加")
                //                oRow.Cells["Gv2BGBMT2"].Style.BackColor = Color.FromArgb(180, 238, 180);

                //            oRow.Cells["品項"].Style.BackColor = Color.FromArgb(100, 149, 237);
                //            oRow.Cells["品項"].Value = "品項";
                //            oRow.Cells["品項"].ReadOnly = false;
                //        }
                //        else
                //        {
                //            if (_項目 == "預算")
                //            {
                //                oRow.Cells["Gv2BGAMT1"].Style.BackColor = Color.FromArgb(180, 238, 180);
                //                oRow.Cells["Gv2BGAMT1"].ReadOnly = false;
                //            }
                //            else if (_項目 == "追加")
                //            {
                //                oRow.Cells["Gv2BGBMT2"].Style.BackColor = Color.FromArgb(180, 238, 180);
                //                oRow.Cells["Gv2BGBMT2"].ReadOnly = false;
                //            }
                //            oRow.Cells["品項"].Style.BackColor = Color.White;
                //            oRow.Cells["品項"].Value = "";
                //            oRow.Cells["品項"].ReadOnly = true;
                //        }
                //    }
                //    else
                //    {
                //        oRow.Cells["品項"].Style.BackColor = Color.White;
                //        oRow.Cells["品項"].Value = "";
                //        oRow.Cells["品項"].ReadOnly = true;
                //    }
                //}

                //// 修繕費
                //if (oRow.Cells["Gv2BUGNO"].Value.ToString() == "33")
                //{
                //    oRow.Cells["品項"].Style.BackColor = Color.FromArgb(100, 149, 237);
                //    oRow.Cells["品項"].Value = "品項";
                //    oRow.Cells["品項"].ReadOnly = false;

                //    if (_項目 == "預算")
                //    {
                //        oRow.Cells["Gv2BGAMT1"].Style.BackColor = Color.FromArgb(180, 238, 180);
                //        oRow.Cells["Gv2BGAMT1"].ReadOnly = true;
                //    }
                //    else if (_項目 == "追加")
                //    {
                //        oRow.Cells["Gv2BGBMT2"].Style.BackColor = Color.FromArgb(180, 238, 180);
                //        oRow.Cells["Gv2BGBMT2"].ReadOnly = true;
                //    }
                //}
                //// 燃料費
                //if (oRow.Cells["Gv2BUGNO"].Value.ToString() == "34" && oRow.Cells["Gv2BGDEP"].Value.ToString() == "8320")
                //{
                //    oRow.Cells["品項"].Style.BackColor = Color.FromArgb(100, 149, 237);
                //    oRow.Cells["品項"].Value = "品項";
                //    oRow.Cells["品項"].ReadOnly = false;

                //    if (_項目 == "預算")
                //    {
                //        oRow.Cells["Gv2BGAMT1"].Style.BackColor = Color.FromArgb(180, 238, 180);
                //        oRow.Cells["Gv2BGAMT1"].ReadOnly = true;
                //    }
                //    else if (_項目 == "追加")
                //    {
                //        oRow.Cells["Gv2BGBMT2"].Style.BackColor = Color.FromArgb(180, 238, 180);
                //        oRow.Cells["Gv2BGBMT2"].ReadOnly = true;
                //    }
                //}
                #endregion
            }

            SqlDataAdapter Ad1 = new SqlDataAdapter(" SELECT UID,FDWG,FCDS,FSIZ,BGDEP,BGYM,BUGNO,BGTYPE,數量,FSMT,金額,REMARK,NUM,CRUSER,CRDATE FROM BUGDA_簽核品項 WHERE UID IN (SELECT UID FROM BUGDA_簽核明細 WHERE 單據單號 = @單據單號) ORDER BY BUGNO,NUM ", GonGinVariable.SqlConnectString);
            Ad1.SelectCommand.CommandType = CommandType.Text;
            Ad1.SelectCommand.Parameters.Clear();
            Ad1.SelectCommand.CommandTimeout = 600;
            Ad1.SelectCommand.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = _單據單號;
            Dt_品項.Clear();
            Ad1.Fill(Dt_品項);
        }

        private void naviDataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            if (Dgv.Columns[e.ColumnIndex].Name.Equals("Gv2BGBMT2"))
            {
                int BGAMT1 = 0;
                if (!string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString()))
                    BGAMT1 = Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString());

                Dgv.Rows[e.RowIndex].Cells["追加後預算"].Value = BGAMT1 + Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString());
            }

            if (Dgv.Columns[e.ColumnIndex].Name.Equals("Gv2BGBMT2"))
            {
                int BGAMT1 = 0;
                if (!string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString()))
                    BGAMT1 = Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString());

                Dgv.Rows[e.RowIndex].Cells["追加後預算"].Value = BGAMT1 + Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString());
            }

            //BGAMT1==================================================================================================
            if (Dgv.Columns[e.ColumnIndex].Name.Equals("Gv2BGAMT1"))
            {
                int BGAMT1 = 0;
                if (!string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString()))
                    BGAMT1 = Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString());

                Dgv.Rows[e.RowIndex].Cells["追加後預算"].Value = BGAMT1 + Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString());
            }

            if (Dgv.Columns[e.ColumnIndex].Name.Equals("Gv2BGAMT1"))
            {
                int BGAMT1 = 0;
                if (!string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString()))
                    BGAMT1 = Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString());

                Dgv.Rows[e.RowIndex].Cells["追加後預算"].Value = BGAMT1 + Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString());
            }
        }

        private void naviDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;


            switch (Dgv.Columns[e.ColumnIndex].Name)
            {
                case "Gv2BGDEPNAME":
                    {
                        if (Dgv.Rows[e.RowIndex].Cells["Gv2BGDEPNAME"].Value.ToString() == "合計")
                        {
                            Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Style.BackColor = Color.FromArgb(238, 174, 238);
                            Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Style.BackColor = Color.FromArgb(238, 174, 238);
                            Dgv.Rows[e.RowIndex].Cells["追加後預算"].Style.BackColor = Color.FromArgb(238, 174, 238);
                        }
                    }
                    break;
            }

        }

        private void naviDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            switch (Dgv.Columns[e.ColumnIndex].Name)
            {
                case "品項":
                    {
                        if (Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString() == "32A" && GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) != "93")
                        {
                            // select DISTINCT 主管助理 =
                            //(SELECT cast(主管助理 AS NVARCHAR) + '  ' from(SELECT 主管助理 = CONCAT(DEPTSUPR + ';',
                            //CASE ISNULL(DEPTSUPR1, '') WHEN '' THEN '' ELSE DEPTSUPR1 + ';' END,
                            //CASE ISNULL(DEPTSUPR2, '') WHEN '' THEN '' ELSE DEPTSUPR2 + ';' END,
                            //CASE ISNULL(DEPTSUPR3, '') WHEN '' THEN '' ELSE DEPTSUPR3 + ';' END,
                            //CASE ISNULL(DEPTSUPR4, '') WHEN '' THEN '' ELSE DEPTSUPR4  END) FROM AMDDEPT WHERE DEPTNO IN('9200', '7140', '9211')) A

                            //FOR XML PATH('')) from(SELECT 主管助理 = CONCAT(DEPTSUPR + ';',
                            //CASE ISNULL(DEPTSUPR1, '') WHEN '' THEN '' ELSE DEPTSUPR1 + ';' END,
                            //CASE ISNULL(DEPTSUPR2, '') WHEN '' THEN '' ELSE DEPTSUPR2 + ';' END,
                            //CASE ISNULL(DEPTSUPR3, '') WHEN '' THEN '' ELSE DEPTSUPR3 + ';' END,
                            //CASE ISNULL(DEPTSUPR4, '') WHEN '' THEN '' ELSE DEPTSUPR4  END) FROM AMDDEPT WHERE DEPTNO IN('9200', '7140', '9211')) A
                            

                            string 主管助理 = string.Empty;
                            _QueryComm.CommandText = " select DISTINCT 主管助理 = " +
                                                     " (SELECT cast(主管助理 AS NVARCHAR) + '  ' from(SELECT 主管助理 = ISNULL(DEPTSUPR, '') + ';' + " +
                                                     " ISNULL(DEPTSUPR1, '') + ';' + ISNULL(DEPTSUPR2, '') + ';' + ISNULL(DEPTSUPR3, '') + ';' + ISNULL(DEPTSUPR4, '') " +
                                                     " FROM AMDDEPT WHERE DEPTNO IN('9200', '7140', '9211')) A " +
                                                     " FOR XML PATH('')) from(SELECT 主管助理 = ISNULL(DEPTSUPR, '') + ';' + " +
                                                     " ISNULL(DEPTSUPR1, '') + ';' + ISNULL(DEPTSUPR2, '') + ';' + ISNULL(DEPTSUPR3, '') + ';' + ISNULL(DEPTSUPR4, '') " +
                                                     " FROM AMDDEPT WHERE DEPTNO IN('9200', '7140', '9211')) A  ";
                            _QueryComm.CommandType = CommandType.Text;
                            _QueryComm.Parameters.Clear();
                            _QueryDr = _QueryComm.ExecuteReader();

                            if (_QueryDr.HasRows)
                            {
                                _QueryDr.Read();
                                主管助理 = _QueryDr["主管助理"].ToString();
                            }
                            _QueryDr.Close();


                            if (!主管助理.Contains(GonGinVariable.ApplicationUser))
                            {
                                MessageBox.Show("非管理部、品保特定人員，請找瓊華、美鋒、陳美淑、李蓉姍 填寫!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        //=========================================================================================================================
                        if (Dgv.Rows[e.RowIndex].Cells["品項"].Value.ToString() == "品項")
                        {
                            string BUGNO = string.Empty;

                            if (Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString() == "25A1" ||
                               Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString() == "31A1")
                                BUGNO = Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString().Substring(0, 3);
                            else
                                BUGNO = Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString();


                            DataTable AA = new DataTable();
                            if (Dt_品項.Select("UID = '" + Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString() + "'").Count() > 0)
                                AA = Dt_品項.Select("UID = '" + Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString() + "'").CopyToDataTable();
                            else
                                AA = Dt_品項.Clone();

                            //AA.Columns.Add("狀態", typeof(String));

                            if (Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString() == "33")
                            {
                                特別自訂品項 pop = new 特別自訂品項(Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString(),
                                                                    BUGNO,
                                                                    Dgv.Rows[e.RowIndex].Cells["Gv2BGYM"].Value.ToString(),
                                                                    Dgv.Rows[e.RowIndex].Cells["Gv2BGDEP"].Value.ToString(),
                                                                    Dgv.Rows[e.RowIndex].Cells["Gv2BGTYPE"].Value.ToString(),
                                                                    AA,
                                                                    Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString());
                                if (pop.ShowDialog() == DialogResult.Yes)
                                {
                                    int 總和 = 0;
                                    foreach (DataRow item in pop.Dt_品項.Rows)
                                    {
                                        總和 += Convert.ToInt32(item["金額"].ToString());
                                    }
                                    if (_項目 == "預算")
                                    {
                                        Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value = 總和.ToString();
                                    }
                                    else if (_項目 == "追加")
                                    {
                                        Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value = 總和.ToString();
                                    }

                                    // 先刪掉在丟入
                                    //if (pop.Dt_品項.Rows.Count > 0)
                                    //{
                                    foreach (DataRow oRow in Dt_品項.Select("UID = '" + Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString() + "'"))
                                    {
                                        oRow.Delete();
                                    }
                                    Dt_品項.AcceptChanges();
                                    foreach (DataRow oRow in pop.Dt_品項.Rows)
                                    {
                                        Dt_品項.ImportRow(oRow);
                                    }
                                    //}
                                    Dt_品項.AcceptChanges();
                                }
                            }
                            else if (Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString() == "34" && Dgv.Rows[e.RowIndex].Cells["Gv2BGDEP"].Value.ToString() == "8320")
                            {
                                燃料費視窗 dop = new 燃料費視窗(Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString(),
                                                                    BUGNO,
                                                                    Dgv.Rows[e.RowIndex].Cells["Gv2BGYM"].Value.ToString(),
                                                                    Dgv.Rows[e.RowIndex].Cells["Gv2BGDEP"].Value.ToString(),
                                                                    Dgv.Rows[e.RowIndex].Cells["Gv2BGTYPE"].Value.ToString(),
                                                                    AA,
                                                                    Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString());
                                if (dop.ShowDialog() == DialogResult.Yes)
                                {
                                    int 總和 = 0;
                                    foreach (DataRow item in dop.Dt_品項.Rows)
                                    {
                                        總和 += Convert.ToInt32(item["金額"].ToString());
                                    }
                                    if (_項目 == "預算")
                                    {
                                        Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value = 總和.ToString();
                                    }
                                    else if (_項目 == "追加")
                                    {
                                        Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value = 總和.ToString();
                                    }

                                    // 先刪掉在丟入
                                    //if (dop.Dt_品項.Rows.Count > 0)
                                    //{
                                    foreach (DataRow oRow in Dt_品項.Select("UID = '" + Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString() + "'"))
                                    {
                                        oRow.Delete();
                                    }
                                    Dt_品項.AcceptChanges();
                                    foreach (DataRow oRow in dop.Dt_品項.Rows)
                                    {
                                        Dt_品項.ImportRow(oRow);
                                    }
                                    //}
                                    Dt_品項.AcceptChanges();
                                }
                            }
                            else
                            {
                                品項明細 form = new 品項明細(Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString(),
                                                             BUGNO,
                                                             Dgv.Rows[e.RowIndex].Cells["Gv2BGYM"].Value.ToString(),
                                                             Dgv.Rows[e.RowIndex].Cells["Gv2BGDEP"].Value.ToString(),
                                                             Dgv.Rows[e.RowIndex].Cells["Gv2BGTYPE"].Value.ToString(),
                                                             AA,
                                                             Dgv.Rows[e.RowIndex].Cells["Gv2BUGNO"].Value.ToString());
                                if (form.ShowDialog() == DialogResult.Yes)
                                {
                                    int 總和 = 0;
                                    foreach (DataRow item in form.Dt_品項.Rows)
                                    {
                                        總和 += Convert.ToInt32(item["金額"].ToString());
                                    }
                                    if (_項目 == "預算")
                                    {
                                        Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value = 總和.ToString();
                                    }
                                    else if (_項目 == "追加")
                                    {
                                        Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value = 總和.ToString();
                                    }

                                    // 先刪掉在丟入
                                    //if (form.Dt_品項.Rows.Count > 0)
                                    //{
                                    foreach (DataRow oRow in Dt_品項.Select("UID = '" + Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString() + "'"))
                                    {
                                        oRow.Delete();
                                    }
                                    Dt_品項.AcceptChanges();
                                    foreach (DataRow oRow in form.Dt_品項.Rows)
                                    {
                                        Dt_品項.ImportRow(oRow);
                                    }
                                    //}
                                    Dt_品項.AcceptChanges();
                                }
                            }
                        }

                        // 新增計算合計明細的Row
                        if (_明細.Rows.Count > 0)
                        {
                            //_明細.Rows.Add();
                            int 預算總和 = 0;
                            int 追加總和 = 0;
                            int 追加後預算總和 = 0;

                            foreach (DataRow oRow in _明細.Rows)
                            {
                                if (!string.IsNullOrEmpty(oRow["UID"].ToString().Trim()))
                                {
                                    預算總和 += Convert.ToInt32(oRow["BGAMT1"].ToString());
                                    追加總和 += Convert.ToInt32(oRow["BGBMT2"].ToString());
                                    追加後預算總和 += Convert.ToInt32(oRow["追加後預算"].ToString());
                                }
                                else
                                {
                                    oRow["BGDEPNAME"] = "合計";
                                    oRow["BGAMT1"] = 預算總和;
                                    oRow["BGBMT2"] = 追加總和;
                                    oRow["追加後預算"] = 追加後預算總和;
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}
