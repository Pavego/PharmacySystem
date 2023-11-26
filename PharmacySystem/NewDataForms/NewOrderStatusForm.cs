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
    public partial class NewOrderStatusForm : Form
    {
        MyDB db;

        public NewOrderStatusForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = textBox1.Text;

            string query = "INSERT INTO OrderStatuses (StatusID, StatusName) " +
                "Values (null, @status)";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@status", status);
            cmd.ExecuteNonQuery();

            db.CloseConnection();
            db.LoadOrderStatuses();

            this.Close();
        }
    }
}
