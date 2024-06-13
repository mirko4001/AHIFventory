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
            InitializeComponent();
            ProductObject = product;
            DataContext = this;
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditProductWindow editProductWindow = new EditProductWindow(ProductObject);
            editProductWindow.ShowDialog();

            if (editProductWindow.SaveOnClose)
            {
                ProductObject.SaveProduct();
            }
        }
    }
}
