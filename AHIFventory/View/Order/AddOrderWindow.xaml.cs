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
    /// Interaction logic for AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public bool SaveOnClose = false;
        public Order OrderObject { get; set; }

        public AddOrderWindow()
        {
            InitializeComponent();
            OrderObject = new Order("Product Name", "Product Description", "Supplier")
            {
                OrderDate = DateTime.Now,
            };

            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SaveOnClose = true;
        }
    }
}
