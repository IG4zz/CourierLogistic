using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System;
using static CourierLogistic.APIHelper.APIHelper;

namespace CourierLogistic.APIHelper
{
    internal class ShippingPackageHelper
    {
        public class ShippingPackageGet
        {
            public int Id { get; set; }
            public int ShippingId { get; set; }
            public int PackageId { get; set; }
            public bool isDelivered { get; set; }
            public bool isReady { get; set; }

            public string isDeliveredText
            {
                get
                {
                    return (isDelivered) ? "Доставлена" : "Не доставлена";
                }
            }

            public string isReadyText
            {
                get
                {
                    return (isReady) ? "Готова к отправке" : "Не готова к отправке";
                }
            }

            public static List<ShippingPackageGet> GetShippingPackages()
            {
                client.Encoding = Encoding.UTF8;

                try
                {
                    List<ShippingPackageGet> shippingPackages = new List<ShippingPackageGet>();
                    var response = client.DownloadString(baseUrl + $"ShippingPackages");
                    shippingPackages = JsonConvert.DeserializeObject<List<ShippingPackageGet>>(response);
                    return shippingPackages;
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }

            }

            public static List<ShippingPackageGet> GetShippingPackagesByShipping(int shippingId)
            {
                client.Encoding = Encoding.UTF8;

                try
                {
                    List<ShippingPackageGet> shippingPackages = new List<ShippingPackageGet>();
                    var response = client.DownloadString(baseUrl + $"ShippingPackagesByShipping?shippingId={shippingId}");
                    shippingPackages = JsonConvert.DeserializeObject<List<ShippingPackageGet>>(response);
                    return shippingPackages;
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }

            }

            public static List<ShippingPackageGet> GetShippingPackageByShipping(int packageId)
            {
                client.Encoding = Encoding.UTF8;

                try
                {
                    List<ShippingPackageGet> shippingPackages = new List<ShippingPackageGet>();
                    var response = client.DownloadString(baseUrl + $"ShippingPackages/{packageId}");
                    shippingPackages = JsonConvert.DeserializeObject<List<ShippingPackageGet>>(response);
                    return shippingPackages;
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }

            }

        }

        public class ShippingPackagePost
        {
            public int Id { get; set; }
            public int ShippingId { get; set; }
            public int PackageId { get; set; }
            public bool isDelivered { get; set; }
            public bool isReady { get; set; }

            public static bool PostShippingPackage(ShippingPackagePost shippingPackage)
            {
                client.Encoding = Encoding.UTF8;
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    client.UploadString(baseUrl + "ShippingPackages", JsonConvert.SerializeObject(shippingPackage));
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
               

                var result = ShippingPackageGet.GetShippingPackageByShipping(shippingPackage.PackageId);

                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static void DeleteShippingPackage(int Id)
            {
                if (MessageBox.Show("Вы действительно хотите удалить выбранную запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    try
                    {
                        client.UploadString(baseUrl + $"ShippingPackages/{Id}", "DELETE", "");
                        MessageBox.Show("Данные удалены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при удалении", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        }
    }
}
