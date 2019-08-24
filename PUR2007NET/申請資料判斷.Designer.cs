namespace PUR2007NET
{
    partial class 申請資料判斷
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(申請資料判斷));
            this.naviTextBox11 = new CosMoComponent.NaviTextBox();
            this.naviDateTimePicker4 = new CosMoComponent.NaviDateTimePicker();
            this.naviTextBox1 = new CosMoComponent.NaviTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.追加 = new System.Windows.Forms.RadioButton();
            this.預算 = new System.Windows.Forms.RadioButton();
            this.挪用 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // naviTextBox11
            // 
            this.naviTextBox11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox11.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox11.Location = new System.Drawing.Point(134, 27);
            this.naviTextBox11.Name = "naviTextBox11";
            this.naviTextBox11.ReadOnly = true;
            this.naviTextBox11.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox11.TabIndex = 194;
            this.naviTextBox11.欄位內容空白提示訊息 = "";
            this.naviTextBox11.欄位內容空白檢查 = false;
            this.naviTextBox11.限制字元位數 = 0;
            // 
            // naviDateTimePicker4
            // 
            this.naviDateTimePicker4.Checked = false;
            this.naviDateTimePicker4.CustomFormat = "yyyy-MM-dd";
            this.naviDateTimePicker4.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviDateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.naviDateTimePicker4.Location = new System.Drawing.Point(134, 27);
            this.naviDateTimePicker4.Name = "naviDateTimePicker4";
            this.naviDateTimePicker4.Size = new System.Drawing.Size(121, 22);
            this.naviDateTimePicker4.TabIndex = 193;
            this.naviDateTimePicker4.ValueChanged += new System.EventHandler(this.naviDateTimePicker4_ValueChanged);
            // 
            // naviTextBox1
            // 
            this.naviTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.naviTextBox1.Font = new System.Drawing.Font("新細明體", 11F);
            this.naviTextBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.naviTextBox1.Location = new System.Drawing.Point(134, 76);
            this.naviTextBox1.Name = "naviTextBox1";
            this.naviTextBox1.ReadOnly = true;
            this.naviTextBox1.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox1.TabIndex = 195;
            this.naviTextBox1.欄位內容空白提示訊息 = "";
            this.naviTextBox1.欄位內容空白檢查 = false;
            this.naviTextBox1.限制字元位數 = 0;
            this.naviTextBox1.TextChanged += new System.EventHandler(this.naviTextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(39, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 197;
            this.label1.Text = "申請月份";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(79, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 19);
            this.label3.TabIndex = 199;
            this.label3.Text = "部門";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(43, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 40);
            this.button1.TabIndex = 200;
            this.button1.Text = "確認";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(172, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 40);
            this.button2.TabIndex = 201;
            this.button2.Text = "取消";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(136, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 202;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.追加);
            this.groupBox1.Controls.Add(this.預算);
            this.groupBox1.Controls.Add(this.挪用);
            this.groupBox1.Location = new System.Drawing.Point(43, 125);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 43);
            this.groupBox1.TabIndex = 203;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "申請項目";
            // 
            // 追加
            // 
            this.追加.AutoSize = true;
            this.追加.Checked = true;
            this.追加.Location = new System.Drawing.Point(112, 21);
            this.追加.Name = "追加";
            this.追加.Size = new System.Drawing.Size(71, 16);
            this.追加.TabIndex = 1;
            this.追加.TabStop = true;
            this.追加.Text = "追加預算";
            this.追加.UseVisualStyleBackColor = true;
            this.追加.Visible = false;
            // 
            // 預算
            // 
            this.預算.AutoSize = true;
            this.預算.Location = new System.Drawing.Point(22, 22);
            this.預算.Name = "預算";
            this.預算.Size = new System.Drawing.Size(71, 16);
            this.預算.TabIndex = 0;
            this.預算.Text = "申請預算";
            this.預算.UseVisualStyleBackColor = true;
            this.預算.Visible = false;
            // 
            // 挪用
            // 
            this.挪用.AutoSize = true;
            this.挪用.Location = new System.Drawing.Point(151, 10);
            this.挪用.Name = "挪用";
            this.挪用.Size = new System.Drawing.Size(47, 16);
            this.挪用.TabIndex = 2;
            this.挪用.Text = "挪用";
            this.挪用.UseVisualStyleBackColor = true;
            this.挪用.Visible = false;
            // 
            // 申請資料判斷
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 244);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.naviTextBox1);
            this.Controls.Add(this.naviTextBox11);
            this.Controls.Add(this.naviDateTimePicker4);
            this.Name = "申請資料判斷";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "申請資料判斷";
            this.Load += new System.EventHandler(this.申請資料判斷_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CosMoComponent.NaviTextBox naviTextBox11;
        private CosMoComponent.NaviDateTimePicker naviDateTimePicker4;
        private CosMoComponent.NaviTextBox naviTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton 挪用;
        private System.Windows.Forms.RadioButton 追加;
        private System.Windows.Forms.RadioButton 預算;
    }
}