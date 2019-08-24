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
    public partial class 特別自訂品項 : Form
    {
        private SqlDataAdapter Ad = null;
        private string _UID = string.Empty;
        private string _BUGNO = string.Empty;
        private string _BGYM = string.Empty;
        private string _BGDEP = string.Empty;
        private string _BGTYPE = string.Empty;
        private string _實際品項 = string.Empty;
        private DataTable _Dt_品項 = new DataTable();
        private DataTable Dt = new DataTable();

        public DataTable Dt_品項
        {
            get { return _Dt_品項; }
            set { _Dt_品項 = value; }
        }

        public 特別自訂品項(string UID, string BUGNO, string BGYM, string BGDEP, string BGTYPE, DataTable 品項, string 實際品項)
        {
            InitializeComponent();

            _UID = UID;
            _BUGNO = BUGNO;
            _BGYM = BGYM;
            _BGDEP = BGDEP;
            _BGTYPE = BGTYPE;
            Dt_品項 = 品項.Copy();
            _實際品項 = 實際品項;
            //naviDataGridView1.Columns["FDWG"].ReadOnly = true;
            //naviDataGridView1.Columns["數量"].ReadOnly = true;
            //naviDataGridView1.Columns["FSMT"].ReadOnly = true;

            naviDataGridView1.Columns["FCDS"].ReadOnly = true;
            naviDataGridView1.Columns["FSIZ"].ReadOnly = true;
            naviDataGridView1.Columns["FDWG"].ReadOnly = true;
        }

        private void 帶入_Click(object sender, EventArgs e)
        {
            this.Validate();
            Dt_品項.AcceptChanges();

            foreach (DataRow oRow in Dt_品項.Rows)
            {
                if (string.IsNullOrEmpty(oRow["數量"].ToString()))
                {
                    MessageBox.Show("數量必填!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (string.IsNullOrEmpty(oRow["FSMT"].ToString()))
                {
                    MessageBox.Show("單價必填!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            int i = 1;
            foreach (DataRow oRow in Dt_品項.Rows)
            {
                oRow["UID"] = _UID;
                oRow["BUGNO"] = _實際品項;
                oRow["BGYM"] = _BGYM;
                oRow["BGDEP"] = _BGDEP;
                oRow["BGTYPE"] = _BGTYPE;
                oRow["NUM"] = i;
                i++;
            }
            Dt_品項.AcceptChanges();
            this.DialogResult = DialogResult.Yes;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void naviDataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            Dgv.Rows[e.RowIndex].Cells["FDWG"].Value.ToString();


        }

        private void 特別自訂品項_Load(object sender, EventArgs e)
        {
            naviDataGridView1.DataSource = Dt_品項.DefaultView;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            品項查詢 form = new 品項查詢(_BUGNO);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                DataTable BB = form.品項明細.Copy();
                if (BB.Rows.Count > 0)
                {
                    foreach (DataRow item in BB.Rows)
                    {
                        Dt_品項.ImportRow(item);
                    }
                }
                Dt_品項.AcceptChanges();
            }
        }

        private void naviDataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            if (Dgv.Columns[e.ColumnIndex].Name.Equals("數量") && !string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["數量"].Value.ToString()))
            {
                decimal BGAMT1 = 0;
                if (!string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["FSMT"].Value.ToString()))
                    BGAMT1 = Convert.ToDecimal(Dgv.Rows[e.RowIndex].Cells["FSMT"].Value.ToString());

                Dgv.Rows[e.RowIndex].Cells["金額"].Value = Math.Ceiling(BGAMT1 * Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["數量"].Value.ToString()));
            }
            else if (Dgv.Columns[e.ColumnIndex].Name.Equals("FSMT") && !string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["FSMT"].Value.ToString()))
            {
                int 數量 = 0;
                if (!string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["數量"].Value.ToString()))
                    數量 = Convert.ToInt32(Dgv.Rows[e.RowIndex].Cells["數量"].Value.ToString());

                Dgv.Rows[e.RowIndex].Cells["金額"].Value = Math.Ceiling(數量 * Convert.ToDecimal(Dgv.Rows[e.RowIndex].Cells["FSMT"].Value.ToString()));
            }
        }

        private void naviDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            switch (Dgv.Columns[e.ColumnIndex].Name)
            {
                case "數量":
                case "FSMT":
                    {
                        Dgv.Rows[e.RowIndex].Cells["數量"].Style.BackColor = Color.FromArgb(180, 238, 180);
                        Dgv.Rows[e.RowIndex].Cells["FSMT"].Style.BackColor = Color.FromArgb(180, 238, 180);
                        //Dgv.Rows[e.RowIndex].Cells["FDWG"].Style.BackColor = Color.FromArgb(180, 238, 180);
                        Dgv.Rows[e.RowIndex].Cells["REMARK"].Style.BackColor = Color.FromArgb(180, 238, 180);
                    }
                    break;
            }
        }

        private void 其他資產_Click(object sender, EventArgs e)
        {
            DataTable Dt = new DataTable();
            Ad = new SqlDataAdapter("SELECT TOP 1 FDWG,FCDS,FSIZ,FSMT FROM BUGDA_ITEM ", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.Text;
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Clear();
            Dt.Clear();
            Ad.Fill(Dt);

            string 判斷 = "其他";

            資產視窗 form = new 資產視窗(Dt, 判斷);

            if (form.ShowDialog() == DialogResult.Yes)
            {
                foreach (DataRow item in form.品項明細.Rows)
                {
                    Dt_品項.ImportRow(item);
                }
            }
        }
    }
}
