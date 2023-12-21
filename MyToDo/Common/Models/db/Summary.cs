using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models.db
{
    public class Summary
    {
        public int Sum { get; set; }

        public int CompletedCount { get; set; }

        public int MemoCount { get; set; }

        public string CompletedRatio { get; set; }
    }
}
