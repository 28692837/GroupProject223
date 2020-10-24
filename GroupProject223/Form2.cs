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
    public partial class MainUser : Form
    {
        public string conStr, str;
        Int64 id;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmm = new SqlCommand();
        SqlDataAdapter adapt = new SqlDataAdapter();
        SqlDataReader read;
        DataSet ds;
        public MainUser()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ruang\Source\Repos\TheBigBang2\GroupProject223\GroupProject223\Airline.mdf;Integrated Security=True";
                conn = new SqlConnection(conStr);
                conn.Open();
                label5.Text = "Connected!";

            }catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
                Application.Exit();
            }

            str = @"SELECT * FROM CLIENTS";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "CLIENTS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember="CLIENTS";
            conn.Close();
        }
        private void update()
        {
            str = @"SELECT * FROM CLIENTS";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "CLIENTS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "CLIENTS";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
         
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string name = textBox6.Text;
            string surname = textBox10.Text;
            string email = textBox9.Text;
            string password = textBox7.Text;

            conn.Open();
            id = Int64.Parse(textBox8.Text);
            str = "UPDATE CLIENTS SET CLIENT_NAME='" + name + "',CLIENT_SURNAME='" + surname + 
                    "',CLIENT_EMAIL='" + email + "',PASSWORD='" + password + "'WHERE CLIENT_ID='"+id+"'";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();

            adapt = new SqlDataAdapter();
            adapt.UpdateCommand= cmm;
            adapt.UpdateCommand.ExecuteNonQuery();
            this.update();
            conn.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            id = Int64.Parse(textBox11.Text);
            str = @"DELETE FROM CLIENTS WHERE CLIENT_ID='" + id + "'";
            cmm = new SqlCommand(str,conn);
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
           string stre = "SELECT * FROM CLIENTS WHERE CLIENT_ID LIKE '%" + textBox8.Text+ "%'";
            adapt = new SqlDataAdapter(stre, conn);
           DataSet ds1 = new DataSet();
            adapt.Fill(ds1,"CLIENTS");
            dataGridView1.DataSource = ds1;
            dataGridView1.DataMember = "CLIENTS";
            if ((textBox8.Text != null ) &&(textBox8.Text!=""))
            {
                string str2 = "SELECT * FROM CLIENTS WHERE CLIENT_ID='" + textBox8.Text + "'";
                cmm = new SqlCommand(str2, conn);
                read = cmm.ExecuteReader();
                while (read.Read())
                {
                    textBox6.Text = (read["CLIENT_NAME"].ToString());
                    textBox10.Text = (read["CLIENT_SURNAME"].ToString());
                    textBox9.Text = (read["CLIENT_EMAIL"].ToString());
                    textBox7.Text = (read["PASSWORD"].ToString());

                }
            }
            conn.Close();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            string stre = "SELECT * FROM CLIENTS WHERE CLIENT_ID LIKE '%" + textBox11.Text + "%'";
            adapt = new SqlDataAdapter(stre, conn);
            DataSet ds1 = new DataSet();
            adapt.Fill(ds1, "CLIENTS");
            dataGridView1.DataSource = ds1;
            dataGridView1.DataMember = "CLIENTS";

            conn.Close();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                id = Int64.Parse(textBox4.Text);
                string name = textBox1.Text;
                string surname = textBox2.Text;
                string email = textBox3.Text;
                string password = textBox5.Text;
                conn.Open();
                str = "INSERT INTO CLIENTS VALUES('" + id + "','" + name + "','" + surname + "','" + email + "','" + password + "')";
                //str = "INSERT INTO CLIENTS VALUES('301111111111','r','r','r')";
                adapt = new SqlDataAdapter();
                cmm = new SqlCommand(str, conn);
                cmm.ExecuteNonQuery();
                this.update();
              
            }
                catch(Exception ex)
            {
                MessageBox.Show("ERROR: Client ID("+textBox4.Text+") already exists");
                conn.Close();
            }
            conn.Close();
        }
    }
}
