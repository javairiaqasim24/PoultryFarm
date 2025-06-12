using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poultary.DL;

namespace Poultary.UI
{
    public partial class loginpage : Form
    {
        public loginpage()
        {
            InitializeComponent();
        }

        private void usernametxt_TextChanged(object sender, EventArgs e)
        {


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            string name = usernametxt .Text;
            string password = passwordtxt .Text;
            bool validity = loginpagedl.ValidateLogin(name, password);
            if(validity)
            {
                MessageBox.Show("login successfull");
                Form1 f = new Form1();
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials entered");
            }
        }

        private void loginpage_Load(object sender, EventArgs e)
        {

        }
    }
}
