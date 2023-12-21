using System.Globalization;
using System.Windows.Data;

namespace MyToDo.Common.Converters
{

        public class BoolToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value != null && bool.TryParse(value.ToString(), out bool result))
                {
                    if (result)
                        return System.Windows.Visibility.Visible;
                }
                return System.Windows.Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value != null && Enum.TryParse(value.ToString(), out System.Windows.Visibility result))
                {
                    if (result == System.Windows.Visibility.Visible)
                        return true;
                }
                return false;
            }
        }

}
