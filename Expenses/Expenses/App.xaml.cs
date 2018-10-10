using Expenses.ViewModels;
using Expenses.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Expenses
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.Expenses = new ExpensesViewModel();
            MainPage = new NavigationPage(new ExpensesPage());
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
