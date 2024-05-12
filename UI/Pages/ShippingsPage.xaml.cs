using CourierLogistic.UI.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static CourierLogistic.APIHelper.ShippingHelper;

namespace CourierLogistic.UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShippingsPage.xaml
    /// </summary>
    public partial class ShippingsPage : Page
    {
        private int userId;
        public ShippingsPage(int userId)
        {
            InitializeComponent();

            SortType.Items.Add("По возрастанию номера заказа");
            SortType.Items.Add("По убыванию номера заказа");

            tBlock_PageName.Text = $"Список заказов, созданных вами";

            this.userId = userId;

            SetItemSource(userId);
        }

        public void SetItemSource(int userId)
        {
            var listShippings = ShippingGet.GetShippings(userId);

            string searchText = tBoxSearch.Text;

            listShippings = listShippings.Where(p => p.Id.ToString().Contains(searchText)).ToList();

            if (CheckActual.IsChecked == true)
                listShippings = listShippings
                    .Where(p => p.isActive.Equals(true))
                    .ToList();

            if (SortType.SelectedIndex == 0)
                listShippings = listShippings
                    .OrderBy(p => p.Id)
                    .ToList();

            if (SortType.SelectedIndex == 1)
                listShippings = listShippings
                    .OrderByDescending(p => p.Id)
                    .ToList();

            uc_ShippingsList.ViewShippings.ItemsSource = listShippings;

        }      

        private void ShippingsInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var data = uc_ShippingsList.ViewShippings.SelectedItem as ShippingGet;
            if (data == null)
                return;
            ShippingPackagesWindow shippingPackagesWindow = new ShippingPackagesWindow(data.Id);
            shippingPackagesWindow.ShowDialog();
        }

        private void CheckActual_Unchecked(object sender, RoutedEventArgs e)
        {
            SetItemSource(userId);
        }

        private void SortType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetItemSource(userId);
        }

        private void tBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetItemSource(userId);
        }

        private void btnCreateShipping_Click(object sender, RoutedEventArgs e)
        {
            ShippingPost shipping = new ShippingPost
            {
                CreatedBy = userId,
                DateTimeStart = DateTime.Now,
                isActive = true
            };

            ShippingPost.PostShipping(shipping);
            SetItemSource(userId);
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            SetItemSource(userId);
        }
    }
}
