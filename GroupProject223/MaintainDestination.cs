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
    public partial class MaintainDestination : Form
    {
        public string conStr, str;
        Int64 id;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmm = new SqlCommand();
        SqlDataAdapter adapt = new SqlDataAdapter();
        SqlDataReader read;
        DataSet ds;
        public MaintainDestination()
        {
            InitializeComponent();
        }


        private void MaintainDestination_Load(object sender, EventArgs e)
        {
            try
            {
                conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ruang\Source\Repos\TheBigBang2\GroupProject223\GroupProject223\Airline.mdf;Integrated Security=True";
                conn = new SqlConnection(conStr);
                conn.Open();
                label5.Text = "Connected!";

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
                Application.Exit();
            }

            str = @"SELECT * FROM DESTINATIONS";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "DESTINATIONS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "DESTINATIONS";
            conn.Close();
        }
        private void update()
        {
            str = @"SELECT * FROM DESTINATIONS";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "DESTINATIONS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "DESTINATIONS";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            string stre = "SELECT * FROM DESTINATIONS WHERE DESTINATION_ID LIKE '%" + textBox7.Text + "%'";
            adapt = new SqlDataAdapter(stre, conn);
            DataSet ds1 = new DataSet();
            adapt.Fill(ds1, "CLIENTS");
            dataGridView1.DataSource = ds1;
            dataGridView1.DataMember = "CLIENTS";
            if ((textBox7.Text != null) && (textBox7.Text != ""))
            {
                string str2 = "SELECT * FROM DESTINATIONS WHERE DESTINATION_ID='" + textBox7.Text + "'";
                cmm = new SqlCommand(str2, conn);
                read = cmm.ExecuteReader();
                while (read.Read())
                {
                    textBox4.Text = (read["DEPARTURE_LOCATION"].ToString());
                    textBox6.Text = (read["DESTINATION_LOCATION"].ToString());
                    textBox5.Text = (read["FLIGHT_DURATION"].ToString());
                    dateTimePicker2.Text = (read["FLIGHT_DATE"].ToString());
                    
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string depLocation = textBox4.Text;
            string destination = textBox6.Text;
            string duration = textBox5.Text;
            DateTime date = dateTimePicker2.Value;

            conn.Open();
           int id = int.Parse(textBox7.Text);
            str = "UPDATE DESTINATIONS SET DEPARTURE_LOCATION='" + depLocation + "',DESTINATION_LOCATION='" + destination +
                    "',FLIGHT_DURATION='" + duration + "',FLIGHT_DATE='" + date + "'WHERE DESTINATION_ID='" + id + "'";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();

            adapt = new SqlDataAdapter();
            adapt.UpdateCommand = cmm;
            adapt.UpdateCommand.ExecuteNonQuery();
            this.update();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
           int  id = int.Parse(textBox11.Text);
            str = @"DELETE FROM DESTINATIONS WHERE DESTINATION_ID='" + id + "'";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();

            adapt = new SqlDataAdapter();
            adapt.DeleteCommand = cmm;
            adapt.DeleteCommand.ExecuteNonQuery();
            this.update();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                string depLocation = textBox1.Text;
                string destination = textBox2.Text;
                string duration = textBox3.Text;
                DateTime date= dateTimePicker1.Value;
                conn.Open();
                str = "INSERT INTO DESTINATIONS VALUES('" + depLocation + "','" + destination + "','" +duration+"','"+date+ "')";
                //str = "INSERT INTO CLIENTS VALUES('301111111111','r','r','r')";
                adapt = new SqlDataAdapter();
                cmm = new SqlCommand(str, conn);
                cmm.ExecuteNonQuery();
                this.update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: Destination ID() already exists");
                conn.Close();
            }
            conn.Close();
        }
    }
}
