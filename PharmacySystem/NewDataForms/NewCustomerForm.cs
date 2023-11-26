using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.NewDataForms
{
    public partial class NewCustomerForm : Form
    {
        MyDB db;

        public NewCustomerForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string age = textBox1.Text;
            string name = textBox2.Text;
            string address = textBox3.Text;
            string phone = textBox4.Text;

            string query = "INSERT INTO Customers (CustomerID, Name, Age, Phone, Address) " +
                "Values (null, @name, @age, @phone, @address)";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();

            db.CloseConnection();
            db.LoadCustomers();

            this.Close();
        }
    }
}
