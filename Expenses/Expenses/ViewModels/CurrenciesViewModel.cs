
namespace Expenses.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Models;
    using Helpers;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class CurrenciesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private bool isEnabled;
        #endregion

        #region Properties
        public List<Currency> MyCurrencies { get; set; }
        public List<CurrencyLocal> MyCurrenciesLocal { get; set; }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Singleton
        private static CurrenciesViewModel instance; // Atributo
        public static CurrenciesViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new CurrenciesViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public CurrenciesViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadCurrencies();
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadCurrencies()
        {
            this.IsRefreshing = true;
            this.IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (connection.IsSuccess)
            {

                var answer = await this.LoadFromAPI();
                if (answer)
                {
                    this.SaveToSqlite();
                }
            }
            else
            {
                await this.LoadFromDB();
            }

            if (this.MyCurrencies == null || this.MyCurrencies.Count == 0)
            {
                this.IsRefreshing = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoCurrenciesMessage, Languages.Accept);
                return;
            }

            this.IsRefreshing = false;
            this.IsEnabled = true;
        }
        private async Task<bool> LoadFromAPI()
        {
            //var response = await this.apiService.GetList<Product>("http://200.55.241.235", "/InvAPI/api", "/Products");
            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlCurrenciesController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            var response = await this.apiService.GetList<Currency>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyCurrencies = (List<Currency>)response.Result; // hay que castearlo
            this.MyCurrenciesLocal = this.MyCurrencies.Select(P => new CurrencyLocal
            {
                CurrencyId = P.CurrencyId,
                Description = P.Description,
                Symbol = P.Symbol,
            }).ToList();
            return true;
        }

        private async Task SaveToSqlite()
        {
            await this.dataService.DeleteAllCurrencies();
            this.dataService.Insert(this.MyCurrenciesLocal); // Nota: En este método no necesitamos el await.
        }

        private async Task LoadFromDB()
        {
            this.MyCurrenciesLocal = await this.dataService.GetAllCurrencies();

            this.MyCurrencies = this.MyCurrenciesLocal.Select(p => new Currency
            {
                CurrencyId = p.CurrencyId,
                Description = p.Description,
                Symbol = p.Symbol,
            }).ToList();
        }
        #endregion

    }

}
