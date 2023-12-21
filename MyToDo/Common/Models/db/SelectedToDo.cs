

namespace MyToDo.Common.Models.db
{
    public class SelectedToDo : ToDoDto
    {
		private string selectedReceiver;

		public string SelectedReceiver
        {
			get { return selectedReceiver; }
			set { selectedReceiver = value; }
		}

	}
}
