using ImTools;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Events;
using Prism.Ioc;
using RestSharp;
using System.Text.Json;

namespace MyToDo.Service
{
    public class HttpRestClient
    {

        protected readonly RestClient client;
        private readonly string apiUrl;

        private static IEventAggregator aggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            client = new RestClient(apiUrl);
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            try
            {
                if (baseRequest.Parameter != null)
                    baseRequest.Route += baseRequest.Parameter;

                var request = new RestRequest(apiUrl + baseRequest.Route, method: baseRequest.Method);

                if (!baseRequest.Route.Contains("user"))
                {
                    request.AddHeader("Authorization", AppSession.Token);
                }

                if (baseRequest.Body != null)
                {
                    var jsonBody = JsonSerializer.Serialize(baseRequest.Body);
                    request.AddJsonBody(jsonBody);
                }

                var response = await client.ExecuteAsync(request);

                if(response.Content != null)
                {
                    ApiResponse<T> apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(response.Content)!;

                    if (response.StatusCode != System.Net.HttpStatusCode.OK && !baseRequest.Route.Contains("user") && apiResponse?.status != null)
                    {
                        aggregator.SendMessage($"{apiResponse.status} : {apiResponse.msg}");
                    }
                    aggregator.SendToast(new Common.Events.ToastModel()
                    {
                        Title="ToDo",
                        Message=response.Content
                    });
                    return apiResponse;
                }
                return new ApiResponse<T>()
                {
                    data = default,
                    status = 400,
                    msg = "Error Request"
                };
            }
            catch (Exception ex)
            {
                aggregator.SendMessage(ex.Message.ToString());
                return null;
            }
        }
    }
}
