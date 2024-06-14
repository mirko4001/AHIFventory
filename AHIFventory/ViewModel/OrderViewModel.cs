using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHIFventory.ViewModel
{
    public class OrderViewModel
    {

        public static ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();

        public OrderViewModel() { }

        public static void LoadOrders()
        {
            Orders.Clear();

            using (var connection = new SqliteConnection("Data Source=assets\\AHIFventoryDB.db"))
            {
                connection.Open();
                string query = "SELECT * FROM tblOrder";

                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new Order(reader);
                        Orders.Add(order);
                    }
                }
            }
        }
            
        public static void SaveOrders()
        {
            foreach (Order order in Orders)
            {
                order.SaveOrder();
            }
        }

    }
}
