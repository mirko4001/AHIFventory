using Microsoft.Data.Sqlite;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHIFventory
{
    public class OrderViewModel
    {

        public static ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public static ObservableCollection<Order> OrdersToDelete { get; set; } = new ObservableCollection<Order>();

        public OrderViewModel() { }

        public static void LoadOrders()
        {
            Log.Information("Loading orders");

            Log.Debug("Clearing all orders from collection");
            Orders.Clear();

            Log.Debug("Importing all orders from sql file");
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
            Log.Information("Saving orders");

            Log.Debug("Deleting all orders inside delete collection");
            foreach (Order order in OrdersToDelete)
            {
                order.DeleteOrder();
            }

            OrdersToDelete.Clear();

            Log.Debug("Saving all orders inside collection");
            foreach (Order order in Orders)
            {
                order.SaveOrder();
            }
        }

    }
}
