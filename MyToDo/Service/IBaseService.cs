using MyToDo.Common.Models;
using MyToDo.Common.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<ApiResponse<int>> AddAsync(TEntity entity);

        Task<ApiResponse<int>> UpdateAsync(TEntity entity);

        Task<ApiResponse<int>> DeleteAsync(int id);

        Task<ApiResponse<PageList<TEntity>>> QueryAsync(PageOptions options);

        Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id);

        Task<ApiResponse<TEntity>> GetLastestAsync(int id);

        Task<ApiResponse<PageList<TEntity>>> GetAllasync(PageOptions options);

    }
}
