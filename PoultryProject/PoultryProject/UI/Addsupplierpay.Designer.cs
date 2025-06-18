namespace PoultryProject.UI
{
    partial class Addsupplierpay
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
            this.txt1 = new System.Windows.Forms.Label();
            this.txtamount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbill = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtdate
            // 
            this.txtdate.Location = new System.Drawing.Point(68, 327);
            this.txtdate.Name = "txtdate";
            this.txtdate.Size = new System.Drawing.Size(307, 22);
            this.txtdate.TabIndex = 77;
            // 
            // txtsupplier
            // 
            this.txtsupplier.FormattingEnabled = true;
            this.txtsupplier.Location = new System.Drawing.Point(69, 97);
            this.txtsupplier.Name = "txtsupplier";
            this.txtsupplier.Size = new System.Drawing.Size(307, 24);
            this.txtsupplier.TabIndex = 76;
            this.txtsupplier.TextUpdate += new System.EventHandler(this.txtsupplier_TextUpdate);
            // 
            // btnadd
            // 
            this.btnadd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnadd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnadd.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnadd.Location = new System.Drawing.Point(81, 427);
            this.btnadd.Margin = new System.Windows.Forms.Padding(4);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(275, 64);
            this.btnadd.TabIndex = 75;
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
            this.label7.Location = new System.Drawing.Point(68, 292);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.TabIndex = 74;
            this.label7.Text = " Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label6.Location = new System.Drawing.Point(68, 213);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 20);
            this.label6.TabIndex = 73;
            this.label6.Text = "Bill ID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt1
            // 
            this.txt1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt1.AutoSize = true;
            this.txt1.BackColor = System.Drawing.Color.Transparent;
            this.txt1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.txt1.Location = new System.Drawing.Point(65, 133);
            this.txt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(67, 20);
            this.txt1.TabIndex = 71;
            this.txt1.Text = "Amount";
            this.txt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtamount
            // 
            this.txtamount.BackColor = System.Drawing.Color.Gainsboro;
            this.txtamount.Location = new System.Drawing.Point(67, 165);
            this.txtamount.Multiline = true;
            this.txtamount.Name = "txtamount";
            this.txtamount.Size = new System.Drawing.Size(313, 35);
            this.txtamount.TabIndex = 70;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label2.Location = new System.Drawing.Point(77, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 69;
            this.label2.Text = "Supplier Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label1.Location = new System.Drawing.Point(87, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 32);
            this.label1.TabIndex = 68;
            this.label1.Text = "Add Supplier Payment";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtbill
            // 
            this.txtbill.FormattingEnabled = true;
            this.txtbill.Location = new System.Drawing.Point(69, 250);
            this.txtbill.Name = "txtbill";
            this.txtbill.Size = new System.Drawing.Size(307, 24);
            this.txtbill.TabIndex = 78;
            this.txtbill.SelectedIndexChanged += new System.EventHandler(this.txtbill_SelectedIndexChanged);
            // 
            // Addsupplierpay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 623);
            this.Controls.Add(this.txtbill);
            this.Controls.Add(this.txtdate);
            this.Controls.Add(this.txtsupplier);
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.txtamount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Addsupplierpay";
            this.Text = "Addsupplierpay";
            this.Load += new System.EventHandler(this.Addsupplierpay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker txtdate;
        private System.Windows.Forms.ComboBox txtsupplier;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label txt1;
        private System.Windows.Forms.TextBox txtamount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtbill;
    }
}