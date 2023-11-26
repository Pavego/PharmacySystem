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
    public partial class OutputTableForm : Form
    {
        DataTable output;

        string message;

        public OutputTableForm(ref DataTable dt, string message)
        {
            output = dt;

            this.message = message;

            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            outputDataGridView.DataSource = output;

            if (message != "")
            {
                MessageBox.Show(message);
            }

            base.OnShown(e);
        }
    }
}
