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
using static PharmacySystem.MyDB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PharmacySystem.NewDataForms
{
    public partial class NewMedicineForm : Form
    {
        MyDB db;

        public NewMedicineForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string medicineType = comboBox1.SelectedValue.ToString();

            string name = textBox1.Text;
            string price = textBox2.Text;
            string critialLevel = textBox3.Text;
            string quantity = textBox4.Text;

            string query = "INSERT INTO Medicines (MedicineID, Name, Type, CriticalLevel, Price, Quantity) " +
                "Values (null, @name, @type, @criticalLavel, @price, @quantity)";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@type", medicineType);
            cmd.Parameters.AddWithValue("@criticalLavel", critialLevel);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.ExecuteNonQuery();

            db.CloseConnection();
            db.LoadMedicines();

            AddMedicineComponent();

            this.Close();
        }

        private void AddMedicineComponent()
        {
            int medId = db.Medicines[db.Medicines.Count - 1].ID;
            int compId = int.Parse(comboBox2.SelectedValue.ToString());

            string query = "INSERT INTO MedicineComponent (MedicineComponentID, MedicineID, ComponentID) " +
                "Values (null, @medicine, @component)";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@medicine", medId);
            cmd.Parameters.AddWithValue("@component", compId);
            cmd.ExecuteNonQuery();

            db.CloseConnection();
            db.LoadMedicineComponents();
        }

        protected override void OnShown(EventArgs e)
        {
            comboBox1.DataSource = db.MedicineTypes;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";

            comboBox2.DataSource = db.Components;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";

            base.OnShown(e);
        }
    }
}
