using System;
using System.Globalization;
using MvpApi.Common.Models;
using MvpApi.Common.ServiceModels;
using Xamarin.Forms;

namespace MvpCompanion.Portable.Converters
{
    internal class UploadStatusBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UploadStatus status)
            {
                switch (status)
                {
                    case UploadStatus.Pending:
                        return Color.LightGray;
                    case UploadStatus.InProgress:
                        return Color.Gold;
                    case UploadStatus.Success:
                        return Color.LightGreen;
                    case UploadStatus.Failed:
                        return Color.FromRgba(0xF6,0x37,0x37, 0xFF);
                    case UploadStatus.None:
                        return Color.Transparent;
                }
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
