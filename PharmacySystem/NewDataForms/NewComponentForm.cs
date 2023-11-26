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
    public partial class NewComponentForm : Form
    {
        MyDB db;

        public NewComponentForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;

            string query = "INSERT INTO Components (ComponentID, ComponentName) " +
                "Values (null, @name)";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();

            db.CloseConnection();
            db.LoadComponents();

            this.Close();
        }
    }
}
