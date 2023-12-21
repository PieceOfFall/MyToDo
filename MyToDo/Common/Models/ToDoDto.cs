

using Microsoft.VisualBasic;

namespace MyToDo.Common.Models
{
	// 待办实体
    public class ToDoDto : BaseDto
    {
        private string title;
        private string content;
        private int status;
        private int? receiverId;
        private int? urgency;
		private DateTime dueDate;

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

        // 接收者id
        public int? ReceiverId
        {
            get => receiverId ?? 0;
            set => receiverId = value;
        }

        // 紧急程度
        public int? Urgency
        {
            get => urgency ?? 3;
            set => urgency = value;
        }

        // 过期时间
        public DateTime DueDate
        {
            get => dueDate;
            set => dueDate = value;
        }
    }
}
