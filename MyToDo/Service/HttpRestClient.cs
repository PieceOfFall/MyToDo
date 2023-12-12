using MyToDo.Common.Models;
using Newtonsoft.Json;
using RestSharp;

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
            var request = new RestRequest(apiUrl + baseRequest.Route,method:baseRequest.Method);

            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            
            var response = await client.ExecuteAsync(request);

            ApiResponse<T>? apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            return apiResponse;
        }
    }
}
