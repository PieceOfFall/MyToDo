﻿using MyToDo.Common.Models;
using MyToDo.Common.Models.db;


namespace MyToDo.Service
{
    public interface IToDoService : IBaseService<ToDoDto>
    {

        Task<ApiResponse<Summary>> SummaryAsync();

        Task<ApiResponse<SelectedToDo>> SelectToDo(int id);

        Task<ApiResponse<int?>> FindIdByUserName(string receiverName);
    }
}
