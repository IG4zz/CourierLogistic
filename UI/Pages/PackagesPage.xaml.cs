using CourierLogistic.UI.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static CourierLogistic.APIHelper.PackageHelper;
using static CourierLogistic.APIHelper.ShippingHelper;

namespace CourierLogistic.UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для PackagesPage.xaml
    /// </summary>
    public partial class PackagesPage : Page
    {
        private int workplaceId;
        public PackagesPage(int workplaceId)
        {
            InitializeComponent();

            this.workplaceId = workplaceId;

            SortType.Items.Add("По возрастанию номера посылки");
            SortType.Items.Add("По убыванию номера посылки");

            tBlock_PageName.Text = $"Список посылок на пункте №{workplaceId}";          
        }

        public void SetItemSource(int workplaceId)
        {
            tBoxNoResult.Visibility = Visibility.Hidden;

            var listPackages = PackageGet.GetPackages();

            string searchText = tBoxSearch.Text;

            listPackages = listPackages
                .Where(p => p.WorkplaceId.Equals(workplaceId))
                .ToList();

            listPackages = listPackages.Where(p => p.Id.ToString().Contains(searchText)
            || p.Sender.ToString().ToLower().Contains(searchText)
            || p.Recipient.ToString().ToLower().Contains(searchText)).
            ToList();

            if (CheckActual.IsChecked == true)
                listPackages = listPackages
                    .Where(p => p.isActive.Equals(true))
                    .ToList();

            if (SortType.SelectedIndex == 0)
                listPackages = listPackages
                    .OrderBy(p => p.Id)
                    .ToList();

            if (SortType.SelectedIndex == 1)
                listPackages = listPackages
                    .OrderByDescending(p => p.Id)
                    .ToList();

            uc_packagesList.ViewPackages.ItemsSource = listPackages;

                if (listPackages.Count == 0)
                tBoxNoResult.Visibility = Visibility.Visible;


        }

        private void SortType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetItemSource(workplaceId);
        }

        private void CheckActual_Unchecked(object sender, RoutedEventArgs e)
        {
            SetItemSource(workplaceId);
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            SetItemSource(workplaceId);
        }

        private void tBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetItemSource(workplaceId);
        }

        private void uc_packagesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int packageId = (uc_packagesList.ViewPackages.SelectedItem as PackageGet).Id;
            PackageStatusHistoryWindow packageStatusHistoryWindow = new PackageStatusHistoryWindow(packageId);
            packageStatusHistoryWindow.ShowDialog();
        }
    }
}
