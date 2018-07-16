using System;
using System.Globalization;
using Xamarin.Forms;

namespace MvpCompanion.Portable.Converters
{
    /// <summary>
    /// Value Converter that can be used for empty strings as well as objects
    /// </summary>
    internal class NullToVisibilityConverter : IValueConverter
    {
        public bool IsInverted { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                if(IsInverted)
                    return string.IsNullOrEmpty(s);

                return !string.IsNullOrEmpty(s);
            }
            else
            {
                if(IsInverted)
                    return value == null;

                return value != null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
