

using Microsoft.VisualBasic;

namespace MyToDo.Common.Models
{
	// 待办实体
    public class ToDoDto : BaseDto
    {
        private string title = string.Empty;
        private string content = string.Empty;
        private int status;
        private int? receiverId;
        private int? urgency;
		private DateTime dueDate;
        private string dueDateStr = string.Empty;
        private string receiverName = string.Empty;


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
            set
            {
                dueDate = value;
                DueDateStr = Utils.DateUtil.DateToStr(value);
            }
        }

        public string DueDateStr
        {
            get => dueDateStr;
            set => dueDateStr = value;
        }

        public string ReceiverName
        {
            get { return receiverName; }
            set { receiverName = value; }
        }
    }
}
