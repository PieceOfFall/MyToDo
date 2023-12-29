using System.Globalization;
using System.Windows.Data;

namespace MyToDo.Common.Converters
{
    class UrgencyToUrgencyStrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                0 => "不紧急不重要",
                1 => "紧急不重要",
                2 => "不紧急重要",
                3 => "紧急重要",
                _ => "未知优先级"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
