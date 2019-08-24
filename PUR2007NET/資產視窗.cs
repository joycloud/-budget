using GonGinLibrary;
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
    public partial class 資產視窗 : Form
    {
        private DataTable _品項明細 = new DataTable();

        public DataTable 品項明細
        {
            get { return _品項明細; }
            set { _品項明細 = value; }
        }

        public 資產視窗(DataTable BB, string 判斷)
        {
            InitializeComponent();

            if (判斷 == "其他")
            {
                BB.Rows[0]["FDWG"] = "";
                BB.Rows[0]["FCDS"] = "";
                BB.Rows[0]["FSIZ"] = "";
                BB.Rows[0]["FSMT"] = 0;
            }
            else
            {
                BB.Rows[0]["FDWG"] = "";
                BB.Rows[0]["FSMT"] = 0;
            }

            _品項明細 = BB.Copy();

            naviDataGridView1.DataSource = _品項明細.DefaultView;

            naviDataGridView1.Columns["數量"].ReadOnly = true;
            naviDataGridView1.Columns["金額"].ReadOnly = true;
            naviDataGridView1.Columns["FSMT"].ReadOnly = true;
            //naviDataGridView1.Columns["REMARK"].ReadOnly = true;
        }

        private void 資產視窗_Load(object sender, EventArgs e)
        {

        }

        private void 帶入_Click(object sender, EventArgs e)
        {
            this.Validate();
            _品項明細.AcceptChanges();
            naviDataGridView1.EndEdit();

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
                _品項明細.Rows.Add();
            }
            // 刪除
            else if (e.Control == true && e.KeyCode == Keys.Delete)
            {
                _品項明細.Rows[j].Delete();
                //Dt_身.AcceptChanges();
            }
            _品項明細.AcceptChanges();
        }

        private void naviDataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            // 輸入品項帶資料
            if (Dgv.Columns[e.ColumnIndex].Name.Equals("FDWG") && !string.IsNullOrEmpty(Dgv.Rows[e.RowIndex].Cells["FDWG"].Value.ToString()))
            {
                GonGinCheckOfDataDuplication BB = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString, "BUGDA_ITEM", "FCDS", "FSIZ", "FSMT", "FDWG = '" +
                                                                                 Dgv.Rows[e.RowIndex].Cells["FDWG"].Value.ToString() + "'");
                Dgv.Rows[e.RowIndex].Cells["FCDS"].Value = BB.傳回值;
                Dgv.Rows[e.RowIndex].Cells["FSIZ"].Value = BB.傳回值二;
                //Dgv.Rows[e.RowIndex].Cells["FSMT"].Value = BB.傳回值三;
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
                        Dgv.Rows[e.RowIndex].Cells["FCDS"].Style.BackColor = Color.FromArgb(180, 238, 180);
                        Dgv.Rows[e.RowIndex].Cells["FDWG"].Style.BackColor = Color.FromArgb(180, 238, 180);
                        Dgv.Rows[e.RowIndex].Cells["FSIZ"].Style.BackColor = Color.FromArgb(180, 238, 180);
                    }
                    break;
            }
        }
    }
}
