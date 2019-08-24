namespace PUR2007NET
{
    partial class 燃料費視窗
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(燃料費視窗));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.naviDataGridView1 = new CosMoComponent.NaviDataGridView();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FDWG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FCDS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FSIZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BGDEP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BGYM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BUGNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BGTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FSMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.金額 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRUSER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AMDUSR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AMDDAY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.帶入 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.button2 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
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
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.naviDataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(807, 387);
            this.splitContainer1.SplitterDistance = 29;
            this.splitContainer1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.ForeColor = System.Drawing.Color.Crimson;
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(329, 20);
            this.label3.TabIndex = 209;
            this.label3.Text = "欲刪除明細，請按前面的序號，在按del。";
            // 
            // naviDataGridView1
            // 
            this.naviDataGridView1.AllowUserToAddRows = false;
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
            this.BGDEP,
            this.BGYM,
            this.BUGNO,
            this.BGTYPE,
            this.數量,
            this.FSMT,
            this.金額,
            this.REMARK,
            this.CRUSER,
            this.CRDATE,
            this.AMDUSR,
            this.AMDDAY,
            this.NUM});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("新細明體", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.naviDataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.naviDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.naviDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.naviDataGridView1.Name = "naviDataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.naviDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.naviDataGridView1.RowHeadersWidth = 30;
            this.naviDataGridView1.RowTemplate.Height = 24;
            this.naviDataGridView1.Size = new System.Drawing.Size(807, 354);
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
            this.naviDataGridView1.啟動三列一粗線 = false;
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
            this.naviDataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.naviDataGridView1_CellValueChanged);
            this.naviDataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.naviDataGridView1_KeyDown);
            // 
            // UID
            // 
            this.UID.DataPropertyName = "UID";
            this.UID.HeaderText = "UID";
            this.UID.Name = "UID";
            this.UID.Visible = false;
            // 
            // FDWG
            // 
            this.FDWG.DataPropertyName = "FDWG";
            this.FDWG.HeaderText = "品項";
            this.FDWG.Name = "FDWG";
            this.FDWG.Width = 150;
            // 
            // FCDS
            // 
            this.FCDS.DataPropertyName = "FCDS";
            this.FCDS.HeaderText = "名稱";
            this.FCDS.Name = "FCDS";
            this.FCDS.Width = 250;
            // 
            // FSIZ
            // 
            this.FSIZ.DataPropertyName = "FSIZ";
            this.FSIZ.HeaderText = "規格尺寸";
            this.FSIZ.Name = "FSIZ";
            this.FSIZ.Width = 200;
            // 
            // BGDEP
            // 
            this.BGDEP.DataPropertyName = "BGDEP";
            this.BGDEP.HeaderText = "預算部門";
            this.BGDEP.Name = "BGDEP";
            this.BGDEP.Visible = false;
            // 
            // BGYM
            // 
            this.BGYM.DataPropertyName = "BGYM";
            this.BGYM.HeaderText = "預算期間";
            this.BGYM.Name = "BGYM";
            this.BGYM.Visible = false;
            // 
            // BUGNO
            // 
            this.BUGNO.DataPropertyName = "BUGNO";
            this.BUGNO.HeaderText = "科目";
            this.BUGNO.Name = "BUGNO";
            this.BUGNO.Visible = false;
            // 
            // BGTYPE
            // 
            this.BGTYPE.DataPropertyName = "BGTYPE";
            this.BGTYPE.HeaderText = "BGTYPE";
            this.BGTYPE.Name = "BGTYPE";
            this.BGTYPE.Visible = false;
            // 
            // 數量
            // 
            this.數量.DataPropertyName = "數量";
            this.數量.HeaderText = "數量";
            this.數量.Name = "數量";
            this.數量.Width = 80;
            // 
            // FSMT
            // 
            this.FSMT.DataPropertyName = "FSMT";
            this.FSMT.HeaderText = "單價";
            this.FSMT.Name = "FSMT";
            this.FSMT.Width = 80;
            // 
            // 金額
            // 
            this.金額.DataPropertyName = "金額";
            this.金額.HeaderText = "金額";
            this.金額.Name = "金額";
            this.金額.ReadOnly = true;
            // 
            // REMARK
            // 
            this.REMARK.DataPropertyName = "REMARK";
            this.REMARK.HeaderText = "備註";
            this.REMARK.Name = "REMARK";
            this.REMARK.Width = 500;
            // 
            // CRUSER
            // 
            this.CRUSER.DataPropertyName = "CRUSER";
            this.CRUSER.HeaderText = "CRUSER";
            this.CRUSER.Name = "CRUSER";
            this.CRUSER.Visible = false;
            // 
            // CRDATE
            // 
            this.CRDATE.DataPropertyName = "CRDATE";
            this.CRDATE.HeaderText = "CRDATE";
            this.CRDATE.Name = "CRDATE";
            this.CRDATE.Visible = false;
            // 
            // AMDUSR
            // 
            this.AMDUSR.DataPropertyName = "AMDUSR";
            this.AMDUSR.HeaderText = "AMDUSR";
            this.AMDUSR.Name = "AMDUSR";
            this.AMDUSR.Visible = false;
            // 
            // AMDDAY
            // 
            this.AMDDAY.DataPropertyName = "AMDDAY";
            this.AMDDAY.HeaderText = "AMDDAY";
            this.AMDDAY.Name = "AMDDAY";
            this.AMDDAY.Visible = false;
            // 
            // NUM
            // 
            this.NUM.DataPropertyName = "NUM";
            this.NUM.HeaderText = "序";
            this.NUM.Name = "NUM";
            this.NUM.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帶入,
            this.toolStripSeparator2,
            this.button2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(807, 37);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // 帶入
            // 
            this.帶入.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.帶入.Image = ((System.Drawing.Image)(resources.GetObject("帶入.Image")));
            this.帶入.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.帶入.Name = "帶入";
            this.帶入.Size = new System.Drawing.Size(63, 34);
            this.帶入.Text = "確定帶入";
            this.帶入.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.帶入.Click += new System.EventHandler(this.帶入_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 37);
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
            // 燃料費視窗
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 424);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "燃料費視窗";
            this.Text = "燃料費視窗";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.燃料費視窗_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Label label3;
        private CosMoComponent.NaviDataGridView naviDataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton 帶入;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FDWG;
        private System.Windows.Forms.DataGridViewTextBoxColumn FCDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn FSIZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn BGDEP;
        private System.Windows.Forms.DataGridViewTextBoxColumn BGYM;
        private System.Windows.Forms.DataGridViewTextBoxColumn BUGNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BGTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn 數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn FSMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn 金額;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARK;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRUSER;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn AMDUSR;
        private System.Windows.Forms.DataGridViewTextBoxColumn AMDDAY;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUM;
    }
}