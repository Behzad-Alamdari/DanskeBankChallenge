using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DBC.WpfClient.Converters
{
    public class NegateBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as bool?;
            if (input == null)
                return Visibility.Visible;

            return input.Value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = value as Visibility?;
            if (visibility == null)
                return null;

            return visibility == Visibility.Visible ? false : true;
        }
    }
}
