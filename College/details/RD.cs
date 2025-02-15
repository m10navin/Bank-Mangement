using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace College.details
{
    public partial class RD : UserControl
    {
        public RD()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student;Integrated Security=True"))
            {
                con.Open();
                string query = "SELECT * FROM [RD] ORDER BY [Account_No]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student;Integrated Security=True"))
            {
                try
                {
                    con.Open();
                    string account = textBox1.Text;
                    string query = "DELETE FROM [RD] WHERE Account_No = @Account_No";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Account_No", account);
                        int rowsAffected = com.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Successfully deleted");
                        }
                        else
                        {
                            MessageBox.Show("No matching rows found. Not deleted");
                        }
                    }
                    string query1 = "SELECT * FROM [RD] ORDER BY [Account_No]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query1, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student;Integrated Security=True"))
            {
                con.Open();
                int account;
                if (int.TryParse(textBox1.Text, out account))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Account_No = {0}", account);
                }
            }
        }
    }
}
