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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string username = textBox1.Text;
            string password = textBox2.Text;
            try
            {
                string query = "SELECT * FROM [Login] WHERE username='" + textBox1.Text + "' AND password='" + textBox2.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    username = textBox1.Text;
                    password = textBox2.Text;
                    Employee form3 = new Employee();
                    form3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Give correct username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            conn.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
