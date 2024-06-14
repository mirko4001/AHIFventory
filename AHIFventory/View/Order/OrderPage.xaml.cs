using AHIFventory.ViewModel;
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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();

            /*
            for (int i = 0; i < 5; i++)
            {
                Order order = new Order("", "", "", "");
                order.SaveOrder();

                OrderUserControl productUserControl = new OrderUserControl(order);
                productUserControl.Margin = new Thickness(5);
                OrdersStackPanel.Children.Add(productUserControl);
            }
            */

            OrderViewModel.LoadOrders();

            UpdateList();

            OrderViewModel.Orders.CollectionChanged += Orders_CollectionChanged;
        }

        private void Orders_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateList();
        }

        public void UpdateList()
        {
            OrdersStackPanel.Children.Clear();

            foreach (Order order in OrderViewModel.Orders)
            {
                OrderUserControl orderUserControl = new OrderUserControl(order);
                orderUserControl.Margin = new Thickness(5);
                OrdersStackPanel.Children.Add(orderUserControl);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
