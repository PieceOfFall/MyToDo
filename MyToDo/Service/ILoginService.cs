using MyToDo.Common.Models;

namespace MyToDo.Service
{
    public interface ILoginService
    {
        Task<ApiResponse<string>> LoginAsync(UserDto user);

        Task<ApiResponse<DepartmentDto>> GetDeptTreeAsync();

        Task<ApiResponse<List<string>>> GetDeptEmployeesAsync(int deptId);
    }
}
