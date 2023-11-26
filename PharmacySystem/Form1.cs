using MySql.Data.MySqlClient;
using PharmacySystem.TableForms;
using System.Data;
using static PharmacySystem.MyDB;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace PharmacySystem
{
    public partial class Form1 : Form
    {
        MyDB db = new MyDB();

        public Form1()
        {
            InitializeComponent();
        }

        #region ShowTableButtons

        private void showComponentsTableButton_Click(object sender, EventArgs e)
        {
            ComponentsTableForm componentsTableForm = new ComponentsTableForm(ref db);
            componentsTableForm.Show();
        }

        private void showCustomersTableButton_Click(object sender, EventArgs e)
        {
            CustomersTableForm customersTableForm = new CustomersTableForm(ref db);
            customersTableForm.Show();
        }

        private void showDoctorsTableButton_Click(object sender, EventArgs e)
        {
            DoctorsTableForm doctorsTableForm = new DoctorsTableForm(ref db);
            doctorsTableForm.Show();
        }

        private void showMedicineComponentsTableButton_Click(object sender, EventArgs e)
        {
            MedicineComponentsTableForm medicineComponentsTableForm = new MedicineComponentsTableForm(ref db);
            medicineComponentsTableForm.Show();
        }

        private void showMedicineTypesTableButton_Click(object sender, EventArgs e)
        {
            MedicineTypesTableForm medicinesTableForm = new MedicineTypesTableForm(ref db);
            medicinesTableForm.Show();
        }

        private void showMedicinesTableButton_Click(object sender, EventArgs e)
        {
            MedicinesTableForm medicinesTablesForm = new MedicinesTableForm(ref db);
            medicinesTablesForm.Show();
        }

        private void showOrdersTableButton_Click(object sender, EventArgs e)
        {
            OrdersTableForm ordersTablesForm = new OrdersTableForm(ref db);
            ordersTablesForm.Show();
        }

        private void showPrescriptionsTableButton_Click(object sender, EventArgs e)
        {
            PrescriptionsTableForm prescriptionsTableForm = new PrescriptionsTableForm(ref db);
            prescriptionsTableForm.Show();
        }

        private void showUsageStatisticsTableButton_Click(object sender, EventArgs e)
        {
            UsageStatisticsTableForm usageStatisticsTableForm = new UsageStatisticsTableForm(ref db);
            usageStatisticsTableForm.Show();
        }


        private void showOrderStatusesTableButton_Click(object sender, EventArgs e)
        {
            OrderStatusesTableForm orderStatusesTableForm = new OrderStatusesTableForm(ref db);
            orderStatusesTableForm.Show();
        }

        #endregion

        #region ActionButtons

        // ��������� ������ �����������, ��������� �����������:
        private void action1Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT Customers.Name AS ���_����������, COUNT(*) AS ����������_�������_�_�������� " +
                "FROM Customers " +
                "JOIN Orders ON Customers.CustomerID = Orders.CustomerID " +
                "JOIN Medicines ON Orders.MedicineID = Medicines.MedicineID " +
                "WHERE Orders.Status = 1 " +
                "GROUP BY Customers.Name;";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, $"����� ����������� �������: {dt.Rows.Count}");
        }

        // ��������� ������ �����������, ��������� ����������� �� ��������� ���������:
        private void action2Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT Customers.Name AS ���_����������, COUNT(*) AS ����������_�������_�_�������� " +
                "FROM Customers " +
                "JOIN Orders ON Customers.CustomerID = Orders.CustomerID " +
                "JOIN Medicines ON Orders.MedicineID = Medicines.MedicineID " +
                "WHERE Orders.Status = 1 AND Medicines.Type = @type " +
                "GROUP BY Customers.Name;";

            int type = int.Parse(action2TypeComboBox.SelectedValue.ToString());

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@type", type);

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, $"����� ����������� �������: {dt.Rows.Count}");
        }

        // ��������� �������� ����� ������������ ������������:
        private void action3Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT Medicines.Name AS ��������_���������, SUM(UsageStatistics.VolumeUsed) AS �������_����� " +
                "FROM UsageStatistics " +
                "JOIN Medicines ON UsageStatistics.MedicineID = Medicines.MedicineID " +
                "GROUP BY Medicines.Name " +
                "ORDER BY �������_����� DESC " +
                "LIMIT 10;";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, "");
        }

        // ��������� �������� ����� ������������ ������������ �� ���������:
        private void action4Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT Medicines.Name AS ��������_���������, SUM(UsageStatistics.VolumeUsed) AS �������_����� " +
                "FROM UsageStatistics " +
                "JOIN Medicines ON UsageStatistics.MedicineID = Medicines.MedicineID " +
                "WHERE Medicines.Type = @type " +
                "GROUP BY Medicines.Name " +
                "ORDER BY �������_����� DESC " +
                "LIMIT 10;";

            int type = int.Parse(action4TypeComboBox.SelectedValue.ToString());

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@type", type);

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, "");
        }

        // ��������� ������ ������������ �������:
        private void action5Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT SUM(UsageStatistics.VolumeUsed) AS ��������_����� " +
                "FROM UsageStatistics " +
                "JOIN Medicines ON UsageStatistics.MedicineID = Medicines.MedicineID " +
                "WHERE UsageStatistics.DateUsed BETWEEN @startDate AND @endDate AND UsageStatistics.MedicineID = @medId;";

            string startDate = action5DateFrom.Text;
            string endDate = action5DateTo.Text;

            int medicineId = int.Parse(action5MedicineComboBox.SelectedValue.ToString());

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@medId", medicineId);

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, "");
        }

        // �����������, ��������� ����������� �����:
        private void action6Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT Medicines.Name AS ��������_���������, MedicinesTypes.Name AS ���������_���������, Medicines.Quantity AS ����������_�������� " +
                "FROM Medicines " +
                "JOIN MedicinesTypes ON Medicines.Type = MedicinesTypes.TypeID " +
                "WHERE Quantity <= CriticalLevel;";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, "");
        }

        // �������� ������� � ��������:
        private void action7Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT Orders.OrderID AS ID, Customers.Name AS ���_����������, OrderStatuses.StatusName AS ������ " +
                "FROM Orders " +
                "JOIN OrderStatuses ON Orders.Status = OrderStatuses.StatusID " +
                "JOIN Customers ON Orders.CustomerID = Customers.CustomerID " +
                "WHERE Status = 1 " +
                "GROUP BY OrderID DESC;";

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, $"����� ������� � ��������: {dt.Rows.Count}");
        }

        // ���������� � ������ ���������� �� ����������� ���������:
        private void action8Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT Customers.Name AS ���_����������, COUNT(*) AS ����������_������� " +
                "FROM Customers " +
                "JOIN Orders ON Customers.CustomerID = Orders.CustomerID " +
                "WHERE Orders.MedicineID = @medId " +
                "GROUP BY Customers.Name " +
                "ORDER BY ����������_������� DESC;";

            int medicineId = int.Parse(action8MedicineComboBox.SelectedValue.ToString());

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@medId", medicineId);

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, "");
        }

        // ���������� � ������ ���������� �� ���������:
        private void action9Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT Customers.Name AS ���_����������, COUNT(*) AS ����������_������� " +
                "FROM Customers " +
                "JOIN Orders ON Customers.CustomerID = Orders.CustomerID " +
                "JOIN Medicines ON Orders.MedicineID = Medicines.MedicineID " +
                "WHERE Medicines.Type = @type " +
                "GROUP BY Customers.Name " +
                "ORDER BY ����������_������� DESC;";

            int type = int.Parse(action9TypeComboBox.SelectedValue.ToString());

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@type", type);

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, "");
        }

        //���������� � ���������� ���������:
        private void action10Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT MedicinesTypes.Name AS ���������_���������, Medicines.Name AS ��������_���������, Medicines.CriticalLevel AS �������_�������, Components.ComponentName as ���������, Medicines.Price AS ����, Medicines.Quantity AS �_������� " +
                "FROM Medicines " +
                "JOIN MedicinesTypes ON Medicines.Type = MedicinesTypes.TypeID " +
                "JOIN MedicineComponent ON Medicines.MedicineID = MedicineComponent.MedicineComponentID " +
                "JOIN Components ON MedicineComponent.ComponentID = Components.ComponentID " +
                "WHERE Medicines.MedicineID = @medId;";

            int medicineId = int.Parse(action10MedicineComboBox.SelectedValue.ToString());

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@medId", medicineId);

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, "");
        }

        // ������� � ���������� ���������:
        private void action11Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT b.Name AS ���_����������, mt.Name AS ���������_���������, m.Name AS ��������_���������, p.Quantity AS ����������_��_�������, p.Instructions AS ����������, c.ComponentName AS ���������, d.Name AS ���_�����, d.Signature AS �������, d.Stamp AS ������ " +
                "FROM Prescriptions p " +
                "JOIN Medicines m ON p.MedicineID = m.MedicineID " +
                "JOIN MedicineComponent mc ON p.MedicineID = mc.MedicineID " +
                "JOIN MedicinesTypes mt ON m.Type = mt.TypeID " +
                "JOIN Components c ON mc.ComponentID = c.ComponentID " +
                "JOIN Customers b ON p.CustomerID = b.CustomerID " +
                "JOIN Doctors d ON p.DoctorID = d.DoctorID " +
                "WHERE b.CustomerID = @customerId;";

            int customerId = int.Parse(action11CustomerComboBox.SelectedValue.ToString());

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@customerId", customerId);

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, "");
        }

        // ����������� �������� �� ������������ ��������:
        private void action12Button_Click(object sender, EventArgs e)
        {
            string query = "SELECT Medicines.Name AS ��������_���������, MedicinesTypes.Name AS ���������_��������� " +
                "FROM Medicines " +
                "JOIN MedicineComponent ON Medicines.MedicineID = MedicineComponent.MedicineID " +
                "JOIN MedicinesTypes ON Medicines.Type = MedicinesTypes.TypeID " +
                "JOIN Components ON MedicineComponent.ComponentID = Components.ComponentID " +
                "WHERE Components.ComponentName = @componentName AND Medicines.Name != @medName;";

            string componentName = db.GetComponentByMedicineID(int.Parse(action12MedicineComboBox.SelectedValue.ToString())).Name;
            string medicineName = action12MedicineComboBox.Text;

            db.OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, db.GetMySqlConnection());
            cmd.Parameters.AddWithValue("@componentName", componentName);
            cmd.Parameters.AddWithValue("@medName", medicineName);

            MySqlDataAdapter msData = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            msData.Fill(dt);

            db.CloseConnection();

            Clear();
            ShowOutput(ref dt, "");
        }

        #endregion

        protected override void OnShown(EventArgs e)
        {
            action2TypeComboBox.DataSource = db.MedicineTypes;
            action2TypeComboBox.ValueMember = "ID";
            action2TypeComboBox.DisplayMember = "Name";

            action4TypeComboBox.DataSource = db.MedicineTypes;
            action4TypeComboBox.ValueMember = "ID";
            action4TypeComboBox.DisplayMember = "Name";

            action5MedicineComboBox.DataSource = db.Medicines;
            action5MedicineComboBox.ValueMember = "ID";
            action5MedicineComboBox.DisplayMember = "Name";

            action8MedicineComboBox.DataSource = db.Medicines;
            action8MedicineComboBox.ValueMember = "ID";
            action8MedicineComboBox.DisplayMember = "Name";

            action9TypeComboBox.DataSource = db.MedicineTypes;
            action9TypeComboBox.ValueMember = "ID";
            action9TypeComboBox.DisplayMember = "Name";

            action10MedicineComboBox.DataSource = db.Medicines;
            action10MedicineComboBox.ValueMember = "ID";
            action10MedicineComboBox.DisplayMember = "Name";

            action11CustomerComboBox.DataSource = db.Customers;
            action11CustomerComboBox.ValueMember = "ID";
            action11CustomerComboBox.DisplayMember = "Name";

            action12MedicineComboBox.DataSource = db.Medicines;
            action12MedicineComboBox.ValueMember = "ID";
            action12MedicineComboBox.DisplayMember = "Name";

            base.OnShown(e);
        }

        private void Clear()
        {

        }

        private void ShowOutput(ref DataTable dt, string message)
        {
            OutputTableForm outputTableForm = new OutputTableForm(ref dt, message);
            outputTableForm.ShowDialog();
        }
    }
}
