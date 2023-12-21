using MyToDo.Common.Models.db;

namespace MyToDo.Common.Models
{
    public class QueryToDo : PageOptions
    {
        public int SenderId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public int? Status { get; set; }
    }
}
