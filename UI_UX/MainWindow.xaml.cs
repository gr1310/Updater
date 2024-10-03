using DataExchangeModule;
using System.Net;
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

using ClientModule;
using ServerModule;
using ViewModels;

namespace UI_UX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadClientInfo VM = new LoadClientInfo();
            this.DataContext = VM;
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            LoadClientInfo VM = new LoadClientInfo();
            this.DataContext = VM;

            MessageBox.Show("Client info refreshed.");
        }


        private void UploadDLL(object sender, RoutedEventArgs e)
        {
            try
            {
                DataExchange.UploadDLLFromClientToServer();

                MessageBox.Show($"File sent successfully.");
            }
            catch(Exception ex )
            {
                MessageBox.Show($"Error sending DLL, Error Encountered: {ex.Message}");
            }
        }

        private async void OnServerStart(object sender, RoutedEventArgs e)
        {
            FileMonitor VM = new FileMonitor();
            this.DataContext = VM;
            StartServerButton.Content = "Server is Running";
            StartServerButton.IsEnabled = false;

            try
            {
                await Task.Run(() => DataRecieve.RecieveDLLFromClient());
                StartServerButton.Content = "Start Server";
                StartServerButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Encountered: {ex.Message}");
                StartServerButton.Content = "Start Server";
                StartServerButton.IsEnabled = true; 
            }
        }
    }
}