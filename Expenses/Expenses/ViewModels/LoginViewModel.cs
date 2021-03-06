﻿namespace Expenses.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Views;
    using Services;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.dataService = new DataService();

            this.IsEnabled = true;
            this.IsRemembered = true;
        }
        #endregion

        #region Commands

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private async void Register()
        {
            await Application.Current.MainPage.DisplayAlert(
                Languages.Atention,
                Languages.NotImplemented,
                Languages.Accept
                );
            return;
        }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept
                    );
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept
                    );
                return;
            }


            if (this.Email.ToLower() == "demo")
            {
                this.Email = "marcos@gmail.com";
            }

            if (this.Password.ToLower() == "demo")
            {
                this.Password = "123456";
            }


            if (!RegexHelper.IsValidEmailAddress(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EMailError,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;  // Muestra el Activity indicator
            this.IsEnabled = false; // Desabilita botones
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept
                    );
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var token = await this.apiService.GetToken(url, this.Email, this.Password);

            if (token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SomethingWrong,
                    Languages.Accept
                    );
                return;
            }

            Settings.TokenType = token.TokenType;
            Settings.AccessToken = token.AccessToken;
            Settings.TokenExpires = token.Expires.ToString();
            Settings.UserName = token.UserName;
            Settings.IsRemembered = this.IsRemembered;

            //Application.Current.MainPage = new NavigationPage(new ItemsPage());

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Currencies = new CurrenciesViewModel();
            mainViewModel.DocumentTypes = new DocumentTypesViewModel();
            mainViewModel.PaymentTypes = new PaymentTypesViewModel();
            mainViewModel.ExpenseTypes = new ExpenseTypesViewModel();
            mainViewModel.Vendors = new VendorsViewModel();
            mainViewModel.Requests = new RequestsViewModel();
            Application.Current.MainPage = new MasterPage();

            //this.IsRunning = false;
            //this.IsEnabled = true;

        }
        #endregion

    }
}
