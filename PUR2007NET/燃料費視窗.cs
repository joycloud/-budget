using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PUR2007NET
{
    public partial class 燃料費視窗 : Form
    {
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

        public 燃料費視窗(string UID, string BUGNO, string BGYM, string BGDEP, string BGTYPE, DataTable 品項, string 實際品項)
        {
            InitializeComponent();

            _UID = UID;
            _BUGNO = BUGNO;
            _BGYM = BGYM;
            _BGDEP = BGDEP;
            _BGTYPE = BGTYPE;
            _實際品項 = 實際品項;
            Dt_品項 = 品項.Copy();
            if (Dt_品項.Rows.Count < 1)
                Dt_品項.Rows.Add();
        }

        private void 燃料費視窗_Load(object sender, EventArgs e)
        {
            naviDataGridView1.DataSource = Dt_品項.DefaultView;

            naviDataGridView1.Columns["金額"].ReadOnly = true;
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

        private void naviDataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // 現在筆數的數量
            int i = naviDataGridView1.Rows.Count;

            // 現在指定的row的位置
            int j = Convert.ToInt32(naviDataGridView1.CurrentRow.Index.ToString());
            naviDataGridView1.EndEdit();

            // 最後一列才新增筆數
            if (e.KeyCode == Keys.Down && i == j + 1)
            {
                _Dt_品項.Rows.Add();
            }
            // 刪除
            //else if (e.Control == true && e.KeyCode == Keys.Delete)
            //{
            //    _Dt_品項.Rows[j].Delete();
            //    //Dt_身.AcceptChanges();
            //}
            //_Dt_品項.AcceptChanges();
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
    }
}
