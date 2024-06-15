using Microsoft.Identity.Client;
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new DeleteOrderConfirmationDialogWindow();
            dialog.ShowDialog();

            if (dialog.IsConfirmed)
            {
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
                    }
                }
            }

            //OrderObject.DeleteOrder();
            OrderViewModel.OrdersToDelete.Add(OrderObject);
            OrderViewModel.Orders.Remove(OrderObject);
        }
    }
}