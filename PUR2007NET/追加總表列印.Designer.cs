namespace PUR2007NET
{
    partial class 追加總表列印
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(追加總表列印));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.naviTextBox11 = new CosMoComponent.NaviTextBox();
            this.naviDateTimePicker4 = new CosMoComponent.NaviDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.naviTextBox1 = new CosMoComponent.NaviTextBox();
            this.naviDateTimePicker1 = new CosMoComponent.NaviDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.naviTextBox2 = new CosMoComponent.NaviTextBox();
            this.naviDateTimePicker2 = new CosMoComponent.NaviDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.naviTextBox3 = new CosMoComponent.NaviTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.naviTextBox4 = new CosMoComponent.NaviTextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(261, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 40);
            this.button2.TabIndex = 210;
            this.button2.Text = "取消";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(132, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 40);
            this.button1.TabIndex = 209;
            this.button1.Text = "確認";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(244, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 207;
            this.label1.Text = "起始日期";
            // 
            // naviTextBox11
            // 
            this.naviTextBox11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox11.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox11.Location = new System.Drawing.Point(339, 17);
            this.naviTextBox11.Name = "naviTextBox11";
            this.naviTextBox11.ReadOnly = true;
            this.naviTextBox11.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox11.TabIndex = 205;
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
            this.naviDateTimePicker4.Location = new System.Drawing.Point(339, 17);
            this.naviDateTimePicker4.Name = "naviDateTimePicker4";
            this.naviDateTimePicker4.Size = new System.Drawing.Size(121, 22);
            this.naviDateTimePicker4.TabIndex = 204;
            this.naviDateTimePicker4.ValueChanged += new System.EventHandler(this.naviDateTimePicker4_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(244, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 213;
            this.label2.Text = "結束日期";
            // 
            // naviTextBox1
            // 
            this.naviTextBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox1.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox1.Location = new System.Drawing.Point(339, 63);
            this.naviTextBox1.Name = "naviTextBox1";
            this.naviTextBox1.ReadOnly = true;
            this.naviTextBox1.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox1.TabIndex = 212;
            this.naviTextBox1.欄位內容空白提示訊息 = "";
            this.naviTextBox1.欄位內容空白檢查 = false;
            this.naviTextBox1.限制字元位數 = 0;
            // 
            // naviDateTimePicker1
            // 
            this.naviDateTimePicker1.Checked = false;
            this.naviDateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.naviDateTimePicker1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.naviDateTimePicker1.Location = new System.Drawing.Point(339, 63);
            this.naviDateTimePicker1.Name = "naviDateTimePicker1";
            this.naviDateTimePicker1.Size = new System.Drawing.Size(121, 22);
            this.naviDateTimePicker1.TabIndex = 211;
            this.naviDateTimePicker1.ValueChanged += new System.EventHandler(this.naviDateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(244, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 19);
            this.label3.TabIndex = 216;
            this.label3.Text = "申請月份";
            // 
            // naviTextBox2
            // 
            this.naviTextBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox2.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox2.Location = new System.Drawing.Point(339, 106);
            this.naviTextBox2.Name = "naviTextBox2";
            this.naviTextBox2.ReadOnly = true;
            this.naviTextBox2.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox2.TabIndex = 215;
            this.naviTextBox2.欄位內容空白提示訊息 = "";
            this.naviTextBox2.欄位內容空白檢查 = false;
            this.naviTextBox2.限制字元位數 = 0;
            // 
            // naviDateTimePicker2
            // 
            this.naviDateTimePicker2.Checked = false;
            this.naviDateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.naviDateTimePicker2.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.naviDateTimePicker2.Location = new System.Drawing.Point(339, 106);
            this.naviDateTimePicker2.Name = "naviDateTimePicker2";
            this.naviDateTimePicker2.Size = new System.Drawing.Size(121, 22);
            this.naviDateTimePicker2.TabIndex = 214;
            this.naviDateTimePicker2.ValueChanged += new System.EventHandler(this.naviDateTimePicker2_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(12, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 19);
            this.label4.TabIndex = 219;
            this.label4.Text = "列印項目";
            // 
            // naviTextBox3
            // 
            this.naviTextBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox3.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox3.Location = new System.Drawing.Point(107, 60);
            this.naviTextBox3.Name = "naviTextBox3";
            this.naviTextBox3.ReadOnly = true;
            this.naviTextBox3.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox3.TabIndex = 218;
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
            "預算",
            "追加"});
            this.comboBox1.Location = new System.Drawing.Point(132, 62);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(96, 23);
            this.comboBox1.TabIndex = 301;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(12, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 19);
            this.label5.TabIndex = 320;
            this.label5.Text = "科目/部門";
            // 
            // naviTextBox4
            // 
            this.naviTextBox4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox4.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox4.Location = new System.Drawing.Point(107, 17);
            this.naviTextBox4.Name = "naviTextBox4";
            this.naviTextBox4.ReadOnly = true;
            this.naviTextBox4.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox4.TabIndex = 319;
            this.naviTextBox4.欄位內容空白提示訊息 = "";
            this.naviTextBox4.欄位內容空白檢查 = false;
            this.naviTextBox4.限制字元位數 = 0;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("新細明體", 11F);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "部門",
            "科目"});
            this.comboBox2.Location = new System.Drawing.Point(132, 19);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(96, 23);
            this.comboBox2.TabIndex = 321;
            this.comboBox2.TextChanged += new System.EventHandler(this.comboBox2_TextChanged);
            // 
            // 追加總表列印
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 215);
            this.Controls.Add(this.naviTextBox4);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.naviTextBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.naviTextBox2);
            this.Controls.Add(this.naviDateTimePicker2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.naviTextBox1);
            this.Controls.Add(this.naviDateTimePicker1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.naviTextBox11);
            this.Controls.Add(this.naviDateTimePicker4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Name = "追加總表列印";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "追加總表列印";
            this.Load += new System.EventHandler(this.追加總表列印_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private CosMoComponent.NaviTextBox naviTextBox11;
        private CosMoComponent.NaviDateTimePicker naviDateTimePicker4;
        private System.Windows.Forms.Label label2;
        private CosMoComponent.NaviTextBox naviTextBox1;
        private CosMoComponent.NaviDateTimePicker naviDateTimePicker1;
        private System.Windows.Forms.Label label3;
        private CosMoComponent.NaviTextBox naviTextBox2;
        private CosMoComponent.NaviDateTimePicker naviDateTimePicker2;
        private System.Windows.Forms.Label label4;
        private CosMoComponent.NaviTextBox naviTextBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private CosMoComponent.NaviTextBox naviTextBox4;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}