using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poultary.BL.Bl;
using Poultary.Interfaces;
using Poultary.BL.Models;
using pro.UI;
using PoultryProject.DL;
using PoultryProject.UI;
namespace Poultary.UI
{
    public partial class Managechicksform : Form
    {
        ChickenBatchInterface ibl = new ChickenbatchBL();
        int currentitemid = -1;
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
        public Managechicksform()
        {
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            this.Shown += ViewOrderAd_Shown;
            pictureBox1.MouseEnter += pictureBox_MouseEnter;
            pictureBox1.MouseLeave += pictureBox_MouseLeave;
            pictureBox3.MouseEnter += pictureBox_MouseEnter;
            pictureBox3.MouseLeave += pictureBox_MouseLeave;
            pictureBox4.MouseEnter += pictureBox_MouseEnter;
            pictureBox4.MouseLeave += pictureBox_MouseLeave;
            pictureBox5.MouseEnter += pictureBox_MouseEnter;
            pictureBox5.MouseLeave += pictureBox_MouseLeave;
            pictureBox6.MouseEnter += pictureBox_MouseEnter;
            pictureBox6.MouseLeave += pictureBox_MouseLeave;
            pictureBox7.MouseEnter += pictureBox_MouseEnter;
            pictureBox7.MouseLeave += pictureBox_MouseLeave;
            //pictureBox15.MouseEnter += pictureBox_MouseEnter;
            //pictureBox15.MouseLeave += pictureBox_MouseLeave;
            pictureBox8.MouseEnter += pictureBox_MouseEnter;
            pictureBox8.MouseLeave += pictureBox_MouseLeave;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            new Addchickform().Show();
        }

        private void Managechicksform_Load(object sender, EventArgs e)
        {
            loadgrid();
            dataGridView2.RowEnter += dataGridView2_rowselected;
            LoadSupplierComboBox();
        }
        private void loadgrid()
        {
            var list = ibl.GetChickenBatches();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["BatchId"].Visible = false;
            dataGridView2.Columns["supplier_id"].Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

            var matchingNames = supplierBillDL.GetSupplierNamesLike(searchText);

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
        private void dataGridView2_rowselected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
            ChickenBatch selectedItem = selectedRow.DataBoundItem as ChickenBatch;
            if (selectedItem == null) return;

            txtname.Text = selectedItem.BatchName;
            txtweight.Text = selectedItem.batchweight.ToString();
            txtquantity.Text = selectedItem.batchquantity.ToString();
            txtdate.Value = selectedItem.purchaseDate;
            txtprice.Text = selectedItem.batchprice.ToString();
            txtsupplier.Text = selectedItem.supplierName;

            currentitemid = selectedItem.BatchId;

        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) { return; }
            ChickenBatch selectedUser = dataGridView2.CurrentRow.DataBoundItem as ChickenBatch;
            if (selectedUser == null) return;
            selectedUser.BatchName = txtname.Text;
            selectedUser.batchweight = double.Parse(txtweight.Text);
            selectedUser.batchquantity = int.Parse(txtquantity.Text);
            selectedUser.purchaseDate = txtdate.Value;
            selectedUser.batchprice = int.Parse(txtprice.Text);
            selectedUser.supplierName = txtsupplier.Text;
            selectedUser.BatchId = currentitemid;
            bool result = ibl.UpdateChickenBatch(selectedUser);
            if (result == true)
            {
                MessageBox.Show("Item Updated Successfully");
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Item Not Updated");
            }
            loadgrid();
        }
        private void ClearInputs()
        {
            txtname.Text = "";
            txtweight.Text = "";
            txtquantity.Text = "";
            txtdate.Value = DateTime.Now;
            txtprice.Text = "";
            txtsupplier.Text = "";
            currentitemid = -1;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;
            ChickenBatch selecteditems = dataGridView2.CurrentRow.DataBoundItem as ChickenBatch;
            if (selecteditems == null) return;

            selecteditems.BatchId = currentitemid;
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {selecteditems.BatchName}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ibl.DeleteChickenBatch(currentitemid))
            {
                MessageBox.Show("Item Deleted Successfully");
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Item Not Deleted");
            }
            loadgrid();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            loadgrid();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            chicksform m = new chicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            chicksform m = new chicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            feedform f = new feedform();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            feedform f = new feedform();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Supplierform m = new Supplierform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier m = new pro.UI.Supplier();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pro.UI.Customer c = new pro.UI.Customer();  
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pro.UI.Customer c = new pro.UI.Customer();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Staff c = new Staff();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Staff c = new Staff();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            customerpayments c = new customerpayments();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void txtdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtpassword_Click(object sender, EventArgs e)
        {

        }

        private void txtemail_Click(object sender, EventArgs e)
        {

        }

        private void txtusername_Click(object sender, EventArgs e)
        {

        }

        private void txtweight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtrole_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Managechicksform().ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mortalityform m = new mortalityform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string word = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(word))
            {
                // Load the full grid when search box is empty
                dataGridView2.DataSource = ibl.GetChickenBatches();
            }
            else
            {
                // Filtered search when text is entered
                var results = ibl.getsearchitem(word);
                dataGridView2.DataSource = results;
            }
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            customerpayments c = new customerpayments();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }
    }
}
