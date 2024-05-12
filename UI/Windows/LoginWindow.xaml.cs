using CourierLogistic.Libs;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static CourierLogistic.APIHelper.UserHelper;

namespace CourierLogistic.UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        List<UserGet> usersList;
        UserGet currentUser;
        public LoginWindow()
        {
            InitializeComponent();

        }

        private void btn_LogIn_Click(object sender, RoutedEventArgs e)
        {
            string Login = tBoxLogin.Text;
            string Password = pBoxLogin.Password;

            usersList = UserGet.GetUsers();

            currentUser = usersList.Find(p => p.Login.Equals(Login) && p.Password.Equals(Password));

            if (currentUser == null)
            {
                MessageBox.Show("Пользователь не найден в системе", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            if (currentUser.RoleId == 2)
            {
                MainWindow mainWindow = new MainWindow(currentUser);
                UserEntryLog.LogUserEntry(currentUser.Id);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Данный пользователь не имеет доступ к этой системе", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void checkBox_ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox_ShowPassword.IsChecked == true)
            {

                pBoxLogin.Visibility = Visibility.Hidden;
                tBoxPasswordShow.Visibility = Visibility.Visible;
            }

            else
            {

                pBoxLogin.Visibility = Visibility.Visible;
                tBoxPasswordShow.Visibility = Visibility.Hidden;
            }
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void tBoxPasswordShow_TextChanged(object sender, TextChangedEventArgs e)
        {
            pBoxLogin.Password = tBoxPasswordShow.Text;
        }

        private void pBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            tBoxPasswordShow.Text = pBoxLogin.Password;
        }
    }
}
