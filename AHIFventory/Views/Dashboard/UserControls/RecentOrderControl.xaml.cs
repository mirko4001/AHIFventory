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
    /// Interaction logic for RecentOrderControl.xaml
    /// </summary>
    public partial class RecentOrderControl : UserControl
    {
        public Product OrderObject { get; set; }
        public RecentOrderControl()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public static readonly DependencyProperty BuyOrSellProperty = DependencyProperty.Register("BuyOrSell", typeof(string), typeof(RecentOrderControl), new PropertyMetadata(""));
        public string BuyOrSell
        {
            get { return (string)GetValue(BuyOrSellProperty); }
            set { SetValue(BuyOrSellProperty, value); }
        }
    }
}
