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
    /// Interaction logic for DeleteOrderConfirmationDialogWindow.xaml
    /// </summary>
    public partial class DeleteOrderConfirmationDialogWindow : Window
    {
        public bool IsConfirmed { get; private set; }

        public DeleteOrderConfirmationDialogWindow()
        {
            InitializeComponent();

            YesButton.Click += (s, e) => { IsConfirmed = true; this.Close(); };
            NoButton.Click += (s, e) => { IsConfirmed = false; this.Close(); };
        }
    }
}
