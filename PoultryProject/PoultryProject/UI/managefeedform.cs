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

namespace Poultary.UI
{
    public partial class managefeedform : Form
    {
        feedinterface ibl = new feedBL();
        ISupplier supplier = new SupplierBL(); 
        public managefeedform()
        {
            InitializeComponent();
            this.Load += managefeedform_Load;
        }

        private void managefeedform_Load(object sender, EventArgs e)
        {
            loadgrid();
        }
        private void loadgrid()
        {
            var list=ibl.getfeed();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["id"].Visible = false; 
            dataGridView2.Columns["supplier_id"].Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            var result=supplier.getsupplierbytype("feed");
            txtsupplier.DataSource = result;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Addfeed addfeed = new Addfeed();
            addfeed.Show();
            this.Close();
        }
    }
}
