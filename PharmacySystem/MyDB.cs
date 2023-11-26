using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem
{
    public class MyDB
    {
        protected readonly MySqlConnection connection = new("server=localhost;port=3306;user=root;password=root;database=pharmacy");
        public MyDB() 
        {
            LoadDataFromServer();
        }

        public void OpenConnection() 
        { 
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open) { 
                connection.Close();}
        }

        public MySqlConnection GetMySqlConnection()
        {
            return connection;
        }

        public void LoadDataFromServer()
        {
            LoadComponents();
            LoadCustomers();
            LoadDoctors();
            LoadMedicines();
            LoadMedicineComponents();
            LoadMedicineTypes();
            LoadOrders();
            LoadOrderStatuses();
            LoadPresctiptions();
            LoadUsageStatistics();
        }

        #region MyDBDataTypes

        public List<Component>? Components { get; set; }
        public List<Customer>? Customers { get; set; }
        public List<Doctor>? Doctors { get; set; }
        public List<Medicine>? Medicines { get; set; }
        public List<MedicineComponent>? MedicineComponents { get; set; }
        public List<Order>? Orders { get; set; }
        public List<OrderStatus> OrderStatuses { get; set; }
        public List<Prescription>? Presctiptions { get; set; }
        public List<UsageStatistic>? UsageStatistics { get; set; }
        public List<MedicineType>? MedicineTypes { get; set; }

        public class Component
        {
            public required int ID { get; set; }
            public required string Name { get; set; }
        }

        public class Customer
        {
            public required int ID { get; set; }
            public required int Age { get; set; }

            public required string Name { get; set; }
            public required string Address { get; set; }
            public required string Phone { get; set; }
        }

        public class Doctor
        {
            public required int ID { get; set; }

            public required string Name { get; set; }
            public required string Signature { get; set; }
            public required string Stamp { get; set; }
        }

        public class Medicine
        {
            public required int ID { get; set; }
            public required int CriticalLevel { get; set; }
            public required int Quantity { get; set; }
            public required int Type { get; set; }

            public required double Price { get; set; }

            public required string Name { get; set; }

        }

        public class MedicineComponent
        {
            public required int ID { get; set; }
            public required int MedicineID { get; set; }
            public required int ComponentID { get; set; }
        }

        public class Order
        {
            public required int ID { get; set; }
            public required int CustomerID { get; set; }
            public required int MedicineID { get; set; }
            public required int Status { get; set; }
            public required int Amount { get; set; }
        }

        public class OrderStatus
        {
            public required int ID { get; set; }
            public required string Name { get; set; }
        }

        public class Prescription
        {
            public required int ID { get; set; }
            public required int CustomerID { get; set; }
            public required int DoctorID { get; set; }
            public required int MedicineID { get; set; }

            public required int Quantity { get; set; }

            public required string Instructions { get; set; }
        }

        public class UsageStatistic
        {
            public required int ID { get; set; }
            public required int MedicineID { get; set; }

            public required int VolumeUsed { get; set;}

            public required string DateUsed { get; set; }

        }

        public class MedicineType
        {
            public required int ID { get; set; }
            public required string Name { get; set; }
        }

        #endregion
        #region LoadData

        public void LoadMedicineTypes()
        {
            List<MedicineType> medicineTypes = new List<MedicineType>();

            OpenConnection();

            string query = "SELECT * FROM `medicinestypes`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                MedicineType medicineType = new()
                {
                    ID = int.Parse(row["TypeID"].ToString()),
                    Name = row["Name"].ToString()
                };

                medicineTypes.Add(medicineType);
            }

            CloseConnection();

            MedicineTypes = medicineTypes;
        }

        public void LoadComponents()
        {
            List<Component> components = new List<Component>();

            OpenConnection();

            string query = "SELECT * FROM `components`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Component component = new()
                {
                    ID = int.Parse(row["ComponentID"].ToString()),
                    Name = row["ComponentName"].ToString()
                };

                components.Add(component);
            }

            CloseConnection();

            Components = components;
        }

        public void LoadCustomers()
        {
            List<Customer> customers = new List<Customer>();

            OpenConnection();

            string query = "SELECT * FROM `customers`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Customer customer = new()
                {
                    ID = int.Parse(row["CustomerID"].ToString()),
                    Age = int.Parse(row["Age"].ToString()),
                    Name = row["Name"].ToString(),
                    Address = row["Address"].ToString(),
                    Phone = row["Phone"].ToString()
                };

                customers.Add(customer);
            }

            CloseConnection();

            Customers = customers;
        }

        public void LoadDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();

            OpenConnection();

            string query = "SELECT * FROM `doctors`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Doctor doctor = new()
                {
                    ID = int.Parse(row["DoctorID"].ToString()),
                    Name = row["Name"].ToString(),
                    Signature = row["Signature"].ToString(),
                    Stamp = row["Stamp"].ToString()
                };

                doctors.Add(doctor);
            }

            CloseConnection();

            Doctors = doctors;
        }

        public void LoadMedicineComponents()
        {
            List<MedicineComponent> medicineComponents = new List<MedicineComponent>();

            OpenConnection();

            string query = "SELECT * FROM `medicinecomponent`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                MedicineComponent medicineComponent = new()
                {
                    ID = int.Parse(row["MedicineComponentID"].ToString()),
                    MedicineID = int.Parse(row["MedicineID"].ToString()),
                    ComponentID = int.Parse(row["ComponentID"].ToString())
                };

                medicineComponents.Add(medicineComponent);
            }

            CloseConnection();

            MedicineComponents = medicineComponents;
        }

        public void LoadMedicines()
        {
            List<Medicine> medicines = new List<Medicine>();

            OpenConnection();

            string query = "SELECT * FROM `medicines`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Medicine medicine = new()
                {
                    ID = int.Parse(row["MedicineID"].ToString()),
                    CriticalLevel = int.Parse(row["CriticalLevel"].ToString()),
                    Quantity = int.Parse(row["Quantity"].ToString()),
                    Type = int.Parse(row["Type"].ToString()),
                    Price = double.Parse(row["Price"].ToString()),
                    Name = row["Name"].ToString()
                };

                medicines.Add(medicine);
            }

            CloseConnection();

            Medicines = medicines;
        }

        public void LoadOrderStatuses()
        {
            List<OrderStatus> orderStatuses = new List<OrderStatus>();

            OpenConnection();

            string query = "SELECT * FROM `orderstatuses`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                OrderStatus orderStatus = new()
                {
                    ID = int.Parse(row["StatusID"].ToString()),
                    Name = row["StatusName"].ToString()
                };

                orderStatuses.Add(orderStatus);
            }

            CloseConnection();

            OrderStatuses = orderStatuses;
        }

        public void LoadOrders()
        {
            List<Order> orders = new List<Order>();

            OpenConnection();

            string query = "SELECT * FROM `orders`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Order order = new()
                {
                    ID = int.Parse(row["OrderID"].ToString()),
                    CustomerID = int.Parse(row["CustomerID"].ToString()),
                    MedicineID = int.Parse(row["MedicineID"].ToString()),
                    Status = int.Parse(row["Status"].ToString()),
                    Amount = int.Parse(row["Amount"].ToString())
                };

                orders.Add(order);
            }

            CloseConnection();

            Orders = orders;
        }

        public void LoadPresctiptions()
        {
            List<Prescription> prescriptions = new List<Prescription>();

            OpenConnection();

            string query = "SELECT * FROM `prescriptions`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Prescription prescription = new()
                {
                    ID = int.Parse(row["PrescriptionID"].ToString()),
                    CustomerID = int.Parse(row["CustomerID"].ToString()),
                    DoctorID = int.Parse(row["DoctorID"].ToString()),
                    MedicineID = int.Parse(row["MedicineID"].ToString()),
                    Quantity = int.Parse(row["Quantity"].ToString()),
                    Instructions = row["Instructions"].ToString()
                };

                prescriptions.Add(prescription);
            }

            CloseConnection();

            Presctiptions = prescriptions;
        }

        public void LoadUsageStatistics()
        {
            List<UsageStatistic> usageStatistics = new List<UsageStatistic>();

            OpenConnection();

            string query = "SELECT * FROM `usagestatistics`;";

            MySqlDataAdapter msData = new MySqlDataAdapter(query, GetMySqlConnection());
            DataTable dt = new DataTable();
            msData.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                UsageStatistic usageStatistic = new()
                {
                    ID = int.Parse(row["UsageID"].ToString()),
                    MedicineID = int.Parse(row["MedicineID"].ToString()),
                    VolumeUsed = int.Parse(row["VolumeUsed"].ToString()),
                    DateUsed = row["DateUsed"].ToString()
                };

                usageStatistics.Add(usageStatistic);
            }

            CloseConnection();

            UsageStatistics = usageStatistics;
        }

        #endregion
        #region SaveData

        public void SaveComponents()
        {
            string query = "";

            foreach (Component component in Components)
            {
                query += $"UPDATE `components` SET `ComponentName` = '{component.Name}' WHERE `ComponentID` = '{component.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void SaveCustomers()
        {
            string query = "";

            foreach (Customer customer in Customers)
            {
                query += $"UPDATE `customers` SET `Name` = '{customer.Name}', `Age` = '{customer.Age}', `Phone` = '{customer.Phone}', `Address` = '{customer.Address}' WHERE `CustomerID` = '{customer.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void SaveDoctors()
        {
            string query = "";

            foreach (Doctor doctor in Doctors)
            {
                query += $"UPDATE `doctors` SET `Name` = '{doctor.Name}', `Signature` = '{doctor.Signature}', `Stamp` = '{doctor.Stamp}' WHERE `DoctorID` = '{doctor.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void SaveMedicines()
        {
            string query = "";

            foreach (Medicine medicine in Medicines)
            {
                query += $"UPDATE `medicines` SET `Name` = '{medicine.Name}', `Type` = '{medicine.Type}', `CriticalLevel` = '{medicine.CriticalLevel}', `Price` = '{medicine.Price}', `Quantity` = '{medicine.Quantity}' WHERE `MedicineID` = '{medicine.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void SaveMedicineComponents()
        {
            string query = "";

            foreach (MedicineComponent medicineComponent in MedicineComponents)
            {
                query += $"UPDATE `medicinecomponent` SET `MedicineID` = '{medicineComponent.MedicineID}', `ComponentID` = '{medicineComponent.ComponentID}' WHERE `MedicineComponentID` = '{medicineComponent.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void SaveOrderStatuses()
        {
            string query = "";

            foreach (OrderStatus orderStatus in OrderStatuses)
            {
                query += $"UPDATE `orderstatuses` SET `StatusName` = '{orderStatus.Name}' WHERE `StatusID` = '{orderStatus.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void SaveOrders()
        {
            string query = "";

            foreach (Order order in Orders)
            {
                query += $"UPDATE `orders` SET `CustomerID` = '{order.CustomerID}', `MedicineID` = '{order.MedicineID}', `Status` = {order.Status}, `Amount` = {order.Amount} WHERE `OrderID` = '{order.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void SavePrescriptions()
        {
            string query = "";

            foreach (Prescription presription in Presctiptions)
            {
                query += $"UPDATE `prescriptions` SET `CustomerID` = '{presription.CustomerID}', `DoctorID` = '{presription.DoctorID}', `MedicineID` = '{presription.MedicineID}', `Quantity` = '{presription.Quantity}', `Instructions` = '{presription.Instructions}' WHERE `PrescriptionID` = '{presription.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void SaveUsage()
        {
            string query = "";

            foreach (UsageStatistic usage in UsageStatistics)
            {
                query += $"UPDATE `usagestatistics` SET `MedicineID` = '{usage.MedicineID}', `VolumeUsed` = '{usage.VolumeUsed}' WHERE `UsageID` = '{usage.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void SaveMedicineTypes()
        {
            string query = "";

            foreach (MedicineType medType in MedicineTypes)
            {
                query += $"UPDATE `medicinestypes` SET `Name` = '{medType.Name}' WHERE `TypeID` = '{medType.ID}';\n";
            }

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, GetMySqlConnection());
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        #endregion

        public Component GetComponentByMedicineID(int medicineId)
        {
            int compId = 0;
            foreach (MedicineComponent medComp in MedicineComponents)
            {
                if (medComp.MedicineID == medicineId)
                {
                    compId = medComp.ComponentID;
                }
            }

            foreach (Component component in Components)
            {
                if (component.ID == compId)
                {
                    return component;
                }
            }

            return null;
        }
    }
}
