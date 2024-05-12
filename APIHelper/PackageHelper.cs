using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using static CourierLogistic.APIHelper.APIHelper;

namespace CourierLogistic.APIHelper
{
    internal class PackageHelper
    {
        public class PackageGet
        {
            public int Id { get; set; }
            public string Sender { get; set; }
            public string Recipient { get; set; }
            public string RecipientInfo { get; set; }
            public string SentTo { get; set; }
            public string StorageUnit { get; set; }
            public string PackageTypeName { get; set; }
            public int PackageType { get; set; }
            public int WorkplaceId { get; set; }
            public int Status { get; set; }
            public string StatusName { get; set; }
            public bool isActive { get; set; }
            public string isActiveText
            {
                get
                {
                    return (isActive) ? "Активен" : "Не активен";
                }
            }


            public static List<PackageGet> GetPackages()
            {
                client.Encoding = Encoding.UTF8;

                try
                {
                    List<PackageGet> packages = new List<PackageGet>();
                    var response = client.DownloadString(baseUrl + "packages");
                    packages = JsonConvert.DeserializeObject<List<PackageGet>>(response);
                    return packages;
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }

            }

            public static List<PackageGet> GetPackage(int Id)
            {
                client.Encoding = Encoding.UTF8;

                try
                {
                    List<PackageGet> packages = new List<PackageGet>();
                    var response = client.DownloadString(baseUrl + $"packages/{Id}");
                    packages = JsonConvert.DeserializeObject<List<PackageGet>>(response);
                    return packages;
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;

                }
            }

        }

        public class PackagePost
        {
            public int Id { get; set; }
            public string Sender { get; set; }
            public string Recipient { get; set; }
            public string RecipientInfo { get; set; }
            public string SentTo { get; set; }
            public string StorageUnit { get; set; }
            public int PackageType { get; set; }
            public int WorkplaceId { get; set; }
            public int Status { get; set; }
            public bool isActive { get; set; }

            public static bool PostPackage(PackagePost package)
            {
                client.Encoding = Encoding.UTF8;
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.UploadString(baseUrl + "packages", JsonConvert.SerializeObject(package));

                var result = PackageGet.GetPackage(package.Id);

                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            public static bool PutPackage(PackagePost package)
            {
                client.Encoding = Encoding.UTF8;
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.UploadString(baseUrl + $"packages/{package.Id}", "PUT", JsonConvert.SerializeObject(package));

                var result = PackageGet.GetPackage(package.Id);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
