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
    public partial class 品項檢視 : Form
    {
        private SqlDataAdapter Ad = null;
        private DataTable Dt_品項 = new DataTable();
        private string _UID = string.Empty;


        public 品項檢視(string UID)
        {
            InitializeComponent();
            _UID = UID;

        }

        private void 品項檢視_Load(object sender, EventArgs e)
        {
            SqlDataAdapter Ad = new SqlDataAdapter(" SELECT UID,FDWG,FCDS,FSIZ,數量,FSMT,金額,REMARK FROM BUGDA_簽核品項 WHERE UID =@UID ORDER BY NUM ", GonGinVariable.SqlConnectString);
            Ad.SelectCommand.CommandType = CommandType.Text;
            Ad.SelectCommand.Parameters.Clear();
            Ad.SelectCommand.CommandTimeout = 600;
            Ad.SelectCommand.Parameters.Add("@UID", SqlDbType.VarChar).Value = _UID;
            Dt_品項.Clear();
            Ad.Fill(Dt_品項);
            
            naviDataGridView1.DataSource = Dt_品項.DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void naviDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView Dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv.Rows.Count == 0)
                return;

            Dgv.Rows[e.RowIndex].Cells["金額"].Style.BackColor = Color.FromArgb(180, 238, 180);
        }
    }
}
