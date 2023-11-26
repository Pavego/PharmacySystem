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
    public partial class PrescriptionsTableForm : Form
    {
        MyDB db;

        public PrescriptionsTableForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            db.SavePrescriptions();
        }

        private void newRowButton_Click(object sender, EventArgs e)
        {
            NewPrescriptionForm newPrescriptionForm = new NewPrescriptionForm(ref db);
            newPrescriptionForm.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            Customer.DataSource = db.Customers;
            Customer.DisplayMember = "Name";
            Customer.ValueMember = "ID";

            Doctor.DataSource = db.Doctors;
            Doctor.DisplayMember = "Name";
            Doctor.ValueMember = "ID";

            Medicine.DataSource = db.Medicines;
            Medicine.DisplayMember = "Name";
            Medicine.ValueMember = "ID";

            dataGridView1.DataSource = db.Presctiptions;

            base.OnShown(e);
        }
    }
}
