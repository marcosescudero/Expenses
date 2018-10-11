using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Expenses
{
    using Helpers;
    using System;
    using ViewModels;
    using Views;
    using Xamarin.Forms;
    
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstance();
            if (Settings.IsRemembered)
            {
                DateTime expireDate;
                if (!DateTime.TryParse(Settings.TokenExpires, out expireDate))
                {
                    // handle parse failure
                    expireDate = DateTime.Parse("Mon, 01 Jan 2018 12:00:00 GMT");
                }
                if (Settings.AccessToken != null && expireDate > DateTime.Now)
                {
                    /*
                    mainViewModel.MeasureUnits = new MeasureUnitsViewModel();
                    mainViewModel.Locations = new LocationsViewModel();
                    mainViewModel.Items = new ItemsViewModel();
                    mainViewModel.Items.IsRefreshing = false;
                    this.MainPage = new MasterPage();
                    */
                    mainViewModel.Currencies = new CurrenciesViewModel();
                    mainViewModel.Expenses = new ExpensesViewModel();
                    MainPage = new NavigationPage(new ExpensesPage());
                }
                else
                {
                    mainViewModel.Login = new LoginViewModel();
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
            {
                mainViewModel.Login = new LoginViewModel();
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
