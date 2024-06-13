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
        public Order OrderTemplate { get; set; }
        public OrderUserControl()
        {
            InitializeComponent();
            OrderTemplate = new Order()
            {
                OrderDate = DateTime.Now,
                Supplier = "Blum GmbH",
                ProductID = 0,
            };
        }

    }
}
