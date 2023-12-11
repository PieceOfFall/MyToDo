using MyToDo.Common.Models;

namespace MyToDo.Service.impl
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        public ToDoService(HttpRestClient client) : base(client, "todos")
        {
        }
    }
}
