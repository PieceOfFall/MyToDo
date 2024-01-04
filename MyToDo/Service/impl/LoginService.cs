using MyToDo.Common.Models;

namespace MyToDo.Service.impl
{
    internal class LoginService(HttpRestClient client) : ILoginService
    {
        private readonly HttpRestClient client = client;

        private readonly string ServiceName = "user";

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

        public async Task<ApiResponse<DepartmentDto>> GetDeptTreeAsync()
        {
            var request = new BaseRequest()
            {
                Route = $"{ServiceName}/simple_dept_tree",
                Method = RestSharp.Method.Get
            };
            return await client.ExecuteAsync<DepartmentDto>(request);
        }

        public async Task<ApiResponse<List<string>>> GetDeptEmployeesAsync(int deptId)
        {
            var request = new BaseRequest()
            {
                Route = $"{ServiceName}/dept_employees",
                Method = RestSharp.Method.Get,
                Parameter = new { deptId }
            };
            return await client.ExecuteAsync<List<string>>(request);
        }
    }
}
