
namespace MyToDo.Common.Models.db
{
    public class ToDo
    {
        public ToDo(ToDoDto dto)
        {
            id = dto.Id;
            title = dto.Title;
            content = dto.Content;
            status = dto.Status;
            createDate = dto.CreateDate;
            updateDate = dto.UpdateDate;
        }

        public int id { get; set; }

        public string title {  get; set; }

        public string content { get; set; }

        public int status { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

    }
}
