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
    public partial class ComponentsTableForm : Form
    {
        MyDB dB;
        public ComponentsTableForm(ref MyDB dB)
        {
            this.dB = dB;
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            dB.SaveComponents();
        }

        private void addNewRow_Click(object sender, EventArgs e)
        {
            NewComponentForm newComponentForm = new NewComponentForm(ref dB);
            newComponentForm.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            dataGridView1.DataSource = dB.Components;
            base.OnShown(e);
        }
    }
}
