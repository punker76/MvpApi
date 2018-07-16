using MvpApi.Common.Services;
using MvpCompanion.Portable.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace MvpCompanion.Portable
{
    public partial class App : Application
	{
	    public static RootPage RootPage { get; set; }

        public static MvpApiService ApiService { get; set; }

		public App ()
		{
			InitializeComponent();

            RootPage = new RootPage();
		    this.MainPage = RootPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
