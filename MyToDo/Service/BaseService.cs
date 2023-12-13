using MyToDo.Common.Models;
using MyToDo.Common.Models.db;

namespace MyToDo.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class 
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;

        public BaseService(HttpRestClient client,string serviceName) 
        {
            this.client = client;
            this.serviceName = serviceName;
        }

        public async Task<ApiResponse<int>> AddAsync(TEntity entity)
        {
            var request = new BaseRequest
            {
                Method = RestSharp.Method.Post,
                Route = $"{serviceName}/add",
                Body = entity
            };
            return await client.ExecuteAsync<int>(request);
        }

        public async Task<ApiResponse<int>> DeleteAsync(int id)
        {
            var request = new BaseRequest
            {
                Method = RestSharp.Method.Delete,
                Route = $"{serviceName}/delete",
                Parameter = new { id }
            };
            return await client.ExecuteAsync<int>(request);
        }

        public async Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            var request = new BaseRequest
            {
                Method = RestSharp.Method.Get,
                Route = $"{serviceName}/get",
                Parameter = new { id }
            };
            return await client.ExecuteAsync<TEntity>(request);
        }

        public Task<ApiResponse<TEntity>> GetLastestAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<PageList<TEntity>>> QueryAsync(PageOptions queryPageOptions)
        {
            var request = new BaseRequest
            {
                Method = RestSharp.Method.Get,
                Route = $"{serviceName}/query",
                Parameter = queryPageOptions
            };
            return await client.ExecuteAsync<PageList<TEntity>>(request);
        }

        public async Task<ApiResponse<int>> UpdateAsync(TEntity entity)
        {
            var request = new BaseRequest
            {
                Method = RestSharp.Method.Put,
                Route = $"{serviceName}/update",
                Body = entity
            };
            return await client.ExecuteAsync<int>(request);
        }
    }
}
