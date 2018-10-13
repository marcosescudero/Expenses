﻿
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

    public class PaymentTypesViewModel : BaseViewModel
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
        public List<PaymentType> MyPaymentTypes { get; set; }
        public List<PaymentTypeLocal> MyPaymentTypesLocal { get; set; }
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
        private static PaymentTypesViewModel instance; // Atributo
        public static PaymentTypesViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new PaymentTypesViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public PaymentTypesViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadPaymentTypes();
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadPaymentTypes()
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

            if (this.MyPaymentTypes == null || this.MyPaymentTypes.Count == 0)
            {
                this.IsRefreshing = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoDocumentTypesMessage, Languages.Accept);
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
            var controller = Application.Current.Resources["UrlPaymentTypesController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            var response = await this.apiService.GetList<DocumentType>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyPaymentTypes = (List<PaymentType>)response.Result; // hay que castearlo
            this.MyPaymentTypesLocal = this.MyPaymentTypes.Select(P => new PaymentTypeLocal
            {
                PaymentTypeId = P.PaymentTypeId,
                Description = P.Description,
            }).ToList();
            return true;
        }

        private async Task SaveToSqlite()
        {
            await this.dataService.DeleteAllDocumentTypes();
            this.dataService.Insert(this.MyPaymentTypesLocal); // Nota: En este método no necesitamos el await.
        }

        private async Task LoadFromDB()
        {
            this.MyPaymentTypesLocal = await this.dataService.GetAllPaymentTypes();

            this.MyPaymentTypes = this.MyPaymentTypesLocal.Select(p => new PaymentType
            {
                PaymentTypeId = p.PaymentTypeId,
                Description = p.Description,
            }).ToList();
        }
        #endregion
    }

}
