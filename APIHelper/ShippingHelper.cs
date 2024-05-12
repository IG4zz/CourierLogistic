using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using static CourierLogistic.APIHelper.APIHelper;

namespace CourierLogistic.APIHelper
{
    internal class ShippingHelper
    {
        public class ShippingGet
        {
            public int Id { get; set; }
            public int CreatedBy { get; set; }
            public int CreatedFor { get; set; }
            public int? AcceptedBy { get; set; }
            public Nullable<int> CountPackages { get; set; }
            public DateTime? DateTimeStart { get; set; }
            public DateTime? DateTimeEnd { get; set; }
            public bool isActive { get; set; }
            public string isActiveText
            {
                get
                {
                    return (isActive) ? "Активен" : "Завершён";
                }
            }

            public static List<ShippingGet> GetShippings(int userId)
            {
                client.Encoding = Encoding.UTF8;

                try
                {
                    List<ShippingGet> shippings = new List<ShippingGet>();
                    var response = client.DownloadString(baseUrl + $"ShippingsByUser?userId={userId}");
                    shippings = JsonConvert.DeserializeObject<List<ShippingGet>>(response);
                    return shippings;
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;

                }

            }

            public static List<ShippingGet> GetShipping(int Id)
            {
                client.Encoding = Encoding.UTF8;

                try
                {
                    List<ShippingGet> shippings = new List<ShippingGet>();
                    var response = client.DownloadString(baseUrl + $"Shippings/{Id}");
                    shippings = JsonConvert.DeserializeObject<List<ShippingGet>>(response);
                    return shippings;
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;

                }
                               
            }

        }

        public class ShippingPost
        {
            public int Id { get; set; }
            public int CreatedBy { get; set; }
            public int? AcceptedBy { get; set; }
            public DateTime? DateTimeStart { get; set; }
            public DateTime? DateTimeEnd { get; set; }
            public bool isActive { get; set; }

            public static bool PostShipping(ShippingPost shipping)
            {
                client.Encoding = Encoding.UTF8;
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.UploadString(baseUrl + "Shippings", JsonConvert.SerializeObject(shipping));

                var result = ShippingGet.GetShipping(shipping.Id);
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
