using MyToDo.Common.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var request = new RestRequest(apiUrl+baseRequest.Route,method:baseRequest.Method);

            //request.AddHeader("Content-Type",baseRequest.ContentType);

            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            ApiResponse<T>? apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            return apiResponse;      

        }
    }
}
