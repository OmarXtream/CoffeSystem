using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cafe;";

        private void create_Click(object sender, EventArgs e)
        {
            String username = usernameInput.Text;
            String Password = passwordInput.Text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("فضلاً قم بملأ الحقول");
                return;
            }

            SaveUser(username, Password);
            usernameInput.Clear();
            passwordInput.Clear();

        }

        private void SaveUser(String username, String password)
        {
            string query = "INSERT INTO users(`username`, `password`) VALUES ( '" + username + "', '" + password + "')";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("تم إضافة الموظف بنجاح");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            String ProductName = productInputName.Text;
            var ProductPrice = productInputPrice.Text;
            if (string.IsNullOrWhiteSpace(ProductName) || string.IsNullOrWhiteSpace(ProductPrice))
            {
                MessageBox.Show("فضلاً قم بملأ الحقول");
                return;
            }
            SaveProduct(ProductName, Convert.ToInt32(ProductPrice));
            productInputName.Clear();
            productInputPrice.Value = 1;


        }


        private void SaveProduct(String ProductName, int ProductPrice)
        {
            string query = "INSERT INTO products(`name`, `price`) VALUES ( '" + ProductName + "', '" + ProductPrice + "')";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("تم إضافة المنتج بنجاح");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void cashier_Click(object sender, EventArgs e)
        {
            Hide();
            Form4 form4 = new Form4();
            form4.Closed += (s, args) => this.Close();
            form4.Show();

        }
    }
}
