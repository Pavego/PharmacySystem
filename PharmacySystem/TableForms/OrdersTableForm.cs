using MySql.Data.MySqlClient;
using PharmacySystem.NewDataForms;
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

namespace PharmacySystem
{
    public partial class OrdersTableForm : Form
    {
        MyDB db;

        public OrdersTableForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private List<Order> statusChangedOrders = new List<Order>();
        private List<int> changedStatuses = new List<int>();
        private void saveButton_Click(object sender, EventArgs e)
        {
            foreach(Order changedstatusOrder in statusChangedOrders)
            {
                if (changedstatusOrder.Status == 2)
                {
                    AddUsage(changedstatusOrder.MedicineID, changedstatusOrder.Amount);
                    CalculateMedicines(changedstatusOrder.MedicineID, changedstatusOrder.Amount);
                }
            }

            statusChangedOrders.Clear();
            changedStatuses.Clear();

            db.SaveOrders();
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

        private void newRowButton_Click(object sender, EventArgs e)
        {
            NewOrderForm newOrderForm = new NewOrderForm(ref db);
            newOrderForm.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            Customer.DataSource = db.Customers;
            Customer.DisplayMember = "Name";
            Customer.ValueMember = "ID";

            Medicine.DataSource = db.Medicines;
            Medicine.DisplayMember = "Name";
            Medicine.ValueMember = "ID";

            Status.DataSource = db.OrderStatuses;
            Status.DisplayMember = "Name";
            Status.ValueMember = "ID";

            dataGridView1.DataSource = db.Orders;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(dataGridView1_CurrentCellDirtyStateChanged);

            base.OnShown(e);
        }

        private int currentOrder = 0;
        private int currentStatus = 0;
        private void dataGridView1_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                currentOrder = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                foreach (Order order in db.Orders)
                {
                    if (order.ID == currentOrder)
                    {
                        currentStatus = order.Status;
                        break;
                    }
                }
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                foreach (Order order in db.Orders)
                {
                    if (order.ID == currentOrder)
                    {
                        if (order.Status != currentStatus && !statusChangedOrders.Contains(order))
                        {
                            statusChangedOrders.Add(order);
                            changedStatuses.Add(currentOrder);
                            //MessageBox.Show(statusChangedOrders.Count.ToString() + " " + changedStatuses.Count.ToString());

                            currentOrder = 0;
                            currentStatus = 0;

                            break;
                        }
                        else if (statusChangedOrders.Contains(order))
                        {
                            if (statusChangedOrders[statusChangedOrders.IndexOf(order)].Status == changedStatuses[statusChangedOrders.IndexOf(order)])
                            {
                                changedStatuses.RemoveAt(statusChangedOrders.IndexOf(order));
                                statusChangedOrders.Remove(order);

                                //MessageBox.Show(statusChangedOrders.Count.ToString() + " " + changedStatuses.Count.ToString());
                            }

                            currentOrder = 0;
                            currentStatus = 0;

                            break;
                        }
                    }
                }
            }
        }
    }
}
