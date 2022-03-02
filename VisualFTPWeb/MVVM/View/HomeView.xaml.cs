using System.Windows;
using System.Windows.Controls;
using VisualFTPWeb.Cores;

namespace VisualFTPWeb.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour MainView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void RegisterCredentials(object sender, RoutedEventArgs e)
        {
            FtpSystem.host = Host.Text;
            FtpSystem.login = Login.Text;
            FtpSystem.password = Password.Text;
            FtpSystem.port = Port.Text;
            FtpSystem.filepath = Filepath.Text;

            FtpSystem.InitializeFtpConnection();
        }
    }
}
