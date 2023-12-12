using MyToDo.Common.Models;
using MyToDo.Common.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Parameter = entity
            };
            return await client.ExecuteAsync<int>(request);
        }

        public async Task<ApiResponse<int>> DeleteAsync(int id)
        {
            var request = new BaseRequest
            {
                Method = RestSharp.Method.Delete,
                Route = $"{serviceName}/delete?id={id}"
            };
            return await client.ExecuteAsync<int>(request);
        }

        public async Task<ApiResponse<PageList<TEntity>>> GetAllasync(PageOptions options)
        {
            var request = new BaseRequest
            {
                Method = RestSharp.Method.Get,
                Route = $"{serviceName}/get/all?pageNum={options.pageNum}&pageSize={options.pageSize}"
            };
            return await client.ExecuteAsync<PageList<TEntity>>(request);
        }

        public async Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            var request = new BaseRequest
            {
                Method = RestSharp.Method.Get,
                Route = $"{serviceName}/get?id={id}"
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
                Parameter = entity,
                ContentType = "application/x-www-form-urlencoded"
            };
            return await client.ExecuteAsync<int>(request);
        }
    }
}
