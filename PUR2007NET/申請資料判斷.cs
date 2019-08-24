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
    public partial class 申請資料判斷 : Form
    {
        private SqlDataAdapter Ad = null;
        private SqlConnection _QueryConn = null;   // 共用
        private SqlCommand _QueryComm = null;      // 共用
        private SqlDataReader _QueryDr = null;     // 共用
        private DataTable Dt_明細 = new DataTable();
        private string 項目 = string.Empty;
        private string 部門 = string.Empty;

        public 申請資料判斷(string 上階部門, string 狀態)
        {
            InitializeComponent();

            _QueryConn = new SqlConnection(GonGinVariable.SqlConnectString);
            _QueryComm = _QueryConn.CreateCommand();

            if (_QueryConn.State == ConnectionState.Closed) _QueryConn.Open();


            naviTextBox1.Text = 上階部門;
            部門 = 上階部門;

            // A預算申請、B預算追加
            if (狀態 == "A")
            {
                預算.Visible = true;
                預算.Checked = true;

                // 預算申請超過20號，就預設下個月份
                if (Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd").Substring(6, 2).ToString()) > 20)
                {
                    naviTextBox11.Text = DateTime.Now.AddMonths(1).ToString("yyyyMMdd").Substring(0, 6);
                }
                else
                    naviTextBox11.Text = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);
            }
            else if (狀態 == "B")
            {
                追加.Visible = true;
                追加.Checked = true;
                naviTextBox11.Text = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);
            }

            // 如果月份已結案，自動跳至下個月
            GonGinCheckOfDataDuplication 結案狀態 = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString, "BUGDA_簽核", "SCTRL", "SCTRL", "SCTRL", "BGDEP = '" + naviTextBox1.Text + "' AND BGYM = '" + naviTextBox11.Text + "'");
           if(結案狀態.傳回值 == "C")
                naviTextBox11.Text = DateTime.Now.AddMonths(1).ToString("yyyyMMdd").Substring(0, 6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(naviTextBox11.Text))
            {
                MessageBox.Show("申請月份未填 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (string.IsNullOrEmpty(naviTextBox1.Text))
            //{
            //    MessageBox.Show("部門未填 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            // 判斷資料是否存在
            //_QueryComm.CommandText = " SELECT * FROM BUGDA WHERE BGDEP = @BGDEP AND BGYM = @BGYM ";
            //_QueryComm.CommandType = CommandType.Text;
            //_QueryComm.Parameters.Clear();
            //_QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = naviTextBox11.Text;
            //_QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = naviTextBox1.Text;
            //_QueryDr = _QueryComm.ExecuteReader();

            //if (!_QueryDr.HasRows)
            //{
            //    MessageBox.Show("資料還未產生 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    _QueryDr.Close();
            //    return;
            //}
            //_QueryDr.Close();

            if (預算.Checked)
                項目 = "預算";
            else if (追加.Checked)
                項目 = "追加";
            else if (挪用.Checked)
                項目 = "挪用";

            // 當月預算只能申請一次
            if (項目 == "預算")
            {
                _QueryComm.CommandText = " SELECT * FROM BUGDA_簽核 WHERE BGDEP = @BGDEP AND BGYM = @BGYM AND BGTYPE = @BGTYPE AND SCTRL = 'Y' ";
                _QueryComm.CommandType = CommandType.Text;
                _QueryComm.Parameters.Clear();
                _QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = naviTextBox1.Text;
                _QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = naviTextBox11.Text;
                _QueryComm.Parameters.Add("@BGTYPE", SqlDbType.VarChar).Value = 項目;
                _QueryDr = _QueryComm.ExecuteReader();

                if (_QueryDr.HasRows)
                {
                    _QueryDr.Read();
                    MessageBox.Show(naviTextBox1.Text + "的" + naviTextBox11.Text + " 預算已有申請過了 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _QueryDr.Close();
                    return;
                }
                _QueryDr.Close();
            }


            Ad = new SqlDataAdapter("PUR2007NET_申請", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.Parameters.Add("@月份起", SqlDbType.VarChar).Value = naviTextBox11.Text;
            Ad.SelectCommand.Parameters.Add("@DEPT", SqlDbType.VarChar).Value = naviTextBox1.Text;
            Dt_明細.Clear();
            Ad.Fill(Dt_明細);

            if (Dt_明細.Rows.Count < 1)
            {
                MessageBox.Show("資料還未產生或是科目未開 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 判斷此筆資料是否申請過
            //_QueryComm.CommandText = " SELECT * FROM BUGDA_簽核 WHERE BGDEP = @BGDEP AND BGYM = @BGYM AND BGTYPE = @BGTYPE AND SCTRL = 'Y' ";
            //_QueryComm.CommandType = CommandType.Text;
            //_QueryComm.Parameters.Clear();
            //_QueryComm.Parameters.Add("@BGDEP", SqlDbType.VarChar).Value = naviTextBox1.Text;
            //_QueryComm.Parameters.Add("@BGYM", SqlDbType.VarChar).Value = naviTextBox11.Text;
            //_QueryComm.Parameters.Add("@BGTYPE", SqlDbType.VarChar).Value = 項目;
            //_QueryDr = _QueryComm.ExecuteReader();

            //if (_QueryDr.HasRows)
            //{
            //    _QueryDr.Read();
            //    // Y表示已經在簽核流程內
            //    //if (_QueryDr["SCTRL"].ToString() == "Y")
            //    //{
            //        MessageBox.Show("此資料已經在跑"+ 項目 + "簽核 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        _QueryDr.Close();
            //        return;
            //    //}
            //}
            //_QueryDr.Close();

            // 新增
            this.Visible = false;
            預算簽核申請 form = new 預算簽核申請(Dt_明細, 項目, 部門, naviTextBox11.Text);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Yes;
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }

        private void naviTextBox1_TextChanged(object sender, EventArgs e)
        {
            _QueryComm.CommandText = " SELECT DEPTNAME FROM AMDDEPT WHERE DEPTNO = @DEPTNO ";
            _QueryComm.CommandType = CommandType.Text;
            _QueryComm.Parameters.Clear();
            _QueryComm.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = naviTextBox1.Text;
            _QueryDr = _QueryComm.ExecuteReader();

            if (_QueryDr.HasRows)
            {
                _QueryDr.Read();
                label2.Text = _QueryDr["DEPTNAME"].ToString();
            }
            _QueryDr.Close();
        }

        private void naviDateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            naviTextBox11.Text = naviDateTimePicker4.Value.ToString("yyyyMM");
        }

        private void 申請資料判斷_Load(object sender, EventArgs e)
        {

        }

        //private void DEPT()
        //{
        //    DataTable DEPT_下拉 = new DataTable();

        //    SqlDataAdapter Ad = new SqlDataAdapter(" SELECT DISTINCT BGDEP,DEPTNAME FROM BUGDA A,AMDDEPT B WHERE A.BGDEP = B.DEPTNO AND ISNULL(PDEPTNO,'') <> '' AND upper(DEPTNAME) <> 'N' ORDER BY A.BGDEP ", GonGinVariable.SqlConnectString);
        //    Ad.SelectCommand.CommandType = CommandType.Text;
        //    Ad.SelectCommand.Parameters.Clear();
        //    Ad.SelectCommand.CommandTimeout = 600;
        //    DEPT_下拉.Clear();
        //    Ad.Fill(DEPT_下拉);


        //    naviMutiComboBox1.DataSource = DEPT_下拉;
        //    naviMutiComboBox1.ValueMember = "DEPTNAME";
        //    naviMutiComboBox1.DisplayMember = "DEPTNAME";
        //    naviMutiComboBox1.SelectedValue = "BGDEP";
        //}
    }
}
