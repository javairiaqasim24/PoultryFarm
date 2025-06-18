using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poultary.BL.Bl;
using Poultary.DL;
using Poultary.Interfaces;
using PoultryProject.UI;
using pro.UI;

namespace Poultary.UI
{
    public partial class chicksform : Form
    {
        chickeninterfaceBL ibl=new chickenBL();
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
        public chicksform()
        {
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel8.Dock = DockStyle.Fill;
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
            pictureBox15.MouseEnter += pictureBox_MouseEnter;
            pictureBox15.MouseLeave += pictureBox_MouseLeave;
            pictureBox8.MouseEnter += pictureBox_MouseEnter;
            pictureBox8.MouseLeave += pictureBox_MouseLeave;
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

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                    }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {

        }

        private void chicksform_Load(object sender, EventArgs e)
        {
            loadgrid();
        }
        private void loadgrid()
        {
            var list = ibl.getinfo();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["batch_Id"].Visible = false;
            dataGridView2.Columns["supplierId"].Visible = false;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new mortalityform().ShowDialog();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Managechicksform m = new Managechicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().ShowDialog();
            this.Close();
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            chicksform c = new chicksform();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            chicksform c = new chicksform();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            feedform f = new feedform();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier s = new Supplier();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            new pro.UI.Supplier().ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new pro.UI.Customer().ShowDialog();
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            this.Hide();
            c.ShowDialog();
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

        private void button7_Click(object sender, EventArgs e)
        {
            customerpayments f = new customerpayments();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            customerpayments f = new customerpayments();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            if (string.IsNullOrEmpty(text))
            {
                loadgrid();

            }
            else
            {
                var list = chickenDL.getinfos(text);
                dataGridView2.DataSource = list;
                dataGridView2.Columns["batch_Id"].Visible = false;
                dataGridView2.Columns["supplierId"].Visible = false;

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        private void load()
        {
           
        }
        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Sellchicks s = new Sellchicks();    
            s.ShowDialog();
           
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Sellchicks().ShowDialog();
            this.Close();

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            new addsupplierbill().ShowDialog();
        }
    }
}
