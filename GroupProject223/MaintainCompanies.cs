using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GroupProject223
{
    public partial class MaintainCompanies : Form
    {
        public string conStr, str;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmm = new SqlCommand();
        SqlDataAdapter adapt = new SqlDataAdapter();
        SqlDataReader read;
        DataSet ds;
        public MaintainCompanies()
        {
            InitializeComponent();
        }

        private void update()
        {
            str = @"SELECT * FROM COMPANIES";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "COMPANIES");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "COMPANIES";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                string location = textBox2.Text;
                conn.Open();
                str = "INSERT INTO COMPANIES VALUES('" +name + "','" +location + "')";
                //str = "INSERT INTO CLIENTS VALUES('301111111111','r','r','r')";
                adapt = new SqlDataAdapter();
                cmm = new SqlCommand(str, conn);
                cmm.ExecuteNonQuery();
                this.update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: COMPANY(" + textBox1.Text + ") already exists");
                conn.Close();
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox8.Text;
            string location = textBox6.Text;


            conn.Open();
            str = "UPDATE COMPANIES SET COMPANY_LOCATION='" + location + "'WHERE COMPANY_NAME='" +name+ "'";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();

            adapt = new SqlDataAdapter();
            adapt.UpdateCommand = cmm;
            adapt.UpdateCommand.ExecuteNonQuery();
            this.update();
            conn.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox11.Text;
            conn.Open();
            Name = textBox11.Text;
            str = @"DELETE FROM COMPANIES WHERE COMPANY_NAME='" + name + "'";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();

            adapt = new SqlDataAdapter();
            adapt.DeleteCommand = cmm;
            adapt.DeleteCommand.ExecuteNonQuery();
            this.update();
            conn.Close();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            string stre = "SELECT * FROM COMPANIES WHERE COMPANY_NAME LIKE '%" + textBox8.Text + "%'";
            adapt = new SqlDataAdapter(stre, conn);
            DataSet ds1 = new DataSet();
            adapt.Fill(ds1, "COMPANIES");
            dataGridView1.DataSource = ds1;
            dataGridView1.DataMember = "COMPANIES";
            if ((textBox8.Text != null) && (textBox8.Text != ""))
            {
                string str2 = "SELECT * FROM COMPANIES WHERE COMPANY_NAME='" + textBox8.Text + "'";
                cmm = new SqlCommand(str2, conn);
                read = cmm.ExecuteReader();
                while (read.Read())
                {
                    textBox6.Text = (read["COMPANY_LOCATION"].ToString());


                }
            }
            conn.Close();
        }

        private void MaintainCompanies_Load(object sender, EventArgs e)
        {
            try
            {
                conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ruang\Source\Repos\TheBigBang2\GroupProject223\GroupProject223\Airline.mdf;Integrated Security=True";
                conn = new SqlConnection(conStr);
                conn.Open();
                label3.Text = "Connected!";

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
                Application.Exit();
            }

            str = @"SELECT * FROM COMPANIES";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "COMPANIES");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "COMPANIES";
            conn.Close();
        }
    }
}
