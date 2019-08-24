using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUR2007NET
{
    public partial class 列印總表判斷 : Form
    {
        private string _Item = string.Empty;
        private string _BGYM = string.Empty;

        public string Item
        {
            get { return _Item; }
            set { _Item = value; }
        }
        public string BGYM
        {
            get { return _BGYM; }
            set { _BGYM = value; }
        }

        public 列印總表判斷()
        {
            InitializeComponent();

            naviTextBox11.Text = DateTime.Now.ToString("yyyyMM");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (直接部門.Checked)
                _Item = "直接";
            else if (間接部門.Checked)
                _Item = "間接";

            _BGYM = naviTextBox11.Text;
            this.DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 列印總表判斷_Load(object sender, EventArgs e)
        {
            
        }

        private void naviDateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            naviTextBox11.Text = naviDateTimePicker4.Value.ToString("yyyyMM");
        }
    }
}
