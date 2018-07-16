using System;
using System.Globalization;
using Xamarin.Forms;

namespace MvpCompanion.Portable.Converters
{
    internal class PathToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return ImageSource.FromUri(new Uri((string) value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
