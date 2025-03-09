using System;
using System.IO;
using System.Windows;

namespace CyberTools
{
    public partial class App : Application
    {
        public App()
        {
            // Håndter eventuelle uventede unntak på applikasjonsnivå
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Skriv unntaket til en fil
            File.WriteAllText("error_log.txt", e.Exception.ToString());
            // Hindre at unntaket krasjer applikasjonen
            e.Handled = true;
        }
    }
}
