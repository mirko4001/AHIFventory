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
using Wpf.Ui.Controls;

namespace AHIFventory
{
    /// <summary>
    /// Interaction logic for ProductUserControl.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        public Product ProductObject { get; set; }

        public ProductUserControl(Product product)
        {
            Log.Information("Initializing product user control");

            InitializeComponent();

            ProductObject = product;

            DataContext = this;
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("Edit product button has been clicked");

            EditProductWindow editProductWindow = new EditProductWindow(ProductObject);
            editProductWindow.ShowDialog();

            if (editProductWindow.SaveOnClose)
            {
                ProductObject.SaveProduct();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("Delete product button has been clicked");

            //ProductObject.DeleteProduct();
            ProductViewModel.ProductsToDelete.Add(ProductObject);
            ProductViewModel.Products.Remove(ProductObject);
        }
    }
}
