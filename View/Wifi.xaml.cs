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
using CyberTools.View;

namespace CyberTools.View
{
    /// <summary>
    /// Interaction logic for Wifi.xaml
    /// </summary>
    public partial class Wifi : Window
    {
        public Wifi()
        {
            InitializeComponent();
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.MainWindow MainWindow = new CyberTools.MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void Keylogger_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.View.KeyLoggerView keyLoggerView = new CyberTools.View.KeyLoggerView();
            keyLoggerView.Show();
            this.Close();
        }

        private void Wifi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
