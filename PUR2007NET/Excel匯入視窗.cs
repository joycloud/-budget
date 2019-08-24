using GonGinLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PUR2007NET
{
    public partial class Excel匯入視窗 : Form
    {
        private ArrayList 儲存 = new ArrayList();
        //定義OleDb======================================================
        //2.提供者名稱  Microsoft.Jet.OLEDB.4.0適用於2003以前版本，Microsoft.ACE.OLEDB.12.0 適用於2007以後的版本處理 xlsx 檔案
        private const string ProviderName = "Microsoft.Jet.OLEDB.4.0;";
        //private const string ProviderName = "Microsoft.ACE.OLEDB.12.0;";
        //3.Excel版本，Excel 8.0 針對Excel2000及以上版本，Excel5.0 針對Excel97。
        private const string ExtendedString = "'Excel 8.0;";
        //4.第一行是否為標題
        private const string Hdr = "No;";
        //5.IMEX=1 通知驅動程序始終將「互混」數據列作為文本讀取
        private const string IMEX = "1';";
        //=============================================================

        //連線字串
        string cs1 = "Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\0042-72244.xls" + ";";  //指定桌面路徑
        //string cs1 = "";  //指定桌面路徑
        string cs2 = "Provider=" + ProviderName;
        string cs3 = "Extended Properties=" + ExtendedString;
        string cs4 = "HDR=" + Hdr;
        string cs5 = "IMEX=" + IMEX;
        //Excel 的工作表名稱 (Excel左下角有的分頁名稱)
        string SheetName = "工作表1";

        private DataTable dt = new DataTable();
        private DataTable Dt_結果 = new DataTable();
        private SqlDataAdapter Ad = null;

        private SqlConnection _QueryConn = null;   // 共用
        private SqlCommand _QueryComm = null;      // 共用
        private SqlDataReader _QueryDr = null;     // 共用

        private DateTime ServerTime;   // 伺服器日期變數

        private string DEPT = "";
        private string DEPTNAME = "";
        private DataTable 科目下拉 = new DataTable();

        public Excel匯入視窗(string DEPT)
        {
            InitializeComponent();

            _QueryConn = new SqlConnection(GonGinLibrary.GonGinVariable.SqlConnectString);
            _QueryComm = _QueryConn.CreateCommand();

            if (_QueryConn.State == ConnectionState.Closed) _QueryConn.Open();

            GonGinGetServerDate ServerDate = new GonGinGetServerDate(GonGinVariable.SqlConnectString);
            ServerTime = ServerDate.日期;

            Dt_結果.Columns.Add("NUM", typeof(int));
            Dt_結果.Columns.Add("UID", typeof(string));
            Dt_結果.Columns.Add("單據單號", typeof(string));
            Dt_結果.Columns.Add("品項", typeof(string));
            Dt_結果.Columns.Add("名稱", typeof(string));
            Dt_結果.Columns.Add("規格尺寸", typeof(string));
            Dt_結果.Columns.Add("數量", typeof(double));
            Dt_結果.Columns.Add("單價", typeof(double));
            Dt_結果.Columns.Add("金額", typeof(double));
            Dt_結果.Columns.Add("備註", typeof(string));

            naviTextBox3.Text = "預算";

            naviTextBox2.Text = ServerTime.AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);

            this.DEPT = DEPT;
            GonGinCheckOfDataDuplication DataDuplication = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString.ToString(), "AMDDEPT", "DEPTNAME", "DEPTNAME", "DEPTNAME", "DEPTNO = '" + DEPT + "'");
            if (!string.IsNullOrEmpty(DataDuplication.傳回值.Trim()))
            {
                DEPTNAME = DataDuplication.傳回值;
                label1.Text = "匯入部門：" + DataDuplication.傳回值 + "(" + DEPT + ")";
            }

            科目下拉.Columns.Add("BUGNO", typeof(String));
            科目下拉.Columns.Add("BUGNA", typeof(String));
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void naviBtnQuery1_Click(object sender, EventArgs e)
        {
            naviTextBox1.Text = "";
            if (DialogResult.No == MessageBox.Show("確定是匯入" + naviTextBox2.Text + "的" + naviTextBox4.Text + naviTextBox3.Text + "?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                return;

            if (DialogResult.OK == openFileDialog1.ShowDialog(this))
            {
                儲存 = new ArrayList();
                foreach (string var in openFileDialog1.FileNames)
                {
                    //naviTextBox1.Text += var.ToString();
                    naviTextBox1.Text += Path.GetFileName(var.ToString()) + "; ";
                    儲存.Add(var.ToString());
                }


                if (!File.Exists(儲存[0].ToString()))
                {
                    MessageBox.Show(儲存[0].ToString() + "，資料不存在!!", "更新訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                匯入(儲存[0].ToString());
            }
        }
        private void 匯入(string xx)
        {
            int Z = 0;
            dt.Clear();
            Dt_結果.Clear();
            //string qs1 = "select * from[" + A1 + "$]";
            //string qs2 = "select * from[" + A2 + "$]";
            //string qs3 = "select * from[" + A3 + "$]";

            cs1 = "Data Source=" + xx + ";";
            cs2 = "Provider=" + (xx.Contains("xlsx") ? "Microsoft.ACE.OLEDB.12.0;" : (xx.Contains("XLSX") ? "Microsoft.ACE.OLEDB.12.0;" : "Microsoft.Jet.OLEDB.4.0;"));

            using (OleDbConnection cn = new OleDbConnection(cs1 + cs2 + cs3 + cs4 + cs5))
            {
                cn.Open();

                System.Data.DataTable Table = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string UID = "";
                string 單據單號 = "";
                int NUM = 0;
                string AAA = "";

                _QueryComm.CommandText = " SELECT UID,單據單號,NUM = ISNULL((SELECT MAX(NUM) FROM BUGDA_簽核品項 B WHERE A.UID=B.UID),0) FROM BUGDA_簽核明細 A " +
                                         " WHERE BUGNO = @BUGNO AND BGDEP = @BGDEP AND BGYM = @BGYM AND BGTYPE = @BGTYPE ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = naviTextBox2.Text;
                _QueryComm.Parameters.Add("@BGTYPE", SqlDbType.VarChar).Value = naviTextBox3.Text;
                _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = DEPT;
                _QueryComm.Parameters.Add("@BUGNO", SqlDbType.VarChar).Value = naviTextBox4.Text;
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    UID = _QueryDr["UID"].ToString();
                    單據單號 = _QueryDr["單據單號"].ToString();
                    NUM = Convert.ToInt32(_QueryDr["NUM"].ToString());
                }
                _QueryDr.Close();


                if (string.IsNullOrEmpty(單據單號) || 單據單號 == null)
                {
                    MessageBox.Show("還未產生" + naviTextBox2.Text + "的" + naviTextBox4.Text + "預算申請!!", "更新訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    SheetName = Table.Rows[i][2].ToString().Replace("'", "").Replace("$", "").Trim().Replace("_xlnm#_FilterDatabase", "");

                    string qs = "select * from[" + SheetName + "$]";
                    //try
                    //{
                    dt = new DataTable();
                    using (OleDbDataAdapter dr = new OleDbDataAdapter(qs, cn))
                    {
                        dr.Fill(dt);
                    }
                    //int n;
                    //foreach (DataRow oRow in dt.Rows)
                    //{
                    //    if (oRow[0].ToString() != "序" && !int.TryParse(oRow[0].ToString(), out n))
                    //        oRow.Delete();
                    //}
                    //dt.AcceptChanges();

                    // 更改欄位名稱
                    //int j = 0;
                    foreach (DataRow oRow in dt.Select("F1='品項'"))
                    {
                        foreach (DataColumn OColumn in oRow.Table.Columns)
                        {
                            //if (oRow[OColumn].ToString() == "預估")
                            //{
                            //    OColumn.ColumnName = "單價" + j;
                            //    j++;
                            //}
                            //else
                            //{
                            if (!string.IsNullOrEmpty(dt.Rows[0][OColumn].ToString().Trim()))
                                OColumn.ColumnName = dt.Rows[0][OColumn].ToString();
                            //}
                        }
                    }
                    dt.AcceptChanges();

                    DataTable Dt暫存 = dt.Copy();

                    //foreach (DataRow oRow in Dt暫存.Rows)
                    //{
                    //    foreach (DataColumn OColumn in dt.Columns)
                    //    {
                    //        //MessageBox.Show(Dt暫存.Rows[1][OColumn.ToString()].ToString());
                    //        //if (Dt暫存.Rows[0][OColumn].ToString().Substring(0,1) == "F")
                    //        if (oRow[OColumn.ToString()].ToString() == "-")
                    //            oRow[OColumn.ToString()] = "1";
                    //    }
                    //}



                    foreach (DataColumn OColumn in dt.Columns)
                    {
                        if (string.IsNullOrEmpty(Dt暫存.Rows[0][OColumn.ToString()].ToString().Trim()))
                            Dt暫存.Columns.Remove(OColumn.ToString());
                    }

                    Dt暫存.Rows[0].Delete();
                    // 移除不必要的
                    //Dt暫存.Columns.Remove("單");
                    //Dt暫存.Columns.Remove("單價1");

                    //Dt暫存.Rows[0].Delete();
                    Dt暫存.AcceptChanges();


                    foreach (DataRow oRow in Dt暫存.Rows)
                    {
                        if (!string.IsNullOrEmpty(oRow["品項"].ToString().Trim()) && !string.IsNullOrEmpty(oRow["名稱"].ToString().Trim()))
                        {
                            // 取BUGDA_ITEM，名稱、規格
                            GonGinCheckOfDataDuplication 資料 = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString, "BUGDA_ITEM", "FDWG", "FCDS", "FSIZ", "FDWG='" + oRow["品項"].ToString() + "'");

                            NUM++;
                            Dt_結果.Rows.Add();
                            Dt_結果.Rows[Z]["NUM"] = NUM;
                            Dt_結果.Rows[Z]["UID"] = UID;
                            Dt_結果.Rows[Z]["單據單號"] = 單據單號;
                            Dt_結果.Rows[Z]["品項"] = oRow["品項"].ToString();

                            if (string.IsNullOrEmpty(資料.傳回值二.Trim()))
                                Dt_結果.Rows[Z]["名稱"] = oRow["名稱"].ToString();
                            else
                                Dt_結果.Rows[Z]["名稱"] = 資料.傳回值二;

                            if (string.IsNullOrEmpty(資料.傳回值三.Trim()))
                                Dt_結果.Rows[Z]["規格尺寸"] = oRow["規格尺寸"].ToString();
                            else
                                Dt_結果.Rows[Z]["規格尺寸"] = 資料.傳回值三;

                            Dt_結果.Rows[Z]["數量"] = oRow["數量"].ToString();
                            Dt_結果.Rows[Z]["單價"] = oRow["單價"].ToString();
                            Dt_結果.Rows[Z]["金額"] = Convert.ToDouble(oRow["數量"].ToString()) * Convert.ToDouble(oRow["單價"].ToString());
                            Dt_結果.Rows[Z]["備註"] = oRow["備註"].ToString();
                            Z++;
                        }
                    }

                    //    }
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //}
                }

                naviDataGridView1.DataSource = Dt_結果.DefaultView;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            naviTextBox3.Text = comboBox1.Text;
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void 存檔_Click(object sender, EventArgs e)
        {
            if (Dt_結果.Rows.Count < 1)
            {
                MessageBox.Show("無明細無法匯入!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GonGinCheckOfDataDuplication 資料 = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString, "BUGDA_簽核", "SCTRL", "SCTRL", "SCTRL", "BGDEP = '" + DEPT + "' AND BGYM = '" + naviTextBox2.Text + "' AND BGTYPE ='" + naviTextBox3.Text + "'");
            if (資料.傳回值 != "Y")
            {
                MessageBox.Show(DEPTNAME + "的" + naviTextBox2.Text + naviTextBox3.Text + "已結案！！", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _QueryComm.CommandText = " SELECT 起始流程= ISNULL(MAX(起始流程),0) FROM FLOW_STEP WHERE 流程名稱='部門預算' AND  " +
                                     " 單據單號 = (SELECT 單據單號 FROM BUGDA_簽核 WHERE BGDEP = @BGDEP AND BGYM = @BGYM AND BGTYPE = @BGTYPE) AND SCTRL = 'Y' ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = naviTextBox2.Text;
            _QueryComm.Parameters.Add("@BGTYPE", SqlDbType.VarChar).Value = naviTextBox3.Text;
            _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = DEPT;
            _QueryDr = _QueryComm.ExecuteReader();

            if (_QueryDr.HasRows)
            {
                _QueryDr.Read();

                if (Convert.ToDouble(_QueryDr["起始流程"].ToString()) > 0)
                {
                    MessageBox.Show("主管已確認，無法匯入！！", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _QueryDr.Close();
                    return;
                }
            }
            _QueryDr.Close();


            if (DialogResult.No == MessageBox.Show("確定導入" + DEPTNAME + "，" + naviTextBox2.Text + "的" + naviTextBox4.Text + naviTextBox3.Text + "?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                return;

            string UID = "";
            double 總金額 = 0;
            double 追加後預算 = 0;
            string AA = "";

            try
            {
                SqlTransaction transaction;
                transaction = _QueryConn.BeginTransaction("Transaction");
                _QueryComm.Transaction = transaction;

                foreach (DataRow oRow in Dt_結果.Rows)
                {
                    UID = oRow["UID"].ToString();
                    AA = "錯誤1";

                    _QueryComm.CommandText = " INSERT INTO BUGDA_簽核品項 (NUM,UID,FDWG,FCDS,BUGNO,數量,金額,CRUSER,CRDATE,FSMT,FSIZ,REMARK,單據單號) " +
                                             " VALUES(@NUM, @UID, @FDWG, @FCDS, @BUGNO, @數量, @金額, @CRUSER, GETDATE(), @FSMT, @FSIZ, @REMARK, @單據單號) ";
                    _QueryComm.Parameters.Clear();
                    _QueryComm.Parameters.Add("@NUM", SqlDbType.Int).Value = Convert.ToInt32(oRow["NUM"].ToString());
                    _QueryComm.Parameters.Add("@UID", SqlDbType.VarChar).Value = oRow["UID"].ToString();
                    _QueryComm.Parameters.Add("@FDWG", SqlDbType.VarChar).Value = oRow["品項"].ToString();
                    _QueryComm.Parameters.Add("@FCDS", SqlDbType.VarChar).Value = oRow["名稱"].ToString();
                    _QueryComm.Parameters.Add("@BUGNO", SqlDbType.VarChar).Value = naviTextBox4.Text;

                    if (string.IsNullOrEmpty(oRow["數量"].ToString().Trim()))
                        _QueryComm.Parameters.Add("@數量", SqlDbType.Decimal).Value = 0;
                    else
                        _QueryComm.Parameters.Add("@數量", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["數量"].ToString());

                    if (string.IsNullOrEmpty(oRow["單價"].ToString().Trim()))
                        _QueryComm.Parameters.Add("@FSMT", SqlDbType.Decimal).Value = 0;
                    else
                        _QueryComm.Parameters.Add("@FSMT", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["單價"].ToString());

                    if (string.IsNullOrEmpty(oRow["金額"].ToString().Trim()))
                        _QueryComm.Parameters.Add("@金額", SqlDbType.Decimal).Value = 0;
                    else
                        _QueryComm.Parameters.Add("@金額", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["金額"].ToString());
                    _QueryComm.Parameters.Add("@CRUSER", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                    _QueryComm.Parameters.Add("@FSIZ", SqlDbType.VarChar).Value = oRow["規格尺寸"].ToString();
                    _QueryComm.Parameters.Add("@REMARK", SqlDbType.VarChar).Value = oRow["備註"].ToString();
                    _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = oRow["單據單號"].ToString();
                    _QueryComm.ExecuteNonQuery();
                }

                AA = "錯誤2";
                _QueryComm.CommandText = " SELECT 總金額=SUM(金額) FROM BUGDA_簽核品項 A WHERE UID = @UID ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@UID", SqlDbType.VarChar).Value = UID;
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    總金額 = Convert.ToDouble(_QueryDr["總金額"].ToString());
                }
                _QueryDr.Close();


                if (naviTextBox3.Text == "預算")
                {
                    AA = "錯誤3";
                    _QueryComm.CommandText = " UPDATE BUGDA_簽核明細 SET BGAMT1 = @BGAMT1,AMDUSR=@AMDUSR,AMDDAY=GETDATE() WHERE UID = @UID ";
                    _QueryComm.Parameters.Clear();
                    _QueryComm.Parameters.Add("@UID", SqlDbType.VarChar).Value = UID;
                    _QueryComm.Parameters.Add("@BGAMT1", SqlDbType.Float).Value = 總金額;
                    _QueryComm.Parameters.Add("@AMDUSR", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                    _QueryComm.ExecuteNonQuery();
                }
                else
                {
                    AA = "錯誤4";
                    _QueryComm.CommandText = " UPDATE BUGDA_簽核明細 SET BGBMT2 = @BGBMT2,AMDUSR=@AMDUSR,AMDDAY=GETDATE() WHERE UID = @UID ";
                    _QueryComm.Parameters.Clear();
                    _QueryComm.Parameters.Add("@UID", SqlDbType.VarChar).Value = UID;
                    _QueryComm.Parameters.Add("@BGBMT2", SqlDbType.Float).Value = 總金額;
                    _QueryComm.Parameters.Add("@AMDUSR", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                    _QueryComm.ExecuteNonQuery();
                }
                AA = "錯誤5";
                _QueryComm.CommandText = " SELECT 追加後預算 = BGAMT1 + BGBMT2 FROM BUGDA_簽核明細 A WHERE UID = @UID ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@UID", SqlDbType.VarChar).Value = UID;
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    追加後預算 = Convert.ToDouble(_QueryDr["追加後預算"].ToString());
                }
                _QueryDr.Close();

                AA = "錯誤6";
                _QueryComm.CommandText = " UPDATE BUGDA_簽核明細 SET 追加後預算 = @追加後預算 WHERE UID = @UID ";
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@UID", SqlDbType.VarChar).Value = UID;
                _QueryComm.Parameters.Add("@追加後預算", SqlDbType.Float).Value = 追加後預算;
                _QueryComm.ExecuteNonQuery();
                transaction.Commit();

                MessageBox.Show("匯入成功，之後請確認資料是否正確!!");
                this.DialogResult = DialogResult.Yes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(AA + ex.Message.ToString(), "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        private void Excel匯入視窗_Load(object sender, EventArgs e)
        {
            DataTable Dt_下拉 = new DataTable();

            Ad = new SqlDataAdapter("PUR2007NET_申請", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.Parameters.Add("@月份起", SqlDbType.VarChar).Value = naviTextBox2.Text;
            Ad.SelectCommand.Parameters.Add("@DEPT", SqlDbType.VarChar).Value = DEPT;
            Dt_下拉.Clear();
            Ad.Fill(Dt_下拉);

            int i = 0;
            foreach (DataRow oRow in Dt_下拉.Rows)
            {
                科目下拉.Rows.Add();
                科目下拉.Rows[i]["BUGNO"] = oRow["BUGNO"].ToString();
                科目下拉.Rows[i]["BUGNA"] = oRow["BUGNA"].ToString();
                i++;
            }
            科目下拉.AcceptChanges();
            科目下拉.DefaultView.Sort = "BUGNO";


            naviMutiComboBox1.DataSource = 科目下拉;
            naviMutiComboBox1.ValueMember = "BUGNO";
            naviMutiComboBox1.DisplayMember = "BUGNA";
            naviMutiComboBox1.SelectedValue = "BUGNO";
        }

        private void naviMutiComboBox1_TextChanged(object sender, EventArgs e)
        {
            naviTextBox4.Text = naviMutiComboBox1.SelectedValue.ToString();
        }
    }
}
