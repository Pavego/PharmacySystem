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
    public partial class MedicineComponentsTableForm : Form
    {
        MyDB db;

        public MedicineComponentsTableForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            db.SaveMedicineComponents();
        }

        private void newRowButton_Click(object sender, EventArgs e)
        {
            NewMedicineComponentForm newMedicineComponentForm = new NewMedicineComponentForm();
            newMedicineComponentForm.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            Medicine.DataSource = db.Medicines;
            Medicine.DisplayMember = "Name";
            Medicine.ValueMember = "ID";

            Component.DataSource = db.Components;
            Component.DisplayMember = "Name";
            Component.ValueMember = "ID";

            dataGridView1.DataSource = db.MedicineComponents;

            base.OnShown(e);
        }
    }
}
