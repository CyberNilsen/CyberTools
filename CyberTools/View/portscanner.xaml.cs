using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// Interaction logic for portscanner.xaml
    /// </summary>
    public partial class portscanner : Window
    {
        public ObservableCollection<string> OpenPorts { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ClosedPorts { get; set; } = new ObservableCollection<string>();

        // You can add a CancellationTokenSource if you want to support cancellation.
        private CancellationTokenSource _cts;

        public portscanner()
        {
            InitializeComponent();
            ResultsList.ItemsSource = OpenPorts;
            ClosedResultsList.ItemsSource = ClosedPorts;
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.MainWindow mainWindow = new CyberTools.MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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


        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            string target = TargetInput.Text.Trim();
            if (string.IsNullOrEmpty(target))
            {
                MessageBox.Show("Please enter a valid IP or domain.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(StartPort.Text, out int startPort) || !int.TryParse(EndPort.Text, out int endPort))
            {
                MessageBox.Show("Please enter valid port numbers.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (startPort < 1 || endPort > 65535 || startPort > endPort)
            {
                MessageBox.Show("Invalid port range! Use 1-65535.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ScanButton.IsEnabled = false;
            OpenPorts.Clear();
            ClosedPorts.Clear();
            ScanProgress.Value = 0;

            _cts = new CancellationTokenSource();

            await ScanPortsAsync(target, startPort, endPort, _cts.Token);

            ScanButton.IsEnabled = true;
        }

        private async Task ScanPortsAsync(string target, int startPort, int endPort, CancellationToken token)
        {
            int totalPorts = endPort - startPort + 1;
            int scannedPorts = 0;
            var tasks = new List<Task>();

            using (SemaphoreSlim semaphore = new SemaphoreSlim(100))
            {
                for (int port = startPort; port <= endPort; port++)
                {
                    if (token.IsCancellationRequested)
                        break;

                    await semaphore.WaitAsync(token);
                    int currentPort = port;
                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            bool isOpen = await IsPortOpenWithRetry(target, currentPort, token);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                if (isOpen)
                                {
                                    OpenPorts.Add($"✅ Port {currentPort} is OPEN");
                                }
                                else
                                {
                                    ClosedPorts.Add($"❌ Port {currentPort} is CLOSED");
                                }
                            });
                        }
                        finally
                        {
                            Interlocked.Increment(ref scannedPorts);
                            Application.Current.Dispatcher.Invoke(() =>
                                ScanProgress.Value = (scannedPorts / (double)totalPorts) * 100);
                            semaphore.Release();
                        }
                    }, token);
                    tasks.Add(task);
                }
                await Task.WhenAll(tasks);
            }
        }

        
        private async Task<bool> IsPortOpenWithRetry(string host, int port, CancellationToken token, int retryCount = 2)
        {
            for (int i = 0; i < retryCount; i++)
            {
                if (await IsPortOpen(host, port, token))
                {
                    return true;
                }
            }
            return false;
        }

  
        private async Task<bool> IsPortOpen(string host, int port, CancellationToken token)
        {
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    var connectTask = client.ConnectAsync(host, port);
                    var timeoutTask = Task.Delay(1000, token);  

                    if (await Task.WhenAny(connectTask, timeoutTask) == connectTask)
                    {
                        return client.Connected;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        private void Ddos_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.View.Ddos ddos = new CyberTools.View.Ddos();
            ddos.Show();
            this.Close();
        }
    }
}
