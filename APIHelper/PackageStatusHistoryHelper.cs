using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static CourierLogistic.APIHelper.APIHelper;
using System.Windows;
using static CourierLogistic.APIHelper.PackageHelper;

namespace CourierLogistic.APIHelper
{
    internal class PackageStatusHistoryHelper
    {
        public class PackageStatusHistoryGet
        {
            public int Id { get; set; }
            public int PackageId { get; set; }
            public int StatusId { get; set; }
            public string StatusName { get; set; }
            public System.DateTime DateTime { get; set; }

            public static List<PackageStatusHistoryGet> GetPackageHistoryById(int packageId)
            {
                client.Encoding = Encoding.UTF8;

                try
                {
                    List<PackageStatusHistoryGet> packageStatusHistoryList = new List<PackageStatusHistoryGet>();
                    var response = client.DownloadString(baseUrl + $"PackageStatusHistories?packageId={packageId}");
                    packageStatusHistoryList = JsonConvert.DeserializeObject<List<PackageStatusHistoryGet>>(response);
                    return packageStatusHistoryList;
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }

            }
        }               
    }
}
