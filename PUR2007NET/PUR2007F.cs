using GonGinLibrary;
using PUR2007NET.A3報表_NEW;
using PUR2007NET.實際報表;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUR2007NET
{
    public partial class PUR2007F : Form
    {
        private DataSet Ds = new DataSet();
        private DataSet Ds_總表 = new DataSet();
        private SqlDataAdapter Ad = null;
        private SqlDataAdapter Ad_D = null;
        private SqlConnection _QueryConn = null;   // 共用
        private SqlCommand _QueryComm = null;      // 共用
        private SqlDataReader _QueryDr = null;     // 共用


        DataTable Dt_頭 = new DataTable();
        DataTable Dt_明細 = new DataTable();
        DataTable Dt_特別預算 = new DataTable();
        DataTable Dt_實際使用 = new DataTable();
        DataTable Dt_實際使用比較 = new DataTable();
        DataSet Ds_實際預算 = new DataSet();
        DataSet Dt_預算使用統計 = new DataSet();

        public PUR2007F()
        {
            InitializeComponent();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 修正 CrystalReport無法運行在.NET4.0問題 , 採用程式自動App.Config
            // 1.記得參考要加入 System.configuration
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            String ConfigString = "<?xml version=\"1.0\"?>\n" +
                                  "<configuration>\n" +
                                  "<startup useLegacyV2RuntimeActivationPolicy=\"true\">\n" +
                                  "<supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.0\"/>\n" +
                                  "</startup>\n" +
                                  "</configuration>";

            if (!System.IO.File.Exists(ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).FilePath.ToString()))
            {
                System.IO.File.WriteAllText(ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).FilePath.ToString(), ConfigString);
            }

            //----------------------------------------------------------------------------------------
            //      建立共用資料庫連接器 
            //----------------------------------------------------------------------------------------
            _QueryConn = new SqlConnection(GonGinVariable.SqlConnectString);
            _QueryComm = _QueryConn.CreateCommand();



            if (_QueryConn.State == ConnectionState.Closed) _QueryConn.Open();

            naviStatusStrip1.StatusOfApplicationName = GonGinVariable.SectionName;
            naviStatusStrip1.StatusOfLoginName = GonGinVariable.ApplicationUserName;

        }

        private void PUR2007F_Load(object sender, EventArgs e)
        {
            Month();
            DEPT();
            月份起.ReadOnly = true;
            if (GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "93")
            {
                button1.Visible = true;
                開啟控管.Visible = true;
                //列印總表.Visible = true;
                toolStripButton1.Visible = true;
                toolStripButton2.Visible = true;
                群組列印.Visible = true;
                特別預算總表.Visible = true;
                實際使用.Visible = true;
            }
            else if (GonGinVariable.ApplicationUser == "1298")
            {
                toolStripButton1.Visible = true;
                //toolStripButton2.Visible = true;
                //群組列印.Visible = true;
            }

            if (GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "93" || GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "96")
            {
                實際使用.Visible = true;
                特別科目.Visible = true;
            }

            if (GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "93" || GonGinVariable.ApplicationUser == "3351" || GonGinVariable.ApplicationUser == "2770")
                費用查詢.Visible = true;

            // 開啟33A_Excel匯入功能
            //if (GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "93" || GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "71")
            //    toolStripButton3.Visible = true;


            //else if (GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "96")
            //列印總表.Visible = true;

            Query();

            // ReadOnly_naviDataGridView2
            naviDataGridView2.Columns["Gv2BGDEP"].ReadOnly = true;
            naviDataGridView2.Columns["Gv2BGYM"].ReadOnly = true;
            naviDataGridView2.Columns["Gv2BUGNO"].ReadOnly = true;
            naviDataGridView2.Columns["Gv2BUGNA"].ReadOnly = true;
            naviDataGridView2.Columns["Gv2BGACNO"].ReadOnly = true;
            naviDataGridView2.Columns["Gv2BGAMT1"].ReadOnly = true;
            naviDataGridView2.Columns["Gv2BGBMT2"].ReadOnly = true;
            naviDataGridView2.Columns["Gv2BGCMT3"].ReadOnly = true;
            naviDataGridView2.Columns["Gv2BGCMT1"].ReadOnly = true;
            naviDataGridView2.Columns["Gv2BGREM"].ReadOnly = true;
            naviDataGridView1.Focus();
            跑馬燈();
        }
        private void 跑馬燈()
        {
            // 抓金萱最近開放的日期
            DataTable 開啟日 = new DataTable();
            Ad = new SqlDataAdapter(" SELECT FULLDATE,預算申請 FROM PEN007 A WHERE ISNULL(預算申請,'') <> '' AND PEN007001 = '1' " +
                                    " AND PEN007006 = '0' AND 預算申請 IN ('A','B') AND " +
                                    " CONVERT(VARCHAR(20), FULLDATE, 21) LIKE SUBSTRING(CONVERT(VARCHAR(20), (SELECT TOP 1 FULLDATE FROM PEN007 A WHERE ISNULL(預算申請, '') <> '' " +
                                    " AND PEN007001 = '1' AND PEN007006 = '0' AND 預算申請 IN('A', 'B') ORDER BY A.FULLDATE DESC), 21), 1, 7) + '%' " +
                                    " AND CONVERT(INT,SUBSTRING(CONVERT(VARCHAR(10),FULLDATE,20),9,2)) < 20 ORDER BY FULLDATE  ", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.Text;
            Ad.SelectCommand.CommandTimeout = 600;
            開啟日.Clear();
            Ad.Fill(開啟日);

            int count = 開啟日.Rows.Count - 1;
            string 起始日 = 開啟日.Rows[0]["FULLDATE"].ToString().Substring(0, 10);
            string 結束日 = 開啟日.Rows[count]["FULLDATE"].ToString().Substring(0, 10);
            string 跑馬燈 = "";

            int 預算月份 = Convert.ToInt32(Convert.ToDateTime(起始日).ToString("yyyy/MM/dd").Substring(0, 4) + Convert.ToDateTime(起始日).ToString("yyyy/MM/dd").Substring(5, 2)) + 1;

            // 如果超過最後一天，抓還未主管簽核的部門
            DataTable 訊息 = new DataTable();
            Ad = new SqlDataAdapter("PUR2007NET_主管未簽", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = 預算月份;
            訊息.Clear();
            Ad.Fill(訊息);


            GonGinCheckOfDataDuplication 公告 = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString, "AMDCONFIG", "FTYPE", "FVALUE", "FVALUE", " FID = '574' ");

            // 如果FTYPE不為空，表示有緊急公告，否則照原先公告內容
            if (string.IsNullOrEmpty(公告.傳回值.Trim()))
            {
                if (訊息.Rows[0]["訊息"].ToString() == "")
                    跑馬燈 = "謝謝大家配合！！！";
                else
                {
                    跑馬燈 = 預算月份.ToString() + "月預算輸入ㄛ!  預計在" + 起始日 + "開放 請於" + 結束日 + "中午前 交回資訊部! 謝謝! 請記得要做主管確認再交回,並請嚴守時間!\r\n" + 訊息.Rows[0]["訊息"].ToString();
                }
            }
            else
                跑馬燈 = 公告.傳回值二;

            naviMarquee1.Refresh();
            naviMarquee1.Text = 跑馬燈;
        }

        private void Month()
        {
            月份起.Text = DateTime.Now.ToString("yyyyMM");
            //月份迄.Text = DateTime.Now.ToString("yyyyMM");
        }
        private void DEPT()
        {
            //DataTable DEPT_下拉 = new DataTable();

            //SqlDataAdapter Ad = new SqlDataAdapter(" SELECT DISTINCT BGDEP,DEPTNAME FROM BUGDA A,AMDDEPT B WHERE A.BGDEP = B.DEPTNO AND ISNULL(PDEPTNO,'') <> '' AND upper(DEPTNAME) <> 'N' ORDER BY A.BGDEP ", GonGinVariable.SqlConnectString);
            //Ad.SelectCommand.CommandType = CommandType.Text;
            //Ad.SelectCommand.Parameters.Clear();
            //Ad.SelectCommand.CommandTimeout = 600;
            //DEPT_下拉.Clear();
            //Ad.Fill(DEPT_下拉);

            //int A = DEPT_下拉.Rows.Count;
            //DEPT_下拉.Rows.Add();

            //// 加入全部選項
            //DEPT_下拉.Rows[A]["BGDEP"] = "";
            //DEPT_下拉.Rows[A]["DEPTNAME"] = "全部";

            //// order by
            //DEPT_下拉.DefaultView.Sort = "BGDEP";

            //naviMutiComboBox1.DataSource = DEPT_下拉;
            //naviMutiComboBox1.ValueMember = "BGDEP";
            //naviMutiComboBox1.DisplayMember = "DEPTNAME";
            //naviMutiComboBox1.SelectedValue = "BGDEP";
        }

        private void 查詢_Click(object sender, EventArgs e)
        {
            checkBox2_Click(null, null);
            月份起.Focus();
            naviDataGridView1.Focus();

            跑馬燈();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 預算簽核申請_Click(object sender, EventArgs e)
        {
            string 申請狀態 = string.Empty;

            // 在PEN007控管可以申請的期限
            //if (GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) != "93")
            //{
            GonGinCheckOfDataDuplication 日期 = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString, "PEN007", "預算申請", "預算申請", "預算申請", " PEN007001='1' AND FULLDATE = '" + DateTime.Now.ToShortDateString() + "' ");
            if (日期.傳回值 != "A" && 日期.傳回值 != "B")
            {
                MessageBox.Show("不在申請期限內，無法申請!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            申請狀態 = 日期.傳回值;
            //}

            string 上階部門 = string.Empty;
            string 主管助理 = string.Empty;
            string 部門 = string.Empty;

            DataTable Dt_助理 = new DataTable();

            // 判斷助理3、4，是否可以申請跨部門、或自己部門
            if (GonGinVariable.ApplicationUserDeptNo == "9600")
            {
                Ad = new SqlDataAdapter(" SELECT DEPTNO,DEPTNAME FROM AMDDEPT WHERE DEPTSUPR1 = @DEPTSUPR3 OR  DEPTSUPR2 = @DEPTSUPR3 OR DEPTSUPR3 = @DEPTSUPR3 OR  DEPTSUPR4 = @DEPTSUPR3 UNION " +
                                        " SELECT DEPTNO, DEPTNAME FROM AMDDEPT WHERE DEPTNO = '9990' ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@DEPTSUPR3", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                Dt_助理.Clear();
                Ad.Fill(Dt_助理);
            }
            else
            {
                Ad = new SqlDataAdapter(" SELECT DEPTNO,DEPTNAME FROM AMDDEPT WHERE DEPTSUPR1 = @DEPTSUPR3 OR  DEPTSUPR2 = @DEPTSUPR3 OR DEPTSUPR3 = @DEPTSUPR3 OR  DEPTSUPR4 = @DEPTSUPR3 ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@DEPTSUPR3", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                Dt_助理.Clear();
                Ad.Fill(Dt_助理);
            }

            // 如果Dt_助理有資料，表示有可申請的部門
            if (Dt_助理.Rows.Count > 0)
            {
                選擇部門 pop = new 選擇部門(Dt_助理, "N");
                if (pop.ShowDialog() == DialogResult.Yes)
                {
                    部門 = pop.部門;

                    申請資料判斷 Form = new 申請資料判斷(部門, 申請狀態);
                    if (Form.ShowDialog() == DialogResult.Yes)
                        Query();
                }
            }
            else
            {
                部門 = GonGinVariable.ApplicationUserDeptNo;

                if (GonGinVariable.SectionName.Contains("微奈"))
                {
                    // 只有上階部門可申請
                    // 微奈的部門資料只有上階部門
                    _QueryComm.CommandText = " SELECT UPDEPT FROM AMDDEPT WHERE DEPTNO = @DEPTNO ";
                    _QueryComm.CommandType = CommandType.Text;
                    _QueryComm.Parameters.Clear();
                    _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = 部門.Substring(0, 2) + "00";
                    _QueryDr = _QueryComm.ExecuteReader();

                    if (_QueryDr.HasRows)
                    {
                        _QueryDr.Read();
                        上階部門 = _QueryDr["UPDEPT"].ToString();
                    }
                    _QueryDr.Close();
                }
                // 沒有部門可選，表示沒有申請權限
                else
                {
                    // 只有上階部門可申請
                    _QueryComm.CommandText = " SELECT UPDEPT FROM AMDDEPT WHERE DEPTNO = @DEPTNO ";
                    _QueryComm.CommandType = CommandType.Text;
                    _QueryComm.Parameters.Clear();
                    _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = 部門;
                    _QueryDr = _QueryComm.ExecuteReader();

                    if (_QueryDr.HasRows)
                    {
                        _QueryDr.Read();
                        上階部門 = _QueryDr["UPDEPT"].ToString();
                    }
                    _QueryDr.Close();
                }

                // 倉儲特殊==================================================
                //if (GonGinVariable.ApplicationUserDeptNo == "9720")
                //{
                //    _QueryComm.CommandText = " SELECT 主管助理 = CONCAT((SELECT PNAME FROM PERSON B WHERE B.PENNO = A.DEPTSUPR) + '、', " +
                //                             " CASE ISNULL(DEPTSUPR1,'') WHEN '' THEN '' ELSE(SELECT PNAME FROM PERSON B WHERE B.PENNO = A.DEPTSUPR1) + '、' END, " +
                //                             " CASE ISNULL(DEPTSUPR2,'') WHEN '' THEN '' ELSE(SELECT PNAME FROM PERSON B WHERE B.PENNO = A.DEPTSUPR2) + '、' END, " +
                //                             " CASE ISNULL(DEPTSUPR3,'') WHEN '' THEN '' ELSE(SELECT PNAME FROM PERSON B WHERE B.PENNO = A.DEPTSUPR3) + '、' END, " +
                //                             " CASE ISNULL(DEPTSUPR4,'') WHEN '' THEN '' ELSE(SELECT PNAME FROM PERSON B WHERE B.PENNO = A.DEPTSUPR4) + '、' END) " +
                //                             " FROM AMDDEPT A WHERE DEPTNO = '9720' ";
                //    _QueryComm.CommandType = CommandType.Text;
                //    _QueryComm.Parameters.Clear();
                //    //_QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = 上階部門;
                //    _QueryDr = _QueryComm.ExecuteReader();

                //    if (_QueryDr.HasRows)
                //    {
                //        _QueryDr.Read();
                //        主管助理 = _QueryDr["主管助理"].ToString();
                //    }
                //    _QueryDr.Close();
                //}
                //else
                //{
                _QueryComm.CommandText = " SELECT cast(PNAME AS NVARCHAR ) + '、' FROM ( " +
                                         " SELECT DISTINCT DEPTSUPR,PNAME = (SELECT PNAME FROM PERSON C WHERE C.PENNO = B.DEPTSUPR) FROM( " +
                                         " SELECT DEPTSUPR FROM AMDDEPT A WHERE DEPTNO = @DEPTNO " +
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
                    主管助理 = 主管助理.TrimEnd('、');
                }
                _QueryDr.Close();
                //}

                if (!主管助理.Contains(GonGinVariable.ApplicationUserName))
                {
                    MessageBox.Show("部門" + 上階部門 + "只有以下人員\r\n" + 主管助理 + "，可申請!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                申請資料判斷 Form = new 申請資料判斷(上階部門, 申請狀態);
                if (Form.ShowDialog() == DialogResult.Yes)
                    Query();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GonGinCommonForm.PopWindowOfPerson a = new GonGinCommonForm.PopWindowOfPerson(GonGinVariable.SqlConnectString, "");
            if (a.ShowDialog() == DialogResult.Yes)
            {
                GonGinVariable.ApplicationUser = a.員工編號;
                GonGinVariable.ApplicationUserName = a.員工名稱;

                naviStatusStrip1.StatusOfLoginName = GonGinVariable.ApplicationUserName;
                Query();
            }
        }

        private void Query()
        {
            //==================================================================================
            Ad = new SqlDataAdapter("PUR2007NET", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.Parameters.Add("@登入者", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
            Ad.SelectCommand.Parameters.Add("@月份起", SqlDbType.VarChar).Value = 月份起.Text;
            Ad.SelectCommand.Parameters.Add("@DEPT", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUserDeptNo;
            //if (naviMutiComboBox1.SelectedValue == null)
            //    Ad.SelectCommand.Parameters.Add("@UPDEPT", SqlDbType.VarChar).Value = "";
            //else
            //    Ad.SelectCommand.Parameters.Add("@UPDEPT", SqlDbType.VarChar).Value = naviMutiComboBox1.SelectedValue.ToString();
            Ds.Clear();
            Ad.Fill(Ds);

            naviDataGridView1.DataSource = Ds.Tables[0].DefaultView;
            naviDataGridView2.DataSource = Ds.Tables[1].DefaultView;

            //int i = 0;
            //foreach (DataGridViewRow oRow in naviDataGridView1.Rows)
            //{
            //    _QueryComm.CommandText = " SELECT 單據單號,起始流程,SCTRL,建立日 FROM FLOW_STEP WHERE 流程名稱 = '部門預算' AND 單據單號=@單據單號 AND SCTRL = 'Y' ORDER BY 起始流程 DESC ";
            //    _QueryComm.CommandType = CommandType.Text;
            //    _QueryComm.Parameters.Clear();
            //    _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviDataGridView1.Rows[i].Cells["Gv1單據單號"].Value.ToString();
            //    _QueryDr = _QueryComm.ExecuteReader();

            //    if (_QueryDr.HasRows)
            //    {
            //        _QueryDr.Read();
            //        //MessageBox.Show(_QueryDr["起始流程"].ToString() + "||" + _QueryDr["SCTRL"].ToString());
            //        if (_QueryDr["起始流程"].ToString() == "2.000" && _QueryDr["SCTRL"].ToString() == "Y" && GonGinVariable.ApplicationUserDeptNo == "9600")
            //        {
            //            naviDataGridView1.Rows[i].Cells["Gv1列印"].ReadOnly = false;
            //            naviDataGridView1.Rows[i].Cells["Gv1列印"].Value = "列印";
            //            naviDataGridView1.Rows[i].Cells["Gv1列印"].Style.BackColor = Color.FromArgb(238, 99, 99);

            //            naviDataGridView1.Rows[i].Cells["Gv1結算"].ReadOnly = false;
            //            naviDataGridView1.Rows[i].Cells["Gv1結算"].Value = "結算";
            //            naviDataGridView1.Rows[i].Cells["Gv1結算"].Style.BackColor = Color.FromArgb(255, 236, 139);

            //            naviDataGridView1.Rows[i].Cells["Gv1修改"].ReadOnly = true;
            //            naviDataGridView1.Rows[i].Cells["Gv1修改"].Value = "";
            //            naviDataGridView1.Rows[i].Cells["Gv1修改"].Style.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            //MessageBox.Show(_QueryDr["起始流程"].ToString() + "||" + _QueryDr["SCTRL"].ToString());
            //            naviDataGridView1.Rows[i].Cells["Gv1列印"].ReadOnly = true;
            //            naviDataGridView1.Rows[i].Cells["Gv1列印"].Value = "";
            //            naviDataGridView1.Rows[i].Cells["Gv1列印"].Style.BackColor = Color.White;

            //            naviDataGridView1.Rows[i].Cells["Gv1結算"].ReadOnly = true;
            //            naviDataGridView1.Rows[i].Cells["Gv1結算"].Value = "";
            //            naviDataGridView1.Rows[i].Cells["Gv1結算"].Style.BackColor = Color.White;

            //            if (naviDataGridView1.Rows[i].Cells["Gv1簽核權限"].Value.Equals("Y"))
            //            {
            //                naviDataGridView1.Rows[i].Cells["Gv1修改"].ReadOnly = false;
            //                naviDataGridView1.Rows[i].Cells["Gv1修改"].Value = "修改";
            //                naviDataGridView1.Rows[i].Cells["Gv1修改"].Style.BackColor = Color.FromArgb(135, 206, 250);
            //            }
            //            else
            //            {
            //                naviDataGridView1.Rows[i].Cells["Gv1修改"].ReadOnly = true;
            //                naviDataGridView1.Rows[i].Cells["Gv1修改"].Value = "";
            //                naviDataGridView1.Rows[i].Cells["Gv1修改"].Style.BackColor = Color.White;
            //            }
            //        }
            //    }
            //    // 沒資料表示都還沒人簽核
            //    else
            //    {
            //        naviDataGridView1.Rows[i].Cells["Gv1列印"].ReadOnly = true;
            //        naviDataGridView1.Rows[i].Cells["Gv1列印"].Value = "";
            //        naviDataGridView1.Rows[i].Cells["Gv1列印"].Style.BackColor = Color.White;

            //        naviDataGridView1.Rows[i].Cells["Gv1結算"].ReadOnly = true;
            //        naviDataGridView1.Rows[i].Cells["Gv1結算"].Value = "";
            //        naviDataGridView1.Rows[i].Cells["Gv1結算"].Style.BackColor = Color.White;

            //        naviDataGridView1.Rows[i].Cells["Gv1修改"].ReadOnly = false;
            //        naviDataGridView1.Rows[i].Cells["Gv1修改"].Value = "修改";
            //        naviDataGridView1.Rows[i].Cells["Gv1修改"].Style.BackColor = Color.FromArgb(135, 206, 250);
            //    }
            //    i++;
            //    _QueryDr.Close();
            //}
        }

        private void naviDateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            月份起.Text = naviDateTimePicker4.Value.ToString("yyyyMM");
        }

        private void naviDataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;


            TT = Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString();

            if (checkBox2.Checked == false)
            {
                if (Ds.Tables[1].Select(" 單據單號 = '" + Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString() + "'").Count() == 0)
                {
                    Dt_明細 = Ds.Tables[1].Clone();
                    naviDataGridView2.DataSource = Dt_明細.DefaultView;
                }
                else
                {
                    Dt_明細 = Ds.Tables[1].Select(" 單據單號 = '" + Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString() + "'").CopyToDataTable();
                    naviDataGridView2.DataSource = Dt_明細.DefaultView;
                }
            }
            else
            {
                if (Ds.Tables[1].Select(" 單據單號 = '" + Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString() + "'").Count() == 0)
                {
                    Dt_明細 = Ds.Tables[6].Clone();
                    naviDataGridView2.DataSource = Dt_明細.DefaultView;
                }
                else
                {
                    Dt_明細 = Ds.Tables[6].Select(" 單據單號 = '" + Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString() + "'").CopyToDataTable();
                    naviDataGridView2.DataSource = Dt_明細.DefaultView;
                }
            }

            // 新增計算合計明細的Row
            if (Dt_明細.Rows.Count > 0)
            {
                Dt_明細.Rows.Add();
                int 預算總和 = 0;
                int 追加總和 = 0;
                int 追加後預算總和 = 0;

                foreach (DataRow oRow in Dt_明細.Rows)
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

            //if (Dgv.Rows[e.RowIndex].Cells["Gv1BGTYPE"].Value.Equals("挪用"))
            //    naviDataGridView2.Columns["Gv2BGCMT3"].Visible = true;
        }

        private void naviDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            //GonGinCheckOfDataDuplication 可輸入逾期加班單 = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString, 
            //    "FLOW_STEP", "MAX(起始流程)", "MAX(起始流程)", "MAX(起始流程)", " 流程名稱 = '部門預算' AND 單據單號='" + Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString() + "'");

            if (Dgv.Rows[e.RowIndex].Cells["Gv1SCTRL"].Value.ToString() != "C")
            {
                _QueryComm.CommandText = " SELECT 起始流程=MIN(起始流程) FROM FLOW_STEP WHERE 流程名稱 = '部門預算' AND 單據單號=@單據單號 AND SCTRL = 'N'";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString();
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    if (_QueryDr["起始流程"].ToString() != Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString())
                    {
                        MessageBox.Show("資料已變更，系統準備重新查詢 !!", "更新訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _QueryDr.Close();
                        查詢_Click(null, null);
                        return;
                    }
                }
                _QueryDr.Close();
            }


            string _簽核權限 = string.Empty;
            if (Dgv.Columns[e.ColumnIndex].Name.Equals("Gv1簽核"))
            {
                //審核();
                if (Dgv.Rows[e.RowIndex].Cells["Gv1簽核權限"].Value.ToString().Equals("Y"))
                    _簽核權限 = "Y";
                else
                    _簽核權限 = "N";

                GonGinCommonForm.流程_審核作業 審核 = new GonGinCommonForm.流程_審核作業("預算系統", "部門預算", naviDataGridView1.CurrentRow.Cells["Gv1單據單號"].Value.ToString(), _簽核權限, "", false);
                DialogResult FlowDialog1 = 審核.ShowDialog();


                if (FlowDialog1 == DialogResult.No)
                {
                    SqlTransaction transaction = _QueryConn.BeginTransaction("Transaction");
                    _QueryComm.Transaction = transaction;

                    foreach (DataRow oRows in Ds.Tables[3].Select("單據單號='" + Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString() + "'"))
                    {
                        _QueryComm.CommandText = " UPDATE FLOW_STEP SET SCTRL='N',狀態='不准',簽核意見=@簽核意見,建立者 = @建立者,建立日 = GETDATE() WHERE UID=@UID AND 單據單號=@單據單號 ";
                        _QueryComm.Parameters.Clear();
                        _QueryComm.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(oRows["UID"].ToString());
                        _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = oRows["單據單號"].ToString();
                        _QueryComm.Parameters.Add("@簽核意見", SqlDbType.VarChar).Value = 審核.簽核意見;
                        _QueryComm.Parameters.Add("@建立者", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                        _QueryComm.ExecuteNonQuery();

                        // 將簽核的當筆資料,存入錄簽核履歷表中
                        _QueryComm.CommandText = " INSERT INTO FLOW_HISTORY ( UID,PUID,簽核流程,流程名稱,單據單號,起始流程,結束流程,送簽人,簽核人,SCTRL,建立日,狀態,簽核意見) " +
                                                 " SELECT NEWID(),UID,簽核流程,流程名稱,單據單號,起始流程,結束流程,'',@簽核人,'N',GETDATE(),'不准',@簽核意見 FROM FLOW_STEP WHERE UID = @UID ";
                        _QueryComm.Parameters.Clear();
                        _QueryComm.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(oRows["UID"].ToString());
                        _QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                        _QueryComm.Parameters.Add("@簽核意見", SqlDbType.VarChar).Value = 審核.簽核意見.ToString();
                        _QueryComm.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    this.DialogResult = DialogResult.Yes;
                    MessageBox.Show("退回完成 !!", "簽核訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Query();
                    月份起.Focus();
                    naviDataGridView1.Focus();
                }
                else if (FlowDialog1 == DialogResult.Yes)
                {
                    if (Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString() == "1.000")
                    {
                        if (MessageBox.Show("是否已經列印過此筆資料，且主管已簽名?", "核准訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            return;
                    }

                    SqlTransaction transaction = _QueryConn.BeginTransaction("Transaction");
                    _QueryComm.Transaction = transaction;

                    if (Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString() == "0.000")
                    {
                        string 上階部門 = "";
                        string 主管 = "";
                        _QueryComm.CommandText = " SELECT UPDEPT FROM AMDDEPT WHERE DEPTNO = @DEPTNO ";
                        _QueryComm.CommandType = CommandType.Text;
                        _QueryComm.Parameters.Clear();
                        _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = Dgv.Rows[e.RowIndex].Cells["Gv1BGDEP"].Value.ToString();
                        _QueryDr = _QueryComm.ExecuteReader();

                        if (_QueryDr.HasRows)
                        {
                            _QueryDr.Read();
                            上階部門 = _QueryDr["UPDEPT"].ToString();
                        }
                        _QueryDr.Close();

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

                        _QueryComm.CommandText = " UPDATE FLOW_STEP SET 簽核人 = @簽核人 WHERE 起始流程 = '1.000' AND 單據單號 = @單據單號 AND 流程名稱 = '部門預算'";
                        _QueryComm.Parameters.Clear();
                        _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = naviDataGridView1.CurrentRow.Cells["Gv1單據單號"].Value.ToString();
                        _QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = 主管 + ";1819;";
                        _QueryComm.ExecuteNonQuery();
                    }


                    foreach (DataRow oRow in Ds.Tables[2].Select("單據單號='" + Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString() + "'AND 起始流程 = '" +
                         Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString() + "'"))
                    {
                        // 當站(在站)單據
                        _QueryComm.CommandText = "UPDATE FLOW_STEP SET SCTRL='Y',簽核意見=@簽核意見,狀態='准',建立者 = @建立者,建立日 = GETDATE() WHERE UID=@UID AND 單據單號=@單據單號";
                        _QueryComm.Parameters.Clear();
                        _QueryComm.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(oRow["UID"].ToString());
                        _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = oRow["單據單號"].ToString();
                        _QueryComm.Parameters.Add("@簽核意見", SqlDbType.VarChar).Value = 審核.簽核意見;
                        _QueryComm.Parameters.Add("@建立者", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                        _QueryComm.ExecuteNonQuery();

                        // 下一站更新送簽人 (將簽核人更新到下一筆的送簽人欄位)
                        foreach (DataRow oDetRow in Ds.Tables[4].Select("單據單號='" + oRow["單據單號"].ToString() + "'"))
                        {
                            _QueryComm.CommandText = "UPDATE FLOW_STEP SET 送簽人=@送簽人 WHERE UID=@UID AND 單據單號=@單據單號 ";
                            _QueryComm.Parameters.Clear();
                            _QueryComm.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(oDetRow["UID"].ToString());
                            _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = oDetRow["單據單號"].ToString();
                            _QueryComm.Parameters.Add("@送簽人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                            _QueryComm.ExecuteNonQuery();
                        }

                        // 將簽核的當筆資料,存入錄簽核履歷表中
                        _QueryComm.CommandText = "INSERT INTO FLOW_HISTORY ( UID,PUID,簽核流程,流程名稱,單據單號,起始流程,結束流程,送簽人,簽核人,SCTRL,建立日,狀態,簽核意見) " +
                                                 "SELECT NEWID(),UID,簽核流程,流程名稱,單據單號,起始流程,結束流程,送簽人,@簽核人,SCTRL,GETDATE(),'准',@簽核意見 FROM FLOW_STEP WHERE UID = @UID";
                        _QueryComm.Parameters.Clear();
                        _QueryComm.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(oRow["UID"].ToString());
                        _QueryComm.Parameters.Add("@簽核人", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                        _QueryComm.Parameters.Add("@簽核意見", SqlDbType.VarChar).Value = 審核.簽核意見.ToString();
                        _QueryComm.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    this.DialogResult = DialogResult.Yes;
                    if (Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString() == "0.000")
                        MessageBox.Show("簽核完成，\r\n你列印了沒 !! \r\n \r\n你列印了沒 !! \r\n \r\n你列印了沒 !! \r\n \r\n你列印了沒 !! \r\n \r\n你列印了沒 !!", "簽核訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("簽核完成 !!", "簽核訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Query();
                    月份起.Focus();
                    naviDataGridView1.Focus();
                }
            }
            // 修改Button=================================================================================================
            else if (Dgv.Columns[e.ColumnIndex].Name.Equals("Gv1修改") && Dgv.Rows[e.RowIndex].Cells["Gv1簽核權限"].Value.ToString().Equals("Y") &&
                    Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString() != "3.000")
            {
                // 抓取明細
                DataTable Dt_新明細 = new DataTable();
                Ad = new SqlDataAdapter(" SELECT UID,單據單號,BGDEP,BGDEPNAME=(SELECT DEPTNAME FROM AMDDEPT B WHERE B.DEPTNO=A.BGDEP),BGYM,BUGNO," +
                                        " A.BUGNO,BUGNA = (SELECT BUGNA FROM BUGNA C WHERE C.BUGNO = A.BUGNO)," +
                                        " BGACNO = (SELECT ACCNO FROM BUGNA C WHERE C.BUGNO = A.BUGNO),BGAMT1,BGBMT2,BGCMT3,追加後預算,未核准追加 = ''," +
                                        " BGREM,CRUSER,CRDATE,AMDUSR,AMDDAY,BGTYPE FROM BUGDA_簽核明細 A WHERE 單據單號 = @單據單號 ORDER BY A.BUGNO ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString();
                Dt_新明細.Clear();
                Ad.Fill(Dt_新明細);


                BUGDA_修改 form = new BUGDA_修改(Dt_新明細, Dgv.Rows[e.RowIndex].Cells["Gv1BGTYPE"].Value.ToString(), Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString(),
                                                 Dgv.Rows[e.RowIndex].Cells["Gv1BGYM"].Value.ToString(), Dgv.Rows[e.RowIndex].Cells["Gv1BGDEP"].Value.ToString(),
                                                 Dgv.Rows[e.RowIndex].Cells["Gv1急件"].Value.ToString());
                if (form.ShowDialog() == DialogResult.Yes)
                    Query();
                月份起.Focus();
                naviDataGridView1.Focus();
            }

            // 結算Button=================================================================================================
            if (Dgv.Columns[e.ColumnIndex].Name.Equals("Gv1結算") && Dgv.Rows[e.RowIndex].Cells["Gv1簽核權限"].Value.ToString().Equals("Y") &&
                Dgv.Rows[e.RowIndex].Cells["Gv1SCTRL"].Value.ToString().Equals("Y") && Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString() == "3.000")
            {
                if (DialogResult.Yes == MessageBox.Show("單據單號：" + Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString() + " 是否確定結算? ", "警告",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    SqlTransaction transaction = _QueryConn.BeginTransaction("Transaction");
                    _QueryComm.Transaction = transaction;

                    DataTable Dt_更新 = new DataTable();
                    Ad = new SqlDataAdapter("SELECT * FROM BUGDA_簽核明細 WHERE 單據單號=@單據單號", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.Text;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString();
                    Dt_更新.Clear();
                    Ad.Fill(Dt_更新);

                    // UPDATE BUGDA
                    if (Dgv.Rows[e.RowIndex].Cells["Gv1BGTYPE"].Value.ToString() == "預算")
                    {
                        foreach (DataRow oRow in Dt_更新.Rows)
                        {
                            _QueryComm.CommandText = " UPDATE BUGDA SET BGAMT1=@BGAMT1,AMDUSR=@AMDUSR,AMDDAY=GETDATE() WHERE BGDEP=@BGDEP AND BGYM=@BGYM AND BGNO=@BGNO";
                            _QueryComm.Parameters.Clear();
                            if (string.IsNullOrEmpty(oRow["BGAMT1"].ToString().Trim()))
                                _QueryComm.Parameters.Add("@BGAMT1", SqlDbType.Decimal).Value = DBNull.Value;
                            else
                                _QueryComm.Parameters.Add("@BGAMT1", SqlDbType.Decimal).Value = Convert.ToDecimal(oRow["BGAMT1"].ToString());
                            _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = oRow["BGDEP"].ToString();
                            _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = oRow["BGYM"].ToString();
                            _QueryComm.Parameters.Add("@BGNO", SqlDbType.VarChar).Value = oRow["BUGNO"].ToString();
                            _QueryComm.Parameters.Add("@AMDUSR", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                            _QueryComm.ExecuteNonQuery();
                        }
                    }
                    else if (Dgv.Rows[e.RowIndex].Cells["Gv1BGTYPE"].Value.ToString() == "追加")
                    {
                        foreach (DataRow oRow in Dt_更新.Rows)
                        {
                            int 前次預算 = 0;

                            _QueryComm.CommandText = " SELECT BGBMT2 FROM BUGDA WHERE BGYM = @BGYM AND BGDEP = @BGDEP AND BGNO=@BGNO ";
                            _QueryComm.CommandType = CommandType.Text;
                            _QueryComm.Parameters.Clear();
                            _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = oRow["BGDEP"].ToString();
                            _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = oRow["BGYM"].ToString();
                            _QueryComm.Parameters.Add("@BGNO", SqlDbType.VarChar).Value = oRow["BUGNO"].ToString();
                            _QueryDr = _QueryComm.ExecuteReader();

                            if (_QueryDr.HasRows)
                            {
                                _QueryDr.Read();
                                前次預算 = Convert.ToInt32(_QueryDr["BGBMT2"].ToString());
                            }
                            _QueryDr.Close();

                            //==============================================================
                            _QueryComm.CommandText = " UPDATE BUGDA SET BGBMT2=@BGBMT2,AMDUSR=@AMDUSR,AMDDAY=GETDATE() WHERE BGDEP=@BGDEP AND BGYM=@BGYM AND BGNO=@BGNO ";
                            _QueryComm.Parameters.Clear();
                            if (string.IsNullOrEmpty(oRow["BGBMT2"].ToString().Trim()))
                                _QueryComm.Parameters.Add("@BGBMT2", SqlDbType.Decimal).Value = DBNull.Value;
                            else
                                _QueryComm.Parameters.Add("@BGBMT2", SqlDbType.Decimal).Value = 前次預算 + Convert.ToDecimal(oRow["BGBMT2"].ToString());
                            _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = oRow["BGDEP"].ToString();
                            _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = oRow["BGYM"].ToString();
                            _QueryComm.Parameters.Add("@BGNO", SqlDbType.VarChar).Value = oRow["BUGNO"].ToString();
                            _QueryComm.Parameters.Add("@AMDUSR", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                            _QueryComm.ExecuteNonQuery();
                        }
                    }

                    // 結算資料更改SCTRL變C 
                    _QueryComm.CommandText = " UPDATE BUGDA_簽核 SET SCTRL = 'C',GRUSER=@GRUSER,GRDATE=GETDATE() WHERE 單據單號=@單據單號";
                    _QueryComm.Parameters.Clear();
                    _QueryComm.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString();
                    _QueryComm.Parameters.Add("@GRUSER", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                    _QueryComm.ExecuteNonQuery();

                    transaction.Commit();
                    this.DialogResult = DialogResult.Yes;
                    MessageBox.Show("結算完成 !!", "簽核訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Query();
                    月份起.Focus();
                    naviDataGridView1.Focus();
                }
            }

            // 列印Button=================================================================================================
            if (Dgv.Columns[e.ColumnIndex].Name.Equals("Gv1列印") && (GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "93" || GonGinVariable.ApplicationUser == "1298"))
            {
                列印_Button(Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString(), Dgv.Rows[e.RowIndex].Cells["Gv1BGYM"].Value.ToString(),
                                Dgv.Rows[e.RowIndex].Cells["Gv1急件"].Value.ToString(), Dgv.Rows[e.RowIndex].Cells["Gv1BGDEPNAME"].Value.ToString(), Dgv.Rows[e.RowIndex].Cells["Gv1BGDEP"].Value.ToString(),
                                Dgv.Rows[e.RowIndex].Cells["Gv1BGTYPE"].Value.ToString());
            }
            else
            {
                if (Dgv.Columns[e.ColumnIndex].Name.Equals("Gv1列印") && Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString().Equals("1.000") &&
                    Dgv.Rows[e.RowIndex].Cells["Gv1SCTRL"].Value.ToString().Equals("N"))
                {
                    列印_Button(Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Value.ToString(), Dgv.Rows[e.RowIndex].Cells["Gv1BGYM"].Value.ToString(),
                                Dgv.Rows[e.RowIndex].Cells["Gv1急件"].Value.ToString(), Dgv.Rows[e.RowIndex].Cells["Gv1BGDEPNAME"].Value.ToString(), Dgv.Rows[e.RowIndex].Cells["Gv1BGDEP"].Value.ToString(),
                                Dgv.Rows[e.RowIndex].Cells["Gv1BGTYPE"].Value.ToString());
                }
            }
        }

        private void 列印_Button(string 單據單號, string BGYM, string 急件, string BGDEPNAME, string BGDEP, string BGTYPE)
        {
            //if (急件 == "")
            //    急件 = " ";

            DataSet 部門報表 = new DataSet();
            Ad = new SqlDataAdapter("PUR2007NET_部門報表", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.Parameters.Add("@單據單號", SqlDbType.VarChar).Value = 單據單號;
            部門報表.Clear();
            Ad.Fill(部門報表);

            int BGSORT1 = 0;
            GonGinCheckOfDataDuplication DataDuplication = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString.ToString(), "AMDDEPT", "BGSORT1", "BGSORT1", "BGSORT1", "DEPTNO = '" + BGDEP + "'");
            if (!string.IsNullOrEmpty(DataDuplication.傳回值.Trim()))
                BGSORT1 = Convert.ToInt32(DataDuplication.傳回值);



            部門報表_NEW Reporter1 = new 部門報表_NEW();
            //Reporter1.SetDataSource(Dt_明細);
            Reporter1.Database.Tables[0].SetDataSource(部門報表.Tables[0]);
            Reporter1.Database.Tables[1].SetDataSource(部門報表.Tables[1]);
            Form ReportForm = new Form();
            CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            CryReport.ReportSource = Reporter1;
            CryReport.Dock = DockStyle.Fill;
            Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
            Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
            Reporter1.ParameterFields["急件"].CurrentValues.AddValue(急件);
            Reporter1.ParameterFields["部門代號"].CurrentValues.AddValue(BGDEP);
            Reporter1.ParameterFields["部門"].CurrentValues.AddValue(BGDEPNAME);
            Reporter1.ParameterFields["BGSORT1"].CurrentValues.AddValue(BGSORT1);
            Reporter1.ParameterFields["單據單號"].CurrentValues.AddValue(單據單號);
            Reporter1.ParameterFields["月份"].CurrentValues.AddValue(BGYM.Substring(0, 4) + "年" + BGYM.Substring(4, 2) + "月");
            Reporter1.ParameterFields["項目"].CurrentValues.AddValue(BGTYPE);

            ReportForm.Controls.Add(CryReport);
            ReportForm.WindowState = FormWindowState.Maximized;
            ReportForm.Show();
            // 我們請國父來說說參選感言

            //Dt_明細.AcceptChanges();
            //if (狀態 == "預算")
            //{
            //    預算申請表 Reporter1 = new 預算申請表();
            //    Reporter1.SetDataSource(Dt_明細);
            //    Form ReportForm = new Form();
            //    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            //    CryReport.ReportSource = Reporter1;
            //    CryReport.Dock = DockStyle.Fill;
            //    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
            //    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
            //    Reporter1.ParameterFields["急件"].CurrentValues.AddValue(急件);
            //    Reporter1.ParameterFields["部門"].CurrentValues.AddValue(部門);
            //    Reporter1.ParameterFields["單據單號"].CurrentValues.AddValue(單據單號);
            //    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(月份);

            //    ReportForm.Controls.Add(CryReport);
            //    ReportForm.WindowState = FormWindowState.Maximized;
            //    ReportForm.Show();
            //}
            //else if (狀態 == "追加")
            //{
            //預算申請表 Reporter1 = new 預算申請表();
            //Reporter1.SetDataSource(Dt_明細);
            //Form ReportForm = new Form();
            //CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            //CryReport.ReportSource = Reporter1;
            //CryReport.Dock = DockStyle.Fill;
            //Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
            //Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);

            //ReportForm.Controls.Add(CryReport);
            //ReportForm.WindowState = FormWindowState.Maximized;
            //ReportForm.Show();
            //}
        }

        int 日期COLOR;
        private void naviDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;


            日期COLOR = Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["Gv1BGYM"].Value.ToString().Substring(5, 1));

            //Dgv.Rows[e.RowIndex].Cells["Gv1BGYM"].Style.ForeColor = Color.FromArgb(220 ,220, 220);

            if (日期COLOR % 2 == 0)
                Dgv.Rows[e.RowIndex].Cells["Gv1BGYM"].Style.BackColor = Color.FromArgb(238, 99, 99);
            else
                Dgv.Rows[e.RowIndex].Cells["Gv1BGYM"].Style.BackColor = Color.FromArgb(135, 206, 235);


            if (Dgv.Rows[e.RowIndex].Cells["Gv1SCTRL"].Value.ToString().Equals("C"))
            {
                Dgv.Rows[e.RowIndex].Cells["Gv1簽核"].ReadOnly = true;
                Dgv.Rows[e.RowIndex].Cells["Gv1簽核"].Value = "";
                Dgv.Rows[e.RowIndex].Cells["Gv1簽核"].Style.BackColor = Color.FromArgb(95, 158, 160);

                Dgv.Rows[e.RowIndex].Cells["Gv1修改"].ReadOnly = true;
                Dgv.Rows[e.RowIndex].Cells["Gv1修改"].Value = "";
                Dgv.Rows[e.RowIndex].Cells["Gv1修改"].Style.BackColor = Color.FromArgb(95, 158, 160);

                Dgv.Rows[e.RowIndex].Cells["Gv1結算"].ReadOnly = true;
                Dgv.Rows[e.RowIndex].Cells["Gv1結算"].Value = "";
                Dgv.Rows[e.RowIndex].Cells["Gv1結算"].Style.BackColor = Color.FromArgb(95, 158, 160);

                Dgv.Rows[e.RowIndex].Cells["Gv1列印"].ReadOnly = true;
                Dgv.Rows[e.RowIndex].Cells["Gv1列印"].Value = "";
                Dgv.Rows[e.RowIndex].Cells["Gv1列印"].Style.BackColor = Color.FromArgb(95, 158, 160);

                Dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(95, 158, 160);
            }
            else
            {
                if (Dgv.Rows[e.RowIndex].Cells["Gv1簽核權限"].Value.ToString().Equals("N"))
                {
                    Dgv.Rows[e.RowIndex].Cells["Gv1修改"].ReadOnly = true;
                    Dgv.Rows[e.RowIndex].Cells["Gv1修改"].Value = "";
                    Dgv.Rows[e.RowIndex].Cells["Gv1修改"].Style.BackColor = Color.White;
                    Dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(190, 190, 190);
                }
                else if (Dgv.Rows[e.RowIndex].Cells["Gv1簽核權限"].Value.ToString().Equals("Y"))
                {
                    Dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;

                    // 結算===============================================================================
                    if (Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString() == "3.000" &&
                        Dgv.Rows[e.RowIndex].Cells["Gv1SCTRL"].Value.ToString().Equals("Y"))
                    {
                        // 跑完流程不可修改
                        Dgv.Rows[e.RowIndex].Cells["Gv1修改"].ReadOnly = true;
                        Dgv.Rows[e.RowIndex].Cells["Gv1修改"].Value = "";
                        Dgv.Rows[e.RowIndex].Cells["Gv1修改"].Style.BackColor = Color.White;

                        Dgv.Rows[e.RowIndex].Cells["Gv1結算"].ReadOnly = false;
                        Dgv.Rows[e.RowIndex].Cells["Gv1結算"].Value = "結算";
                        Dgv.Rows[e.RowIndex].Cells["Gv1結算"].Style.BackColor = Color.FromArgb(255, 236, 139);
                    }
                    else
                    {
                        Dgv.Rows[e.RowIndex].Cells["Gv1修改"].ReadOnly = false;
                        Dgv.Rows[e.RowIndex].Cells["Gv1修改"].Value = "修改";
                        Dgv.Rows[e.RowIndex].Cells["Gv1修改"].Style.BackColor = Color.FromArgb(135, 206, 250);

                        Dgv.Rows[e.RowIndex].Cells["Gv1結算"].ReadOnly = true;
                        Dgv.Rows[e.RowIndex].Cells["Gv1結算"].Value = "";
                        Dgv.Rows[e.RowIndex].Cells["Gv1結算"].Style.BackColor = Color.White;
                    }
                }

                Dgv.Rows[e.RowIndex].Cells["Gv1簽核"].Value = "簽核";
                Dgv.Rows[e.RowIndex].Cells["Gv1簽核"].Style.BackColor = Color.FromArgb(102, 205, 170);


                // 列印===============================================================================
                // 資訊部
                if (GonGinVariable.ApplicationUserDeptNo.Substring(0, 2) == "93" || GonGinVariable.ApplicationUser == "1298")
                {
                    Dgv.Rows[e.RowIndex].Cells["Gv1列印"].ReadOnly = false;
                    Dgv.Rows[e.RowIndex].Cells["Gv1列印"].Value = "列印";
                    Dgv.Rows[e.RowIndex].Cells["Gv1列印"].Style.BackColor = Color.FromArgb(238, 99, 99);
                }
                else
                {

                    if (Dgv.Rows[e.RowIndex].Cells["Gv1在站流程"].Value.ToString() == "1.000" &&
                        Dgv.Rows[e.RowIndex].Cells["Gv1SCTRL"].Value.ToString().Equals("N"))
                    //&& Dgv.Rows[e.RowIndex].Cells["Gv1簽核權限"].Value.ToString().Equals("Y"))
                    {
                        Dgv.Rows[e.RowIndex].Cells["Gv1列印"].ReadOnly = false;
                        Dgv.Rows[e.RowIndex].Cells["Gv1列印"].Value = "列印";
                        Dgv.Rows[e.RowIndex].Cells["Gv1列印"].Style.BackColor = Color.FromArgb(238, 99, 99);
                    }
                    else
                    {
                        Dgv.Rows[e.RowIndex].Cells["Gv1列印"].ReadOnly = true;
                        Dgv.Rows[e.RowIndex].Cells["Gv1列印"].Value = "";
                        Dgv.Rows[e.RowIndex].Cells["Gv1列印"].Style.BackColor = Color.White;
                    }
                }
            }
            Dgv.Rows[e.RowIndex].Cells["Gv1單據單號"].Style.ForeColor = Color.FromArgb(24, 116, 205);
            Dgv.Rows[e.RowIndex].Cells["Gv1BGDEP"].Style.ForeColor = Color.FromArgb(24, 116, 205);
            Dgv.Rows[e.RowIndex].Cells["Gv1BGDEPNAME"].Style.ForeColor = Color.FromArgb(24, 116, 205);
        }

        private void 資料更新_Click(object sender, EventArgs e)
        {
            查詢_Click(null, null);
        }

        private void 列印總表_Click(object sender, EventArgs e)
        {
            #region
            //列印總表判斷 form = new 列印總表判斷();
            //if (form.ShowDialog() == DialogResult.Yes)
            //{
            //    Ad = new SqlDataAdapter("PUR2007NET_總表", GonGinVariable.SqlConnectString);
            //    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            //    Ad.SelectCommand.CommandTimeout = 600;
            //    Ad.SelectCommand.Parameters.Clear();
            //    Ad.SelectCommand.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd");
            //    Ad.SelectCommand.Parameters.Add("@Item", SqlDbType.VarChar).Value = form.Item;
            //    Ds_總表.Clear();
            //    Ad.Fill(Ds_總表);

            //    string Date = form.BGYM.Substring(0, 4) + "年" + form.BGYM.Substring(4, 2) + "月份";
            //    DataTable 公司預算 = Ds_總表.Tables[1].Select("序 = '1'").CopyToDataTable();
            //    DataTable 公司成本 = Ds_總表.Tables[1].Select("序 = '2'").CopyToDataTable();
            //    DataTable 庫房外購金額 = Ds_總表.Tables[1].Select("序 = '3'").CopyToDataTable();

            //    // 抓出未簽核至財務部的部門
            //    DataTable AA = new DataTable();
            //    Ad = new SqlDataAdapter(" SELECT DISTINCT 部門,DEPTNAME = (SELECT DEPTNAME FROM AMDDEPT D WHERE D.DEPTNO = C.部門), " +
            //                            " 單據單號 = (SELECT A.單據單號 FROM BUGDA_簽核 A, FLOW_STEP B WHERE C.部門 = A.BGDEP AND A.單據單號 = B.單據單號 AND  " +
            //                            " B.流程名稱 = '部門預算' AND B.起始流程 = '1.000' AND B.SCTRL = 'Y' AND A.SCTRL = 'Y' AND BGYM = @BGYM) " +
            //                            " FROM BUGNA_部門 C", GonGinVariable.SqlConnectString);
            //    Ad.SelectCommand.CommandType = CommandType.Text;
            //    Ad.SelectCommand.CommandTimeout = 600;
            //    Ad.SelectCommand.Parameters.Clear();
            //    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = form.BGYM;
            //    AA.Clear();
            //    Ad.Fill(AA);

            //    DataTable Dt_未完成 = AA.Select("ISNULL(單據單號,'') = ''").CopyToDataTable();
            //    string 部門 = string.Empty;
            //    foreach (DataRow item in Dt_未完成.Rows)
            //        部門 += item["DEPTNAME"] + "、";

            //    //
            //    if (Dt_未完成.Rows.Count > 0)
            //        MessageBox.Show("月份" + Date + "\r\n" + 部門 + "\r\n\r\n以上部門預算尚未簽核至財務部，\r\n 只可瀏覽報表，不可列印。", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    if (form.Item == "直接")
            //    {
            //        // 直接部門報表
            //        預算總表_直接 Reporter1 = new 預算總表_直接();
            //        Reporter1.Database.Tables[0].SetDataSource(Ds_總表.Tables[0]);
            //        Reporter1.Database.Tables[1].SetDataSource(公司預算);
            //        Reporter1.Database.Tables[2].SetDataSource(公司成本);
            //        Reporter1.Database.Tables[3].SetDataSource(庫房外購金額);
            //        Form ReportForm = new Form();
            //        CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            //        CryReport.ReportSource = Reporter1;
            //        CryReport.Dock = DockStyle.Fill;

            //        if (Dt_未完成.Rows.Count > 0)
            //            CryReport.ShowPrintButton = false;

            //        Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
            //        Reporter1.ParameterFields["Date"].CurrentValues.AddValue(Date);

            //        ReportForm.Controls.Add(CryReport);
            //        ReportForm.WindowState = FormWindowState.Maximized;
            //        ReportForm.Show();
            //    }
            //    else
            //    {
            //        // 間接部門報表
            //        預算總表_間接 Reporter1 = new 預算總表_間接();
            //        Reporter1.Database.Tables[0].SetDataSource(Ds_總表.Tables[0]);
            //        Reporter1.Database.Tables[1].SetDataSource(公司預算);
            //        Reporter1.Database.Tables[2].SetDataSource(公司成本);
            //        Reporter1.Database.Tables[3].SetDataSource(庫房外購金額);
            //        Form ReportForm = new Form();
            //        CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            //        CryReport.ReportSource = Reporter1;
            //        CryReport.Dock = DockStyle.Fill;

            //        if (Dt_未完成.Rows.Count > 0)
            //            CryReport.ShowPrintButton = false;

            //        Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
            //        Reporter1.ParameterFields["Date"].CurrentValues.AddValue(Date);

            //        ReportForm.Controls.Add(CryReport);
            //        ReportForm.WindowState = FormWindowState.Maximized;
            //        ReportForm.Show();
            //    }
            //}
            #endregion
        }

        private void naviDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            switch (Dgv.Columns[e.ColumnIndex].Name)
            {
                case "品項":
                    {
                        if (Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString() != "0" && Dgv.Rows[e.RowIndex].Cells["Gv2BGTYPE"].Value.ToString() == "預算" &&
                            !string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString()))
                        {
                            品項檢視 form = new 品項檢視(Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString());
                            form.ShowDialog();
                        }
                        else if (Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString() != "0" && Dgv.Rows[e.RowIndex].Cells["Gv2BGTYPE"].Value.ToString() == "追加" &&
                            !string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString()))
                        {
                            品項檢視 form = new 品項檢視(Dgv.Rows[e.RowIndex].Cells["Gv2UID"].Value.ToString());
                            form.ShowDialog();
                        }
                    }
                    break;
            }
        }

        private void naviDataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            switch (Dgv.Columns[e.ColumnIndex].Name)
            {
                case "品項":
                    {
                        if (Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString() != "0" && Dgv.Rows[e.RowIndex].Cells["Gv2BGTYPE"].Value.ToString() == "預算" &&
                            !string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Value.ToString()))
                        {
                            Dgv.Rows[e.RowIndex].Cells["品項"].Style.BackColor = Color.FromArgb(180, 238, 180);
                            Dgv.Rows[e.RowIndex].Cells["品項"].Value = "品項";
                        }
                        else if (Dgv.Rows[e.RowIndex].Cells["Gv2BGTYPE"].Value.ToString() == "預算")
                        {
                            Dgv.Rows[e.RowIndex].Cells["品項"].Style.BackColor = Color.White;
                            Dgv.Rows[e.RowIndex].Cells["品項"].Value = "";
                            Dgv.Rows[e.RowIndex].Cells["追加後預算"].ReadOnly = true;
                        }

                        else if (Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString() != "0" && Dgv.Rows[e.RowIndex].Cells["Gv2BGTYPE"].Value.ToString() == "追加" &&
                                 !string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Value.ToString()))
                        {
                            Dgv.Rows[e.RowIndex].Cells["品項"].Style.BackColor = Color.FromArgb(180, 238, 180);
                            Dgv.Rows[e.RowIndex].Cells["品項"].Value = "品項";
                        }
                        else if (Dgv.Rows[e.RowIndex].Cells["Gv2BGTYPE"].Value.ToString() == "追加")
                        {
                            Dgv.Rows[e.RowIndex].Cells["品項"].Style.BackColor = Color.White;
                            Dgv.Rows[e.RowIndex].Cells["品項"].Value = "";
                            Dgv.Rows[e.RowIndex].Cells["追加後預算"].ReadOnly = true;
                        }

                        if (Dgv.Rows[e.RowIndex].Cells["Gv2BGDEPNAME"].Value.ToString() == "合計")
                        {
                            Dgv.Rows[e.RowIndex].Cells["Gv2BGAMT1"].Style.BackColor = Color.FromArgb(238, 174, 238);
                            Dgv.Rows[e.RowIndex].Cells["Gv2BGBMT2"].Style.BackColor = Color.FromArgb(238, 174, 238);
                            Dgv.Rows[e.RowIndex].Cells["追加後預算"].Style.BackColor = Color.FromArgb(238, 174, 238);
                        }
                        Dgv.Rows[e.RowIndex].Cells["Gv2單據單號"].Style.ForeColor = Color.FromArgb(24, 116, 205);
                    }
                    break;
                case "追加後預算":
                    {
                        e.CellStyle.Format = "###,###,###";
                    }
                    break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            追加總表列印 pop = new 追加總表列印();
            if (pop.ShowDialog() == DialogResult.Yes)
            {
                DataSet 追加總表 = new DataSet();
                Ad = new SqlDataAdapter("PUR2007NET_品項總表", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@起", SqlDbType.DateTime).Value = pop.起始日期;
                Ad.SelectCommand.Parameters.Add("@迄", SqlDbType.DateTime).Value = pop.結束日期;
                Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = pop.BGYM;
                Ad.SelectCommand.Parameters.Add("@項目", SqlDbType.VarChar).Value = pop.項目;
                Ad.SelectCommand.Parameters.Add("@科目部門", SqlDbType.VarChar).Value = pop.科目部門;
                追加總表.Clear();
                Ad.Fill(追加總表);

                if (pop.科目部門 == "部門")
                {
                    追加總表 Reporter1 = new 追加總表();
                    //Reporter1.SetDataSource(追加總表.Tables[0]);
                    Reporter1.Database.Tables[0].SetDataSource(追加總表.Tables[0]);
                    Reporter1.Database.Tables[1].SetDataSource(追加總表.Tables[1]);
                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["日期"].CurrentValues.AddValue(pop.BGYM);
                    Reporter1.ParameterFields["項目"].CurrentValues.AddValue(pop.項目);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(pop.BGYM.Substring(0, 4) + "年" + pop.BGYM.Substring(4, 2) + "月");
                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else
                {
                    品類總表_科目 Reporter1 = new 品類總表_科目();
                    Reporter1.SetDataSource(追加總表.Tables[0]);
                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["日期"].CurrentValues.AddValue(pop.BGYM);
                    Reporter1.ParameterFields["項目"].CurrentValues.AddValue(pop.項目);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(pop.BGYM.Substring(0, 4) + "年" + pop.BGYM.Substring(4, 2) + "月");
                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            追加總表列印 pop = new 追加總表列印();
            if (pop.ShowDialog() == DialogResult.Yes)
            {
                // Update到PUR預算========================================================================================
                DataSet Ds_使用統計 = new DataSet();
                Ad = new SqlDataAdapter("PUR_預算使用統計", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = pop.BGYM;
                Ad.SelectCommand.Parameters.Add("@OPTION", SqlDbType.VarChar).Value = "";
                Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = "";
                Ds_使用統計.Clear();
                Ad.Fill(Ds_使用統計);


                DataTable Dt_PUR預算 = new DataTable();
                // 抓出最後簽核的人
                Ad = new SqlDataAdapter(" SELECT * FROM PUR預算 WHERE BGYM = @BGYM ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = pop.BGYM;
                Dt_PUR預算.Clear();
                Ad.Fill(Dt_PUR預算);

                try
                {
                    using (SqlConnection conn = new SqlConnection(GonGinVariable.SqlConnectString))
                    {
                        conn.Open();
                        SqlCommand comm = conn.CreateCommand();
                        comm.Connection = conn;

                        if (Dt_PUR預算.Rows.Count > 0)
                        {
                            // 更新PUR預算
                            foreach (DataRow oRows in Ds_使用統計.Tables[5].Rows)
                            {
                                comm.Parameters.Clear();
                                comm.CommandType = CommandType.Text;
                                comm.CommandText = "UPDATE PUR預算 SET 預算申請 = @預算申請, 使用金額 = @使用金額 WHERE BGYM=@BGYM AND BGNO=@BGNO ";
                                comm.Parameters.Add("@預算申請", SqlDbType.Decimal).Value = Convert.ToDecimal(oRows["預算申請"].ToString());
                                comm.Parameters.Add("@使用金額", SqlDbType.Decimal).Value = Convert.ToDecimal(oRows["預算使用"].ToString());
                                comm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = pop.BGYM;
                                comm.Parameters.Add("@BGNO", SqlDbType.VarChar).Value = oRows["科目"].ToString();
                                comm.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            foreach (DataRow oRows in Ds_使用統計.Tables[5].Rows)
                            {
                                comm.Parameters.Clear();
                                comm.CommandType = CommandType.Text;
                                comm.CommandText = "INSERT PUR預算 (BGYM,BGNO,預算申請,使用金額,CRUSER,CRDATE) VALUES (@BGYM,@BGNO,@預算申請,@使用金額,@CRUSER,GETDATE()) ";
                                comm.Parameters.Add("@預算申請", SqlDbType.Decimal).Value = Convert.ToDecimal(oRows["預算申請"].ToString());
                                comm.Parameters.Add("@使用金額", SqlDbType.Decimal).Value = Convert.ToDecimal(oRows["預算使用"].ToString());
                                comm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = pop.BGYM;
                                comm.Parameters.Add("@BGNO", SqlDbType.VarChar).Value = oRows["科目"].ToString();
                                comm.Parameters.Add("@CRUSER", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                                comm.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // 品項總表============================================================================================
                DataTable 品項總表 = new DataTable();
                Ad = new SqlDataAdapter("PUR2007NET_追加總表", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@起", SqlDbType.DateTime).Value = pop.起始日期;
                Ad.SelectCommand.Parameters.Add("@迄", SqlDbType.DateTime).Value = pop.結束日期;
                Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = pop.BGYM;
                Ad.SelectCommand.Parameters.Add("@項目", SqlDbType.VarChar).Value = pop.項目;
                品項總表.Clear();
                Ad.Fill(品項總表);

                預算申請表 Reporter1 = new 預算申請表();
                Reporter1.SetDataSource(品項總表);
                Form ReportForm = new Form();
                CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                CryReport.ReportSource = Reporter1;
                CryReport.Dock = DockStyle.Fill;
                Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                Reporter1.ParameterFields["急件"].CurrentValues.AddValue("N");
                //Reporter1.ParameterFields["部門"].CurrentValues.AddValue("");
                Reporter1.ParameterFields["單據單號"].CurrentValues.AddValue("");
                Reporter1.ParameterFields["月份"].CurrentValues.AddValue(pop.BGYM.Substring(0, 4) + "年" + pop.BGYM.Substring(4, 2) + "月");
                Reporter1.ParameterFields["項目"].CurrentValues.AddValue(pop.項目);

                ReportForm.Controls.Add(CryReport);
                ReportForm.WindowState = FormWindowState.Maximized;
                ReportForm.Show();
            }
        }

        private void 科目總表_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Query();
                naviDataGridView1.DataSource = Ds.Tables[5].DefaultView;
                naviDataGridView2.DataSource = Ds.Tables[6].DefaultView;
            }
            else
                Query();
        }

        string TT = string.Empty;

        private void naviDataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            // ToolTip===================================================================================================================
            //this.toolTip1.Hide(this);
            // 

            //String TipsString = string.Empty;
            //switch (Dgv.Columns[e.ColumnIndex].Name)
            //{
            //    case "Gv2簽核人":
            //        {
            //            string[] sArray = Dgv.Rows[e.RowIndex].Cells["Gv2簽核人"].Value.ToString().Split(';');
            //            foreach (string j in sArray)
            //            {
            //                if (!string.IsNullOrEmpty(j))
            //                {
            //                    _QueryComm.CommandText = " SELECT PNAME FROM PERSON WHERE PENNO=@PENNO ";
            //                    _QueryComm.CommandType = CommandType.Text;
            //                    _QueryComm.Parameters.Clear();
            //                    _QueryComm.Parameters.Add("@PENNO", SqlDbType.VarChar).Value = j;
            //                    _QueryDr = _QueryComm.ExecuteReader();

            //                    if (_QueryDr.HasRows)
            //                    {
            //                        _QueryDr.Read();
            //                        TipsString += _QueryDr["PNAME"] + ",";
            //                    }
            //                    _QueryDr.Close();
            //                }
            //            }

            //            if (!String.IsNullOrEmpty(TipsString))
            //            {                            
            //                Rectangle r = Dgv.GetCellDisplayRectangle(Dgv.Rows[e.RowIndex].Cells["Gv2簽核人"].ColumnIndex, Dgv.Rows[e.RowIndex].Cells["Gv2簽核人"].RowIndex, false);
            //                this.toolTip1.BackColor = Color.Yellow;
            //                this.toolTip1.ToolTipTitle = "簽核人";
            //                this.toolTip1.ToolTipIcon = ToolTipIcon.Info;
            //                this.toolTip1.IsBalloon = true;
            //                //this.toolTip1.Show(TipsString, this, Dgv.Rows[e.RowIndex].Cells["Gv2簽核人"].ColumnIndex, Dgv.Rows[e.RowIndex].Cells["Gv2簽核人"].RowIndex);
            //                this.toolTip1.Show(TipsString, this, Dgv.Location.X + r.Location.X + r.Width - Dgv.RowHeadersWidth, Dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location.Y + 80);
            //                //this.toolTip1.Show(AA.ToString() + "||" + BB + TipsString, this, 100, Dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location.Y + 20);
            //            }
            //        }
            //        break;
            //}
        }

        private void 類別總表_Click(object sender, EventArgs e)
        {
            群組列印視窗 form = new 群組列印視窗();
            if (form.ShowDialog() == DialogResult.Yes)
            {

            }
        }

        private void 開啟控管_Click(object sender, EventArgs e)
        {
            控管視窗 form = new 控管視窗();
            if (form.ShowDialog() == DialogResult.Yes)
                跑馬燈();
        }

        private void 特別預算總表_Click(object sender, EventArgs e)
        {
            特別預算視窗 pop = new 特別預算視窗("特別");

            if (pop.ShowDialog() == DialogResult.Yes)
            {
                string 公司名稱 = "";

                if (GonGinVariable.SectionName.Contains("微奈"))
                {
                    DataSet 暫存 = new DataSet();

                    Ad = new SqlDataAdapter("dbo.PUR3001NET", GonGinVariable.SqlConnectString);    //資料庫查詢
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = pop.BGYM;
                    Ad.SelectCommand.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = "%";
                    Ad.SelectCommand.Parameters.Add("@BGNO", SqlDbType.VarChar).Value = "";
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ds.Clear();
                    Ad.Fill(暫存);

                    Dt_特別預算 = 暫存.Tables[2].Copy();
                    公司名稱 = "微奈科技股份有限公司";
                }
                else
                {
                    Ad = new SqlDataAdapter("PUR2007NET_特別預算總表", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = pop.BGYM;
                    Dt_特別預算.Clear();
                    Ad.Fill(Dt_特別預算);

                    公司名稱 = GonGinVariable.公司名稱;
                }

                特別預算總表 Reporter1 = new 特別預算總表();
                Reporter1.SetDataSource(Dt_特別預算);
                Form ReportForm = new Form();
                CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                CryReport.ReportSource = Reporter1;
                CryReport.Dock = DockStyle.Fill;
                Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(公司名稱);
                Reporter1.ParameterFields["急件"].CurrentValues.AddValue("N");
                //Reporter1.ParameterFields["部門"].CurrentValues.AddValue("");
                Reporter1.ParameterFields["單據單號"].CurrentValues.AddValue("");
                Reporter1.ParameterFields["月份"].CurrentValues.AddValue(pop.BGYM.Substring(0, 4) + "年" + pop.BGYM.Substring(4, 2) + "月");
                Reporter1.ParameterFields["項目"].CurrentValues.AddValue("");

                ReportForm.Controls.Add(CryReport);
                ReportForm.WindowState = FormWindowState.Maximized;
                ReportForm.Show();
            }
        }

        private void 實際使用_Click(object sender, EventArgs e)
        {
            特別預算視窗 pop = new 特別預算視窗("實際使用");
            pop.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // 判斷使用者有權限且已開的部門
            DataTable Dt_助理 = new DataTable();

            // 判斷助理3、4，是否可以申請跨部門、或自己部門
            if (GonGinVariable.ApplicationUserDeptNo == "9600")
            {
                Ad = new SqlDataAdapter(" SELECT DEPTNO,DEPTNAME FROM AMDDEPT WHERE DEPTSUPR1 = @DEPTSUPR3 OR  DEPTSUPR2 = @DEPTSUPR3 OR DEPTSUPR3 = @DEPTSUPR3 OR  DEPTSUPR4 = @DEPTSUPR3 UNION " +
                                        " SELECT DEPTNO, DEPTNAME FROM AMDDEPT WHERE DEPTNO = '9990' ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@DEPTSUPR3", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                Dt_助理.Clear();
                Ad.Fill(Dt_助理);
            }
            else
            {
                Ad = new SqlDataAdapter(" SELECT DEPTNO,DEPTNAME FROM AMDDEPT WHERE DEPTSUPR1 = @DEPTSUPR3 OR  DEPTSUPR2 = @DEPTSUPR3 OR DEPTSUPR3 = @DEPTSUPR3 OR  DEPTSUPR4 = @DEPTSUPR3 ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@DEPTSUPR3", SqlDbType.VarChar).Value = GonGinVariable.ApplicationUser;
                Dt_助理.Clear();
                Ad.Fill(Dt_助理);
            }

            選擇部門 form = new 選擇部門(Dt_助理, "Y");
            if (form.ShowDialog() == DialogResult.Yes)
            {
                Excel匯入視窗 pop = new Excel匯入視窗(form.部門);
                if (pop.ShowDialog() == DialogResult.Yes)
                {
                    //MessageBox.Show("33A資料已匯入成功", "更新訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    資料更新_Click(null, null);
                }
            }
        }

        private void 特別科目_Click(object sender, EventArgs e)
        {
            //string BGYM = "";
            //_QueryComm.CommandText = " SELECT BGYM = MAX(BGYM) FROM BUGDA_簽核 WHERE SCTRL = 'Y' ";
            //_QueryComm.CommandType = CommandType.Text;
            //_QueryComm.Parameters.Clear();
            //_QueryDr = _QueryComm.ExecuteReader();

            //if (_QueryDr.HasRows)
            //{
            //    _QueryDr.Read();
            //    BGYM = _QueryDr["BGYM"].ToString();
            //}
            //_QueryDr.Close();

            特別科目 pop = new 特別科目();
            if (pop.ShowDialog() == DialogResult.Yes)
            {
                資料更新_Click(null, null);
            }
        }

        private void 費用查詢_Click(object sender, EventArgs e)
        {
            費用查詢 pop = new 費用查詢();
            pop.ShowDialog();

        }
    }
}
