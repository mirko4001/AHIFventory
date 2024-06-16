using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AHIFventory
{
    public class Product : INotifyPropertyChanged
    {

        private int? productID;
        public int? ProductID
        {
            get
            {
                return productID;
            }

            set
            {
                if (productID != value)
                {
                    productID = value;
                    onPropertyChanged("ProductID");
                }
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name != value)
                {
                    name = value;
                    onPropertyChanged("Name");
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if (description != value)
                {
                    description = value;
                    onPropertyChanged("Description");
                }
            }
        }

        private double price;
        public double Price
        {
            get
            {
                return price;
            }

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

        private int stock;
        public int Stock
        {
            get
            {
                return stock;
            }

            set
            {
                if (stock != value)
                {
                    stock = value;

                    if (stock < 0)
                    {
                        stock = 0;
                    }

                    onPropertyChanged("Stock");
                    onPropertyChanged("LowOnStock");
                }
            }
        }

        private int stockWarning;
        public int StockWarning
        {
            get
            {
                return stockWarning;
            }

            set
            {
                if (stockWarning != value)
                {
                    stockWarning = value;

                    if (stockWarning < 0)
                    {
                        stockWarning = 0;
                    }

                    onPropertyChanged("StockWarning");
                    onPropertyChanged("LowOnStock");
                }
            }
        }

        private string image;
        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                if (image != value)
                {
                    image = value;
                    onPropertyChanged("Image");
                }
            }
        }

        private bool notificationSent;
        public bool NotificationSent
        {
            get
            {
                return notificationSent;
            }

            set
            {
                notificationSent = value;
                onPropertyChanged("NotificationSent");
            }
        }

        public bool LowOnStock
        {
            get
            {
                bool value = Stock <= StockWarning;

                if (value)
                {
                    if (!NotificationSent)
                    {
                        GlobalFunction.ShowSnackbar("Info", $"Product '{Name}' is low on stock!", TimeSpan.FromSeconds(4));
                        NotificationSent = true;
                    }
                }
                else
                {
                    if (NotificationSent)
                    {
                        NotificationSent = false;
                    }
                }

                return value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Product(string name, string description, string image)
        { 
            Name = name;
            Description = description;
            Image = image;
            NotificationSent = false;
        }

        public Product(SqliteDataReader reader)
        {
            ProductID = reader.IsDBNull(reader.GetOrdinal("ProductID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ProductID"));
            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name"));
            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"));
            Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDouble(reader.GetOrdinal("Price"));
            Stock = reader.IsDBNull(reader.GetOrdinal("Stock")) ? 0 : reader.GetInt32(reader.GetOrdinal("Stock"));
            StockWarning = reader.IsDBNull(reader.GetOrdinal("StockWarning")) ? 0 : reader.GetInt32(reader.GetOrdinal("StockWarning"));
            Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image"));
            NotificationSent = reader.IsDBNull(reader.GetOrdinal("NotificationSent")) ? false : reader.GetBoolean(reader.GetOrdinal("NotificationSent"));
        }


        public void SaveProduct()
        {
            using (var connection = new SqliteConnection("Data Source=assets\\AHIFventoryDB.db"))
            {
                connection.Open();

                string query;

                // Check if this is a new product or an existing one
                if (ProductID == null)
                {
                    // Insert new product
                    query = @"INSERT INTO tblProduct (Name, Description, Price, Stock, StockWarning, Image, NotificationSent) 
                      VALUES (@Name, @Description, @Price, @Stock, @StockWarning, @Image, @NotificationSent)";
                }
                else
                {
                    // Update existing product
                    query = @"UPDATE tblProduct 
                      SET Name = @Name, Description = @Description, Price = @Price, 
                          Stock = @Stock, StockWarning = @StockWarning, Image = @Image, NotificationSent = @NotificationSent
                      WHERE ProductID = @ProductID";
                }

                using (var command = new SqliteCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Name", Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@Stock", Stock);
                    command.Parameters.AddWithValue("@StockWarning", StockWarning);
                    command.Parameters.AddWithValue("@Image", Image ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NotificationSent", NotificationSent);


                    if (ProductID != null)
                    {
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                    }

                    command.ExecuteNonQuery();
                }

                // If this was a new product, get the last inserted ID
                if (ProductID == null)
                {
                    query = "SELECT last_insert_rowid()";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        ProductID = (int)(long)command.ExecuteScalar();
                    }
                }
            }

            //ProductViewModel.LoadProducts();
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
                var query = "DELETE FROM tblProduct WHERE ProductID = @ProductID";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.ExecuteNonQuery();
                }
            }

            //ProductViewModel.LoadProducts();
        }

        public override string ToString()
        {
            return $"{Name}, {Price}€ per unit";
        }

    }
}
