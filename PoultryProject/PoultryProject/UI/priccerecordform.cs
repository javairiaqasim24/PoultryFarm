using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoultryProject.BL.Bl;
using PoultryProject.Interfaces;

namespace PoultryProject.UI
{
    public partial class priccerecordform : Form
    {
        ISupplierpayemnts ibl = new supplierpaymentBL();
        public priccerecordform()
        {
            InitializeComponent();
        }

        private void priccerecordform_Load(object sender, EventArgs e)
        {
            loadgrid();
        }
        private void loadgrid()
        {
            var list = ibl.getsupplierpayments();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["supplierid"].Visible = false; 
            dataGridView2.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;

        }
    }
}
