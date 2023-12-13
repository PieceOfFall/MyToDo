using ImTools;
using MyToDo.Common.Models;
using RestSharp;
using System.Text.Json;

namespace MyToDo.Service
{
    public class HttpRestClient
    {

        protected readonly RestClient client;
        private string apiUrl;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            client = new RestClient(apiUrl);
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            if (baseRequest.Parameter != null)
                baseRequest.Route += baseRequest.Parameter;

            var request = new RestRequest(apiUrl + baseRequest.Route, method: baseRequest.Method);

            if(baseRequest.Body != null)
            {
                var jsonBody = JsonSerializer.Serialize(baseRequest.Body);
                request.AddJsonBody(jsonBody);
            }

            var response = await client.ExecuteAsync(request);

            ApiResponse<T>? apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            return apiResponse;
        }
    }
}
