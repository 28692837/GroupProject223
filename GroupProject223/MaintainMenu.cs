using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GroupProject223
{
    public partial class MaintainMenu : Form
    {
        public string conStr, str;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmm = new SqlCommand();
        SqlDataAdapter adapt = new SqlDataAdapter();
        SqlDataReader read;
        DataSet ds;
        public MaintainMenu()
        {
            InitializeComponent();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            string stre = "SELECT * FROM IN_FLIGHT_MENU WHERE MEAL_NAME LIKE '%" + textBox8.Text + "%'";
            adapt = new SqlDataAdapter(stre, conn);
            DataSet ds1 = new DataSet();
            adapt.Fill(ds1, "IN_FLIGHT_MENU");
            dataGridView1.DataSource = ds1;
            dataGridView1.DataMember = "IN_FLIGHT_MENU";
            if ((textBox8.Text != null) && (textBox8.Text != ""))
            {
                string str2 = "SELECT * FROM IN_FLIGHT_MENU WHERE MEAL_NAME='" + textBox8.Text + "'";
                cmm = new SqlCommand(str2, conn);
                read = cmm.ExecuteReader();
                while (read.Read())
                {
                   // textBox8.Text = (read["MEAL_NAME"].ToString());
                    textBox6.Text = (read["MEAL_DESCRIPTION"].ToString());
                    textBox10.Text = (read["MEAL_PRICE"].ToString());
                }
            }
            conn.Close();
        }

        private void update()
        {
            str = @"SELECT * FROM IN_FLIGHT_MENU";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "IN_FLIGHT_MENU");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "IN_FLIGHT_MENU";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                string name = textBox1.Text;
                string des = textBox2.Text;
                double price = Convert.ToDouble(textBox3.Text);
                conn.Open();
                str = "INSERT INTO IN_FLIGHT_MENU VALUES('" + name+ "','" +des+ "','" +price + "')";
                //str = "INSERT INTO CLIENTS VALUES('301111111111','r','r','r')";
                adapt = new SqlDataAdapter();
                cmm = new SqlCommand(str, conn);
                cmm.ExecuteNonQuery();
                this.update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: MENU ID(" + textBox1.Text + ") already exists");
                conn.Close();
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox8.Text;
            string des = textBox6.Text;
            double price = Convert.ToDouble(textBox10.Text);

            conn.Open();
            
            str = "UPDATE IN_FLIGHT_MENU SET MEAL_NAME='" + name + "',MEAL_DESCRIPTION='" +des +
                    "',MEAL_PRICE='" +price + "'WHERE MEAL_NAME='" +name + "'";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();

            adapt = new SqlDataAdapter();
            adapt.DeleteCommand = cmm;
            adapt.DeleteCommand.ExecuteNonQuery();
            this.update();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox11.Text;
            conn.Open();
            Name = textBox11.Text;
            str = @"DELETE FROM IN_FLIGHT_MENU WHERE MEAL_NAME='" + name + "'";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();

            adapt = new SqlDataAdapter();
            adapt.DeleteCommand = cmm;
            adapt.DeleteCommand.ExecuteNonQuery();
            this.update();
            conn.Close();
        }

        private void MaintainMenu_Load(object sender, EventArgs e)
        {
            try
            {
                conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ruang\Source\Repos\TheBigBang2\GroupProject223\GroupProject223\Airline.mdf;Integrated Security=True";
                conn = new SqlConnection(conStr);
                conn.Open();
                label4.Text = "Connected!";

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
                Application.Exit();
            }

            str = @"SELECT * FROM IN_FLIGHT_MENU";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "IN_FLIGHT_MENU");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "IN_FLIGHT_MENU";
            conn.Close();
        }
    }
}
