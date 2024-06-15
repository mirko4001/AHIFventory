using Microsoft.Identity.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AHIFventory
{
    /// <summary>
    /// Interaction logic for OrderUserControl.xaml
    /// </summary>
    public partial class OrderUserControl : UserControl
    {
        public bool isBuyOrder { get; set; } = true;
        public bool isSellOrder { get; set; } = false;
        public Order OrderObject { get; set; }
        public OrderUserControl(Order order)
        {
            Log.Information("Initializing order user control");

            InitializeComponent();
            OrderObject = order;

            if (OrderObject.Action == "Sell")
            {
                isBuyOrder = false;
                isSellOrder = true;
            }

            DataContext = this;
        }

        /*
        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditProductWindow editProductWindow = new EditProductWindow(OrderObject);
            editProductWindow.ShowDialog();

            if (editProductWindow.SaveOnClose)
            {
                OrderObject.SaveProduct();
            }
        }
        */

        private void Delete()
        {
            Log.Information("Delete order has been confirmed");

            DeleteOrderFlyout.IsOpen = false;
            OrderViewModel.OrdersToDelete.Add(OrderObject);
            OrderViewModel.Orders.Remove(OrderObject);
        }

        private void DeleteNoButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Debug("Delete order and don't revert changes");

            Delete();
        }

        private void DeleteYesButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Debug("Delete order and revert changes");

            foreach (Product product in ProductViewModel.Products)
            {
                if (product.Name == OrderObject.ProductName)
                {
                    if (OrderObject.Action == "Buy")
                    {
                        product.Stock -= OrderObject.Quantity;
                    }
                    else
                    {
                        product.Stock += OrderObject.Quantity;
                    }

                    break;
                }
            }

            Delete();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteOrderFlyout.IsOpen = true;
        }
    }
}