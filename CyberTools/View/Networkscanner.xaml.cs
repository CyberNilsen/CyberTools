using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CyberTools.View
{
    public partial class NetworkScanner : Window
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private List<NetworkDevice> discoveredDevices = new List<NetworkDevice>();

        public NetworkScanner()
        {
            InitializeComponent();

            // Set default subnet based on local IP
            try
            {
                string localIp = GetLocalIPAddress();
                if (!string.IsNullOrEmpty(localIp))
                {
                    string[] parts = localIp.Split('.');
                    if (parts.Length >= 3)
                    {
                        SubnetInput.Text = $"{parts[0]}.{parts[1]}.{parts[2]}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting local IP: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.MainWindow mainWindow = new CyberTools.MainWindow();
            mainWindow.Show();
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

        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            string subnet = SubnetInput.Text.Trim();
            if (string.IsNullOrEmpty(subnet))
            {
                MessageBox.Show("Please enter a valid subnet (e.g., 192.168.1)", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update UI
            ScanButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            DevicesList.Items.Clear();
            discoveredDevices.Clear();
            ScanProgress.Value = 0;
            ScanDetailsLabel.Content = "Scanning network...";

            // Create a new cancellation token source
            _cts = new CancellationTokenSource();

            try
            {
                await ScanNetworkAsync(subnet, _cts.Token);
            }
            catch (OperationCanceledException)
            {
                ScanDetailsLabel.Content = "Scan cancelled.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during scan: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ScanDetailsLabel.Content = "Scan failed. See error details.";
            }
            finally
            {
                // Reset UI
                ScanButton.IsEnabled = true;
                CancelButton.IsEnabled = false;
                ScanProgress.Value = 100;
            }
        }

        private async Task ScanNetworkAsync(string subnet, CancellationToken cancellationToken)
        {
            int totalHosts = 254; // Total hosts in a typical class C subnet
            int scannedHosts = 0;

            List<Task> pingTasks = new List<Task>();

            for (int i = 1; i <= 254; i++)
            {
                string ipAddress = $"{subnet}.{i}";
                pingTasks.Add(PingHostAsync(ipAddress, cancellationToken));

                // Process in batches to avoid overwhelming the network
                if (pingTasks.Count >= 20 || i == 254)
                {
                    await Task.WhenAny(Task.WhenAll(pingTasks), Task.Delay(5000, cancellationToken));
                    scannedHosts += pingTasks.Count;

                    // Update progress
                    int progressValue = (int)((double)scannedHosts / totalHosts * 100);
                    Dispatcher.Invoke(() =>
                    {
                        ScanProgress.Value = progressValue;
                        ScanDetailsLabel.Content = $"Scanning: {scannedHosts}/{totalHosts} hosts checked. Found {discoveredDevices.Count} devices.";
                    });

                    pingTasks.Clear();

                    // Check cancellation
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            // Final update
            Dispatcher.Invoke(() =>
            {
                if (discoveredDevices.Count == 0)
                {
                    ScanDetailsLabel.Content = "Scan complete. No devices found.";
                }
                else
                {
                    ScanDetailsLabel.Content = $"Scan complete. Found {discoveredDevices.Count} devices.";
                }
            });
        }

        private async Task PingHostAsync(string ipAddress, CancellationToken cancellationToken)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = await ping.SendPingAsync(ipAddress, 1000);

                    if (reply.Status == IPStatus.Success)
                    {
                        // Try to get hostname
                        string hostname = string.Empty;
                        try
                        {
                            IPHostEntry hostEntry = await Dns.GetHostEntryAsync(ipAddress);
                            hostname = hostEntry.HostName;
                        }
                        catch
                        {
                            hostname = "Unknown";
                        }

                        // Try to get MAC address
                        string macAddress = GetMacAddress(ipAddress);

                        // Add device to our list
                        var device = new NetworkDevice
                        {
                            IPAddress = ipAddress,
                            Hostname = hostname,
                            MacAddress = macAddress,
                            ResponseTime = reply.RoundtripTime
                        };

                        Dispatcher.Invoke(() =>
                        {
                            discoveredDevices.Add(device);
                            DevicesList.Items.Add($"IP: {device.IPAddress} | Hostname: {device.Hostname} | MAC: {device.MacAddress} | Response: {device.ResponseTime}ms");
                        });
                    }
                }
            }
            catch (Exception)
            {
                // Just ignore failed pings
            }
        }

        private string GetMacAddress(string ipAddress)
        {
            try
            {
                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;

                if (SendARP(BitConverter.ToUInt32(IPAddress.Parse(ipAddress).GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) == 0)
                {
                    string[] str = new string[macAddr.Length];
                    for (int i = 0; i < macAddr.Length; i++)
                    {
                        str[i] = macAddr[i].ToString("x2");
                    }
                    return string.Join(":", str);
                }
            }
            catch
            {
                // Ignore errors
            }
            return "Unknown";
        }

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(uint destIp, uint srcIP, byte[] macAddr, ref uint physicalAddrLen);

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cts.Cancel();
            ScanDetailsLabel.Content = "Cancelling scan...";
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ddos_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.View.Ddos ddos = new CyberTools.View.Ddos();
            ddos.Show();
            this.Close();
        }
    }

    public class NetworkDevice
    {
        public string IPAddress { get; set; } = string.Empty;
        public string Hostname { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;
        public long ResponseTime { get; set; }
    }
}