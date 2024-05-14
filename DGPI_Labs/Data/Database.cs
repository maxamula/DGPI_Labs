using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DGPI_Labs.Data
{
    public class Database : INotifyPropertyChanged
    {
        public Database()
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;
            _connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString);
            _connection.Open();
            DataTable = new DataTable();
            Update();
        }
        private SqlConnection _connection;

        public event PropertyChangedEventHandler PropertyChanged;

        void Update()
        {
            var command = new SqlCommand("SELECT * FROM [dbo].[Products]", _connection);
            var adapter = new SqlDataAdapter(command);
            DataTable.Clear();
            adapter.Fill(DataTable);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataTable"));
        }

        private DataTable _dataTable;
        public DataTable DataTable 
        { 
            get => _dataTable;
            private set
            {
                _dataTable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataTable"));
            }
        }

        public void UpdateSelectedItem()
        {
            var command = new SqlCommand("UPDATE [dbo].[Products] SET Name = @Name, Price = @Price, Count = @Count, Units = @Units WHERE Id = @Id", _connection);
            command.Parameters.AddWithValue("@Id", SelectedItem["Id"]);
            command.Parameters.AddWithValue("@Name", SelectedItem["Name"]);
            command.Parameters.AddWithValue("@Price", SelectedItem["Price"]);
            command.Parameters.AddWithValue("@Count", SelectedItem["Count"]);
            command.Parameters.AddWithValue("@Units", SelectedItem["Units"]);
            command.ExecuteNonQuery();
            Update();
        }

        public void DeleteSelectedItem()
        {
            var command = new SqlCommand("DELETE FROM [dbo].[Products] WHERE Id = @Id", _connection);

            command.Parameters.AddWithValue("@Id", SelectedItem["Id"]);
            command.ExecuteNonQuery();
            Update();
        }

        public void AddItem()
        {
            var command = new SqlCommand("INSERT INTO [dbo].[Products] (Id, Name, Price, Count) VALUES (@Id, @Name, @Price, @Count)", _connection);
            command.Parameters.AddWithValue("@Name", "New Product");
            command.Parameters.AddWithValue("@Price", 0);
            command.Parameters.AddWithValue("@Count", 0);
            var id = 0;
            foreach (DataRow row in DataTable.Rows)
            {
                id = Math.Max(id, (int)row["Id"]);
            }
            command.Parameters.AddWithValue("@Id", id + 1);
            command.ExecuteNonQuery();
            Update();
        }

        private DataRowView _selectedItem;
        public DataRowView SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }

    }
}
