﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MyToDo.Common.Converters
{
    [ValueConversion(typeof(Color), typeof(Brush))]
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && int.TryParse(value.ToString(), out int result))
            {
                if (result == 0)
                    return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null&& bool.TryParse(value.ToString(),out bool result))
            {
                if (result)
                    return 1;
            }
            return 0;
        }
    }
}
