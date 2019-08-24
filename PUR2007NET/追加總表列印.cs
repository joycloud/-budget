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
    public partial class 追加總表列印 : Form
    {
        private SqlDataAdapter Ad = null;
        private DateTime _起始日期 = new DateTime();
        private DateTime _結束日期 = new DateTime();
        private string _BGYM = string.Empty;
        private string _項目 = string.Empty;
        private string _科目部門 = string.Empty;

        public DateTime 起始日期
        {
            get { return _起始日期; }
            set { _起始日期 = value; }
        }
        public DateTime 結束日期
        {
            get { return _結束日期; }
            set { _結束日期 = value; }
        }
        public string BGYM
        {
            get { return _BGYM; }
            set { _BGYM = value; }
        }
        public string 項目
        {
            get { return _項目; }
            set { _項目 = value; }
        }
        public string 科目部門
        {
            get { return _科目部門; }
            set { _科目部門 = value; }
        }


        public 追加總表列印()
        {
            InitializeComponent();

            if (Convert.ToInt32(DateTime.Now.ToString("dd")) > 20)
                naviTextBox2.Text = DateTime.Now.AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);
            else
                naviTextBox2.Text = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);

            naviTextBox1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            naviTextBox11.Text = naviTextBox1.Text.Substring(0, 8) + "01";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(naviTextBox11.Text.Trim()))
            {
                MessageBox.Show("起始日期未填 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(naviTextBox1.Text.Trim()))
            {
                MessageBox.Show("結束日期未填 !!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _起始日期 = Convert.ToDateTime(naviTextBox11.Text);
            _結束日期 = Convert.ToDateTime(naviTextBox1.Text);
            _BGYM = naviTextBox2.Text;
            項目 = naviTextBox3.Text;
            科目部門 = naviTextBox4.Text;
            this.DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void naviDateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            naviTextBox11.Text = naviDateTimePicker4.Text;
        }

        private void naviDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            naviTextBox1.Text = naviDateTimePicker1.Text;
        }

        private void naviDateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            naviTextBox2.Text = naviDateTimePicker2.Value.ToString("yyyyMM");
        }

        private void 追加總表列印_Load(object sender, EventArgs e)
        {
            naviTextBox3.Text = "預算";
            naviTextBox4.Text = "部門";
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            naviTextBox3.Text = comboBox1.SelectedItem.ToString();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            naviTextBox4.Text = comboBox2.Text;
        }
    }
}
