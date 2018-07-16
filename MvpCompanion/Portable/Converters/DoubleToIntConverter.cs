using System;
using System.Globalization;
using Xamarin.Forms;

namespace MvpCompanion.Portable.Converters
{
    internal class DoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double source)
            {
                return System.Convert.ToInt32(source);
            }
            else
            {
                return value;
            }
        }
    }
}
