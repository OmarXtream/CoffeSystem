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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cafe;";


        private void about_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 form2 = new Form2();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void create_Click(object sender, EventArgs e)
        {
            Hide();
            Form3 form3 = new Form3();
            form3.Closed += (s, args) => this.Close();
            form3.Show();
        }

        private void sign_Click(object sender, EventArgs e)
        {
            String username = usernameInput.Text;
            String Password = passwordInput.Text;

            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("فضلاً قم بملأ الحقول");
                return;
            }
            string query = "SELECT * FROM users WHERE username = '"+ username+ "' AND password='" + Password + "';";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            // Let's do it !
            try
            {
                // Open the database
                databaseConnection.Open();

                // Execute the query
                reader = commandDatabase.ExecuteReader();

                // All succesfully executed, now do something

                // IMPORTANT : 
                // If your query returns result, use the following processor :

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // As our database, the array will contain : ID 0, username 1,password 2, IsStaff 3
                        // Do something with every received database ROW
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                        MessageBox.Show(row[1] + " مرحباً بك ");
                        if(Convert.ToInt32(row[3]) == 1)
                        {
                            //MessageBox.Show("You Are an Admin");
                            Hide();
                            Form3 form3 = new Form3();
                            form3.Closed += (s, args) => this.Close();
                            form3.Show(); 


                        }
                        else
                        {
                            //MessageBox.Show("You Are a user");
                            Hide();
                            Form4 form4 = new Form4();
                            form4.Closed += (s, args) => this.Close();
                            form4.Show();

                        }
                    }
                }
                else
                {
                    MessageBox.Show("تفاصيل تسجيل الدخول الخاصه بك خاطئه , فضلاً حاول مجدداً");
                    return;

                }

                // Finally close the connection
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }

        }

    }
}
