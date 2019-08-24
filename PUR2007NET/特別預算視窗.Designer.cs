namespace PUR2007NET
{
    partial class 特別預算視窗
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(特別預算視窗));
            this.label3 = new System.Windows.Forms.Label();
            this.naviTextBox2 = new CosMoComponent.NaviTextBox();
            this.naviDateTimePicker2 = new CosMoComponent.NaviDateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.naviTextBox1 = new CosMoComponent.NaviTextBox();
            this.naviDateTimePicker1 = new CosMoComponent.NaviDateTimePicker();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(116, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 19);
            this.label3.TabIndex = 317;
            this.label3.Text = "申請月份";
            // 
            // naviTextBox2
            // 
            this.naviTextBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox2.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox2.Location = new System.Drawing.Point(211, 330);
            this.naviTextBox2.Name = "naviTextBox2";
            this.naviTextBox2.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox2.TabIndex = 316;
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
            this.naviDateTimePicker2.Location = new System.Drawing.Point(211, 330);
            this.naviDateTimePicker2.Name = "naviDateTimePicker2";
            this.naviDateTimePicker2.Size = new System.Drawing.Size(121, 22);
            this.naviDateTimePicker2.TabIndex = 315;
            this.naviDateTimePicker2.ValueChanged += new System.EventHandler(this.naviDateTimePicker2_ValueChanged);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(246, 372);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 40);
            this.button2.TabIndex = 314;
            this.button2.Text = "取消";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(117, 372);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 40);
            this.button1.TabIndex = 313;
            this.button1.Text = "確認";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(116, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 322;
            this.label1.Text = "起始月份";
            // 
            // naviTextBox1
            // 
            this.naviTextBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox1.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox1.Location = new System.Drawing.Point(211, 299);
            this.naviTextBox1.Name = "naviTextBox1";
            this.naviTextBox1.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox1.TabIndex = 321;
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
            this.naviDateTimePicker1.Location = new System.Drawing.Point(211, 299);
            this.naviDateTimePicker1.Name = "naviDateTimePicker1";
            this.naviDateTimePicker1.Size = new System.Drawing.Size(121, 22);
            this.naviDateTimePicker1.TabIndex = 320;
            this.naviDateTimePicker1.ValueChanged += new System.EventHandler(this.naviDateTimePicker1_ValueChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox4.ForeColor = System.Drawing.Color.Crimson;
            this.checkBox4.Location = new System.Drawing.Point(23, 3);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(352, 41);
            this.checkBox4.TabIndex = 324;
            this.checkBox4.Text = "特別科目(財務輸入，系統帶入的科目11、12)";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton9);
            this.groupBox1.Controls.Add(this.radioButton8);
            this.groupBox1.Controls.Add(this.radioButton7);
            this.groupBox1.Controls.Add(this.radioButton6);
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.ForeColor = System.Drawing.Color.ForestGreen;
            this.groupBox1.Location = new System.Drawing.Point(38, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 250);
            this.groupBox1.TabIndex = 326;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "列印項目";
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton9.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radioButton9.Location = new System.Drawing.Point(16, 101);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(234, 20);
            this.radioButton9.TabIndex = 8;
            this.radioButton9.Text = "I.當月預算+當月實際(BUGDA)";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radioButton8.Location = new System.Drawing.Point(236, 49);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(122, 20);
            this.radioButton8.TabIndex = 7;
            this.radioButton8.Text = "E.預算比較表";
            this.radioButton8.UseVisualStyleBackColor = true;
            this.radioButton8.Visible = false;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton7.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radioButton7.Location = new System.Drawing.Point(236, 23);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(170, 20);
            this.radioButton7.TabIndex = 6;
            this.radioButton7.Text = "D.總經理科目排序表";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.Visible = false;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton6.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radioButton6.Location = new System.Drawing.Point(16, 75);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(122, 20);
            this.radioButton6.TabIndex = 5;
            this.radioButton6.Text = "C.實際申請表";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radioButton5.Location = new System.Drawing.Point(16, 49);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(210, 20);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.Text = "B.預算+前兩月實際申請表";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radioButton4.Location = new System.Drawing.Point(16, 221);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(314, 20);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.Text = "(施工)H.一年的實際(分六大群，單位萬)";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radioButton3.Location = new System.Drawing.Point(16, 195);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(306, 20);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "(施工)G.一年的實際 + 預算(不分部門)";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radioButton2.Location = new System.Drawing.Point(16, 169);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(346, 20);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "(施工)F.組別細項(3A紙張，分群、組、部門)";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radioButton1.Location = new System.Drawing.Point(16, 23);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(122, 20);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "A.預算申請表";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // 特別預算視窗
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 439);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.naviTextBox1);
            this.Controls.Add(this.naviDateTimePicker1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.naviTextBox2);
            this.Controls.Add(this.naviDateTimePicker2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "特別預算視窗";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特別預算視窗";
            this.Load += new System.EventHandler(this.特別預算視窗_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private CosMoComponent.NaviTextBox naviTextBox2;
        private CosMoComponent.NaviDateTimePicker naviDateTimePicker2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private CosMoComponent.NaviTextBox naviTextBox1;
        private CosMoComponent.NaviDateTimePicker naviDateTimePicker1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton9;
    }
}