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

        public EditProductWindow(Product product)
        {
            InitializeComponent();
            ProductObject = product;
            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SaveOnClose = true;
        }
    }
}
