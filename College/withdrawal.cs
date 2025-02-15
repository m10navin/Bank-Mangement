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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace College
{
    public partial class Withdrawal : Form
    {
        public Withdrawal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter the details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student;Integrated Security=True"))
                {
                    try
                    {
                        con.Open();
                        string account = textBox1.Text;
                        string amount = textBox2.Text;

                        string query = "UPDATE [Savings] SET Deposit_amount=(Deposit_amount - @Amount) WHERE Account_No = @Account_No AND Deposit_amount > @Amount";

                        using (SqlCommand com = new SqlCommand(query, con))
                        {
                            com.Parameters.AddWithValue("@Amount", amount);
                            com.Parameters.AddWithValue("@Account_No", account);
                            int rowsAffected = com.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Successfully withdrawn");
                            }
                            else
                            {
                                MessageBox.Show("No matching account number found or withdrawal amount is greater than your account's amount ");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer customer = new Customer();
            customer.Show();
        }
    }
}
