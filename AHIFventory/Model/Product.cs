﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private string category;
        public string Category
        {
            get
            {
                return category;
            }

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
            get
            {
                return price;
            }

            set
            {
                if (price != value)
                {
                    price = value;
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
                    onPropertyChanged("Stock");
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
                    onPropertyChanged("StockWarning");
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

        public bool LowOnStock
        {
            get
            {
                return Stock <= StockWarning;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Product(string name, string description, string image)
        { 
            this.Name = name;
            this.Description = description;
            this.Image = image;
        }

        public Product(SqliteDataReader reader)
        {
            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
            Name = reader.GetString(reader.GetOrdinal("Name"));
            Description = reader.GetString(reader.GetOrdinal("Description"));
            Category = reader.GetString(reader.GetOrdinal("Category"));
            Price = reader.GetInt32(reader.GetOrdinal("Price"));
            Stock = reader.GetInt32(reader.GetOrdinal("Stock"));
            StockWarning = reader.GetInt32(reader.GetOrdinal("StockWarning"));
        }

        public void SaveProduct()
        {
            using (var connection = new SqliteConnection("Data Source=your_database_path_here"))
            {
                connection.Open();

                string query;

                // Check if this is a new product or an existing one
                if (ProductID == null)
                {
                    // Insert new product
                    query = "INSERT INTO tblProduct (Name, Description, Category, Price, Stock, StockWarning, Image) VALUES (@Name, @Description, @Category, @Price, @Stock, @StockWarning, @Image)";
                }
                else
                {
                    // Update existing product
                    query = "UPDATE tblProduct SET Name = @Name, Description = @Description, Category = @Category, Price = @Price, Stock = @Stock, StockWarning = @StockWarning, Image = @Image WHERE ProductID = @ProductID";
                }

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@Category", Category);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@Stock", Stock);
                    command.Parameters.AddWithValue("@StockWarning", StockWarning);
                    command.Parameters.AddWithValue("@Image", Image);

                    // Add the ProductID parameter for the update case
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
        }

    }
}
