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
    public partial class UsageStatisticsTableForm : Form
    {
        MyDB db;
        public UsageStatisticsTableForm(ref MyDB db)
        {
            this.db = db;

            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            db.SaveUsage();
        }

        protected override void OnShown(EventArgs e)
        {
            Medicine.DataSource = db.Medicines;
            Medicine.DisplayMember = "Name";
            Medicine.ValueMember = "ID";

            dataGridView1.DataSource = db.UsageStatistics;

            base.OnShown(e);
        }
    }
}
