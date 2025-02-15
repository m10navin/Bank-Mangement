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

namespace College.account
{
    public partial class rd : UserControl
    {
        public rd()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string imagelocation = "";
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagelocation = openFileDialog.FileName;
                    pictureBox2.ImageLocation = imagelocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string imagelocation = "";
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagelocation = openFileDialog.FileName;
                    pictureBox1.ImageLocation = imagelocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please upload a signature.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("Please upload a government proof.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Please enter the details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student;Integrated Security=True"))
                    {
                        sqlConnection.Open();
                        string name = textBox1.Text;
                        decimal amountDeposit = decimal.Parse(textBox7.Text);
                        string occupation = textBox2.Text;
                        decimal income = decimal.Parse(textBox3.Text);
                        string mobile = textBox4.Text;
                        decimal n = decimal.Parse(textBox6.Text);
                        decimal amountWithInterest = (decimal)Math.Pow(1 + (double)(12 / 100) / 2, (double)n) * amountDeposit;
                        using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO [RD] (Name, Occupation, Annual_Income, Mobile, Deposit_amount, Amount_with_interest) VALUES (@Name, @Occupation, @Annual_Income, @Mobile, @Deposit_amount, @Amount_with_interest)", sqlConnection))
                        {
                            sqlCommand.Parameters.AddWithValue("@Name", name);
                            sqlCommand.Parameters.AddWithValue("@Occupation", occupation);
                            sqlCommand.Parameters.AddWithValue("@Annual_Income", income);
                            sqlCommand.Parameters.AddWithValue("@Mobile", mobile);
                            sqlCommand.Parameters.AddWithValue("@Deposit_amount", amountDeposit);
                            sqlCommand.Parameters.AddWithValue("@Amount_with_interest", amountWithInterest);
                            int rowsAffected = sqlCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Successfully saved");
                            }
                            else
                            {
                                MessageBox.Show("Not saved");
                            }

                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error saving data to database: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving data to database: " + ex.Message);
                }
                ClearFormControls();
            }
        }
        private void ClearFormControls()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            pictureBox2.Image = null;
            pictureBox1.Image = null;
        }

    }
}
