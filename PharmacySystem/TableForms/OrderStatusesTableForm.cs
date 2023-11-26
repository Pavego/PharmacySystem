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

namespace PharmacySystem.TableForms
{
    public partial class OrderStatusesTableForm : Form
    {
        MyDB db;
        public OrderStatusesTableForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            db.SaveOrderStatuses();
        }

        private void newRowButton_Click(object sender, EventArgs e)
        {
            NewOrderStatusForm newOrderStatusForm = new NewOrderStatusForm();
            newOrderStatusForm.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            dataGridView1.DataSource = db.OrderStatuses;

            base.OnShown(e);
        }
    }
}
