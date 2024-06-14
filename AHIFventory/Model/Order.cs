using AHIFventory.ViewModel;
using Microsoft.Data.Sqlite;
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

        private string productDescription;
        public string ProductDescription
        {
            get { return productDescription; }
            set
            {
                if (productDescription != value)
                {
                    productDescription = value;
                    onPropertyChanged("ProductDescription");
                }
            }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    onPropertyChanged("Category");
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
                    onPropertyChanged("Quantity");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Order() { }
        
        public Order(string productName, string productDescription, string category, string supplier)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Category = category;
            Supplier = supplier;
        }

        public Order(SqliteDataReader reader)
        {
            OrderID = reader.IsDBNull(reader.GetOrdinal("OrderID")) ? 0 : reader.GetInt32(reader.GetOrdinal("OrderID"));
            OrderDate = reader.IsDBNull(reader.GetOrdinal("OrderDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("OrderDate"));
            Supplier = reader.IsDBNull(reader.GetOrdinal("Supplier")) ? null : reader.GetString(reader.GetOrdinal("Supplier"));
            ProductID = reader.IsDBNull(reader.GetOrdinal("ProductID")) ? 0 : reader.GetInt32(reader.GetOrdinal("ProductID"));
            ProductName = reader.IsDBNull(reader.GetOrdinal("ProductName")) ? null : reader.GetString(reader.GetOrdinal("ProductName"));
            ProductDescription = reader.IsDBNull(reader.GetOrdinal("ProductDescription")) ? null : reader.GetString(reader.GetOrdinal("ProductDescription"));
            Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString(reader.GetOrdinal("Category"));
            Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("Price"));
            Quantity = reader.IsDBNull(reader.GetOrdinal("Quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("Quantity"));
        }

        public void SaveOrder()
        {
            using (var connection = new SqliteConnection("Data Source=assets\\AHIFventoryDB.db"))
            {
                connection.Open();

                string query;

                if (OrderID == 0)
                {
                    // Insert new order
                    query = @"INSERT INTO tblOrder (OrderDate, Supplier, ProductID, ProductName, ProductDescription, Category, Price, Quantity) 
                              VALUES (@OrderDate, @Supplier, @ProductID, @ProductName, @ProductDescription, @Category, @Price, @Quantity)";
                }
                else
                {
                    // Update existing order
                    query = @"UPDATE tblOrder 
                              SET OrderDate = @OrderDate, Supplier = @Supplier, ProductID = @ProductID, ProductName = @ProductName, 
                                  ProductDescription = @ProductDescription, Category = @Category, Price = @Price, Quantity = @Quantity 
                              WHERE OrderID = @OrderID";
                }

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", OrderID);
                    command.Parameters.AddWithValue("@OrderDate", OrderDate);
                    command.Parameters.AddWithValue("@Supplier", Supplier ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.AddWithValue("@ProductName", ProductName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ProductDescription", ProductDescription ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Category", Category ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@Quantity", Quantity);

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

            OrderViewModel.LoadOrders();
        }

        public void DeleteProduct()
        {
            if (ProductID == null)
            {
                //throw new InvalidOperationException("Product ID cannot be null when deleting a product.");
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

            OrderViewModel.LoadOrders();
        }
    }
}