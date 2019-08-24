namespace PUR2007NET
{
    partial class 品項檢視
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(品項檢視));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.naviDataGridView1 = new CosMoComponent.NaviDataGridView();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FDWG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FCDS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FSIZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FSMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.金額 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.button2 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.naviDataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 37);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.naviDataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1218, 391);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 1;
            // 
            // naviDataGridView1
            // 
            this.naviDataGridView1.AllowUserToAddRows = false;
            this.naviDataGridView1.AllowUserToDeleteRows = false;
            this.naviDataGridView1.AllowUserToOrderColumns = true;
            this.naviDataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.naviDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.naviDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.naviDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UID,
            this.FDWG,
            this.FCDS,
            this.FSIZ,
            this.數量,
            this.FSMT,
            this.金額,
            this.REMARK});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("新細明體", 11F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.naviDataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.naviDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.naviDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.naviDataGridView1.Name = "naviDataGridView1";
            this.naviDataGridView1.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.naviDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.naviDataGridView1.RowHeadersWidth = 20;
            this.naviDataGridView1.RowTemplate.Height = 24;
            this.naviDataGridView1.Size = new System.Drawing.Size(1218, 362);
            this.naviDataGridView1.TabIndex = 3;
            this.naviDataGridView1.儲存格合計 = null;
            this.naviDataGridView1.儲存格提示功能啟動或關閉 = false;
            this.naviDataGridView1.儲存格提示字串 = null;
            this.naviDataGridView1.儲存格為零值時抑制顯示 = false;
            this.naviDataGridView1.匯出檔名 = null;
            this.naviDataGridView1.匯出欄位名稱字串 = "ALL";
            this.naviDataGridView1.匯出結果字串 = "";
            this.naviDataGridView1.取得最大值欄位名稱 = "";
            this.naviDataGridView1.取得欄位最大值結果 = 0;
            this.naviDataGridView1.啟動三列一粗線 = true;
            this.naviDataGridView1.啟動儲存格日期選擇 = true;
            this.naviDataGridView1.啟動只顯示所設定欄位 = false;
            this.naviDataGridView1.啟動奇數資料列的儲存格顏色 = false;
            this.naviDataGridView1.啟動座標顏色顯示 = false;
            this.naviDataGridView1.指定儲存格停留欄位 = -1;
            this.naviDataGridView1.排序欄位 = null;
            this.naviDataGridView1.排序欄位顯示名稱 = null;
            this.naviDataGridView1.複製貼上列時忽略欄位 = null;
            this.naviDataGridView1.設計階段資料篩選指令 = null;
            this.naviDataGridView1.設計階段連結字串 = "Data Source=10.1.1.6;Initial Catalog=TEST;Integrated Security=False;User ID=;Pass" +
    "word=;MultipleActiveResultSets=True;Connect Timeout=0";
            this.naviDataGridView1.資料匯入XML功能啟動或關閉 = true;
            this.naviDataGridView1.資料匯出XML功能啟動或關閉 = true;
            this.naviDataGridView1.資料維護控制項 = null;
            this.naviDataGridView1.資料繫結時啟動自動增行功能 = true;
            this.naviDataGridView1.資料繫結時啟動自動增行序號欄位 = "";
            this.naviDataGridView1.資料繫結時空白列判定欄位 = "FQANO11";
            this.naviDataGridView1.關閉使用小算盤功能 = false;
            this.naviDataGridView1.關閉凍結目前行功能 = false;
            this.naviDataGridView1.關閉匯入XML格式檔案功能 = false;
            this.naviDataGridView1.關閉匯出XML格式檔案功能 = false;
            this.naviDataGridView1.關閉區塊複製至剪貼功能 = false;
            this.naviDataGridView1.關閉區塊複製至剪貼含抬頭功能 = false;
            this.naviDataGridView1.關閉右鍵複製至剪貼及匯出功能 = false;
            this.naviDataGridView1.關閉按鍵判斷處理程序 = false;
            this.naviDataGridView1.關閉排序功能 = false;
            this.naviDataGridView1.關閉換行鍵右移儲存格功能 = false;
            this.naviDataGridView1.關閉搜尋功能 = false;
            this.naviDataGridView1.關閉整體匯出EXCEL檔功能 = false;
            this.naviDataGridView1.關閉整體匯出格式EXCEL檔功能 = false;
            this.naviDataGridView1.關閉整體複製至剪貼功能 = false;
            this.naviDataGridView1.關閉目前位置插入一列功能 = false;
            this.naviDataGridView1.關閉目前列複製功能 = false;
            this.naviDataGridView1.關閉目前列貼上功能 = false;
            this.naviDataGridView1.關閉自訂顯示欄位設定功能 = false;
            this.naviDataGridView1.關閉菜單所有預設功能 = false;
            this.naviDataGridView1.關閉隠藏目前行功能 = false;
            this.naviDataGridView1.顯示列號 = true;
            this.naviDataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.naviDataGridView1_CellFormatting);
            // 
            // UID
            // 
            this.UID.DataPropertyName = "UID";
            this.UID.HeaderText = "UID";
            this.UID.Name = "UID";
            this.UID.ReadOnly = true;
            this.UID.Visible = false;
            // 
            // FDWG
            // 
            this.FDWG.DataPropertyName = "FDWG";
            this.FDWG.HeaderText = "品項";
            this.FDWG.Name = "FDWG";
            this.FDWG.ReadOnly = true;
            this.FDWG.Width = 200;
            // 
            // FCDS
            // 
            this.FCDS.DataPropertyName = "FCDS";
            this.FCDS.HeaderText = "名稱";
            this.FCDS.Name = "FCDS";
            this.FCDS.ReadOnly = true;
            this.FCDS.Width = 250;
            // 
            // FSIZ
            // 
            this.FSIZ.DataPropertyName = "FSIZ";
            this.FSIZ.HeaderText = "規格尺寸";
            this.FSIZ.Name = "FSIZ";
            this.FSIZ.ReadOnly = true;
            this.FSIZ.Width = 200;
            // 
            // 數量
            // 
            this.數量.DataPropertyName = "數量";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.數量.HeaderText = "數量";
            this.數量.Name = "數量";
            this.數量.ReadOnly = true;
            this.數量.Width = 40;
            // 
            // FSMT
            // 
            this.FSMT.DataPropertyName = "FSMT";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.FSMT.DefaultCellStyle = dataGridViewCellStyle3;
            this.FSMT.HeaderText = "單價";
            this.FSMT.Name = "FSMT";
            this.FSMT.ReadOnly = true;
            // 
            // 金額
            // 
            this.金額.DataPropertyName = "金額";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.金額.DefaultCellStyle = dataGridViewCellStyle4;
            this.金額.HeaderText = "金額";
            this.金額.Name = "金額";
            this.金額.ReadOnly = true;
            // 
            // REMARK
            // 
            this.REMARK.DataPropertyName = "REMARK";
            this.REMARK.HeaderText = "備註";
            this.REMARK.Name = "REMARK";
            this.REMARK.ReadOnly = true;
            this.REMARK.Width = 300;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.button2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1218, 37);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("新細明體", 10F);
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(39, 34);
            this.button2.Text = "關閉";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // 品項檢視
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 428);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "品項檢視";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "品項檢視";
            this.Load += new System.EventHandler(this.品項檢視_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.naviDataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private CosMoComponent.NaviDataGridView naviDataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FDWG;
        private System.Windows.Forms.DataGridViewTextBoxColumn FCDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn FSIZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn 數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn FSMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn 金額;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARK;
    }
}