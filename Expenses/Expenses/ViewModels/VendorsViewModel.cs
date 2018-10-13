
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

    public class VendorsViewModel : BaseViewModel
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
        public List<Vendor> MyVendors { get; set; }
        public List<VendorLocal> MyVendorsLocal { get; set; }
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
        private static VendorsViewModel instance; // Atributo
        public static VendorsViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new VendorsViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public VendorsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadVendors();
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadVendors()
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

            if (this.MyVendors == null || this.MyVendors.Count == 0)
            {
                this.IsRefreshing = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoVendorsMessage, Languages.Accept);
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
            var controller = Application.Current.Resources["UrlVendorsController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            var response = await this.apiService.GetList<DocumentType>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyVendors = (List<Vendor>)response.Result; // hay que castearlo
            this.MyVendorsLocal = this.MyVendors.Select(P => new VendorLocal
            {
                VendorId = P.VendorId,
                Name = P.Name,
                Alias = P.Alias,
                Cuit = P.Cuit,
            }).ToList();
            return true;
        }

        private async Task SaveToSqlite()
        {
            await this.dataService.DeleteAllVendors();
            this.dataService.Insert(this.MyVendorsLocal); // Nota: En este método no necesitamos el await.
        }

        private async Task LoadFromDB()
        {
            this.MyVendorsLocal = await this.dataService.GetAllVendors();

            this.MyVendors = this.MyVendorsLocal.Select(p => new Vendor
            {
                VendorId = p.VendorId,
                Name = p.Name,
                Alias = p.Alias,
                Cuit = p.Cuit,
            }).ToList();
        }
        #endregion
    }

}
