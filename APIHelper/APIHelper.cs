using System.Net;

namespace CourierLogistic.APIHelper
{
    public class APIHelper
    {
        public static readonly WebClient client = new WebClient();
        public static readonly string baseUrl = "http://localhost:51567/api/";
    }
}
