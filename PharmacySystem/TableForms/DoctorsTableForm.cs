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
    public partial class DoctorsTableForm : Form
    {
        MyDB db;

        public DoctorsTableForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            db.SaveDoctors();
        }

        private void newRowButton_Click(object sender, EventArgs e)
        {
            NewDoctorForm newDoctorForm = new NewDoctorForm(ref db);
            newDoctorForm.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            dataGridView1.DataSource = db.Doctors;

            base.OnShown(e);
        }
    }
}
