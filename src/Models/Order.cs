using AHIFventory;
using Microsoft.Data.Sqlite;
using Serilog;
using System;
using System.ComponentModel;

namespace AHIFventory
{
    public class Order : INotifyPropertyChanged
    {
        private int orderID;
        public int OrderID
        {
            get { return orderID; }
            set
            {
                if (orderID != value)
                {
                    orderID = value;
                    onPropertyChanged("OrderID");
                }
            }
        }

        private DateTime orderDate;
        public DateTime OrderDate
        {
            get { return orderDate; }
            set
            {
                if (orderDate != value)
                {
                    orderDate = value;
                    onPropertyChanged("OrderDate");
                }
            }
        }

        private string supplier;
        public string Supplier
        {
            get { return supplier; }
            set
            {
                if (supplier != value)
                {
                    supplier = value;
                    onPropertyChanged("Supplier");
                }
            }
        }

        private int productID;
        public int ProductID
        {
            get { return productID; }
            set
            {
                if (productID != value)
                {
                    productID = value;
                    onPropertyChanged("ProductID");
                }
            }
        }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set
            {
                if (productName != value)
                {
                    productName = value;
                    onPropertyChanged("ProductName");
                }
            }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;

                    if (price < 0)
                    {
                        price = 0;
                    }

                    onPropertyChanged("Price");
                }
            }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;

                    if (quantity < 0)
                    {
                        quantity = 0;
                    }

                    onPropertyChanged("Quantity");
                }
            }
        }

        private string action;
        public string Action
        {
            get { return action; }
            set
            {
                if (action != value)
                {
                    action = value;
                    onPropertyChanged("Action");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public Order(string productName, string supplier)
        {
            ProductName = productName;
            Supplier = supplier;
        }

        public Order(SqliteDataReader reader)
        {
            OrderID = reader.IsDBNull(reader.GetOrdinal("OrderID")) ? 0 : reader.GetInt32(reader.GetOrdinal("OrderID"));
            OrderDate = reader.IsDBNull(reader.GetOrdinal("OrderDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("OrderDate"));
            Supplier = reader.IsDBNull(reader.GetOrdinal("Supplier")) ? null : reader.GetString(reader.GetOrdinal("Supplier"));
            ProductID = reader.IsDBNull(reader.GetOrdinal("ProductID")) ? 0 : reader.GetInt32(reader.GetOrdinal("ProductID"));
            ProductName = reader.IsDBNull(reader.GetOrdinal("ProductName")) ? null : reader.GetString(reader.GetOrdinal("ProductName"));
            Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("Price"));
            Quantity = reader.IsDBNull(reader.GetOrdinal("Quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("Quantity"));
            Action = reader.IsDBNull(reader.GetOrdinal("Action")) ? null : reader.GetString(reader.GetOrdinal("Action"));
        }

        public void SaveOrder()
        {
            Log.Information("Saving order to sql file");

            using (var connection = new SqliteConnection("Data Source=assets\\AHIFventoryDB.db"))
            {
                connection.Open();

                string query;

                if (OrderID == 0)
                {
                    Log.Debug("Insert new order");
                    query = @"INSERT INTO tblOrder (OrderDate, Supplier, ProductID, ProductName, Price, Quantity, Action) 
                              VALUES (@OrderDate, @Supplier, @ProductID, @ProductName, @Price, @Quantity, @Action)";
                }
                else
                {
                    Log.Debug("Update existing order");
                    query = @"UPDATE tblOrder 
                              SET OrderDate = @OrderDate, Supplier = @Supplier, ProductID = @ProductID, ProductName = @ProductName, 
                                  Price = @Price, Quantity = @Quantity, Action = @Action 
                              WHERE OrderID = @OrderID";
                }

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", OrderID);
                    command.Parameters.AddWithValue("@OrderDate", OrderDate);
                    command.Parameters.AddWithValue("@Supplier", Supplier ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.AddWithValue("@ProductName", ProductName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@Action", Action ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }

                if (OrderID == 0)
                {
                    query = "SELECT last_insert_rowid()";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        OrderID = (int)(long)command.ExecuteScalar();
                    }
                }
            }
        }

        public void DeleteOrder()
        {
            Log.Information("Deleting order from sql file");

            if (OrderID == null)
            {
               Log.Warning("Order ID cannot be null when deleting a order.");
                return;
            }

            using (var connection = new SqliteConnection("Data Source=assets\\AHIFventoryDB.db"))
            {
                connection.Open();
                var query = "DELETE FROM tblOrder WHERE OrderID = @OrderID";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", OrderID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}