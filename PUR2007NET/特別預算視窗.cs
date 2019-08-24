using GonGinLibrary;
using PUR2007NET.A3報表_NEW;
using PUR2007NET.實際報表;
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
    public partial class 特別預算視窗 : Form
    {
        private string _起始日 = string.Empty;
        private string _BGYM = string.Empty;
        private string _組別細項 = string.Empty;
        private string _特別科目 = string.Empty;
        private SqlDataAdapter Ad = null;
        private string _狀態 = "";

        DataTable Dt_群組名稱 = new DataTable();
        DataTable Dt_實際使用 = new DataTable();
        DataTable Dt_實際使用比較 = new DataTable();
        DataSet Ds_實際預算 = new DataSet();
        DataSet Dt_預算使用統計 = new DataSet();

        public string 起始日
        {
            get { return _起始日; }
            set { _起始日 = value; }
        }
        public string BGYM
        {
            get { return _BGYM; }
            set { _BGYM = value; }
        }
        public string 組別細項
        {
            get { return _組別細項; }
            set { _組別細項 = value; }
        }

        public string 特別科目
        {
            get { return _特別科目; }
            set { _特別科目 = value; }
        }

        public 特別預算視窗(string 狀態)
        {
            InitializeComponent();

            _狀態 = 狀態;

            if (狀態 == "特別")
            {
                groupBox1.Visible = false;
                checkBox4.Visible = false;
                this.Text = "特別預算視窗";
            }
            else if (狀態 == "實際使用")
                this.Text = "實際使用統計";

            if (Convert.ToInt32(DateTime.Now.ToString("dd")) > 20)
                naviTextBox2.Text = DateTime.Now.AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);
            else
                naviTextBox2.Text = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);
        }

        private void naviDateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            naviTextBox2.Text = naviDateTimePicker2.Value.ToString("yyyyMM");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(naviTextBox2.Text))
            {
                MessageBox.Show("申請月份未填 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _起始日 = naviTextBox1.Text;
            _BGYM = naviTextBox2.Text;

            if (_狀態 != "特別")
            {
                if (radioButton1.Checked)
                    _組別細項 = "A";
                else if (radioButton5.Checked)
                    _組別細項 = "B";
                else if (radioButton6.Checked)
                    _組別細項 = "C";
                else if (radioButton7.Checked)
                    _組別細項 = "D";
                else if (radioButton8.Checked)
                    _組別細項 = "E";
                else if (radioButton2.Checked)
                    _組別細項 = "F";
                else if (radioButton9.Checked)
                    _組別細項 = "I";

                else if (radioButton3.Checked)
                {
                    if (string.IsNullOrEmpty(naviTextBox1.Text.Trim()))
                    {
                        MessageBox.Show("起始月份未填 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _組別細項 = "G";
                }
                else if (radioButton4.Checked)
                {
                    if (string.IsNullOrEmpty(naviTextBox1.Text.Trim()))
                    {
                        MessageBox.Show("起始月份未填 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _組別細項 = "H";
                }

                if (checkBox4.Checked)
                    _特別科目 = "Y";


                DataTable 月份欄位 = new DataTable();
                月份欄位.Columns.Add("月份1", typeof(String));
                月份欄位.Columns.Add("月份2", typeof(String));
                月份欄位.Columns.Add("月份3", typeof(String));
                月份欄位.Columns.Add("月份4", typeof(String));
                月份欄位.Columns.Add("月份5", typeof(String));
                月份欄位.Columns.Add("月份6", typeof(String));
                月份欄位.Columns.Add("月份7", typeof(String));
                月份欄位.Columns.Add("月份8", typeof(String));
                月份欄位.Columns.Add("月份9", typeof(String));
                月份欄位.Columns.Add("月份10", typeof(String));
                月份欄位.Columns.Add("月份11", typeof(String));
                月份欄位.Columns.Add("月份12", typeof(String));

                string 特別 = "";
                if (_特別科目 == "Y")
                    特別 = "★";

                if (_組別細項 == "A")
                {
                    Ad = new SqlDataAdapter("PUR2007NET_群組列印_大群", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = _BGYM;
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_實際使用.Clear();
                    Ad.Fill(Dt_實際使用);


                    預算申請表NEW Reporter1 = new 預算申請表NEW();
                    //Reporter1.SetDataSource(Dt_明細);
                    Reporter1.Database.Tables[0].SetDataSource(Dt_實際使用);

                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(_BGYM.Substring(0, 4) + "年" + _BGYM.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["特別"].CurrentValues.AddValue(特別);
                    Reporter1.ParameterFields["第一群"].CurrentValues.AddValue(第一群);
                    Reporter1.ParameterFields["第二群"].CurrentValues.AddValue(第二群);
                    Reporter1.ParameterFields["第三群"].CurrentValues.AddValue(第三群);
                    Reporter1.ParameterFields["第四群"].CurrentValues.AddValue(第四群);
                    Reporter1.ParameterFields["第五群"].CurrentValues.AddValue(第五群);
                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (_組別細項 == "B")
                {
                    Ad = new SqlDataAdapter("PUR2007NET_群組列印_大群", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = _BGYM;
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_實際使用.Clear();
                    Ad.Fill(Dt_實際使用);

                    // 前一個月
                    string 前一個月 = string.Empty;
                    if (_BGYM.Substring(4, 2) == "01")
                        前一個月 = (Convert.ToInt32(_BGYM.Substring(0, 4)) - 1).ToString() + "12";

                    else
                        前一個月 = _BGYM.Substring(0, 4) + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString();

                    if (前一個月.Length == 5)
                        前一個月 = _BGYM.Substring(0, 4) + "0" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString();


                    Ad = new SqlDataAdapter("PUR_預算使用統計", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = 前一個月;
                    Ad.SelectCommand.Parameters.Add("@OPTION", SqlDbType.VarChar).Value = "";
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_預算使用統計.Clear();
                    Ad.Fill(Dt_預算使用統計);

                    DataTable Dt_實際使用1 = Dt_實際使用.Copy();
                    Dt_實際使用1.Columns.Add("Total", typeof(decimal));
                    Dt_實際使用1.Columns.Add("上月合計", typeof(decimal));

                    // 科目11、12丟入上個月實際
                    foreach (DataRow oRow in Dt_實際使用1.Rows)
                    {
                        foreach (DataRow oRows in Dt_預算使用統計.Tables[0].Rows)
                        {
                            if (oRow["BUGNO"].ToString() == oRows["BUGNO"].ToString())
                            {
                                oRow["Total"] = Convert.ToDecimal(oRows["合計"].ToString());
                                oRow["上月合計"] = Convert.ToDecimal(oRows["上月合計"].ToString());
                            }
                        }
                    }

                    Dt_實際使用1.AcceptChanges();


                    // 科目11、12、15A、54丟入上個月實際
                    DataTable Dt_實際使用2 = Dt_預算使用統計.Tables[0].Copy();

                    // 結束月份加一個月
                    string AA = _BGYM.Substring(4, 2);
                    if (AA == "01")
                        AA = "12";
                    else
                    {
                        if ((Convert.ToInt32(AA) - 1).ToString().Length < 2)
                            AA = "0" + (Convert.ToInt32(AA) - 1).ToString();
                        else
                            AA = (Convert.ToInt32(AA) - 1).ToString();
                    }

                    string BB = _BGYM.Substring(4, 2);

                    if (BB == "01")
                        BB = (Convert.ToInt32(_BGYM.Substring(0, 4)) - 1).ToString() + "年" + "12月";
                    else
                    {
                        if ((Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString().Length < 2)
                            BB = _BGYM.Substring(0, 4) + "年0" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                        else
                            BB = _BGYM.Substring(0, 4) + "年" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                    }

                    DateTime 前月 = Convert.ToDateTime(_BGYM.Substring(0, 4) + "/" + _BGYM.Substring(4, 2) + "/01");
                    前月 = 前月.AddMonths(-2);

                    string CC = 前月.ToString("MM" + "月");


                    預算實際申請表 Reporter1 = new 預算實際申請表();
                    //Reporter1.SetDataSource(Dt_明細);
                    Reporter1.Database.Tables[0].SetDataSource(Dt_實際使用1);
                    //Reporter1.Database.Tables[1].SetDataSource(Dt_實際使用1);
                    //Reporter1.Database.Tables[2].SetDataSource(Dt_實際使用2);
                    //Reporter1.Database.Tables[3].SetDataSource(Dt_預算使用統計.Tables[6]);
                    //Reporter1.Database.Tables[4].SetDataSource(Dt_實際使用比較);

                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(_BGYM.Substring(0, 4) + "年" + _BGYM.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["月份1"].CurrentValues.AddValue(BB);
                    Reporter1.ParameterFields["月份2"].CurrentValues.AddValue(CC);
                    Reporter1.ParameterFields["特別"].CurrentValues.AddValue(特別);
                    Reporter1.ParameterFields["第一群"].CurrentValues.AddValue(第一群);
                    Reporter1.ParameterFields["第二群"].CurrentValues.AddValue(第二群);
                    Reporter1.ParameterFields["第三群"].CurrentValues.AddValue(第三群);
                    Reporter1.ParameterFields["第四群"].CurrentValues.AddValue(第四群);
                    Reporter1.ParameterFields["第五群"].CurrentValues.AddValue(第五群);
                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (_組別細項 == "C")
                {
                    // 前一個月
                    string 前一個月 = string.Empty;
                    if (_BGYM.Substring(4, 2) == "01")
                        前一個月 = (Convert.ToInt32(_BGYM.Substring(0, 4)) - 1).ToString() + "12";

                    else
                        前一個月 = _BGYM.Substring(0, 4) + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString();

                    if (前一個月.Length == 5)
                        前一個月 = _BGYM.Substring(0, 4) + "0" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString();


                    Ad = new SqlDataAdapter("PUR_預算使用統計", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = 前一個月;
                    Ad.SelectCommand.Parameters.Add("@OPTION", SqlDbType.VarChar).Value = "";
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_預算使用統計.Clear();
                    Ad.Fill(Dt_預算使用統計);

                    // 科目11、12、15A、54丟入上個月實際
                    DataTable Dt_實際使用2 = Dt_預算使用統計.Tables[0].Copy();

                    // 結束月份加一個月
                    string AA = _BGYM.Substring(4, 2);
                    if (AA == "01")
                        AA = "12";
                    else
                    {
                        if ((Convert.ToInt32(AA) - 1).ToString().Length < 2)
                            AA = "0" + (Convert.ToInt32(AA) - 1).ToString();
                        else
                            AA = (Convert.ToInt32(AA) - 1).ToString();
                    }

                    string BB = _BGYM.Substring(4, 2);
                    if (BB == "01")
                        BB = (Convert.ToInt32(_BGYM.Substring(0, 4)) - 1).ToString() + "年" + "12月";
                    else
                    {
                        if ((Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString().Length < 2)
                            BB = _BGYM.Substring(0, 4) + "年0" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                        else
                            BB = _BGYM.Substring(0, 4) + "年" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                    }

                    實際申請表 Reporter1 = new 實際申請表();
                    Reporter1.Database.Tables[0].SetDataSource(Dt_實際使用2);

                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(_BGYM.Substring(0, 4) + "年" + _BGYM.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["月份1"].CurrentValues.AddValue(BB);
                    Reporter1.ParameterFields["月份2"].CurrentValues.AddValue(BB);
                    Reporter1.ParameterFields["特別"].CurrentValues.AddValue(特別);
                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (_組別細項 == "D")
                {
                    // 前一個月
                    string 前一個月 = string.Empty;
                    if (_BGYM.Substring(4, 2) == "01")
                        前一個月 = (Convert.ToInt32(_BGYM.Substring(0, 4)) - 1).ToString() + "12";

                    else
                        前一個月 = _BGYM.Substring(0, 4) + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString();

                    if (前一個月.Length == 5)
                        前一個月 = _BGYM.Substring(0, 4) + "0" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString();


                    Ad = new SqlDataAdapter("PUR_預算使用統計", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = 前一個月;
                    Ad.SelectCommand.Parameters.Add("@OPTION", SqlDbType.VarChar).Value = "";
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_預算使用統計.Clear();
                    Ad.Fill(Dt_預算使用統計);

                    // 結束月份加一個月
                    string AA = _BGYM.Substring(4, 2);
                    if (AA == "01")
                        AA = "12";
                    else
                    {
                        if ((Convert.ToInt32(AA) - 1).ToString().Length < 2)
                            AA = "0" + (Convert.ToInt32(AA) - 1).ToString();
                        else
                            AA = (Convert.ToInt32(AA) - 1).ToString();
                    }

                    string BB = _BGYM.Substring(4, 2);
                    if (BB == "01")
                        BB = (Convert.ToInt32(_BGYM.Substring(0, 4)) - 1).ToString() + "年" + "12月";
                    else
                    {
                        if ((Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString().Length < 2)
                            BB = _BGYM.Substring(0, 4) + "年0" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                        else
                            BB = _BGYM.Substring(0, 4) + "年" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                    }

                    總經理排序 Reporter1 = new 總經理排序();
                    Reporter1.Database.Tables[0].SetDataSource(Dt_預算使用統計.Tables[6]);

                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(_BGYM.Substring(0, 4) + "年" + _BGYM.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["月份1"].CurrentValues.AddValue(BB);
                    Reporter1.ParameterFields["月份2"].CurrentValues.AddValue(BB);
                    Reporter1.ParameterFields["特別"].CurrentValues.AddValue(特別);
                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (_組別細項 == "E")
                {
                    Ad = new SqlDataAdapter("PUR2007NET_群組列印_大群", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = _BGYM;
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_實際使用.Clear();
                    Ad.Fill(Dt_實際使用);

                    Ad = new SqlDataAdapter("PUR2007NET_群組列印_大群_T", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = _BGYM;
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_實際使用比較.Clear();
                    Ad.Fill(Dt_實際使用比較);

                    string BB = _BGYM.Substring(4, 2);
                    if (BB == "01")
                        BB = "12月";
                    else
                    {
                        if ((Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString().Length < 2)
                            BB = (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                        else
                            BB = (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                    }


                    預算比較 Reporter1 = new 預算比較();
                    Reporter1.Database.Tables[0].SetDataSource(Dt_實際使用);
                    Reporter1.Database.Tables[1].SetDataSource(Dt_實際使用比較);

                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(_BGYM.Substring(0, 4) + "年" + _BGYM.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["特別"].CurrentValues.AddValue(特別);
                    Reporter1.ParameterFields["月份1"].CurrentValues.AddValue(BB);

                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (_組別細項 == "F")
                {
                    Ad = new SqlDataAdapter("PUR2007NET_群組列印_大群", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = _BGYM;
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_實際使用.Clear();
                    Ad.Fill(Dt_實際使用);


                    string 前一個月 = string.Empty;
                    if (_BGYM.Substring(4, 2) == "01")
                        前一個月 = (Convert.ToInt32(_BGYM.Substring(0, 4)) - 1).ToString() + "12";
                    else
                        前一個月 = _BGYM.Substring(0, 4) + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString();

                    if (前一個月.Length == 5)
                        前一個月 = _BGYM.Substring(0, 4) + "0" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString();


                    Ad = new SqlDataAdapter("PUR_預算使用統計", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = 前一個月;
                    Ad.SelectCommand.Parameters.Add("@OPTION", SqlDbType.VarChar).Value = "";
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_預算使用統計.Clear();
                    Ad.Fill(Dt_預算使用統計);


                    string CC = _BGYM.Substring(4, 2) + "月預算";

                    string DD = _BGYM.Substring(4, 2);
                    if (DD == "01")
                        DD = "12月實際";
                    else
                    {
                        if ((Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString().Length < 2)
                            DD = "0" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月實際";
                        else
                            DD = (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月實際";
                    }

                    // 加入群組的科目明細
                    DataTable 群組資料 = Dt_預算使用統計.Tables[2].Copy();
                    群組資料.Columns.Add("第一群", typeof(decimal));
                    群組資料.Columns.Add("第二群", typeof(decimal));
                    群組資料.Columns.Add("第三群", typeof(decimal));
                    群組資料.Columns.Add("第四群", typeof(decimal));
                    群組資料.Columns.Add("第五群", typeof(decimal));
                    群組資料.Columns.Add("群組合計", typeof(decimal));

                    foreach (DataRow oRow in 群組資料.Rows)
                    {
                        foreach (DataRow oRows in Dt_實際使用.Rows)
                        {
                            if (oRow["BUGNO"].ToString() == oRows["BUGNO"].ToString())
                            {
                                oRow["第一群"] = oRows["第一群"].ToString();
                                oRow["第二群"] = oRows["第二群"].ToString();
                                oRow["第三群"] = oRows["第三群"].ToString();
                                oRow["第四群"] = oRows["第四群"].ToString();
                                oRow["第五群"] = oRows["第五群"].ToString();
                                oRow["群組合計"] = oRows["群組合計"].ToString();
                            }
                        }
                    }
                    群組資料.AcceptChanges();

                    DataTable AA = 群組資料.Select("BUGNO = '999'").CopyToDataTable();
                    int 預算 = Convert.ToInt32(AA.Rows[0]["群組合計"].ToString());

                    A3明細總表_NEW Reporter1 = new A3明細總表_NEW();
                    //Reporter1.SetDataSource(Dt_明細);
                    Reporter1.Database.Tables[0].SetDataSource(群組資料);
                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(_BGYM.Substring(0, 4) + "年" + _BGYM.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["月份1"].CurrentValues.AddValue(CC);
                    Reporter1.ParameterFields["月份2"].CurrentValues.AddValue(DD);
                    Reporter1.ParameterFields["預算"].CurrentValues.AddValue(預算);
                    Reporter1.ParameterFields["實際"].CurrentValues.AddValue(Dt_預算使用統計.Tables[3].Rows[0]["實際"]);
                    Reporter1.ParameterFields["特別"].CurrentValues.AddValue(特別);
                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (_組別細項 == "G")
                {
                    // 下個月
                    string EDATE = "";
                    if (_BGYM.Substring(4, 2) == "12")
                        EDATE = (Convert.ToInt32(_BGYM.Substring(0, 4)) + 1).ToString() + "01";
                    else
                        EDATE = (Convert.ToInt32(_BGYM) + 1).ToString();


                    Ad = new SqlDataAdapter("PUR2007NET_實際預算", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@起始日", SqlDbType.VarChar).Value = _起始日;
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = EDATE;
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Ds_實際預算.Clear();
                    Ad.Fill(Ds_實際預算);


                    月份欄位.Rows.Add();

                    string 月份範圍 = "";

                    int j = 1;
                    foreach (DataRow item in Ds_實際預算.Tables[0].Rows)
                    {
                        if (j == 1)
                            月份範圍 = item["月份"].ToString().Substring(0, 4) + "年" + item["月份"].ToString().Substring(4, 2) + "月";

                        if (j == Ds_實際預算.Tables[0].Rows.Count)
                            月份範圍 += "～" + item["月份"].ToString().Substring(0, 4) + "年" + item["月份"].ToString().Substring(4, 2) + "月";

                        月份欄位.Rows[0]["月份" + j.ToString()] = item["月份"].ToString().Substring(4, 2) + "月";
                        //月份欄位.Rows[0]["預算" + j.ToString()] = item["月份"].ToString().Substring(4, 2) + "月預算";
                        j++;
                    }

                    實際使用報表_季 Reporter1 = new 實際使用報表_季();
                    //Reporter1.SetDataSource(Dt_明細);
                    Reporter1.Database.Tables[0].SetDataSource(月份欄位);
                    Reporter1.Database.Tables[1].SetDataSource(Ds_實際預算.Tables[1]);
                    Reporter1.Database.Tables[2].SetDataSource(Ds_實際預算.Tables[3]);
                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(月份範圍);
                    Reporter1.ParameterFields["特別"].CurrentValues.AddValue(特別);
                    //Reporter1.ParameterFields["月份"].CurrentValues.AddValue(pop.BGYM.Substring(0, 4) + "年" + pop.BGYM.Substring(4, 2) + "月");
                    //Reporter1.ParameterFields["月份1"].CurrentValues.AddValue(BB);
                    //Reporter1.ParameterFields["月份2"].CurrentValues.AddValue(BB);

                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (_組別細項 == "H")
                {
                    string EDATE = "";
                    if (_BGYM.Substring(4, 2) == "12")
                        EDATE = (Convert.ToInt32(_BGYM.Substring(0, 4)) + 1).ToString() + "01";
                    else
                        EDATE = (Convert.ToInt32(_BGYM) + 1).ToString();


                    Ad = new SqlDataAdapter("PUR2007NET_群實際", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@起始日", SqlDbType.VarChar).Value = _起始日;
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = EDATE;
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Ds_實際預算.Clear();
                    Ad.Fill(Ds_實際預算);

                    月份欄位.Rows.Add();
                    string 月份範圍 = "";

                    int j = 1;
                    foreach (DataRow item in Ds_實際預算.Tables[0].Rows)
                    {
                        if (j == 1)
                            月份範圍 = item["月份"].ToString().Substring(0, 4) + "年" + item["月份"].ToString().Substring(4, 2) + "月";

                        if (j == Ds_實際預算.Tables[0].Rows.Count)
                            月份範圍 += "～" + item["月份"].ToString().Substring(0, 4) + "年" + item["月份"].ToString().Substring(4, 2) + "月";

                        月份欄位.Rows[0]["月份" + j.ToString()] = item["月份"].ToString().Substring(4, 2) + "月";
                        //月份欄位.Rows[0]["預算" + j.ToString()] = item["月份"].ToString().Substring(4, 2) + "月預算";
                        j++;
                    }

                    月份群_季 Reporter1 = new 月份群_季();
                    //Reporter1.SetDataSource(Dt_明細);
                    Reporter1.Database.Tables[0].SetDataSource(月份欄位);
                    Reporter1.Database.Tables[1].SetDataSource(Ds_實際預算.Tables[1]);
                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(月份範圍);
                    Reporter1.ParameterFields["特別"].CurrentValues.AddValue(特別);
                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (_組別細項 == "I")
                {
                    Ad = new SqlDataAdapter("PUR2007NET_BUGDA", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = _BGYM;
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_實際使用.Clear();
                    Ad.Fill(Dt_實際使用);


                    Ad = new SqlDataAdapter("PUR_預算使用統計", GonGinVariable.SqlConnectString);
                    Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Ad.SelectCommand.CommandTimeout = 600;
                    Ad.SelectCommand.Parameters.Clear();
                    Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = _BGYM;
                    Ad.SelectCommand.Parameters.Add("@OPTION", SqlDbType.VarChar).Value = "";
                    Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = _特別科目;
                    Dt_預算使用統計.Clear();
                    Ad.Fill(Dt_預算使用統計);

                    DataTable Dt_實際使用1 = Dt_實際使用.Copy();
                    Dt_實際使用1.Columns.Add("Total", typeof(decimal));
                    Dt_實際使用1.Columns.Add("上月合計", typeof(decimal));
                    Dt_實際使用1.Columns.Add("預算小計", typeof(decimal));
                    Dt_實際使用1.Columns.Add("實際小計", typeof(decimal));
                    // 科目11、12丟入上個月實際
                    foreach (DataRow oRow in Dt_實際使用1.Rows)
                    {
                        foreach (DataRow oRows in Dt_預算使用統計.Tables[0].Rows)
                        {
                            if (oRow["BUGNO"].ToString() == oRows["BUGNO"].ToString())
                            {
                                oRow["Total"] = Convert.ToDecimal(oRows["合計"].ToString());
                                if (oRow["小計"].ToString() != "0")
                                    oRow["預算小計"] = Convert.ToDecimal(oRow["小計"].ToString());
                                if (oRows["小計"].ToString() != "0")
                                    oRow["實際小計"] = Convert.ToDecimal(oRows["小計"].ToString());
                            }
                        }
                    }

                    Dt_實際使用1.AcceptChanges();


                    // 科目11、12、15A、54丟入上個月實際
                    DataTable Dt_實際使用2 = Dt_預算使用統計.Tables[0].Copy();

                    // 結束月份加一個月
                    string AA = _BGYM.Substring(4, 2);
                    if (AA == "01")
                        AA = "12";
                    else
                    {
                        if ((Convert.ToInt32(AA) - 1).ToString().Length < 2)
                            AA = "0" + (Convert.ToInt32(AA) - 1).ToString();
                        else
                            AA = (Convert.ToInt32(AA) - 1).ToString();
                    }

                    string BB = _BGYM.Substring(4, 2);

                    if (BB == "01")
                        BB = (Convert.ToInt32(_BGYM.Substring(0, 4)) - 1).ToString() + "年" + "12月";
                    else
                    {
                        if ((Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString().Length < 2)
                            BB = _BGYM.Substring(0, 4) + "年0" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                        else
                            BB = _BGYM.Substring(0, 4) + "年" + (Convert.ToInt32(_BGYM.Substring(4, 2)) - 1).ToString() + "月";
                    }

                    //DateTime 前月 = Convert.ToDateTime(_BGYM.Substring(0, 4) + "/" + _BGYM.Substring(4, 2) + "/01");
                    //前月 = 前月.AddMonths(-2);

                    //string CC = 前月.ToString("MM" + "月");


                    預算實際申請表02 Reporter1 = new 預算實際申請表02();
                    Reporter1.Database.Tables[0].SetDataSource(Dt_實際使用1);

                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(_BGYM.Substring(0, 4) + "年" + _BGYM.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["月份1"].CurrentValues.AddValue(BB);
                    Reporter1.ParameterFields["月份2"].CurrentValues.AddValue(BB);
                    Reporter1.ParameterFields["特別"].CurrentValues.AddValue(特別);
                    Reporter1.ParameterFields["第一群"].CurrentValues.AddValue(第一群);
                    Reporter1.ParameterFields["第二群"].CurrentValues.AddValue(第二群);
                    Reporter1.ParameterFields["第三群"].CurrentValues.AddValue(第三群);
                    Reporter1.ParameterFields["第四群"].CurrentValues.AddValue(第四群);
                    Reporter1.ParameterFields["第五群"].CurrentValues.AddValue(第五群);
                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
            }
            else
                this.DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void naviDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            naviTextBox1.Text = naviDateTimePicker1.Value.ToString("yyyyMM");
        }

        string 第一群 = "";
        string 第二群 = "";
        string 第三群 = "";
        string 第四群 = "";
        string 第五群 = "";
        private void 特別預算視窗_Load(object sender, EventArgs e)
        {
            DataTable 部門名 = new DataTable();
            Ad = new SqlDataAdapter(" SELECT * FROM PUR2007NET_群組名稱 ORDER BY 大群 ", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.Text;
            Ad.SelectCommand.CommandTimeout = 600;
            部門名.Clear();
            Ad.Fill(部門名);


            第一群 = 部門名.Rows[0]["NAME"].ToString();
            第二群 = 部門名.Rows[1]["NAME"].ToString();
            第三群 = 部門名.Rows[2]["NAME"].ToString();
            第四群 = 部門名.Rows[3]["NAME"].ToString();
            第五群 = 部門名.Rows[4]["NAME"].ToString();
        }
    }
}
