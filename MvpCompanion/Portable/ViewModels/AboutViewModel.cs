using System;
using System.Reflection;
using System.Threading.Tasks;
using MvpCompanion.Portable.Common;

namespace MvpCompanion.Portable.ViewModels
{
    public class AboutViewModel : PageViewModelBase
    {
        private readonly Assembly portableAssembly;
        private bool feedbackHubButtonVisibility;
        private int daysToKeepErrorLogs = 5;

        public AboutViewModel()
        {
            portableAssembly = typeof(AboutViewModel).GetTypeInfo().Assembly;
        }

        public string AppVersion => portableAssembly?.FullName.Split(',')[1]?.Split('=')[1];

        public int DaysToKeepErrorLogs
        {
            get
            {
                //if (roamingSettings.Values.TryGetValue("DaysToKeepErrorLogs", out object rawValue))
                //{
                //    daysToKeepErrorLogs = Convert.ToInt32(rawValue);
                //}
                //else
                //{
                //    roamingSettings.Values["DaysToKeepErrorLogs"] = daysToKeepErrorLogs;
                //}

                return daysToKeepErrorLogs;
            }
            set
            {
                SetProperty(ref daysToKeepErrorLogs, value);

                //roamingSettings.Values["DaysToKeepErrorLogs"] = value;
            }
        }
        
        public bool FeedbackHubButtonVisibility
        {
            get => feedbackHubButtonVisibility;
            set => SetProperty(ref feedbackHubButtonVisibility, value);
        }

        public async void EmailButton_Click(object sender, EventArgs e)
        {
            await CreateEmailAsync();
        }

        public async void FeedbackButton_Click(object sender, EventArgs e)
        {
            //await StoreServicesFeedbackLauncher.GetDefault().LaunchAsync();
        }
        
        private async Task CreateEmailAsync()
        {
            try
            {
                IsBusy = true;
                IsBusyMessage = "opening email...";

                //var email = new EmailMessage();
                //email.To.Add(new EmailRecipient("awesome.apps@outlook.com", "Lancelot Software"));
                //email.Subject = $"MVP Companion {AppVersion}";
                //email.Body = "[write your message here]\r\n\n";

                //await EmailManager.ShowComposeNewEmailAsync(email);
            }
            catch (Exception ex)
            {
                await new MessageDialog($"Something went wrong trying to open your email application automatically. You can still manually send an email to awesome.apps@outlook.com. /r/n/nError: {ex.Message}")
                    .ShowAsync();
            }
            finally
            {
                IsBusy = false;
                IsBusyMessage = "";
            }
        }
        
        #region Navigation

        //public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        //{
        //    FeedbackHubButtonVisibility = StoreServicesFeedbackLauncher.IsSupported() 
        //        ? Visibility.Visible 
        //        : Visibility.Collapsed;
            
        //    return base.OnNavigatedToAsync(parameter, mode, state);
        //}

        //public override Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        //{
        //    return base.OnNavigatedFromAsync(pageState, suspending);
        //}

        #endregion
    }
}