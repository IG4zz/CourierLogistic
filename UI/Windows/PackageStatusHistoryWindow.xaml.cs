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
using static CourierLogistic.APIHelper.PackageStatusHistoryHelper;

namespace CourierLogistic.UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для PackageStatusHistoryWindow.xaml
    /// </summary>
    public partial class PackageStatusHistoryWindow : Window
    {
        public PackageStatusHistoryWindow(int packageId)
        {
            InitializeComponent();
            DGridPackageStatusHistory.ItemsSource = PackageStatusHistoryGet.GetPackageHistoryById(packageId);
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
