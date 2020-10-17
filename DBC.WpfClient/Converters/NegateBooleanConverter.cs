using System;
using System.Globalization;
using System.Windows.Data;

namespace DBC.WpfClient.Converters
{
    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as bool?;
            if (input == null)
                return null;

            return !input.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as bool?;
            if (input == null)
                return null;

            return !input.Value;
        }
    }
}
