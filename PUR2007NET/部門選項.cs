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
    public partial class 部門選項 : Form
    {
        private DataTable Dt_dept = new DataTable();
        private string _開啟項目 = string.Empty;
        private string _部門字串 = string.Empty;

        public string 開啟項目
        {
            get { return _開啟項目; }
            set { _開啟項目 = value; }
        }
        public string 部門字串
        {
            get { return _部門字串; }
            set { _部門字串 = value; }
        }

        public 部門選項(string 預算申請, string 預算部門)
        {
            InitializeComponent();
            comboBox1.Text = 預算申請;
            if(string.IsNullOrEmpty(預算申請))
                comboBox1.Text = "無";


            SqlDataAdapter Ad1 = new SqlDataAdapter(" SELECT SEL=CAST(0 AS BIT),DEPTNO, DEPTNAME FROM AMDDEPT WHERE ISNULL(GRDEPT1,'') <> ''  ", GonGinVariable.SqlConnectString);
            Ad1.SelectCommand.CommandType = CommandType.Text;
            Ad1.SelectCommand.Parameters.Clear();
            Ad1.SelectCommand.CommandTimeout = 600;
            Dt_dept.Clear();
            Ad1.Fill(Dt_dept);

            naviDataGridView1.DataSource = Dt_dept.DefaultView;


            string[] sArray = 預算部門.Split(';');
            foreach (string i in sArray)
            {
                if (!string.IsNullOrEmpty(i.Trim()))
                {
                    foreach (DataRow item in Dt_dept.Rows)
                    {
                        if (item["DEPTNO"].ToString() == i)
                            item["SEL"] = "True";
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Dt_dept.AcceptChanges();

            //if (Dt_dept.Select("SEL='True'").Count() > 0)
            //    DataTable AA = Dt_dept.Select("SEL='True'").CopyToDataTable();
            foreach (DataRow oRow in Dt_dept.Select("SEL='True'"))
                部門字串 += oRow["DEPTNO"].ToString() + ";";
            

            if (comboBox1.Text == "無")
                _開啟項目 = "";
            else 
                _開啟項目 = comboBox1.Text;


            this.DialogResult = DialogResult.Yes;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "A")
                label2.Text = "預算";
            else if (comboBox1.Text == "B")
                label2.Text = "追加";
            else
                label2.Text = "";
        }
    }
}
