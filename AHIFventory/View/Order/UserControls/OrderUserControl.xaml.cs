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
        public Product ProductTemplate { get; set; }
        public Order OrderTemplate { get; set; }
        public double OrderPrice { get; set; }
        public OrderUserControl()
        {
            InitializeComponent();

            /*
            ProductTemplate = new Product("", "", "")
            {
                ProductID = 69,
                Name = "Glas",
                Description = "Product Description"
            };
            OrderTemplate = new Order()
            {
                OrderID = 0,
                OrderDate = DateTime.Now,
                Supplier = "Blum GmbH",
                ProductID = (int)ProductTemplate.ProductID,
                Quantity = 5,
            };

            OrderPrice = ProductTemplate.Price * OrderTemplate.Quantity;
            */

            DataContext = this;
        }

        public Order OrderObject { get; set; }

        public OrderUserControl(Order order)
        {
            InitializeComponent();
            OrderObject = order;
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
            OrderObject.DeleteProduct();
        }
    }
}