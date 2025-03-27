using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CyberTools.View
{
    /// <summary>
    /// Interaction logic for Ddos.xaml
    /// </summary>
    public partial class Ddos : Window
    {
        Process pythonProcess;
        public Ddos()
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
            CyberTools.View.Wifi wifi = new CyberTools.View.Wifi();
            wifi.Show();
            this.Close();
        }

        private void Portscanner_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.View.portscanner portscanner = new CyberTools.View.portscanner();
            portscanner.Show();
            this.Close();
        }

        private void network_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.View.NetworkScanner Networkscanner = new CyberTools.View.NetworkScanner();
            Networkscanner.Show();
            this.Close();
        }

        private void ddos_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.View.Ddos ddos = new CyberTools.View.Ddos();
            ddos.Show();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            string targetIP = txtIP.Text;
            string targetPort = txtPort.Text;

            if (string.IsNullOrEmpty(targetIP) || string.IsNullOrEmpty(targetPort))
            {
                lblStatus.Content = "Enter IP and Port!";
                return;
            }

            pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = "python";  
            pythonProcess.StartInfo.Arguments = $"CyberTools.Main\\ddos.py {targetIP} {targetPort}";
            pythonProcess.StartInfo.UseShellExecute = false;
            pythonProcess.StartInfo.CreateNoWindow = true;
            pythonProcess.StartInfo.RedirectStandardOutput = true;
            pythonProcess.StartInfo.RedirectStandardError = true;

            pythonProcess.OutputDataReceived += (s, args) => Dispatcher.Invoke(() =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                    lblStatus.Content = args.Data;
            });

            pythonProcess.Start();
            pythonProcess.BeginOutputReadLine();

            lblStatus.Content = "Attack Started!";
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
           
            
                pythonProcess.Kill();
                lblStatus.Content = "Attack Stopped!";
            
        }

    }
}
