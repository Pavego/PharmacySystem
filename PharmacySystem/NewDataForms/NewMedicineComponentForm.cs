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
    public partial class NewMedicineComponentForm : Form
    {
        MyDB db = new MyDB();

        public NewMedicineComponentForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string medicine = comboBox1.SelectedValue.ToString();
            string component = comboBox2.SelectedValue.ToString();

            string query = "INSERT INTO MedicineComponent (MedicineComponentID, MedicineID, ComponentID) " +
                "Values (null, @medicine, @component)";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@medicine", medicine);
            cmd.Parameters.AddWithValue("@component", component);
            cmd.ExecuteNonQuery();

            db.CloseConnection();
            db.LoadMedicineComponents();

            this.Close();
        }

        protected override void OnShown(EventArgs e)
        {
            comboBox1.DataSource = db.Medicines;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";

            comboBox2.DataSource = db.Components;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";

            base.OnShown(e);
        }
    }
}
