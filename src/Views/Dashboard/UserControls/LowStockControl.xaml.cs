using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Interaction logic for LowStockControl.xaml
    /// </summary>
    public partial class LowStockControl : UserControl
    {
        public LowStockControl()
        {
            InitializeComponent();
            Loaded += LowStockControl_Loaded;
            ProductViewModel.Products.CollectionChanged += Products_CollectionChanged;
        }

        private void Products_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            LoadLowStockProducts();
        }

        private void LowStockControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLowStockProducts();
        }

        private void LoadLowStockProducts()
        {
            ProductsStackPanel.Children.Clear();
            int count = 0;
            if (ProductViewModel.Products.Count > 0)
            {
                var lowStockProducts = ProductViewModel.Products
                    .Where(p => p.Stock <= p.StockWarning);

                foreach (var product in lowStockProducts)
                {
                    if (count >= 2)
                    {
                        break;
                    }
                    ProductUserControl productControl = new ProductUserControl(product);
                    productControl.Margin = new Thickness(10);
                    ProductsStackPanel.Children.Add(productControl);
                    count++;
                }
            }
        }
    }
}
