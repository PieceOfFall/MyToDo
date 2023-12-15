using MyToDo.Common.Models;

namespace MyToDo.Service.impl
{
    internal class LoginService : ILoginService
    {
        private readonly HttpRestClient client;

        private readonly string ServiceName = "user";

        public LoginService(HttpRestClient client) 
        {
            this.client = client;
        }
        public async Task<ApiResponse<string>> LoginAsync(UserDto user)
        {
            var request = new BaseRequest()
            {
                Route = $"{ServiceName}/login",
                Method = RestSharp.Method.Put,
                Body = user
            };
            return await client.ExecuteAsync<string>(request);
        }
    }
}
