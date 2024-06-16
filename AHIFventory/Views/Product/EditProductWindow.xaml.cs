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

            if (string.IsNullOrEmpty(ProductObject.Image) || string.IsNullOrEmpty(ProductObject.Image.Trim()))
            {
                ProductObject.Image = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fstatic.vecteezy.com%2Fsystem%2Fresources%2Fpreviews%2F004%2F141%2F669%2Foriginal%2Fno-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg&f=1&nofb=1&ipt=34a004bb91d28dd9906e84b1bc52e2314d7c3a07d91cd5040efc76794d74b52a&ipo=images";
            }

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
