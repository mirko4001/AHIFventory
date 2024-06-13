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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();

            Product product = new Product("Screws", "Stuff used for stuff", "https://upload.wikimedia.org/wikipedia/commons/0/0c/Phillips_screw.jpg");
            product.SaveProduct();

            ProductUserControl productUserControl = new ProductUserControl(product);
            productUserControl.Margin = new Thickness(5);
            ProductsStackPanel.Children.Add(productUserControl);
        }
    }
}
