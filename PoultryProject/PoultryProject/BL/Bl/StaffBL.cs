using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pro.BL.Model;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pro.BL.Bl
{
    public class StaffBL:Interface.Istaff
    {
        public bool Add(Staffs s)
        {
            if (string.IsNullOrWhiteSpace(s.Name))
            {
                MessageBox.Show("Name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.Contact))
            {
                MessageBox.Show("Contact is required.");
                return false;
            }

            if (s.Contact.Length < 11 || s.Contact.Length > 15)
            {
                MessageBox.Show("Contact must be 11 to 15 characters long.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.CNIC))
            {
                MessageBox.Show("Cnic is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(s.Role))
            {
                MessageBox.Show("Please select an option from the ComboBox.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return DL.StaffDL.AddStaff(s);
        }

        public bool Update(Staffs s, int id)
        {
            if (string.IsNullOrWhiteSpace(s.Name))
            {
                MessageBox.Show("Name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.Contact))
            {
                MessageBox.Show("Contact is required.");
                return false;
            }

            if (s.Contact.Length < 11 || s.Contact.Length > 15)
            {
                MessageBox.Show("Contact must be 11 to 15 characters long.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.CNIC))
            {
                MessageBox.Show("Cnic is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(s.Role))
            {
                MessageBox.Show("Please select an option from the ComboBox.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            return DL.StaffDL.UpdateStaff(s, id);
        }

        public bool delete(int id)
        {
            return DL.StaffDL.DeleteStaff(id);
        }

        public List<Staffs> GetStaff()
        {
            return DL.StaffDL.GetStaff();
        }

        public List<Staffs> GetStaffbyName(string name)
        {
            return DL.StaffDL.SearchStaffByName(name);
        }
       
    }
}
