using RestSharp;

namespace MyToDo.Service
{
    public class BaseRequest
    {
        public const string JSON = "application/json";
        public Method Method { get; set; }
        public string Route { get; set; }

        public string ContentType { get; set; }

        public object Parameter { get; set; }
    }
}
