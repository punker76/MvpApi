using System;
using System.Globalization;
using Xamarin.Forms;

namespace MvpCompanion.Portable.Converters
{
    internal class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;

            try
            {
                return new DateTimeOffset(date);
            }
            catch
            {
                return DateTimeOffset.MinValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dto = (DateTimeOffset)value;

            try
            {
                return dto.DateTime;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
