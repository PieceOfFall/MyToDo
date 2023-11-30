using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 系统导航菜单实体类
namespace MyToDo.Common.Models
{
    public class MenuBar : BindableBase
    {
		// 菜单图标
		private string icon;

		public string Icon
		{
			get { return icon; }
			set { icon = value; }
		}

		// 菜单名称
		private string title;

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		// 菜单命名空间
		private string nameSpace;

		public string NameSpace
		{
			get { return nameSpace; }
			set { nameSpace = value; }
		}

	}
}
