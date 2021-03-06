﻿using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace MvpApi.Uwp.Converters
{
    internal class BoolToIconConverter : IValueConverter
    {
        public IconElement TrueIcon { get; set; }
        public IconElement FalseIcon { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool val)
            {
                return val ? TrueIcon : FalseIcon;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is IconElement icon)
            {
                return icon == TrueIcon;
            }

            return null;
        }
    }
}
