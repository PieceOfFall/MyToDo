using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToDo.Common.Models.db;

namespace MyToDo.Common.Models
{
    public class QueryToDo : PageOptions
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int? Status { get; set; }
    }
}
