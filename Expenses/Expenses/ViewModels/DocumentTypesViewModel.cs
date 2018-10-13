
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

    public class DocumentTypesViewModel : BaseViewModel
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
        public List<DocumentType> MyDocumentTypes { get; set; }
        public List<DocumentTypeLocal> MyDocumentTypesLocal { get; set; }
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
        private static DocumentTypesViewModel instance; // Atributo
        public static DocumentTypesViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new DocumentTypesViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public DocumentTypesViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadDocumentTypes();
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadDocumentTypes()
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

            if (this.MyDocumentTypes == null || this.MyDocumentTypes.Count == 0)
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
            var controller = Application.Current.Resources["UrlDocumentTypesController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            var response = await this.apiService.GetList<Currency>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyDocumentTypes = (List<DocumentType>)response.Result; // hay que castearlo
            this.MyDocumentTypesLocal = this.MyDocumentTypes.Select(P => new DocumentTypeLocal
            {
                DocumentTypeId = P.DocumentTypeId,
                Description = P.Description,
                DocumentCode = P.DocumentCode,
            }).ToList();
            return true;
        }

        private async Task SaveToSqlite()
        {
            await this.dataService.DeleteAllDocumentTypes();
            this.dataService.Insert(this.MyDocumentTypesLocal); // Nota: En este método no necesitamos el await.
        }

        private async Task LoadFromDB()
        {
            this.MyDocumentTypesLocal = await this.dataService.GetAllDocumentTypes();

            this.MyDocumentTypes = this.MyDocumentTypesLocal.Select(p => new DocumentType
            {
                DocumentTypeId = p.DocumentTypeId,
                Description = p.Description,
                DocumentCode = p.DocumentCode,
            }).ToList();
        }
        #endregion
    }

}
