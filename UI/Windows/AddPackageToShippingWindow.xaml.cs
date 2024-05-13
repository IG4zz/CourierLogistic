using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static CourierLogistic.APIHelper.ShippingPackageHelper;
using static CourierLogistic.APIHelper.PackageHelper;

namespace CourierLogistic.UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddPackageToShippingWindow.xaml
    /// </summary>
    public partial class AddPackageToShippingWindow : Window
    {
        private ShippingPackagePost shippingPackage = new ShippingPackagePost();
        private int shippingId;
        public AddPackageToShippingWindow(int shippingId)
        {
            InitializeComponent();
            var shippingPackages = ShippingPackageGet.GetShippingPackages();
            var data = PackageGet.GetPackages();

            foreach (var item in shippingPackages)
            {
                var buffer = data.Find(p => p.Id.Equals(item.PackageId));
                data.Remove(buffer);
                buffer = data.Find(p => p.isActive.Equals(false));
                data.Remove(buffer);
            }


            cmbBoxPackages.ItemsSource = data;
            this.shippingId = shippingId;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var data = cmbBoxPackages.SelectedItem as PackageGet;

            if(data == null)
            {
                MessageBox.Show("Выберите посылку для добавления", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }               

            shippingPackage.PackageId = data.Id;
            shippingPackage.ShippingId = shippingId;

            if (MessageBox.Show("Вы действительно хотите сохранить новые данные?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                ShippingPackagePost.PostShippingPackage(shippingPackage);
                MessageBox.Show("Данные сохранены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
