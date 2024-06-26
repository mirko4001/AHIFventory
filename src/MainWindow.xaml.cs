﻿using Serilog;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
               .CreateLogger();

            GlobalFunction.SnackbarService.SetSnackbarPresenter(SnackbarPresenter);

            ProductViewModel.LoadProducts();
            OrderViewModel.LoadOrders();

            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RootNavigation.Navigate(typeof(DashboardPage));
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            ProductViewModel.SaveProducts();
            OrderViewModel.SaveOrders();

            Log.CloseAndFlush();
        }
    }
}