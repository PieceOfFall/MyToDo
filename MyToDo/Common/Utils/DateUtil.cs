

namespace MyToDo.Common.Utils
{
    public class DateUtil
    {
        public static string DateToStr(DateTime dateTime) 
            => $"{dateTime.Year}.{dateTime.Month}.{dateTime.Day}";

    }
}
