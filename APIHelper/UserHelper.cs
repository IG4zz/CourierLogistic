using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using static CourierLogistic.APIHelper.APIHelper;

namespace CourierLogistic.APIHelper
{
    public class UserHelper
    {
        public class UserGet
        {
            public int Id { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public int RoleId { get; set; }
            public int WorkplaceId { get; set; }


            public static List<UserGet> GetUsers()
            {
                client.Encoding = Encoding.UTF8;

                try
                {
                    List<UserGet> user = new List<UserGet>();
                    var response = client.DownloadString(baseUrl + $"Users");
                    user = JsonConvert.DeserializeObject<List<UserGet>>(response);

                    return user;

                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не отвечатет \nПопробуйте позже", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(0);
                    return null;


                }


            }

        }


    }
}
