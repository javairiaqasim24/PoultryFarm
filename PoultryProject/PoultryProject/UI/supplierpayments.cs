using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poultary;
using Poultary.BL.Models;
using Poultary.UI;
using PoultryProject.BL.Bl;
using PoultryProject.BL.Models;
using PoultryProject.DL;
using PoultryProject.Interfaces;
using pro.UI;

namespace PoultryProject.UI
{
    public partial class supplierpayments : Form
    {
        int currentitemid = -1;
        IsupplierBill ibl = new supplierBillBL();
        Isupplierpay idl = new SupplierpayBL();
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);

        public supplierpayments()
        {
            InitializeComponent();
            timer1.Interval = 10;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            this.Shown += ViewOrderAd_Shown;
            timer1.Tick += timer1_Tick;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            new Addsupplierpay().ShowDialog();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) { return; }
            supplierpay selectedUser = dataGridView2.CurrentRow.DataBoundItem as supplierpay;
            if (selectedUser == null) return;
            selectedUser.billid = Convert.ToInt32(txtbill.Text);
            selectedUser.notes = txtnotes.Text;
            selectedUser.payedamount = double.Parse(txtpayed.Text);
            selectedUser.dueamount = double.Parse(txtdue.Text);
            selectedUser.suppliername = txtsupplier.Text;
            selectedUser.id = currentitemid;
            bool result = idl.updatepayments(selectedUser);
            if (result == true)
            {
                MessageBox.Show("Item Updated Successfully");
                
            }
            else
            {
                MessageBox.Show("Item Not Updated");
            }
            loadgrid();
        }
        private void dataGridView2_rowselected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
            supplierpay selectedItem = selectedRow.DataBoundItem as supplierpay;
            if (selectedItem == null) return;
           txtnotes.Text=selectedItem.notes;
            txtbill.Text = selectedItem.billid.ToString();
            txtdue.Text = selectedItem.dueamount.ToString();
            txtpayed.Text = selectedItem.payedamount.ToString();
            txtsupplier.Text = selectedItem.suppliername;

            currentitemid = selectedItem.id;

        }
        private void LoadSupplierComboBox()
        {
            txtsupplier.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtsupplier.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsupplier.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            txtsupplier.DropDownStyle = ComboBoxStyle.DropDown;
        }
        private void txtsupplier_TextUpdate(object sender, EventArgs e)
        {
            string searchText = txtsupplier.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
                return;

            var matchingNames = ibl.getsuppliernames(searchText);

            var autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(matchingNames.ToArray());
            txtsupplier.AutoCompleteCustomSource = autoSource;

            txtsupplier.DataSource = null;
            txtsupplier.Items.Clear();
            txtsupplier.Items.AddRange(matchingNames.ToArray());

            txtsupplier.DroppedDown = true;
            txtsupplier.SelectionStart = txtsupplier.Text.Length;
            txtsupplier.SelectionLength = 0;
        }

        private void supplierpayments_Load(object sender, EventArgs e)
        {
            loadgrid();
            LoadSupplierComboBox();
            dataGridView2.RowEnter += dataGridView2_rowselected;
        }
        private void loadgrid()
        {
            var list = idl.getsupplierpayments();
            dataGridView2.DataSource=list;
            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["supplierid"].Visible = false; 

            dataGridView2.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(text))
            {
                var list = SupplierPayDL.SearchSupplierPaymentsByName(text);
                dataGridView2.DataSource = list;
            }
            else
            {
                loadgrid(); 
            }

            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["supplierid"].Visible = false;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            loadgrid();
        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

              
        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.BackColor = hoverColor;
                pictureBox.Cursor = Cursors.Hand;


            }
        }
        private void ViewOrderAd_Shown(object sender, EventArgs e)
        {
            flowLayoutPanel1.Width = PanelCollapsedWidth;

            this.PerformLayout();
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.BackColor = Color.Transparent;
                pictureBox.Cursor = Cursors.Default;


            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isPanelCollapsed)
            {
                flowLayoutPanel1.Width += SlideStep;
                if (flowLayoutPanel1.Width >= PanelExpandedWidth)
                {
                    timer1.Stop();
                    isPanelCollapsed = false;
                }
            }
            else
            {
                flowLayoutPanel1.Width -= SlideStep;
                if (flowLayoutPanel1.Width <= PanelCollapsedWidth)
                {
                    timer1.Stop();
                    isPanelCollapsed = true;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            customerpayments customerpayments = new customerpayments();
            this.Hide();
            customerpayments.ShowDialog();
            this.Close();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            customerpayments customerpayments = new customerpayments();
            this.Hide();
            customerpayments.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chicksform m = new chicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            chicksform m = new chicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            feedform f = new feedform();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            feedform f = new feedform();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new pro.UI.Customer().ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier s = new pro.UI.Supplier();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier s = new pro.UI.Supplier();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Staff f = new Staff();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Staff f = new Staff();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            priccerecordform d = new priccerecordform();
            this.Hide();
            d.ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            supplierpayments s = new supplierpayments();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
