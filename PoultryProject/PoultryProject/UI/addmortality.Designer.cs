namespace Poultary.UI
{
    partial class addmortality
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
            this.txtdate = new System.Windows.Forms.DateTimePicker();
            this.txtsupplier = new System.Windows.Forms.ComboBox();
            this.btnadd = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtreason = new System.Windows.Forms.TextBox();
            this.txt1 = new System.Windows.Forms.Label();
            this.txtquantity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtdate
            // 
            this.txtdate.Location = new System.Drawing.Point(74, 313);
            this.txtdate.Name = "txtdate";
            this.txtdate.Size = new System.Drawing.Size(307, 22);
            this.txtdate.TabIndex = 67;
            this.txtdate.ValueChanged += new System.EventHandler(this.txtdate_ValueChanged);
            // 
            // txtsupplier
            // 
            this.txtsupplier.FormattingEnabled = true;
            this.txtsupplier.Location = new System.Drawing.Point(75, 83);
            this.txtsupplier.Name = "txtsupplier";
            this.txtsupplier.Size = new System.Drawing.Size(307, 24);
            this.txtsupplier.TabIndex = 66;
            this.txtsupplier.TextUpdate += new System.EventHandler(this.txtsupplier_TextUpdate);
            // 
            // btnadd
            // 
            this.btnadd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnadd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnadd.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnadd.Location = new System.Drawing.Point(87, 413);
            this.btnadd.Margin = new System.Windows.Forms.Padding(4);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(275, 64);
            this.btnadd.TabIndex = 63;
            this.btnadd.Text = "ADD";
            this.btnadd.UseVisualStyleBackColor = false;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label7.Location = new System.Drawing.Point(74, 278);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.TabIndex = 62;
            this.label7.Text = " Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label6.Location = new System.Drawing.Point(74, 199);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 20);
            this.label6.TabIndex = 61;
            this.label6.Text = "Reason";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtreason
            // 
            this.txtreason.BackColor = System.Drawing.Color.Gainsboro;
            this.txtreason.Location = new System.Drawing.Point(72, 231);
            this.txtreason.Multiline = true;
            this.txtreason.Name = "txtreason";
            this.txtreason.Size = new System.Drawing.Size(313, 35);
            this.txtreason.TabIndex = 60;
            this.txtreason.TextChanged += new System.EventHandler(this.txtreason_TextChanged);
            // 
            // txt1
            // 
            this.txt1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt1.AutoSize = true;
            this.txt1.BackColor = System.Drawing.Color.Transparent;
            this.txt1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.txt1.Location = new System.Drawing.Point(71, 119);
            this.txt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(70, 20);
            this.txt1.TabIndex = 57;
            this.txt1.Text = "Quantity";
            this.txt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt1.Click += new System.EventHandler(this.txt1_Click);
            // 
            // txtquantity
            // 
            this.txtquantity.BackColor = System.Drawing.Color.Gainsboro;
            this.txtquantity.Location = new System.Drawing.Point(73, 151);
            this.txtquantity.Multiline = true;
            this.txtquantity.Name = "txtquantity";
            this.txtquantity.Size = new System.Drawing.Size(313, 35);
            this.txtquantity.TabIndex = 56;
            this.txtquantity.TextChanged += new System.EventHandler(this.txtquantity_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label2.Location = new System.Drawing.Point(83, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 55;
            this.label2.Text = "Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label1.Location = new System.Drawing.Point(140, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 32);
            this.label1.TabIndex = 53;
            this.label1.Text = "Add Mortality";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addmortality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 612);
            this.Controls.Add(this.txtdate);
            this.Controls.Add(this.txtsupplier);
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtreason);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.txtquantity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addmortality";
            this.Text = "addmortality";
            this.Load += new System.EventHandler(this.addmortality_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker txtdate;
        private System.Windows.Forms.ComboBox txtsupplier;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtreason;
        private System.Windows.Forms.Label txt1;
        private System.Windows.Forms.TextBox txtquantity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}