using System;
using System.Windows;
using Microsoft.Win32;

namespace BetterStock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StockManager StockManager;

        public MainWindow()
        {
            StockManager = new StockManager();
            InitializeComponent();

            this.Stop.IsEnabled = false;
        }
        
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            int interval;

            try
            {
                interval = (int)TimeSpan.FromMinutes(int.Parse(Interval.Text)).TotalMilliseconds;
            }
            catch (FormatException)
            {
                this.Interval.Text = "Enter proper number value.";
                return;
            }

            StockManager.StartRefreshingStockValues(interval, FilePath.Text);

            this.Interval.IsEnabled = false;
            this.Start.IsEnabled = false;
            this.Stop.IsEnabled = true;
        }
        
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            StockManager.StopRefreshingStockValues();

            this.Interval.IsEnabled = true;
            this.Start.IsEnabled = true;
            this.Stop.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog {DefaultExt = ".txt"};

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string file = dlg.FileName;
                this.FilePath.Text = file;
            }
        }
    }
}
