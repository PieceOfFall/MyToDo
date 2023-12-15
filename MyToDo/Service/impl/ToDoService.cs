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
    }
}
