using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace College.account
{
    public partial class fd : UserControl
    {
        public fd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int textbox6Value;
            if (pictureBox1.Image == null || pictureBox2.Image == null ||
                string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) ||
                string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox7.Text) ||
                string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Please fill in all the details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(textBox6.Text, out textbox6Value))
            {
                if (textbox6Value > 11)
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
                            decimal amountWithInterest = ((decimal)Math.Pow(1 + (double)(12 / 100) / 2, (double)n) * amountDeposit);
                            using (SqlCommand insertCommand = new SqlCommand("INSERT INTO [FD] (Name, Occupation, Annual_Income, Mobile, Deposit_amount, Amount_with_interest) VALUES (@Name, @Occupation, @Annual_Income, @Mobile, @Deposit_amount, @Amount_with_interest)", sqlConnection))
                            {
                                insertCommand.Parameters.AddWithValue("@Name", name);
                                insertCommand.Parameters.AddWithValue("@Occupation", occupation);
                                insertCommand.Parameters.AddWithValue("@Annual_Income", income);
                                insertCommand.Parameters.AddWithValue("@Mobile", mobile);
                                insertCommand.Parameters.AddWithValue("@Deposit_amount", amountDeposit);
                                insertCommand.Parameters.AddWithValue("@Amount_with_interest", amountWithInterest);

                                int rowsInserted = insertCommand.ExecuteNonQuery();

                                if (rowsInserted > 0)
                                {
                                    MessageBox.Show("Successfully saved");
                                }
                                else
                                {
                                    MessageBox.Show("Insert failed");
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("SQL error updating data in the database: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating data in the database: " + ex.Message);
                    }

                    ClearFormControls();
                }
                else
                {
                    MessageBox.Show("Time period should be more than 12 months", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
    }
}
