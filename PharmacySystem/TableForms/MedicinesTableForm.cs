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
    public partial class MedicinesTableForm : Form
    {
        MyDB db;

        public MedicinesTableForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            db.SaveMedicines();
        }

        private void newRowButton_Click(object sender, EventArgs e)
        {
            NewMedicineForm newMedicineForm = new NewMedicineForm(ref db);
            newMedicineForm.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            Type.DataSource = db.MedicineTypes;
            Type.DisplayMember = "Name";
            Type.ValueMember = "ID";

            dataGridView1.DataSource = db.Medicines;

            base.OnShown(e);
        }
    }
}
