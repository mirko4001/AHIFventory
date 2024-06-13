using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHIFventory
{
    public class ProductViewModel
    {
        public static ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public ProductViewModel() { }

        public static void LoadProducts()
        {
            using (var connection = new SqliteConnection("Data Source=assets\\AHIFventoryDB.db"))
            {
                connection.Open();
                string query = "SELECT * FROM tblProduct";

                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product(reader);
                        Products.Add(product);
                    }
                }
            }
        }

        public static void SaveProducts()
        {
            foreach (Product product in Products) 
            {
                product.SaveProduct();
            }
        }
    }
}
