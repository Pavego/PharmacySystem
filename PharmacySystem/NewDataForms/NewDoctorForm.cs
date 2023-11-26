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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PharmacySystem.NewDataForms
{
    public partial class NewDoctorForm : Form
    {
        MyDB db;
        public NewDoctorForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string signature = textBox2.Text;
            string stamp = textBox3.Text;

            string query = "INSERT INTO Doctors (DoctorID, Name, Signature, Stamp) " +
                "Values (null, @name, @signature, @stamp)";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@signature", signature);
            cmd.Parameters.AddWithValue("@stamp", stamp);
            cmd.ExecuteNonQuery();

            db.CloseConnection();
            db.LoadDoctors();

            this.Close();
        }
    }
}
