namespace Expenses.ViewModels
{
    using Common.Models;
    using Expenses.Helpers;
    using Expenses.Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class RequestsViewModel : BaseViewModel
    {
        #region Attributes
        private bool isRefreshing;
        private bool isEnabled;
        private ObservableCollection<RequestItemViewModel> requests;
        #endregion

        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Properties
        public List<Request> MyRequests { get; set; }
        public List<RequestLocal> MyRequestsLocal { get; set; }

        public ObservableCollection<RequestItemViewModel> Requests
        {
            get { return this.requests; }
            set { SetValue(ref this.requests, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Singleton
        private static RequestsViewModel instance; // Atributo
        public static RequestsViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new RequestsViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public RequestsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            LoadRequests();
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadRequests()
        {
            this.IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (connection.IsSuccess)
            {
                var answer = await this.LoadRequestsFromAPI();
                if (answer)
                {
                    this.SaveRequestsToDB();
                }
            }
            else
            {
                await this.LoadRequestsFromDB();
            }

            if (this.MyRequests == null || this.MyRequests.Count == 0)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoRequestMessage, Languages.Accept);
                return;
            }

            this.RefreshList();
            this.IsRefreshing = false;

        }

        private async Task LoadRequestsFromDB()
        {

            this.MyRequestsLocal = await this.dataService.GetAllRequests();

            this.MyRequests = this.MyRequestsLocal.Select(p => new Request
            {
                Approved = p.Approved,
                Comments = p.Comments,
                Description = p.Description,
                ExpenseDateEnd = p.ExpenseDateEnd,
                ExpenseDateStart = p.ExpenseDateStart,
                RequestId = p.ExpenseId,
                UserId = p.UserId,
            }).ToList();
        }

        private async Task SaveRequestsToDB()
        {
            await this.dataService.DeleteAllRequests();
            this.dataService.Insert(this.MyRequestsLocal);
        }

        private async Task<bool> LoadRequestsFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlRequestsController"].ToString(); // Obtengo el controlador del diccionario de recursos.
            var response = await this.apiService.GetList<Request>(url, prefix, controller);
            //var response = await this.apiService.GetList<Expense>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyRequests = (List<Request>)response.Result; // hay que castearlo
            this.MyRequestsLocal = this.MyRequests.Select(p => new RequestLocal
            {
                Approved = p.Approved,
                Comments = p.Comments,
                Description = p.Description,
                ExpenseDateEnd = p.ExpenseDateEnd,
                ExpenseDateStart = p.ExpenseDateStart,
                ExpenseId = p.RequestId,
                UserId = p.UserId,
            }).ToList();
            return true;
        }

        public void RefreshList()
        {
            // Expresion válida pero de BAJA PERFORMANCE.!!!
            //var myList = new List<ProductItemViewModel>();
            //foreach (var item in list)
            //{
            //    myList.Add(new ProductItemViewModel
            //    {
            //    });
            //}

            // Expresion Lamda (ALTA PERFORMANCE)
            var myListRequestItemViewModel = this.MyRequests.Select(p => new RequestItemViewModel
            {
                Approved = p.Approved,
                Comments = p.Comments,
                Description = p.Description,
                ExpenseDateEnd = p.ExpenseDateEnd,
                ExpenseDateStart = p.ExpenseDateStart,
                RequestId = p.RequestId,
                UserId = p.UserId,
            });
            this.Requests = new ObservableCollection<RequestItemViewModel>(
                myListRequestItemViewModel.OrderBy(p => p.Description));

        }


        #endregion

    }
}