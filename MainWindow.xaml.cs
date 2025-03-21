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
using CyberTools.View;

namespace CyberTools;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void Keylogger_Click(object sender, RoutedEventArgs e)
    {
        CyberTools.View.KeyLoggerView keyLoggerView = new CyberTools.View.KeyLoggerView();
        keyLoggerView.Show();
        this.Close();
    }

    private void Main_Click(object sender, RoutedEventArgs e)
    {
        CyberTools.MainWindow MainWindow = new CyberTools.MainWindow();
        MainWindow.Show();
        this.Close();
    }

    private void Wifi_Click(object sender, RoutedEventArgs e)
    {
        CyberTools.View.Wifi wifi = new CyberTools.View.Wifi();
        wifi.Show();
        this.Close();
    }


}