using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupProject223
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainUser m1 = new MainUser();
            m1.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MaintainMenu m2 = new MaintainMenu();
            m2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MaintainCompanies m3 = new MaintainCompanies();
            m3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MaintainDestination m4 = new MaintainDestination();
            m4.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MaintainBooking m5 = new MaintainBooking();
            m5.Show();
        }
    }
}
