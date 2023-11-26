namespace PharmacySystem
{
    partial class ComponentsTableForm
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
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            addNewRow = new Button();
            saveButton = new Button();
            ID = new DataGridViewTextBoxColumn();
            ComponentName = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(addNewRow);
            splitContainer1.Panel2.Controls.Add(saveButton);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 600;
            splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ID, ComponentName });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(600, 450);
            dataGridView1.TabIndex = 0;
            // 
            // addNewRow
            // 
            addNewRow.Dock = DockStyle.Top;
            addNewRow.Location = new Point(0, 23);
            addNewRow.Name = "addNewRow";
            addNewRow.Size = new Size(196, 23);
            addNewRow.TabIndex = 1;
            addNewRow.Text = "Новая запись";
            addNewRow.UseVisualStyleBackColor = true;
            addNewRow.Click += addNewRow_Click;
            // 
            // saveButton
            // 
            saveButton.Dock = DockStyle.Top;
            saveButton.Location = new Point(0, 0);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(196, 23);
            saveButton.TabIndex = 0;
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // ID
            // 
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // ComponentName
            // 
            ComponentName.DataPropertyName = "Name";
            ComponentName.HeaderText = "Название компонента";
            ComponentName.Name = "ComponentName";
            // 
            // ComponentsTableForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "ComponentsTableForm";
            Text = "ComponentsTableForm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private Button addNewRow;
        private Button saveButton;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn ComponentName;
    }
}