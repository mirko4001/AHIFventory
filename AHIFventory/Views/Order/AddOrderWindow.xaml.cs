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
using Wpf.Ui.Controls;

namespace AHIFventory
{
    /// <summary>
    /// Interaction logic for AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public bool SaveOnClose = false;
        public Order OrderObject { get; set; }
        public Product ProductObject { get; set; }

        public AddOrderWindow()
        {
            Log.Information("Initializing add order window");

            InitializeComponent();

            foreach (Product product in ProductViewModel.Products)
            {
                if (ProductObject == null)
                {
                    ProductObject = product;
                }

                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = product;
                ProductComboBox.Items.Add(comboBoxItem);
            }

            OrderObject = new Order("Product Name", "Supplier")
            {
                OrderDate = DateTime.Now,
                Action = "Buy",
            };

            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("Add order has been confirmed");

            OrderObject.Price = ProductObject.Price * OrderObject.Quantity;
            OrderObject.ProductName = ProductObject.Name;

            if (OrderObject.Action == "Sell")
            {
                if (ProductObject.Stock >= OrderObject.Quantity)
                {
                    ProductObject.Stock -= OrderObject.Quantity;
                }
                else
                {
                    GlobalFunction.ShowCustomMessageBox("Warning", "Not enough Stock");
                    return;
                }
            }

            if (OrderObject.Action == "Buy")
            {
                ProductObject.Stock += OrderObject.Quantity;
            }

            SaveOnClose = true;
            Close();
        }
    }
}
