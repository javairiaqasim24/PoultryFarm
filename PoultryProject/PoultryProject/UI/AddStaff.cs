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
            string name = txtname.Text;
            string contact = txtcontact.Text;
            string cnic = txtCNIC.Text;
            string type = comboBoxType.SelectedItem.ToString();

            Staffs c = new Staffs(name, contact,type,cnic);
            bool result = staff.Add(c);
            if (result)
            {
                MessageBox.Show("Staff added successfully.");
                main.LoadStaff();
                this.Close();

            }
            else
            {
                MessageBox.Show("Failed to add Staff. Please try again.");
            }
        }
    }
}
