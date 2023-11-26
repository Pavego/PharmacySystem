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

        public OutputTableForm(ref DataTable dt)
        {
            output = dt;

            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            outputDataGridView.DataSource = output;
            base.OnShown(e);
        }
    }
}
