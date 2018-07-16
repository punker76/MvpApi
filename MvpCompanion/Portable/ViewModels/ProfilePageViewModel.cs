using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvpApi.Common.ServiceModels;
using MvpCompanion.Portable.Helpers;

namespace MvpCompanion.Portable.ViewModels
{
    public class ProfilePageViewModel : PageViewModelBase
    {
        public ProfilePageViewModel()
        {
            //if (DesignMode.DesignModeEnabled)
            //{
            //    Mvp = DesignTimeHelpers.GenerateSampleMvp();
            //}
        }
        
        public ProfileViewModel Mvp { get; set; } = (App.RootPage.BindingContext as ShellPageViewModel)?.Mvp;

        public string ProfileImagePath { get; set; } = (App.RootPage.BindingContext as ShellPageViewModel)?.ProfileImagePath;
        
        public void LoginButton_Click(object sender, EventArgs e)
        {
            //BootStrapper.Current.NavigationService.Navigate(typeof(LoginPage));
        }

        public void LogoutButton_Click(object sender, EventArgs e)
        {
            // Passing a bool true parameter performs logout
            //BootStrapper.Current.NavigationService.Navigate(typeof(LoginPage), true);
        }
        
        #region Navigation

        //public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        //{
        //    if (App.ShellPage.DataContext is ShellPageViewModel shellVm && shellVm.IsLoggedIn)
        //    {

        //    }
        //    else
        //    {
        //        await BootStrapper.Current.NavigationService.NavigateAsync(typeof(LoginPage));
        //    }
        //}

        //public override Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        //{
        //    return base.OnNavigatedFromAsync(pageState, suspending);
        //}

        #endregion
    }
}
