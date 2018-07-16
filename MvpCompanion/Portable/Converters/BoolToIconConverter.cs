using System;
using System.Globalization;
using Xamarin.Forms;

namespace MvpCompanion.Portable.Converters
{
    internal class BoolToIconConverter : IValueConverter
    {
        public ImageSource TrueIcon { get; set; }
        public ImageSource FalseIcon { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val)
            {
                return val ? TrueIcon : FalseIcon;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ImageSource icon)
            {
                return icon == TrueIcon;
            }

            return null;
        }
    }
}