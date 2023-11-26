namespace PharmacySystem
{
    partial class OutputTableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            outputDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).BeginInit();
            SuspendLayout();
            // 
            // outputDataGridView
            // 
            outputDataGridView.AllowUserToAddRows = false;
            outputDataGridView.AllowUserToDeleteRows = false;
            outputDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            outputDataGridView.Dock = DockStyle.Fill;
            outputDataGridView.Location = new Point(0, 0);
            outputDataGridView.Name = "outputDataGridView";
            outputDataGridView.Size = new Size(800, 450);
            outputDataGridView.TabIndex = 0;
            // 
            // OutputTableForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(outputDataGridView);
            Name = "OutputTableForm";
            Text = "OutputTableForm";
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView outputDataGridView;
    }
}