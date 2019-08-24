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
    public partial class 選擇部門 : Form
    {
        private SqlConnection _QueryConn = null;   // 共用
        private SqlCommand _QueryComm = null;      // 共用
        private SqlDataReader _QueryDr = null;     // 共用


        private string 部門字串 = string.Empty;
        private String _部門 = string.Empty;
        private string FUN = string.Empty;

        public string 部門
        {
            get { return _部門; }
            set { _部門 = value; }
        }

        public 選擇部門(DataTable AA, string FUN)
        {
            InitializeComponent();
            
            _QueryConn = new SqlConnection(GonGinLibrary.GonGinVariable.SqlConnectString);
            _QueryComm = _QueryConn.CreateCommand();

            if (_QueryConn.State == ConnectionState.Closed) _QueryConn.Open();

            this.FUN = FUN;
            naviMutiComboBox1.DataSource = AA;
            naviMutiComboBox1.ValueMember = "DEPTNO";
            naviMutiComboBox1.DisplayMember = "DEPTNAME";
            naviMutiComboBox1.SelectedValue = "DEPTNO";
        }

        private void naviMutiComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = naviMutiComboBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("申請部門不可空白!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // 判斷是新增，還是匯入
            if (FUN == "N")
            {
                // 判斷管控是否有開啟此部門
                string 部門代號 = naviMutiComboBox1.SelectedValue.ToString();

                _QueryComm.CommandText = " SELECT 預算部門 FROM PEN007 WHERE PEN007001 = '1' AND FULLDATE=CONVERT(VARCHAR(10),GETDATE(),111) ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    部門字串 = _QueryDr["預算部門"].ToString();
                }
                _QueryDr.Close();

                // 有字串，表示有限制部門
                int k = 0;
                if (!string.IsNullOrEmpty(部門字串.Trim()))
                {
                    string[] sArray = 部門字串.Split(';');
                    foreach (string i in sArray)
                    {
                        if (!string.IsNullOrEmpty(i))
                        {
                            // 有此部門就++
                            if (i == 部門代號)
                                k++;
                        }
                    }
                    // 沒開放此部門
                    if (k == 0)
                    {
                        MessageBox.Show("未開啟" + textBox1.Text + "的申請!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                // 抓月份
                string BGYM = "";
                _QueryComm.CommandText = " SELECT BGYM = MAX(BGYM) FROM BUGDA_簽核 WHERE SCTRL = 'Y' ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    BGYM = _QueryDr["BGYM"].ToString();
                }
                _QueryDr.Close();

                if (DateTime.Now.AddMonths(1).ToString("yyyyMM") != BGYM)
                    BGYM = DateTime.Now.ToString("yyyyMM");

                // 判斷excel匯入的部門，是否已先建立
                _QueryComm.CommandText = " SELECT * FROM BUGDA_簽核 WHERE BGYM = @BGYM AND BGDEP=@BGDEP ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = BGYM;
                _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = naviMutiComboBox1.SelectedValue.ToString();
                _QueryDr = _QueryComm.ExecuteReader();

                if (!_QueryDr.HasRows)
                {
                    _QueryDr.Close();
                    MessageBox.Show(textBox1.Text + "未建立申請!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _QueryDr.Close();


                // 判斷是否有33A科目
                //_QueryComm.CommandText = " SELECT * FROM BUGDA_簽核明細 WHERE BGYM = @BGYM AND BGDEP = @BGDEP AND BUGNO = '33A' ";
                //_QueryComm.CommandType = CommandType.Text;
                //_QueryComm.Parameters.Clear();
                //_QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = BGYM;
                //_QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = naviMutiComboBox1.SelectedValue.ToString();
                //_QueryDr = _QueryComm.ExecuteReader();

                //if (!_QueryDr.HasRows)
                //{
                //    _QueryDr.Close();
                //    MessageBox.Show(textBox1.Text + "沒有建立33A科目，請聯絡MIS!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                //_QueryDr.Close();

                
                // 判斷主管還未簽核
                _QueryComm.CommandText = " SELECT 起始流程=MIN(起始流程) FROM FLOW_STEP " +
                                         " WHERE 簽核流程 = '預算系統' AND 單據單號 IN(SELECT 單據單號 FROM BUGDA_簽核 WHERE SCTRL = 'Y' AND BGYM = @BGYM AND BGDEP = @BGDEP) AND SCTRL = 'N' ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = BGYM;
                _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = naviMutiComboBox1.SelectedValue.ToString();
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    if (Convert.ToDouble(_QueryDr["起始流程"].ToString()) > 0)
                    {
                        _QueryDr.Close();
                        MessageBox.Show(textBox1.Text + "已簽核過無法匯入，請先退回至第一關!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                _QueryDr.Close();
            }

            _部門 = naviMutiComboBox1.SelectedValue.ToString();
            this.DialogResult = DialogResult.Yes;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
