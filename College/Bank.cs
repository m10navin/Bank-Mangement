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

namespace College
{
    public partial class Bank : Form
    {
        public Bank()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
              
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if (comboBox1.SelectedItem.ToString() == "Bank Employee")
                {
                    this.Hide();
                    Login form2 = new Login();
                    form2.Show();
                }
                else if (comboBox1.SelectedItem.ToString() == "Customer")
                {
                    this.Hide();
                    Customer form4 = new Customer();
                    form4.Show();
                }
            }
            else
            {
                MessageBox.Show("Please select a user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
