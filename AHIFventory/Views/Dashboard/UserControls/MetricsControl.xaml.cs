using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MetricsControl.xaml
    /// </summary>
    public partial class MetricsControl : UserControl
    {
        public ObservableCollection<Order> O { get; set; } = OrderViewModel.Orders;
        public ObservableCollection<Order> RecentOrders { get; set; }
        public ObservableCollection<Product> P { get; set; } = ProductViewModel.Products;
        public MetricsControl()
        {
            InitializeComponent();

            RecentOrders = new ObservableCollection<Order>(O.OrderByDescending(o => o.OrderDate).Take(3));

            this.Loaded += UpdateOrders;
            DataContext = this;
        }

        private void UpdateOrders(object sender, RoutedEventArgs e)
        {
            OrdersStackPanel.Children.Clear();
            foreach (Order order in RecentOrders)
            {
                OrderUserControl orderUserControl = new OrderUserControl(order);
                orderUserControl.Margin = new Thickness(50);
                OrdersStackPanel.Children.Add(orderUserControl);
            }
        }
    }
}
