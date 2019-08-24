namespace PUR2007NET
{
    partial class 列印總表判斷
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(列印總表判斷));
            this.label1 = new System.Windows.Forms.Label();
            this.naviTextBox11 = new CosMoComponent.NaviTextBox();
            this.naviDateTimePicker4 = new CosMoComponent.NaviDateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.間接部門 = new System.Windows.Forms.RadioButton();
            this.直接部門 = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 200;
            this.label1.Text = "申請月份";
            // 
            // naviTextBox11
            // 
            this.naviTextBox11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.naviTextBox11.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviTextBox11.Location = new System.Drawing.Point(113, 17);
            this.naviTextBox11.Name = "naviTextBox11";
            this.naviTextBox11.Size = new System.Drawing.Size(103, 25);
            this.naviTextBox11.TabIndex = 199;
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
            this.naviDateTimePicker4.Location = new System.Drawing.Point(113, 17);
            this.naviDateTimePicker4.Name = "naviDateTimePicker4";
            this.naviDateTimePicker4.Size = new System.Drawing.Size(121, 22);
            this.naviDateTimePicker4.TabIndex = 198;
            this.naviDateTimePicker4.ValueChanged += new System.EventHandler(this.naviDateTimePicker4_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.間接部門);
            this.groupBox1.Controls.Add(this.直接部門);
            this.groupBox1.Location = new System.Drawing.Point(22, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 43);
            this.groupBox1.TabIndex = 206;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "列印項目";
            // 
            // 間接部門
            // 
            this.間接部門.AutoSize = true;
            this.間接部門.Location = new System.Drawing.Point(120, 22);
            this.間接部門.Name = "間接部門";
            this.間接部門.Size = new System.Drawing.Size(71, 16);
            this.間接部門.TabIndex = 1;
            this.間接部門.Text = "間接部門";
            this.間接部門.UseVisualStyleBackColor = true;
            // 
            // 直接部門
            // 
            this.直接部門.AutoSize = true;
            this.直接部門.Checked = true;
            this.直接部門.Location = new System.Drawing.Point(22, 22);
            this.直接部門.Name = "直接部門";
            this.直接部門.Size = new System.Drawing.Size(71, 16);
            this.直接部門.TabIndex = 0;
            this.直接部門.TabStop = true;
            this.直接部門.Text = "直接部門";
            this.直接部門.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(151, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 40);
            this.button2.TabIndex = 205;
            this.button2.Text = "取消";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(22, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 40);
            this.button1.TabIndex = 204;
            this.button1.Text = "列印";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // 列印總表判斷
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 168);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.naviTextBox11);
            this.Controls.Add(this.naviDateTimePicker4);
            this.Name = "列印總表判斷";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "列印總表判斷";
            this.Load += new System.EventHandler(this.列印總表判斷_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CosMoComponent.NaviTextBox naviTextBox11;
        private CosMoComponent.NaviDateTimePicker naviDateTimePicker4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton 間接部門;
        private System.Windows.Forms.RadioButton 直接部門;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}