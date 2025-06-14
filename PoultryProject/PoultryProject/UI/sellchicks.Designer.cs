namespace PoultryProject.UI
{
    partial class Sellchicks
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
            this.combocustomer = new System.Windows.Forms.ComboBox();
            this.txtweight = new System.Windows.Forms.TextBox();
            this.btnprint = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtamount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtusername = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtcontact = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpaidamount = new System.Windows.Forms.TextBox();
            this.btnsave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtdate
            // 
            this.txtdate.Location = new System.Drawing.Point(190, 579);
            this.txtdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdate.Name = "txtdate";
            this.txtdate.Size = new System.Drawing.Size(345, 26);
            this.txtdate.TabIndex = 67;
            this.txtdate.ValueChanged += new System.EventHandler(this.txtdate_ValueChanged);
            // 
            // combocustomer
            // 
            this.combocustomer.FormattingEnabled = true;
            this.combocustomer.Location = new System.Drawing.Point(197, 132);
            this.combocustomer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.combocustomer.Name = "combocustomer";
            this.combocustomer.Size = new System.Drawing.Size(352, 28);
            this.combocustomer.TabIndex = 66;
            //this.combocustomer.SelectedIndexChanged += new System.EventHandler(this.combocustomer_SelectedIndexChanged);
            // 
            // txtweight
            // 
            this.txtweight.BackColor = System.Drawing.Color.Gainsboro;
            this.txtweight.Location = new System.Drawing.Point(195, 285);
            this.txtweight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtweight.Multiline = true;
            this.txtweight.Name = "txtweight";
            this.txtweight.Size = new System.Drawing.Size(352, 43);
            this.txtweight.TabIndex = 64;
            this.txtweight.TextChanged += new System.EventHandler(this.txtweight_TextChanged);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnprint.Location = new System.Drawing.Point(123, 673);
            this.btnprint.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(192, 80);
            this.btnprint.TabIndex = 63;
            this.btnprint.Text = "Print bill";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label7.Location = new System.Drawing.Point(190, 535);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 25);
            this.label7.TabIndex = 62;
            this.label7.Text = "Purchase Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label6.Location = new System.Drawing.Point(190, 349);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 25);
            this.label6.TabIndex = 61;
            this.label6.Text = "Total amount";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtamount
            // 
            this.txtamount.BackColor = System.Drawing.Color.Gainsboro;
            this.txtamount.Location = new System.Drawing.Point(197, 378);
            this.txtamount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtamount.Multiline = true;
            this.txtamount.Name = "txtamount";
            this.txtamount.Size = new System.Drawing.Size(352, 43);
            this.txtamount.TabIndex = 60;
            this.txtamount.TextChanged += new System.EventHandler(this.txtamount_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label5.Location = new System.Drawing.Point(190, 256);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 25);
            this.label5.TabIndex = 59;
            this.label5.Text = "Weight";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtusername
            // 
            this.txtusername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtusername.AutoSize = true;
            this.txtusername.BackColor = System.Drawing.Color.Transparent;
            this.txtusername.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.txtusername.Location = new System.Drawing.Point(192, 179);
            this.txtusername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(78, 25);
            this.txtusername.TabIndex = 58;
            this.txtusername.Text = "Contact";
            this.txtusername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label2.Location = new System.Drawing.Point(199, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 25);
            this.label2.TabIndex = 55;
            this.label2.Text = "Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtcontact
            // 
            this.txtcontact.BackColor = System.Drawing.Color.Gainsboro;
            this.txtcontact.Location = new System.Drawing.Point(195, 208);
            this.txtcontact.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtcontact.Multiline = true;
            this.txtcontact.Name = "txtcontact";
            this.txtcontact.Size = new System.Drawing.Size(352, 43);
            this.txtcontact.TabIndex = 54;
            this.txtcontact.TextChanged += new System.EventHandler(this.txtcontact_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label1.Location = new System.Drawing.Point(270, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 40);
            this.label1.TabIndex = 53;
            this.label1.Text = "Sell Chicks";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(34)))));
            this.label3.Location = new System.Drawing.Point(199, 437);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 25);
            this.label3.TabIndex = 68;
            this.label3.Text = "Paid amount";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtpaidamount
            // 
            this.txtpaidamount.BackColor = System.Drawing.Color.Gainsboro;
            this.txtpaidamount.Location = new System.Drawing.Point(197, 466);
            this.txtpaidamount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtpaidamount.Multiline = true;
            this.txtpaidamount.Name = "txtpaidamount";
            this.txtpaidamount.Size = new System.Drawing.Size(352, 43);
            this.txtpaidamount.TabIndex = 69;
            this.txtpaidamount.TextChanged += new System.EventHandler(this.txtpaidamount_TextChanged);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnsave.Location = new System.Drawing.Point(454, 673);
            this.btnsave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(192, 80);
            this.btnsave.TabIndex = 70;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // Sellchicks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 805);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.txtpaidamount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtdate);
            this.Controls.Add(this.combocustomer);
            this.Controls.Add(this.txtweight);
            this.Controls.Add(this.btnprint);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtamount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtusername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtcontact);
            this.Controls.Add(this.label1);
            this.Name = "Sellchicks";
            this.Text = "Sellchicks";
            this.Load += new System.EventHandler(this.Sellchicks_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker txtdate;
        private System.Windows.Forms.ComboBox combocustomer;
        private System.Windows.Forms.TextBox txtweight;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtamount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label txtusername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtcontact;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtpaidamount;
        private System.Windows.Forms.Button btnsave;
    }
}