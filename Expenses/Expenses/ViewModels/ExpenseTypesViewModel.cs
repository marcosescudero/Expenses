
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

    public class ExpenseTypesViewModel : BaseViewModel
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
        public List<ExpenseType> MyExpenseTypes { get; set; }
        public List<ExpenseTypeLocal> MyExpenseTypesLocal { get; set; }
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
        private static ExpenseTypesViewModel instance; // Atributo
        public static ExpenseTypesViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new ExpenseTypesViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public ExpenseTypesViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadExpenseTypes();
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadExpenseTypes()
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

            if (this.MyExpenseTypes == null || this.MyExpenseTypes.Count == 0)
            {
                this.IsRefreshing = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoExpenseTypesMessage, Languages.Accept);
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
            var controller = Application.Current.Resources["UrlExpenseTypesController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            var response = await this.apiService.GetList<ExpenseType>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyExpenseTypes = (List<ExpenseType>)response.Result; // hay que castearlo
            this.MyExpenseTypesLocal = this.MyExpenseTypes.Select(P => new ExpenseTypeLocal
            {
                ExpenseTypeId = P.ExpenseTypeId,
                Description = P.Description,
            }).ToList();
            return true;
        }

        private async Task SaveToSqlite()
        {
            await this.dataService.DeleteAllExpenseTypes();
            this.dataService.Insert(this.MyExpenseTypesLocal); // Nota: En este método no necesitamos el await.
        }

        private async Task LoadFromDB()
        {
            this.MyExpenseTypesLocal = await this.dataService.GetAllExpenseTypes();

            this.MyExpenseTypes = this.MyExpenseTypesLocal.Select(p => new ExpenseType
            {
                ExpenseTypeId=p.ExpenseTypeId,
                Description=p.Description,
            }).ToList();
        }
        #endregion
    }

}
