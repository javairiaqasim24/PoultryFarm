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
using Poultary.BL.Models;
using Poultary.Interfaces;

namespace Poultary.UI
{
    public partial class addmortality : Form
    {
        mortalityinterface idl = new mortalityBl();
        public addmortality()
        {
            InitializeComponent();
        }

        private void addmortality_Load(object sender, EventArgs e)
        {
            var list=idl.getbatchnames();
            txtsupplier.DataSource = list;
            txtsupplier.DisplayMember= "batchName";
            txtsupplier.ValueMember = "batchId";
            txtsupplier.SelectedIndex = -1;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtsupplier.SelectedValue == null)
            {
                MessageBox.Show("Please select a batch.");
                return;
            }

            int batchId = Convert.ToInt32(txtsupplier.SelectedValue);
            string batchName = txtsupplier.Text;

            if (!int.TryParse(txtquantity.Text, out int count))
            {
                MessageBox.Show("Please enter a valid mortality count.");
                return;
            }

            DateTime date = txtdate.Value;
            string reason = txtreason.Text;

            // Manually set values using the constructor that matches your model
            var obj = new mortality(0, batchId, batchName, count, 0, date, reason);

            bool result = idl.addmortality(obj);
            if (result)
            {
                MessageBox.Show("Mortality added successfully");
            }
            else
            {
                MessageBox.Show("Failed to add mortality. Check if the batch exists.");
            }
        }

    }
}
