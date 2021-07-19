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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cafe;";
        double TTax = 15;
        private void Form4_Load(object sender, EventArgs e)
        {
            // Your query,
            string query = "SELECT * FROM products";

            // Prepare the connection
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
                        // As our database, the array will contain : ID 0, name 1,price 2
                        // Do something with every received database ROW
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2) };
                        createButton(row[1], row[1]);

                    }
                }
                else
                {
                    MessageBox.Show("لا يوجد منتجات حالياً");
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

        //int i = 0;
        int x = 0;
        private void createButton(String name, String Text)
        {
            //This block dynamically creates a Button and adds it to the form
            Button btn = new Button();
            btn.Name = name;
            btn.Text = Text;
            btn.Location = new Point(150, 150 + x);
            btn.BackColor = System.Drawing.Color.White;
            //Hook our button up to our generic button handler
            btn.Click += new EventHandler(btn_Click);

            Controls.Add(btn);
            //i += 100;
            x += 30;

        }

        void btn_Click(object sender, EventArgs e)
        {
            string buttonText = ((Button)sender).Text;

            // Your query,
            string query = "SELECT * FROM products WHERE name='" + buttonText + "'";

            // Prepare the connection
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
                        // As our database, the array will contain : ID 0, name 1,price 2
                        // Do something with every received database ROW
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2) };
                        AddCost(Convert.ToInt32(row[2]));

                    }
                }
                else
                {
                    MessageBox.Show("لم يتم العثور على المنتج");
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
        double TCost = 0;
        double TTotal = 0;
        double TotalTax = 0;
        private void AddCost(double price)
        {
            // Cost
            TCost = TCost + price;
            cost.Text = "Cost:" + TCost;

            // Tax
            double result = price * (TTax / 100);
            TotalTax = TotalTax + result;
            tax.Text = "VAT(15%):" + TotalTax;

            // Total
            TTotal = TotalTax + TCost;
            total.Text = "Total:"+ TTotal;







        }

        private void Clear_Click(object sender, EventArgs e)
        {
           TCost = 0;
           TTotal = 0;
           TotalTax = 0;

            cost.Text = "Cost:" + TCost;
            tax.Text = "VAT(15%):" + TotalTax;
            total.Text = "Total:" + TTotal;

        }
    }
    }
