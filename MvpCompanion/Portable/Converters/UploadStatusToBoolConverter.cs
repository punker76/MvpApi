using System;
using System.Globalization;
using MvpApi.Common.Models;
using MvpApi.Common.ServiceModels;
using Xamarin.Forms;

namespace MvpCompanion.Portable.Converters
{
    public class UploadStatusToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UploadStatus status)
            {
                return status == UploadStatus.Failed;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val && val == true)
            {
                return UploadStatus.Failed;
            }

            return default(UploadStatus);
        }
    }
}
