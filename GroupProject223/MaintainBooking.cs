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
    public partial class MaintainBooking : Form
    {
        public string conStr, str;
        Int64 id;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmm = new SqlCommand();
        SqlDataAdapter adapt = new SqlDataAdapter();
        SqlDataReader read;
        DataSet ds;
        public MaintainBooking()
        {
            InitializeComponent();
        }

        private void update()
        {
            str = @"SELECT * FROM BOOKINGS";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "BOOKINGS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "BOOKINGS";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date= dateTimePicker1.Value;
                string paymentMethod = comboBox1.Text;
                double paid = double.Parse(textBox3.Text);
                conn.Open();
                str = "INSERT INTO BOOKINGS VALUES('" + date+ "','" + paymentMethod + "','" + paid + "')";
                //str = "INSERT INTO CLIENTS VALUES('301111111111','r','r','r')";
                adapt = new SqlDataAdapter();
                cmm = new SqlCommand(str, conn);
                cmm.ExecuteNonQuery();
                this.update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: BOOKING already exists");
                conn.Close();
            }
            conn.Close();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            string stre = "SELECT * FROM BOOKINGS WHERE BOOKING_ID LIKE '%" + textBox8.Text + "%'";
            adapt = new SqlDataAdapter(stre, conn);
            DataSet ds1 = new DataSet();
            adapt.Fill(ds1, "BOOKINGS");
            dataGridView1.DataSource = ds1;
            dataGridView1.DataMember = "BOOKINGS";
            if ((textBox8.Text != null) && (textBox8.Text != ""))
            {
                string str2 = "SELECT * FROM BOOKINGS WHERE BOOKING_ID='" + textBox8.Text + "'";
                cmm = new SqlCommand(str2, conn);
                read = cmm.ExecuteReader();
                while (read.Read())
                {
                    dateTimePicker2.Text = (read["BOOKING_DATE"].ToString());
                    comboBox2.Text = (read["PAYMENT_METHOD"].ToString());
                    textBox1.Text = (read["AMOUNT_PAID"].ToString());

                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker2.Value;
            string pMethod = comboBox2.Text;
            double amount = double.Parse(textBox1.Text);

            conn.Open();
          int  id = int.Parse(textBox8.Text);
            str = "UPDATE BOOKINGS SET BOOKING_DATE='" + date + "',PAYMENT_METHOD='" + pMethod +
                    "',AMOUNT_PAID='" + amount + "'WHERE BOOKING_ID='" + id + "'";
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
            id = Int64.Parse(textBox11.Text);
            str = @"DELETE FROM BOOKINGS WHERE BOOKING_ID='" + id + "'";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();

            adapt = new SqlDataAdapter();
            adapt.DeleteCommand = cmm;
            adapt.DeleteCommand.ExecuteNonQuery();
            this.update();
            conn.Close();
        }

        private void MaintainBooking_Load(object sender, EventArgs e)
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

            str = @"SELECT * FROM BOOKINGS";
            cmm = new SqlCommand(str, conn);
            ds = new DataSet();
            adapt.SelectCommand = cmm;
            adapt.Fill(ds, "BOOKINGS");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "BOOKINGS";
            conn.Close();
        }
    }
}
