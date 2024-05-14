using System.Net;
using System.Text;

namespace CourierLogistic.APIHelper
{
    public class APIHelper
    {
        public static readonly WebClient client = new WebClient { Encoding = Encoding.UTF8 };
        public static readonly string baseUrl = "http://localhost:51567/api/";
    }
}
