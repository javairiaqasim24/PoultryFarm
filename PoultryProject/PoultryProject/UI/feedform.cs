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
using PoultryProject.UI;

namespace Poultary.UI
{
    public partial class feedform : Form
    {
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
        Ifeedinfo ibl = new feedinfoBL();
        public feedform()
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
        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                loadgrid();
            }
            else
            {
                var list = ibl.searchinfo(text);
                dataGridView2.DataSource = list;
                dataGridView2.Columns["id"].Visible = false;
                dataGridView2.Columns["batchname"].Visible = false;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void feedform_Load(object sender, EventArgs e)
        {
            loadgrid();
        }
        private void loadgrid()
        {
            var list = ibl.getinfo();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["batchname"].Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
         new   managefeedform().ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            new trackfeedform().ShowDialog();
            this.Close();
        }
    }
}
