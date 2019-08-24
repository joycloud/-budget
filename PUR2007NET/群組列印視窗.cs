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
    public partial class 群組列印視窗 : Form
    {
        private SqlDataAdapter Ad = null;
        private DataTable 部門大群 = new DataTable();
        private DataTable 部門群組 = new DataTable();

        private SqlConnection _QueryConn = null;   // 共用
        private SqlCommand _QueryComm = null;      // 共用
        private SqlDataReader _QueryDr = null;     // 共用

        public 群組列印視窗()
        {
            InitializeComponent();

            _QueryConn = new SqlConnection(GonGinVariable.SqlConnectString);
            _QueryComm = _QueryConn.CreateCommand();

            if (_QueryConn.State == ConnectionState.Closed) _QueryConn.Open();

            naviTextBox3.Text = "預算";

            // 超過20號，就預設下個月份
            if (Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd").Substring(6, 2).ToString()) > 20)
                naviTextBox2.Text = DateTime.Now.AddMonths(1).ToString("yyyyMMdd").Substring(0, 6);
            else
                naviTextBox2.Text = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked == false)
            {
                if (string.IsNullOrEmpty(naviTextBox3.Text))
                {
                    MessageBox.Show("列印項目未填 !!", "更新訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(naviTextBox2.Text))
                {
                    MessageBox.Show("申請月份未填 !!", "更新訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DataSet 群組報表 = new DataSet();

            // 判斷groupbox裡面必須點選一個radiobutton
            //var buttons = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);

            if (radioButton4.Checked)
            {
                Ad = new SqlDataAdapter("PUR2007NET_群組列印_無科目", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = naviTextBox2.Text;
                Ad.SelectCommand.Parameters.Add("@項目", SqlDbType.VarChar).Value = naviTextBox3.Text;
                群組報表.Clear();
                Ad.Fill(群組報表);

                群組_總表無科目 Reporter1 = new 群組_總表無科目();
                //Reporter1.SetDataSource(Dt_明細);
                Reporter1.Database.Tables[0].SetDataSource(群組報表.Tables[0]);
                Form ReportForm = new Form();
                CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                CryReport.ReportSource = Reporter1;
                CryReport.Dock = DockStyle.Fill;
                Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                Reporter1.ParameterFields["月份"].CurrentValues.AddValue(naviTextBox2.Text.Substring(0, 4) + "年" + naviTextBox2.Text.Substring(4, 2) + "月");
                Reporter1.ParameterFields["項目"].CurrentValues.AddValue(naviTextBox3.Text);

                ReportForm.Controls.Add(CryReport);
                ReportForm.WindowState = FormWindowState.Maximized;
                ReportForm.Show();
            }
            else if (radioButton5.Checked)
            {
                Ad = new SqlDataAdapter("PUR2007NET_群組列印_大群", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = naviTextBox2.Text;
                Ad.SelectCommand.Parameters.Add("@SBGNO", SqlDbType.VarChar).Value = "";
                群組報表.Clear();
                Ad.Fill(群組報表);


                群組_大群總表 Reporter1 = new 群組_大群總表();
                //Reporter1.SetDataSource(Dt_明細);
                Reporter1.Database.Tables[0].SetDataSource(群組報表.Tables[0]);
                Form ReportForm = new Form();
                CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                CryReport.ReportSource = Reporter1;
                CryReport.Dock = DockStyle.Fill;
                Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                Reporter1.ParameterFields["月份"].CurrentValues.AddValue(naviTextBox2.Text.Substring(0, 4) + "年" + naviTextBox2.Text.Substring(4, 2) + "月");

                ReportForm.Controls.Add(CryReport);
                ReportForm.WindowState = FormWindowState.Maximized;
                ReportForm.Show();
            }
            else if (radioButton6.Checked)
            {
                Ad = new SqlDataAdapter("PUR2007NET_群組列印_群組", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@群組", SqlDbType.VarChar).Value = naviTextBox4.Text;
                Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = naviTextBox2.Text;
                Ad.SelectCommand.Parameters.Add("@項目", SqlDbType.VarChar).Value = naviTextBox3.Text;
                群組報表.Clear();
                Ad.Fill(群組報表);

                string 組別 = string.Empty;
                foreach (DataRow oRows in 群組報表.Tables[2].Rows)
                {
                    組別 += oRows["群組"].ToString() + ",";
                }
                組別 = 組別.TrimEnd(',');
                組別 = "(" + 組別 + ")組";


                String 串部門 = string.Empty;
                int X = 0;
                foreach (DataRow item in 群組報表.Tables[1].Rows)
                {
                    串部門 += "(" + item["BGDEP"].ToString() + ")" + item["DEPTNAME"].ToString() + ",";
                    if (X % 8 == 7)
                        串部門 = 串部門 + "\r\n";
                    X++;
                }
                串部門 = "群組：" + 串部門.TrimEnd(',');


                群組_總表 Reporter1 = new 群組_總表();
                //Reporter1.SetDataSource(Dt_明細);
                Reporter1.Database.Tables[0].SetDataSource(群組報表.Tables[0]);
                Form ReportForm = new Form();
                CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                CryReport.ReportSource = Reporter1;
                CryReport.Dock = DockStyle.Fill;
                Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                Reporter1.ParameterFields["部門"].CurrentValues.AddValue(串部門);
                Reporter1.ParameterFields["月份"].CurrentValues.AddValue(naviTextBox2.Text.Substring(0, 4) + "年" + naviTextBox2.Text.Substring(4, 2) + "月");
                Reporter1.ParameterFields["項目"].CurrentValues.AddValue(naviTextBox3.Text);
                Reporter1.ParameterFields["代號"].CurrentValues.AddValue("第" + naviTextBox4.Text + "群" + 組別);

                ReportForm.Controls.Add(CryReport);
                ReportForm.WindowState = FormWindowState.Maximized;
                ReportForm.Show();
            }
            else
            {
                if (string.IsNullOrEmpty(naviTextBox1.Text))
                {
                    MessageBox.Show("部門群組未填 !!", "更新訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Ad = new SqlDataAdapter("PUR2007NET_群組列印", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@上階部門", SqlDbType.VarChar).Value = naviTextBox1.Text;
                Ad.SelectCommand.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = naviTextBox2.Text;
                Ad.SelectCommand.Parameters.Add("@項目", SqlDbType.VarChar).Value = naviTextBox3.Text;
                群組報表.Clear();
                Ad.Fill(群組報表);


                string 上階部門 = string.Empty;
                string 代號 = string.Empty;
                _QueryComm.CommandText = " SELECT DEPTNAME,DEPT12,B.大群 FROM AMDDEPT A,PUR群組 B WHERE DEPTNO = @DEPTNO AND A.DEPT12=B.群組 ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = naviTextBox1.Text;
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    上階部門 = _QueryDr["DEPTNAME"].ToString();
                    代號 = "第" + _QueryDr["大群"].ToString() + "群_第" + _QueryDr["DEPT12"].ToString() + "組";
                }
                _QueryDr.Close();


                String 串部門 = string.Empty;
                int X = 0;
                foreach (DataRow item in 群組報表.Tables[2].Rows)
                {
                    串部門 += "(" + item["BGDEP"].ToString() + ")" + item["DEPTNAME"].ToString() + ",";
                    if (X % 8 == 7)
                        串部門 = 串部門 + "\r\n";
                    X++;
                }
                串部門 = "群組：" + 串部門.TrimEnd(',');


                if (radioButton1.Checked)
                {
                    群組_總表 Reporter1 = new 群組_總表();
                    //Reporter1.SetDataSource(Dt_明細);
                    Reporter1.Database.Tables[0].SetDataSource(群組報表.Tables[0]);
                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["部門"].CurrentValues.AddValue(串部門);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(naviTextBox2.Text.Substring(0, 4) + "年" + naviTextBox2.Text.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["項目"].CurrentValues.AddValue(naviTextBox3.Text);
                    Reporter1.ParameterFields["代號"].CurrentValues.AddValue(代號);

                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (radioButton2.Checked)
                {
                    //群組_品類總表 Reporter1 = new 群組_品類總表();
                    ////Reporter1.SetDataSource(Dt_明細);
                    //Reporter1.Database.Tables[0].SetDataSource(群組報表.Tables[3]);
                    //Form ReportForm = new Form();
                    //CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    //CryReport.ReportSource = Reporter1;
                    //CryReport.Dock = DockStyle.Fill;
                    //Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    //Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    //Reporter1.ParameterFields["部門"].CurrentValues.AddValue(串部門);
                    //Reporter1.ParameterFields["月份"].CurrentValues.AddValue(naviTextBox2.Text.Substring(0, 4) + "年" + naviTextBox2.Text.Substring(4, 2) + "月");
                    //Reporter1.ParameterFields["項目"].CurrentValues.AddValue(naviTextBox3.Text);
                    //Reporter1.ParameterFields["代號"].CurrentValues.AddValue(代號);

                    //ReportForm.Controls.Add(CryReport);
                    //ReportForm.WindowState = FormWindowState.Maximized;
                    //ReportForm.Show();


                    群組_總表NEW1 Reporter1 = new 群組_總表NEW1();
                    //Reporter1.SetDataSource(Dt_明細);
                    Reporter1.Database.Tables[0].SetDataSource(群組報表.Tables[0]);
                    Reporter1.Database.Tables[1].SetDataSource(群組報表.Tables[3]);
                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["部門"].CurrentValues.AddValue(串部門);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(naviTextBox2.Text.Substring(0, 4) + "年" + naviTextBox2.Text.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["項目"].CurrentValues.AddValue(naviTextBox3.Text);
                    Reporter1.ParameterFields["代號"].CurrentValues.AddValue(代號);

                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
                else if (radioButton3.Checked)
                {
                    //群組_品類部門 Reporter1 = new 群組_品類部門();
                    ////Reporter1.SetDataSource(Dt_明細);
                    //Reporter1.Database.Tables[0].SetDataSource(群組報表.Tables[1]);
                    //Form ReportForm = new Form();
                    //CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    //CryReport.ReportSource = Reporter1;
                    //CryReport.Dock = DockStyle.Fill;
                    //Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    //Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    //Reporter1.ParameterFields["部門"].CurrentValues.AddValue(串部門);
                    //Reporter1.ParameterFields["月份"].CurrentValues.AddValue(naviTextBox2.Text.Substring(0, 4) + "年" + naviTextBox2.Text.Substring(4, 2) + "月");
                    //Reporter1.ParameterFields["項目"].CurrentValues.AddValue(naviTextBox3.Text);
                    //Reporter1.ParameterFields["代號"].CurrentValues.AddValue(代號);

                    //ReportForm.Controls.Add(CryReport);
                    //ReportForm.WindowState = FormWindowState.Maximized;
                    //ReportForm.Show();

                    群組_總表NEW2 Reporter1 = new 群組_總表NEW2();
                    //Reporter1.SetDataSource(Dt_明細);
                    Reporter1.Database.Tables[0].SetDataSource(群組報表.Tables[0]);
                    Reporter1.Database.Tables[1].SetDataSource(群組報表.Tables[1]);
                    Form ReportForm = new Form();
                    CrystalDecisions.Windows.Forms.CrystalReportViewer CryReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    CryReport.ReportSource = Reporter1;
                    CryReport.Dock = DockStyle.Fill;
                    Reporter1.ParameterFields["列印人"].CurrentValues.AddValue(GonGinVariable.ApplicationUserName);
                    Reporter1.ParameterFields["CompanyName"].CurrentValues.AddValue(GonGinVariable.公司名稱);
                    Reporter1.ParameterFields["部門"].CurrentValues.AddValue(串部門);
                    Reporter1.ParameterFields["月份"].CurrentValues.AddValue(naviTextBox2.Text.Substring(0, 4) + "年" + naviTextBox2.Text.Substring(4, 2) + "月");
                    Reporter1.ParameterFields["項目"].CurrentValues.AddValue(naviTextBox3.Text);
                    Reporter1.ParameterFields["代號"].CurrentValues.AddValue(代號);

                    ReportForm.Controls.Add(CryReport);
                    ReportForm.WindowState = FormWindowState.Maximized;
                    ReportForm.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 群組列印視窗_Load(object sender, EventArgs e)
        {
            // 下拉
            Ad = new SqlDataAdapter(" SELECT DEPT12 = '第' + DEPT12 + '組',DEPTNO,DEPTNAME FROM AMDDEPT A WHERE ISNULL(DEPT12,'') <> '' ORDER BY CONVERT(INT,A.DEPT12) ", GonGinVariable.SqlConnectString);    //資料庫查詢
            Ad.SelectCommand.CommandType = CommandType.Text;
            部門群組.Clear();
            Ad.Fill(部門群組);

            naviMutiComboBox1.DataSource = 部門群組.DefaultView;
            naviMutiComboBox1.ValueMember = "DEPT12";
            naviMutiComboBox1.ValueMember = "DEPTNO";
            naviMutiComboBox1.DisplayMember = "DEPTNAME";


            Ad = new SqlDataAdapter(" SELECT DISTINCT 大群,群組 = '第' + CONVERT(VARCHAR(5),大群) + '群' FROM PUR群組 ORDER BY 大群 ", GonGinVariable.SqlConnectString);    //資料庫查詢
            Ad.SelectCommand.CommandType = CommandType.Text;
            部門大群.Clear();
            Ad.Fill(部門大群);

            naviMutiComboBox2.DataSource = 部門大群.DefaultView;
            naviMutiComboBox2.ValueMember = "大群";
            naviMutiComboBox2.ValueMember = "大群";
            naviMutiComboBox2.DisplayMember = "群組";

            radioButton4_Click(null, null);
            naviTextBox1.Text = "1100";
        }

        private void naviMutiComboBox1_TextChanged(object sender, EventArgs e)
        {
            naviTextBox1.Text = naviMutiComboBox1.SelectedValue.ToString();
        }

        private void naviDateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            naviTextBox2.Text = naviDateTimePicker2.Value.ToString("yyyyMM");
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            naviTextBox3.Text = comboBox1.SelectedItem.ToString();
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            naviTextBox3.Visible = true;
            comboBox1.Visible = true;
            label4.Visible = true;
            naviTextBox1.Visible = true;
            naviMutiComboBox1.Visible = true;
            naviTextBox4.Visible = true;
            naviMutiComboBox2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;

            if (radioButton4.Checked)
            {
                naviTextBox4.Visible = false;
                naviMutiComboBox2.Visible = false;
                label2.Visible = false;

                naviTextBox1.Text = "";
                naviTextBox1.Visible = false;
                naviMutiComboBox1.Visible = false;
                label1.Visible = false;
            }
            else if (radioButton5.Checked)
            {
                naviTextBox4.Visible = false;
                naviMutiComboBox2.Visible = false;
                label2.Visible = false;

                naviTextBox1.Text = "";
                naviTextBox1.Visible = false;
                naviMutiComboBox1.Visible = false;
                label1.Visible = false;

                label4.Visible = false;
                naviTextBox3.Visible = false;
                comboBox1.Visible = false;
            }
            else if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
            {
                naviTextBox4.Visible = false;
                naviMutiComboBox2.Visible = false;
                label2.Visible = false;

            }
            else if (radioButton6.Checked)
            {
                naviTextBox1.Text = "";
                naviTextBox1.Visible = false;
                naviMutiComboBox1.Visible = false;
                label1.Visible = false;
            }

            //else
            //    naviTextBox1.Text = "1100";
        }

        private void naviMutiComboBox2_TextChanged(object sender, EventArgs e)
        {
            naviTextBox4.Text = naviMutiComboBox2.SelectedValue.ToString();
        }
    }
}
