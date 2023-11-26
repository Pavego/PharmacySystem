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
    public partial class CustomersTableForm : Form
    {
        MyDB db;

        public CustomersTableForm(ref MyDB db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            db.SaveCustomers();
        }

        private void newRowButton_Click(object sender, EventArgs e)
        {
            NewCustomerForm newCustomerForm = new NewCustomerForm(ref db);
            newCustomerForm.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            dataGridView1.DataSource = db.Customers;

            base.OnShown(e);
        }
    }
}
