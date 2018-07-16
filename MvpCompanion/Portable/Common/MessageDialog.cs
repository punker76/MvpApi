using System.Threading.Tasks;
using Xamarin.Forms;

namespace MvpCompanion.Portable.Common
{
    /// <summary>
    /// This is a substitute of UWP's MessageDialog
    /// </summary>
    public class MessageDialog
    {
        public MessageDialog()
        {
        }

        public MessageDialog(string title)
        {
            this.Title = title;
        }

        public MessageDialog(string message, string title)
        {
            this.Message = message;
            this.Title = title;
        }

        public string Title { get; set; }

        public string Message { get; set; }
        
        public async Task ShowAsync()
        {
            // if both fields are populated
            if (!string.IsNullOrEmpty(Message) && !string.IsNullOrEmpty(Message))
            {
                await Application.Current.MainPage.DisplayAlert(this.Title, this.Message, "close");
            }
            // if only message is populated
            else if(!string.IsNullOrEmpty(Message))
            {
                await Application.Current.MainPage.DisplayAlert(this.Title, this.Message, "ok", "cancel");
            }
        }
    }
}
