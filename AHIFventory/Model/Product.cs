using Microsoft.Data.Sqlite;
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

        private int productID;
        public int ProductID
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

        public Product() { }

        public void LoadProduct(SqliteDataReader reader)
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
            using (var connection = new SqliteConnection("Data Source=assets\\AHIFventoryDB.db"))
            {
                connection.Open();

                string query = "INSERT INTO Products (ProductID, Name, Description, Category, Price, Stock, StockWarning) VALUES (@ProductID, @Name, @Description, @Category, @Price, @Stock, @StockWarning)";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", this.ProductID);
                    command.Parameters.AddWithValue("@Name", this.Name);
                    command.Parameters.AddWithValue("@Description", this.Description);
                    command.Parameters.AddWithValue("@Category", this.Category);
                    command.Parameters.AddWithValue("@Price", this.Price);
                    command.Parameters.AddWithValue("@Stock", this.Stock);
                    command.Parameters.AddWithValue("@StockWarning", this.StockWarning);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
