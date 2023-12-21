using MyToDo.Common.Models;
using MyToDo.Common.Models.db;

namespace MyToDo.Service.impl
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient client;

            public ToDoService(HttpRestClient client) : base(client, "todos")
        {
            this.client = client;
        }

        public async Task<ApiResponse<Summary>> SummaryAsync()
        {
            var request = new BaseRequest()
            {
                Route = "todos/summary",
                Method = RestSharp.Method.Get,
            };

            return await client.ExecuteAsync<Summary>(request);
        }

        public async Task<ApiResponse<SelectedToDo>> SelectToDo(int id)
        {
            var request = new BaseRequest()
            {
                Method = RestSharp.Method.Get,
                Route = "todos/get",
                Parameter = new { id }
            };
            return await client.ExecuteAsync<SelectedToDo>(request);
        }

        public async Task<ApiResponse<int?>> FindIdByUserName(string receiver)
        {
            var request = new BaseRequest()
            {
                Method = RestSharp.Method.Get,
                Route = "todos/receiver/get",
                Parameter = new { receiver }
            };
            return await client.ExecuteAsync<int?>(request);
        }
    }
}
