
namespace MyToDo.Common.Models
{
    public class BaseDto
    {
		private int id;

		public int Id
		{
			get => id; 
			set => id = value; 
		}

		private DateTime createDate;

		public DateTime CreateDate
		{
			get => createDate;
			set => createDate = value;
        }

		private DateTime updateDate;

		public DateTime UpdateDate
		{
			get => updateDate;
            set => updateDate = value; 
		}

	}
}
