using System.Globalization;
using System.Windows.Data;

namespace MyToDo.Common.Converters
{
        public class ReverseBoolToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value != null && bool.TryParse(value.ToString(), out bool result))
                {
                    if (result)
                        return System.Windows.Visibility.Collapsed;
                }
                return System.Windows.Visibility.Visible;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value != null && Enum.TryParse(value.ToString(), out System.Windows.Visibility result))
                {
                    if (result == System.Windows.Visibility.Collapsed)
                        return true;
                }
                return false;
            }
        }

}
