using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pro.BL.Model;
using pro.BL.Bl;
using pro.Interface;
using MySql.Data.MySqlClient;

namespace pro.UI
{
    public partial class AddStaff : Form
    {
        Istaff staff = new StaffBL();
        private Staff main;
        public AddStaff(Staff main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtname.Text.Trim();
                string contact = txtcontact.Text.Trim();
                string cnic = txtCNIC.Text.Trim();

                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(contact) ||
                    string.IsNullOrWhiteSpace(cnic) ||
                    comboBoxType.SelectedItem == null)
                {
                    MessageBox.Show("All fields are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string type = comboBoxType.SelectedItem.ToString();

                Staffs staffMember = new Staffs(name, contact, type, cnic);
                bool result = staff.Add(staffMember);

                if (result)
                {
                    MessageBox.Show("Staff added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    main.LoadStaff();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add staff. Please try again.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                if (ex.Message.Contains("cnic_UNIQUE") || ex.Message.ToLower().Contains("cnic"))
                {
                    MessageBox.Show("A staff member with this CNIC already exists. Please enter a unique CNIC.", "Duplicate CNIC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Message.Contains("name_UNIQUE") || ex.Message.ToLower().Contains("name"))
                {
                    MessageBox.Show("A staff member with this name already exists. Please enter a unique name.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("A staff member with the same data already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
