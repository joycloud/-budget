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
    public partial class 預算簽核申請 : Form
    {
        private SqlDataAdapter Ad = null;
        private SqlConnection _QueryConn = null;   // 共用
        private SqlCommand _QueryComm = null;      // 共用
        private SqlDataReader _QueryDr = null;     // 共用
        private DataTable Dt1 = new DataTable();
        private DataTable Dt2 = new DataTable();
        private string _項目 = string.Empty;
        private string _部門 = string.Empty;
        private string _申請月份 = string.Empty;

        public 預算簽核申請(DataTable Dt_明細, string 項目, string 部門, string 申請月份)
        {
            InitializeComponent();
            _項目 = 項目;
            _部門 = 部門;
            _申請月份 = 申請月份;
            label1.Text = "申請項目：" + "(" + _項目 + ")";

            _QueryConn = new SqlConnection(GonGinLibrary.GonGinVariable.SqlConnectString);
            _QueryComm = _QueryConn.CreateCommand();

            if (_QueryConn.State == ConnectionState.Closed) _QueryConn.Open();

            Dt2 = Dt_明細.Copy();
            naviTextBox2.Text = "自動產生";

            naviDataGridView1.DataSource = Dt2.DefaultView;
        }

        private void 導入簽核_Click(object sender, EventArgs e)
        {
            string 急要件 = string.Empty;
            if (一般.Checked)
                急要件 = "";
            else if (急件.Checked)
                急要件 = "Y";

            if (DialogResult.Yes == MessageBox.Show(" 請確認以下科目是否沒有缺少，\r\n如果缺少請聯絡MIS先別導入，\r\n無缺少則導入簽核流程? ", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                #region
                //DataTable Dt_簽核 = new DataTable();
                //Ad = new SqlDataAdapter(" SELECT * FROM BUGDA_簽核 ORDER BY 申請單號 DESC ", GonGinVariable.SqlConnectString);
                //Ad.SelectCommand.CommandType = CommandType.Text;
                //Ad.SelectCommand.CommandTimeout = 600;
                //Ad.SelectCommand.Parameters.Clear();
                //Dt_簽核.Clear();
                //Ad.Fill(Dt_簽核);

                //if (Dt_簽核.Rows.Count > 0)
                //{
                //    if (Dt_簽核.Rows[0]["申請單號"].ToString().Substring(0, 6) == DateTime.Now.ToString("yyyyMMdd").Substring(2, 6))
                //        naviTextBox2.Text = (Convert.ToInt32(Dt_簽核.Rows[0]["申請單號"].ToString()) + 1).ToString();
                //    else
                //        naviTextBox2.Text = DateTime.Now.ToString("yyyyMMdd").Substring(2, 6) + "0001";
                //}
                #endregion
                string 步驟 = string.Empty;
                步驟 = "導入簽核";

                SqlTransaction transaction;
                transaction = _QueryConn.BeginTransaction("Transaction");
                _QueryComm.Transaction = transaction;
                try
                {
                    _QueryComm.CommandText = " SELECT * FROM BUGDA_簽核 ORDER BY 單據單號 DESC ";
                    _QueryComm.CommandType = CommandType.Text;
                    _QueryComm.Parameters.Clear();
                    _QueryDr = _QueryComm.ExecuteReader();

                    if (_QueryDr.HasRows)
                    {
                        _QueryDr.Read();
                        if (_QueryDr["單據單號"].ToString().Substring(0, 6) == DateTime.Now.ToString("yyyyMMdd").Substring(2, 6))
                        {
                            naviTextBox2.Text = (Convert.ToInt32(_QueryDr["單據單號"].ToString()) + 1).ToString();
                        }
                        else
                        {
                            naviTextBox2.Text = DateTime.Now.ToString("yyyyMMdd").Substring(2, 6) + "0001";
                        }
                    }
                    else
                        naviTextBox2.Text = DateTime.Now.ToString("yyyyMMdd").Substring(2, 6) + "0001";
                    _QueryDr.Close();


                    // 抓出追加次數
                    int 追加次數 = 0;
                    if (_項目 == "追加")
                    {
                        _QueryComm.CommandText = " SELECT 追加次數 = COUNT(追加次數) FROM BUGDA_簽核 WHERE BGTYPE = '追加' AND BGYM = @BGYM AND BGDEP = @BGDEP  ";
                        _QueryComm.CommandType = CommandType.Text;
                        _QueryComm.Parameters.Clear();
                        _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = _申請月份;
                        _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = _部門;
                        _QueryDr = _QueryComm.ExecuteReader();

                        if (_QueryDr.HasRows)
                        {
                            _QueryDr.Read();
                            追加次數 = Convert.ToInt32(_QueryDr["追加次數"].ToString()) + 1;
                        }
                        else
                            追加次數 = 1;
                        _QueryDr.Close();
                    }

                    #region
                    //_QueryComm.CommandText = " SELECT 追加次數 = CASE WHEN ISNULL(MAX(追加次數),'') = '' THEN 0 ELSE MAX(追加次數) END FROM BUGDA_簽核 WHERE BGTYPE = '追加' AND BGYM = @BGYM AND BGDEP = @BGDEP  ";
                    //_QueryComm.CommandType = CommandType.Text;
                    //_QueryComm.Parameters.Clear();
                    //_QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = _申請月份;
                    //_QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = _部門;
                    //_QueryDr = _QueryComm.ExecuteReader();

                    //if (_QueryDr.HasRows)
                    //{
                    //    _QueryDr.Read();
                    //    追加次數 = Convert.ToInt32(_QueryDr["追加次數"].ToString()) + 1;
                    //}
                    //else
                    //    追加次數 = 1;
                    //_QueryDr.Close();

                    //int 追加次數 = 0;
                    #endregion

                    // 新增頭檔
                    _QueryComm.CommandText = " INSERT INTO BUGDA_簽核 (單據單號,BGDEP,BGYM,SCTRL,CRUSER,CRDATE,BGTYPE,急件,追加次數) " +
                                                 " VALUES(@單據單號,@BGDEP,@BGYM,'Y',@CRUSER,GETDATE(),@BGTYPE,@急件,@追加次數) ";
                    _QueryComm.CommandType = CommandType.Text;
                    _QueryComm.Parameters.Clear();
                    _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
                    _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = Dt2.Rows[0]["BGDEP"].ToString();
                    _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = Dt2.Rows[0]["BGYM"].ToString();
                    _QueryComm.Parameters.Add("@CRUSER", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                    _QueryComm.Parameters.Add("@BGTYPE", SqlDbType.VarChar).Value = _項目;
                    _QueryComm.Parameters.Add("@急件", SqlDbType.VarChar).Value = 急要件;
                    _QueryComm.Parameters.Add("@追加次數", SqlDbType.Int).Value = 追加次數;
                    _QueryComm.ExecuteNonQuery();

                    foreach (DataRow oRow in Dt2.Rows)  // 逐筆處理明細檔資料
                    {
                        _QueryComm.CommandText = " INSERT INTO BUGDA_簽核明細 (UID,單據單號,BGDEP,BGYM,BUGNO,BGAMT1,BGBMT2,追加後預算,BGREM,CRUSER,CRDATE,BGTYPE) VALUES " +
                                                 " (newid(),@單據單號,@BGDEP,@BGYM,@BUGNO,@BGAMT1,@BGBMT2,@追加後預算,@BGREM,@CRUSER,GETDATE(),@BGTYPE) ";
                        _QueryComm.CommandType = CommandType.Text;
                        _QueryComm.Parameters.Clear();
                        _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
                        _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = oRow["BGYM"].ToString();
                        _QueryComm.Parameters.Add("@BUGNO", SqlDbType.VarChar).Value = oRow["BUGNO"].ToString();
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
                        _QueryComm.Parameters.Add("@CRUSER", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                        _QueryComm.Parameters.Add("@BGREM", SqlDbType.VarChar).Value = oRow["BGREM"].ToString();
                        if (string.IsNullOrEmpty(oRow["追加後預算"].ToString().Trim()))
                            _QueryComm.Parameters.Add("@追加後預算", SqlDbType.Decimal).Value = DBNull.Value;
                        else
                            _QueryComm.Parameters.Add("@追加後預算", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["追加後預算"].ToString());
                        _QueryComm.Parameters.Add("@BGTYPE", SqlDbType.VarChar).Value = _項目;
                        _QueryComm.ExecuteNonQuery();
                    }
                    // ============================================================================================

                    // 導入簽核流程
                    _QueryComm.CommandText = " INSERT INTO FLOW_STEP (UID,簽核流程,單據單號,串會簽,流程名稱,起始流程,SCTRL,狀態,建立者,建立日) " +
                                             " SELECT NEWID(), 簽核流程, @單據單號, 串會簽, 流程名稱, 起始流程, 'N', '未簽', @建立者, GETDATE() " +
                                             " FROM FLOW_DEF WHERE 流程名稱 = '部門預算'";
                    _QueryComm.CommandType = CommandType.Text;
                    _QueryComm.Parameters.Clear();
                    _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
                    _QueryComm.Parameters.Add("@建立者", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                    _QueryComm.ExecuteNonQuery();

                    transaction.Commit();

                    // 更改第一關簽核人
                    步驟 = "更改簽核人";
                    更改簽核人();

                    this.DialogResult = DialogResult.Yes;
                    MessageBox.Show("導入簽核完成 !!", "簽核訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(步驟 + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void 更改簽核人()
        {
            // 抓上階部門
            string 上階部門 = string.Empty;
            string 主管 = string.Empty;
            string 主管助理 = string.Empty;
            _QueryComm.CommandText = " SELECT UPDEPT FROM AMDDEPT WHERE DEPTNO = @DEPTNO ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = _部門;
            _QueryDr = _QueryComm.ExecuteReader();

            if (_QueryDr.HasRows)
            {
                _QueryDr.Read();
                上階部門 = _QueryDr["UPDEPT"].ToString();
            }
            _QueryDr.Close();

            if (GonGinVariable.ApplicationUserDeptNo == "9600" && _部門 == "9990")
            {
                // 財務部主管
                _QueryComm.CommandText = " SELECT DEPTSUPR,BACKNO,總經理室 = (SELECT DEPTSUPR FROM AMDDEPT WHERE DEPTNO = '9990' ) FROM AMDDEPT WHERE DEPTNO = '9600' ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    主管 = _QueryDr["DEPTSUPR"].ToString() + ";" + _QueryDr["BACKNO"].ToString() + ";" + _QueryDr["總經理室"].ToString();
                }
                _QueryDr.Close();
            }
            else
            {
                // 取出主管&助理3&助理4
                _QueryComm.CommandText = " SELECT DEPTSUPR,BACKNO FROM AMDDEPT WHERE DEPTNO = @DEPTNO ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = 上階部門;
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    主管 = _QueryDr["DEPTSUPR"].ToString() + ";" + _QueryDr["BACKNO"].ToString();
                }
                _QueryDr.Close();
            }

            //if (GonGinVariable.ApplicationUserDeptNo == "9720")
            //{
            //    _QueryComm.CommandText = " SELECT 主管助理 = DEPTSUPR + ';' + " +
            //                             " CASE ISNULL(DEPTSUPR1,'') WHEN '' THEN '' ELSE DEPTSUPR1 +';' END + " +
            //                             " CASE ISNULL(DEPTSUPR2,'') WHEN '' THEN '' ELSE DEPTSUPR2 +';' END + " +
            //                             " CASE ISNULL(DEPTSUPR3,'') WHEN '' THEN '' ELSE DEPTSUPR3 +';' END + " +
            //                             " CASE ISNULL(DEPTSUPR4,'') WHEN '' THEN '' ELSE DEPTSUPR4 +';' END FROM AMDDEPT WHERE DEPTNO = '9720' ";
            //    _QueryComm.CommandType = CommandType.Text;
            //    _QueryComm.Parameters.Clear();
            //    _QueryDr = _QueryComm.ExecuteReader();

            //    if (_QueryDr.HasRows)
            //    {
            //        _QueryDr.Read();
            //        主管助理 = _QueryDr["主管助理"].ToString();
            //    }
            //    _QueryDr.Close();

            //    if (!主管助理.Contains(GonGinVariable.ApplicationUser))
            //        主管助理 = 主管助理 + GonGinVariable.ApplicationUser + ";";
            //}
            //else
            //{

            // 取出主管&助理1、2、3、4
            _QueryComm.CommandText = " SELECT cast(DEPTSUPR AS NVARCHAR ) + ';' FROM( " +
                                     " SELECT DISTINCT DEPTSUPR FROM(SELECT DEPTSUPR FROM AMDDEPT A WHERE DEPTNO = @DEPTNO " +
                                     " UNION SELECT DEPTSUPR = (CASE ISNULL(DEPTSUPR1, '') WHEN '' THEN '' ELSE DEPTSUPR1 END) FROM AMDDEPT A WHERE DEPTNO = @DEPTNO " +
                                     " UNION SELECT DEPTSUPR = (CASE ISNULL(DEPTSUPR2, '') WHEN '' THEN '' ELSE DEPTSUPR2 END) FROM AMDDEPT A WHERE DEPTNO = @DEPTNO " +
                                     " UNION SELECT DEPTSUPR = (CASE ISNULL(DEPTSUPR3, '') WHEN '' THEN '' ELSE DEPTSUPR3 END) FROM AMDDEPT A WHERE DEPTNO = @DEPTNO " +
                                     " UNION SELECT DEPTSUPR = (CASE ISNULL(DEPTSUPR4, '') WHEN '' THEN '' ELSE DEPTSUPR4 END) FROM AMDDEPT A WHERE DEPTNO = @DEPTNO) B) D " +
                                     " FOR XML PATH('') ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = 上階部門;
            _QueryDr = _QueryComm.ExecuteReader();

            if (_QueryDr.HasRows)
            {
                _QueryDr.Read();
                主管助理 = _QueryDr[0].ToString();
            }
            _QueryDr.Close();

            if (!主管助理.Contains(GonGinVariable.ApplicationUser))
                主管助理 = 主管助理 + GonGinVariable.ApplicationUser + ";";
            //}

            // 抓財務人員
            DataTable Dt_財務人員 = new DataTable();
            string 資訊人員 = string.Empty;
            SqlDataAdapter Ad = new SqlDataAdapter(" SELECT PENNO FROM PERSON WHERE DEPT IN ('9300','9310') AND ISNULL(LDAY,'') ='' ", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.Text;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.CommandTimeout = 600;
            Dt_財務人員.Clear();
            Ad.Fill(Dt_財務人員);

            foreach (DataRow item in Dt_財務人員.Rows)
            {
                資訊人員 += item["PENNO"] + ";";
            }

            // 抓第一關的UID，丟給HISTORY的第一關PUID
            string PUID = string.Empty;
            _QueryComm.CommandText = " SELECT UID FROM FLOW_STEP WHERE 單據單號 = @單據單號 AND 起始流程 = '0.000' ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
            _QueryDr = _QueryComm.ExecuteReader();

            if (_QueryDr.HasRows)
            {
                _QueryDr.Read();
                PUID = _QueryDr["UID"].ToString();
            }
            _QueryDr.Close();

            // 第一關
            _QueryComm.CommandText = " UPDATE FLOW_STEP SET 送簽人=@送簽人,簽核人=@簽核人,階段名稱 = '申請人' WHERE 單據單號=@單據單號 AND 起始流程 = '0.000' AND 流程名稱 = '部門預算' ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@送簽人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            _QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = 主管助理 + "1819;";
            _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
            _QueryComm.ExecuteNonQuery();

            // 第二關
            _QueryComm.CommandText = " UPDATE FLOW_STEP SET 簽核人=@簽核人,階段名稱 = '部門主管' WHERE 單據單號=@單據單號 AND 起始流程 = '1.000' AND 流程名稱 = '部門預算' ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            //_QueryComm.Parameters.Add("@送簽人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            _QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = 主管 + ";1819;";
            _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
            _QueryComm.ExecuteNonQuery();

            // 第三關
            _QueryComm.CommandText = " UPDATE FLOW_STEP SET 簽核人=@簽核人,階段名稱 = '資訊部' WHERE 單據單號=@單據單號 AND 起始流程 = '2.000' AND 流程名稱 = '部門預算' ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            //_QueryComm.Parameters.Add("@送簽人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            _QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = 資訊人員;
            _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
            _QueryComm.ExecuteNonQuery();

            // 第四關
            _QueryComm.CommandText = " UPDATE FLOW_STEP SET 簽核人=@簽核人,階段名稱 = 'GM' WHERE 單據單號=@單據單號 AND 起始流程 = '3.000' AND 流程名稱 = '部門預算' ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            //_QueryComm.Parameters.Add("@送簽人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            _QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = 資訊人員;
            _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
            _QueryComm.ExecuteNonQuery();

            // 第五關
            //_QueryComm.CommandText = " UPDATE FLOW_STEP SET 簽核人=@簽核人,階段名稱 = 'GM' WHERE 單據單號=@單據單號 AND 起始流程 = '4.000' AND 流程名稱 = '部門預算' ";
            //_QueryComm.CommandType = CommandType.Text;
            //_QueryComm.Parameters.Clear();
            ////_QueryComm.Parameters.Add("@送簽人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            //_QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = 資訊人員;
            //_QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
            //_QueryComm.ExecuteNonQuery();

            // HISTORY紀錄_建立
            _QueryComm.CommandText = " INSERT INTO FLOW_HISTORY (UID,簽核流程,流程名稱,單據單號,起始流程,結束流程,送簽人,簽核人,SCTRL,建立日,狀態) " +
                                     " VALUES (newid(),@簽核流程,@流程名稱,@單據單號,0,0,@送簽人,@簽核人,'Y',GETDATE(),'建立')";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@流程名稱", SqlDbType.VarChar).Value = "部門預算";
            _QueryComm.Parameters.Add("@簽核流程", SqlDbType.VarChar).Value = "預算系統";
            _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
            _QueryComm.Parameters.Add("@送簽人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            _QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            _QueryComm.ExecuteNonQuery();

            // HISTORY紀錄_第一關
            //_QueryComm.CommandText = " INSERT INTO FLOW_HISTORY (UID,PUID,簽核流程,流程名稱,單據單號,起始流程,結束流程,送簽人,簽核人,SCTRL,建立日,狀態) " +
            //                         " VALUES (newid(),@PUID,@簽核流程,@流程名稱,@單據單號,0,1,@送簽人,@簽核人,'Y',GETDATE(),'准')";
            //_QueryComm.CommandType = CommandType.Text;
            //_QueryComm.Parameters.Clear();
            //_QueryComm.Parameters.Add("@流程名稱", SqlDbType.VarChar).Value = "部門預算";
            //_QueryComm.Parameters.Add("@簽核流程", SqlDbType.VarChar).Value = "預算系統";
            //_QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviTextBox2.Text;
            //_QueryComm.Parameters.Add("@送簽人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            //_QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            //_QueryComm.Parameters.Add("@PUID", SqlDbType.VarChar).Value = PUID;
            //_QueryComm.ExecuteNonQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 預算簽核申請_Load(object sender, EventArgs e)
        {
            naviDataGridView1.Columns["Gv2BGDEP"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BGYM"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BUGNO"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BUGNA"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BGACNO"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BGREM"].ReadOnly = true;
            naviDataGridView1.Columns["Gv2BGCMT1"].ReadOnly = true;

            //if (_項目 == "預算")
            //naviDataGridView1.Columns["Gv2BGAMT1"].Visible = true;
            //else if (_項目 == "追加")
            //naviDataGridView1.Columns["Gv2BGBMT2"].Visible = true;
            if (_項目 == "挪用")
            {
                naviDataGridView1.Columns["Gv2BGCMT3"].Visible = true;
            }
        }
    }
}
