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

namespace CourierLogistic.UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShippingPackagesWindow.xaml
    /// </summary>
    public partial class ShippingPackagesWindow : Window
    {
        private readonly int shippingId;
        List<ShippingPackageGet> shippingPackages = new List<ShippingPackageGet>();
        public ShippingPackagesWindow(int shippingId)
        {
            InitializeComponent();
            shippingPackages = ShippingPackageGet.GetShippingPackagesByShipping(shippingId);
            DGridShippingPackages.ItemsSource = shippingPackages;
            this.shippingId = shippingId;
        }

        private void btnAddPackageToShipping_Click(object sender, RoutedEventArgs e)
        {
            AddPackageToShippingWindow addPackageToShippingWindow = new AddPackageToShippingWindow(shippingId);
            addPackageToShippingWindow.ShowDialog();
        }

        private void btnDeletePackageFromShipping_Click(object sender, RoutedEventArgs e)
        {
            var data = DGridShippingPackages.SelectedItem as ShippingPackageGet;

            if (data == null)
            {
                MessageBox.Show("Не выбрана посылка для удаления", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }              

            ShippingPackagePost.DeleteShippingPackage(data.Id);
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            shippingPackages = ShippingPackageGet.GetShippingPackagesByShipping(shippingId);
            DGridShippingPackages.ItemsSource = shippingPackages;
        }
    }
}
