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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace PharmacySystem.NewDataForms
{
    public partial class NewOrderForm : Form
    {
        MyDB db;

        public NewOrderForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int customer = int.Parse(comboBox1.SelectedValue.ToString());
            int medicine = int.Parse(comboBox2.SelectedValue.ToString());
            int status = int.Parse(comboBox3.SelectedValue.ToString());
            int quantity = int.Parse(textBox1.Text);

            if (IsEnoughInStock(medicine, quantity))
            {
                if (HasPresription(customer, medicine))
                {
                    if (CheckPrescriptionQuantity(customer, medicine, quantity))
                    {
                        string query = "INSERT INTO Orders (OrderID, CustomerID, MedicineID, Status, Amount) " +
                            "Values (null, @customer, @medicine, @status, @quantity)";

                        db.OpenConnection();

                        MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
                        cmd.Parameters.AddWithValue("@customer", customer);
                        cmd.Parameters.AddWithValue("@medicine", medicine);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.ExecuteNonQuery();

                        db.CloseConnection();
                        db.LoadOrders();

                        if (status == 2)
                        {
                            AddUsage(medicine, quantity);
                            CalculateMedicines(medicine, quantity);
                        }

                        CalculatePrescription(customer, medicine, quantity);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Препаратов в заказе больше чем осталось по рецепту, либо препарат уже отпущен.");
                    }
                }
                else
                {
                    MessageBox.Show("У покупателя нет рецепта на данный препарат.");
                }
            }
            else
            {
                MessageBox.Show("Препаратов в наличии не хватает для такого заказа.\n");
            }
        }

        private void CalculatePrescription(int customerId, int medicineId, int quantity)
        {
            foreach (Prescription presc in db.Presctiptions)
            {
                if (presc.CustomerID == customerId)
                {
                    if (presc.MedicineID == medicineId)
                    {
                        presc.Quantity -= quantity;

                        string query = $"UPDATE `prescriptions` SET `Quantity` = '{presc.Quantity}' WHERE `PrescriptionID` = '{presc.ID}';";

                        db.OpenConnection();

                        MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
                        cmd.ExecuteNonQuery();

                        db.CloseConnection();
                        db.LoadMedicines();

                        break;
                    }
                }
            }
        }

        private bool CheckPrescriptionQuantity(int customerId, int medicineId, int quantity)
        {
            foreach (Prescription presc in db.Presctiptions)
            {
                if (presc.CustomerID == customerId)
                {
                    if (presc.MedicineID == medicineId)
                        return presc.Quantity >= quantity;
                }
            }

            return false;
        }

        private bool HasPresription(int customerId, int medicineId)
        {
            foreach (Prescription presc in db.Presctiptions)
            {
                if (presc.CustomerID == customerId)
                {
                    if (presc.MedicineID == medicineId)
                        return true;
                }
            }

            return false;
        }

        private bool IsEnoughInStock(int medId, int quantity)
        {
            foreach (Medicine med in db.Medicines)
            {
                if (med.ID == medId)
                { 
                    return med.Quantity >= quantity;
                }
            }

            return false;
        }

        private void AddUsage(int medId, int quantity)
        {
            DateTimePicker dateTimePicker = new DateTimePicker();
            dateTimePicker.Value = DateTime.Today;

            string today = dateTimePicker.Value.Date.ToString("yyyy-MM-dd");

            string query = "INSERT INTO UsageStatistics (UsageID, MedicineID, VolumeUsed, DateUsed) " +
                "Values (null, @medicine, @volume, @date)";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@medicine", medId);
            cmd.Parameters.AddWithValue("@volume", quantity);
            cmd.Parameters.AddWithValue("@date", today);
            cmd.ExecuteNonQuery();

            db.CloseConnection();
            db.LoadUsageStatistics();
        }

        private void CalculateMedicines(int medId, int quantity)
        { 
            foreach (Medicine med in db.Medicines)
            {
                if (med.ID == medId)
                {
                    med.Quantity -= quantity;

                    string query = $"UPDATE `medicines` SET `Quantity` = '{med.Quantity}' WHERE `MedicineID` = '{medId}';";

                    db.OpenConnection();

                    MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
                    cmd.ExecuteNonQuery();

                    db.CloseConnection();
                    db.LoadMedicines();

                    break;
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            comboBox1.DataSource = db.Customers;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";

            comboBox2.DataSource = db.Medicines;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";

            comboBox3.DataSource = db.OrderStatuses;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "ID";

            base.OnShown(e);
        }
    }
}
