namespace PUR2007NET
{
    partial class 特別科目
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(特別科目));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.naviTextBox3 = new CosMoComponent.NaviTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.naviDataGridView1 = new CosMoComponent.NaviDataGridView();
            this.Gv1BUGNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gv1BUGNA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.存檔 = new System.Windows.Forms.ToolStripButton();
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
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.naviTextBox3);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.naviDataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(461, 575);
            this.splitContainer1.SplitterDistance = 37;
            this.splitContainer1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(308, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 332;
            this.label2.Text = "綠底才可修改";
            // 
            // naviTextBox3
            // 
            this.naviTextBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox3.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox3.Location = new System.Drawing.Point(3, 7);
            this.naviTextBox3.Name = "naviTextBox3";
            this.naviTextBox3.ReadOnly = true;
            this.naviTextBox3.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox3.TabIndex = 330;
            this.naviTextBox3.欄位內容空白提示訊息 = "";
            this.naviTextBox3.欄位內容空白檢查 = false;
            this.naviTextBox3.限制字元位數 = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("新細明體", 11F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "預算"});
            this.comboBox1.Location = new System.Drawing.Point(3, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 331;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(130, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 329;
            this.label1.Text = ".....";
            // 
            // naviDataGridView1
            // 
            this.naviDataGridView1.AllowUserToAddRows = false;
            this.naviDataGridView1.AllowUserToDeleteRows = false;
            this.naviDataGridView1.AllowUserToOrderColumns = true;
            this.naviDataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.naviDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.naviDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.naviDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Gv1BUGNO,
            this.Gv1BUGNA});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("新細明體", 11F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.naviDataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.naviDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.naviDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.naviDataGridView1.Name = "naviDataGridView1";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.naviDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.naviDataGridView1.RowHeadersWidth = 30;
            this.naviDataGridView1.RowTemplate.Height = 24;
            this.naviDataGridView1.Size = new System.Drawing.Size(461, 534);
            this.naviDataGridView1.TabIndex = 0;
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
            this.naviDataGridView1.資料匯入XML功能啟動或關閉 = false;
            this.naviDataGridView1.資料匯出XML功能啟動或關閉 = false;
            this.naviDataGridView1.資料維護控制項 = null;
            this.naviDataGridView1.資料繫結時啟動自動增行功能 = false;
            this.naviDataGridView1.資料繫結時啟動自動增行序號欄位 = "";
            this.naviDataGridView1.資料繫結時空白列判定欄位 = "";
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
            this.naviDataGridView1.關閉整體匯出EXCEL檔功能 = true;
            this.naviDataGridView1.關閉整體匯出格式EXCEL檔功能 = true;
            this.naviDataGridView1.關閉整體複製至剪貼功能 = true;
            this.naviDataGridView1.關閉目前位置插入一列功能 = false;
            this.naviDataGridView1.關閉目前列複製功能 = false;
            this.naviDataGridView1.關閉目前列貼上功能 = false;
            this.naviDataGridView1.關閉自訂顯示欄位設定功能 = false;
            this.naviDataGridView1.關閉菜單所有預設功能 = false;
            this.naviDataGridView1.關閉隠藏目前行功能 = false;
            this.naviDataGridView1.顯示列號 = true;
            this.naviDataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.naviDataGridView1_CellFormatting);
            this.naviDataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.naviDataGridView1_CellValueChanged);
            // 
            // Gv1BUGNO
            // 
            this.Gv1BUGNO.DataPropertyName = "BUGNO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Gv1BUGNO.DefaultCellStyle = dataGridViewCellStyle2;
            this.Gv1BUGNO.HeaderText = "科目";
            this.Gv1BUGNO.Name = "Gv1BUGNO";
            this.Gv1BUGNO.ReadOnly = true;
            this.Gv1BUGNO.Width = 50;
            // 
            // Gv1BUGNA
            // 
            this.Gv1BUGNA.DataPropertyName = "BUGNA";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Gv1BUGNA.DefaultCellStyle = dataGridViewCellStyle3;
            this.Gv1BUGNA.HeaderText = "科目名稱";
            this.Gv1BUGNA.Name = "Gv1BUGNA";
            this.Gv1BUGNA.ReadOnly = true;
            this.Gv1BUGNA.Width = 120;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.存檔,
            this.button2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(461, 37);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(33, 34);
            this.toolStripButton1.Text = "修改";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // 存檔
            // 
            this.存檔.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.存檔.Image = ((System.Drawing.Image)(resources.GetObject("存檔.Image")));
            this.存檔.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.存檔.Name = "存檔";
            this.存檔.Size = new System.Drawing.Size(37, 34);
            this.存檔.Text = "存檔";
            this.存檔.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.存檔.Visible = false;
            this.存檔.Click += new System.EventHandler(this.存檔_Click);
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
            // 特別科目
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 612);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "特別科目";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特別科目";
            this.Load += new System.EventHandler(this.特別科目_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton 存檔;
        private System.Windows.Forms.ToolStripButton button2;
        private CosMoComponent.NaviDataGridView naviDataGridView1;
        private System.Windows.Forms.Label label1;
        private CosMoComponent.NaviTextBox naviTextBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gv1BUGNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gv1BUGNA;
    }
}