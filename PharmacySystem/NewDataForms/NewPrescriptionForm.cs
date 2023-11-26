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

namespace PharmacySystem.NewDataForms
{
    public partial class NewPrescriptionForm : Form
    {
        MyDB db;

        public NewPrescriptionForm(ref MyDB db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int customer = int.Parse(comboBox1.SelectedValue.ToString());
            int doctor = int.Parse(comboBox2.SelectedValue.ToString());
            int medicine = int.Parse(comboBox3.SelectedValue.ToString());

            int quantity = int.Parse(textBox1.Text);

            string instruction = textBox2.Text;
            string signature = textBox3.Text;
            string stamp = textBox4.Text;

            if (IsCorrectDoctor(doctor, stamp, signature))
            {
                string query = "INSERT INTO Prescriptions (PrescriptionID, CustomerID, DoctorID, MedicineID, Quantity, Instructions) " +
                    "Values (null, @custId, @docId, @medId, @quantity, @instr)";

                db.OpenConnection();

                MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
                cmd.Parameters.AddWithValue("@custId", customer);
                cmd.Parameters.AddWithValue("@docId", doctor);
                cmd.Parameters.AddWithValue("@medId", medicine);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@instr", instruction);
                cmd.ExecuteNonQuery();

                db.CloseConnection();
                db.LoadPresctiptions();

                this.Close();
            }
            else
            {
                MessageBox.Show("Подпись или печать врача не совпадает.");
            }
        }

        private bool IsCorrectDoctor(int docId, string stamp, string signature)
        {
            foreach (Doctor doctor  in db.Doctors)
            {
                if (doctor.ID == docId)
                {
                    return doctor.Stamp == stamp && doctor.Signature == signature;
                }
            }

            return false;
        }

        protected override void OnShown(EventArgs e)
        {
            comboBox1.DataSource = db.Customers;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";

            comboBox2.DataSource = db.Doctors;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";

            comboBox3.DataSource = db.Medicines;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "ID";

            base.OnShown(e);
        }
    }
}
