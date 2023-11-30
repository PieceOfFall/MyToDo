using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
	// 任务栏
    public class TaskBar : BindableBase
    {
		private string icon;
        private string title;
        private string content;
        private string color;
        private string target;

		// 图标
        public string Icon
		{
			get { return icon; }
			set { icon = value; }
		}

		// 标题
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		// 内容
		public string Content
		{
			get { return content; }
			set { content = value; }
		}

		// 颜色
		public string Color
		{
			get { return color; }
			set { color = value; }
		}

		// 触发目标
		public string Target
		{
			get { return target; }
			set { target = value; }
		}

	}
}
