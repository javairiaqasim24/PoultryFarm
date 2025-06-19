using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pro.BL.Bl;
using pro.Interface;
using pro.BL.Model;
using pro.UI;
using PoultryProject.UI;

namespace pro.UI
{
    public partial class AddSupplier : Form
    {
        Isupplier supp = new SupplierBL();
        public AddSupplier()
        {
            InitializeComponent();
            
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtname.Text.Trim();
                string contact = txtcontact.Text.Trim();
                string address = txtaddress.Text.Trim();

                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(contact) ||
                    string.IsNullOrWhiteSpace(address) ||
                    comboBoxType.SelectedItem == null)
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string type = comboBoxType.SelectedItem.ToString();

                Suppliers supplier = new Suppliers(name, contact, address, type);
                bool result = supp.Add(supplier);

                if (result)
                {
                    MessageBox.Show("Supplier added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add supplier. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("A supplier with the same name already exists. Please use a unique name.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddSupplier_Load(object sender, EventArgs e)
        {

        }
    }
}
