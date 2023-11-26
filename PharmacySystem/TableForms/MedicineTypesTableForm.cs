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
    public partial class MedicineTypesTableForm : Form
    {
        MyDB db;

        public MedicineTypesTableForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            db.SaveMedicineTypes();
        }

        private void newRowButton_Click(object sender, EventArgs e)
        {
            NewMedicineTypeForm newMedicineTypeForm = new NewMedicineTypeForm();
            newMedicineTypeForm.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            dataGridView1.DataSource = db.MedicineTypes;

            base.OnShown(e);
        }
    }
}
