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
using System.Windows.Shapes;

namespace AHIFventory
{
    /// <summary>
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>

    public partial class EditProductWindow : Window
    {
        public bool SaveOnClose = false;
        public Product ProductObject { get; set; }
        public Product ProductMain { get; set; }

        public EditProductWindow(Product product)
        {
            Log.Information("Initializing edit product window");

            InitializeComponent();

            ProductObject = new Product(product.Name, product.Description, product.Image)
            {
                ProductID = product.ProductID,
                Stock = product.Stock,
                Price = product.Price,
                StockWarning = product.StockWarning,
            };

            ProductMain = product;

            DataContext = this;
        }

        public EditProductWindow()
        {
            Log.Information("Initializing edit product window");

            InitializeComponent();

            ProductObject = new Product("New Product", "Product Description", "");

            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("Product changes confirmed");

            if (ProductMain != null && ProductObject != null)
            {
                ProductMain.Name = ProductObject.Name;
                ProductMain.Description = ProductObject.Description;
                ProductMain.Stock = ProductObject.Stock;
                ProductMain.ProductID = ProductObject.ProductID;
                ProductMain.Price = ProductObject.Price;
                ProductMain.StockWarning = ProductObject.StockWarning;
                ProductMain.Image = ProductObject.Image;
            }

            SaveOnClose = true;
        }
    }
}
