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
    public partial class 品項查詢 : Form
    {
        private DataTable Dt = new DataTable();
        private DataTable _品項明細 = new DataTable();
        private SqlDataAdapter Ad = null;
        private string _BUGNO = string.Empty;

        public DataTable 品項明細
        {
            get { return _品項明細; }
            set { _品項明細 = value; }
        }

        public 品項查詢(string BUGNO)
        {
            InitializeComponent();
            _BUGNO = BUGNO;
            //DataTable 下拉 = new DataTable();
            //SqlDataAdapter Ad = new SqlDataAdapter(" SELECT DISTINCT FDWG FROM BUGDA_ITEM ", GonGinVariable.SqlConnectString);
            //Ad.SelectCommand.CommandType = CommandType.Text;
            //Ad.SelectCommand.Parameters.Clear();
            //Ad.SelectCommand.CommandTimeout = 600;
            //Ad.SelectCommand.Parameters.Add("@BUGNO", SqlDbType.VarChar).Value = BUGNO;
            //下拉.Clear();
            //Ad.Fill(下拉);

            //// order by
            ////下拉.DefaultView.Sort = "FTNA";

            //comboBox3.DataSource = 下拉;
            //comboBox3.ValueMember = "FDWG";
            //comboBox3.DisplayMember = "FDWG";
            //comboBox3.SelectedValue = "FDWG";
        }
        private void 品項查詢_Load(object sender, EventArgs e)
        {
            if (_BUGNO == "31A" || _BUGNO == "31A1")
            {
                Ad = new SqlDataAdapter(" SELECT SEL=CONVERT(BIT,0),FTNA,A.FDWG,FCDS,FSIZ,FSMT = (CASE WHEN P.FSMT > 0 THEN P.FSMT ELSE A.FSMT END),BUGNO " +
                                        " FROM BUGDA_ITEM A LEFT JOIN (SELECT FDWG,FTYP,FSMT= (CASE WHEN ROUND(SUM(FQ11+FQ21)+0.004,2)=  0 THEN 0 ELSE CONVERT(decimal(18,2),(ROUND(SUM(FNT1+FNT2)/SUM(FQ11+FQ21)+0.004,2))) END) " +
                                        " FROM FOX02 B WHERE FYM = (SELECT INVYYMM FROM AMDSYS WHERE ITEM=2) GROUP BY B.FDWG, B.FTYP) P ON A.FDWG = P.FDWG AND A.FTYP = P.FTYP WHERE BUGNO IN('31A', '21A') ORDER BY A.FTNA,FDWG ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Dt.Clear();
                Ad.Fill(Dt);
            }
            else if (_BUGNO == "33")
            {
                Ad = new SqlDataAdapter("SELECT SEL=CONVERT(BIT,0),FTNA,FDWG,FCDS,FSIZ,FSMT,FMNA,BUGNO FROM BUGDA_ITEM WHERE FTNA = '資產' AND FDWG <> '其它設備' ORDER BY FMNA,FDWG  ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Dt.Clear();
                Ad.Fill(Dt);
            }
            else if (_BUGNO == "32A")
            {
                Ad = new SqlDataAdapter("SELECT SEL=CONVERT(BIT,0),FTNA,FDWG,FCDS,FSIZ,FSMT,FMNA,BUGNO FROM BUGDA_ITEM WHERE BUGNO IN ('32') ORDER BY FMNA,FDWG  ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Dt.Clear();
                Ad.Fill(Dt);
            }
            else if (_BUGNO == "33A")
            {
                Ad = new SqlDataAdapter("SELECT SEL=CONVERT(BIT,0),FTNA,FDWG,FCDS,FSIZ,FSMT,FMNA,BUGNO FROM BUGDA_ITEM WHERE BUGNO IN ('25A','31A') ORDER BY FMNA,FDWG  ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Dt.Clear();
                Ad.Fill(Dt);
            }
            else
            {
                Ad = new SqlDataAdapter("SELECT SEL=CONVERT(BIT,0),FTNA,FDWG,FCDS,FSIZ,FSMT,FMNA,BUGNO FROM BUGDA_ITEM WHERE BUGNO = @BUGNO ", GonGinVariable.SqlConnectString);
                Ad.SelectCommand.CommandType = CommandType.Text;
                Ad.SelectCommand.CommandTimeout = 600;
                Ad.SelectCommand.Parameters.Clear();
                Ad.SelectCommand.Parameters.Add("@BUGNO", SqlDbType.VarChar).Value = _BUGNO;
                Dt.Clear();
                Ad.Fill(Dt);
            }

            naviDataGridView1.DataSource = Dt.DefaultView;
        }

        private void 確定_Click(object sender, EventArgs e)
        {
            Dt.AcceptChanges();

            if (Dt.Rows.Count == 0)
            {
                MessageBox.Show("無資料可帶入!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            品項明細 = new DataTable();
            //品項明細.Columns.Add("UID", typeof(String));
            品項明細.Columns.Add("FDWG", typeof(String));
            品項明細.Columns.Add("FCDS", typeof(String));
            品項明細.Columns.Add("FSIZ", typeof(String));
            品項明細.Columns.Add("FSMT", typeof(decimal));
            //品項明細.Columns.Add("BGDEP", typeof(String));
            //品項明細.Columns.Add("BGYM", typeof(String));
            //品項明細.Columns.Add("BUGNO", typeof(String));
            //品項明細.Columns.Add("BGTYPE", typeof(String));


            DataTable AA = Dt.Select("SEL").CopyToDataTable();

            int i = 0;
            foreach (DataRow item in AA.Rows)
            {
                品項明細.Rows.Add();
                品項明細.Rows[i]["FDWG"] = item["FDWG"];
                品項明細.Rows[i]["FCDS"] = item["FCDS"];
                品項明細.Rows[i]["FSIZ"] = item["FSIZ"];
                品項明細.Rows[i]["FSMT"] = item["FSMT"];
                i++;
            }

            int 資產 = 0;
            foreach (DataRow item in 品項明細.Rows)
            {
                if (item["FDWG"].ToString() == "其它設備")
                    資產++;
            }

            if (資產 > 0)
            {
                DataTable BB = 品項明細.Select("FDWG = '其它設備'").CopyToDataTable();
                資產視窗 pop = new 資產視窗(BB,"無");
                if (pop.ShowDialog() == DialogResult.Yes)
                {
                    foreach (DataRow item in pop.品項明細.Rows)
                    {
                        品項明細.ImportRow(item);
                    }
                }
                品項明細.AcceptChanges();
                int j = 0;
                foreach (DataRow item in 品項明細.Rows)
                {
                    if (item["FDWG"].ToString() == "其它設備")
                    {
                        item.Delete();
                    }
                    j++;
                }
            }
            品項明細.AcceptChanges();

         

            this.DialogResult = DialogResult.Yes;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox3.Text;
        }
    }
}
