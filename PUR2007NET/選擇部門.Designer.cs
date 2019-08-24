namespace PUR2007NET
{
    partial class 選擇部門
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(選擇部門));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.naviMutiComboBox1 = new CosMoComponent.NaviMutiComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(58, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 40);
            this.button1.TabIndex = 201;
            this.button1.Text = "確認";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(147, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 40);
            this.button2.TabIndex = 202;
            this.button2.Text = "取消";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox1.Location = new System.Drawing.Point(73, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(123, 23);
            this.textBox1.TabIndex = 204;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(69, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 19);
            this.label1.TabIndex = 205;
            this.label1.Text = "選擇欲申請部門";
            // 
            // naviMutiComboBox1
            // 
            this.naviMutiComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.naviMutiComboBox1.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.naviMutiComboBox1.ForeColor = System.Drawing.Color.MediumPurple;
            this.naviMutiComboBox1.FormattingEnabled = true;
            this.naviMutiComboBox1.Location = new System.Drawing.Point(73, 66);
            this.naviMutiComboBox1.Name = "naviMutiComboBox1";
            this.naviMutiComboBox1.Size = new System.Drawing.Size(139, 24);
            this.naviMutiComboBox1.TabIndex = 206;
            this.naviMutiComboBox1.SelectedValueChanged += new System.EventHandler(this.naviMutiComboBox1_SelectedValueChanged);
            // 
            // 選擇部門
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 205);
            this.Controls.Add(this.naviMutiComboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "選擇部門";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "選擇部門";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private CosMoComponent.NaviMutiComboBox naviMutiComboBox1;
    }
}