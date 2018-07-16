using MvpApi.Common.Models;

namespace MvpCompanion.Portable.ViewModels
{
    public class PageViewModelBase : BindableBase
    {
        private bool isBusy;
        private string isBusyMessage;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string IsBusyMessage
        {
            get => isBusyMessage;
            set => SetProperty(ref isBusyMessage, value);
        }
    }
}