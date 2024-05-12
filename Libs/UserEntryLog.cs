using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using static CourierLogistic.APIHelper.APIHelper;

namespace CourierLogistic.Libs
{
    public class UserEntryLog
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public DateTime DateTime { get; set; }

        public static bool LogUserEntry(int userId)
        {
            UserEntryLog userEntryLog = new UserEntryLog
            {
                IdUser = userId,
                DateTime = DateTime.Now

            };

            client.Encoding = Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.UploadString(baseUrl + "EntryHistories", JsonConvert.SerializeObject(userEntryLog));

            return true;

        }
    }
}
