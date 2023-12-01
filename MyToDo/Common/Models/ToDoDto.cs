using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
	// 待办实体
    public class ToDoDto : BaseDto
    {
        private string title;
        private string content;
        private int status;

		// 标题
		public string Title
		{
			get => title;
			set => title = value;
		}

		// 内容
		public string Content
		{
			get => content;
			set => content = value;
		}

		// 状态
		public int Status
		{
			get => status;
			set => status = value;
		}

	}
}
