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

        private void saveButton_Click(object sender, EventArgs e)
        {
            db.SaveOrders();
        }

        private void newRowButton_Click(object sender, EventArgs e)
        {
            NewOrderForm newOrderForm = new NewOrderForm();
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

            base.OnShown(e);
        }
    }
}
