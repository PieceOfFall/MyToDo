using RestSharp;

namespace MyToDo.Service
{
    public class BaseRequest
    {
        public const string JSON = "application/json";
        public Method Method { get; set; }
        public string Route { get; set; }

        public string ContentType { get; set; }

        private object parameter;

        public object Parameter
        {
            get { return parameter; }
            set 
            {
                var queryString = string.Join("&", value
                                        .GetType()
                                        .GetProperties()
                                        .Where(property => property.GetValue(value) != null)
                                        .Select(property => $"{property.Name}={Uri.EscapeDataString(property.GetValue(value)?.ToString() ?? "")}")
                                        .ToArray());

                parameter = queryString; 
            }
        }

    }
}
