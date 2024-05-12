using CourierLogistic.APIHelper;
using CourierLogistic.UI.Pages;
using System.Windows;

namespace CourierLogistic.UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserHelper.UserGet currentUser;
        public MainWindow(UserHelper.UserGet currentUser)
        {
            InitializeComponent();
            tBlockCurrentUser.Text = $"Пользователь: {currentUser.Login}";
            this.currentUser = currentUser;
            btnPackages_Click(null, null);
        }

        private void btnShippings_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ShippingsPage(currentUser.Id));
        }

        private void btnPackages_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new PackagesPage(currentUser.WorkplaceId));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

    }
}
