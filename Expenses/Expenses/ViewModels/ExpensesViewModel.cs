
namespace Expenses.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Common.Models;
    using Views;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class ExpensesViewModel : BaseViewModel
    {

        #region Attributes
        private bool isRefreshing;
        private string filter;
        private bool isEnabled;
        private Request request;
        private ObservableCollection<ExpenseItemViewModel> expenses;
        private decimal totalExpenses;
        private string requestDescription;
        #endregion

        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Properties
        public List<Expense> MyExpenses { get; set; }
        public List<ExpenseLocal> MyExpensesLocal { get; set; }
        

        public Request Request
        {
            get { return this.request; }
            set { SetValue(ref this.request, value); }
        }

        public string RequestDescription { get; set; }

        public ObservableCollection<ExpenseItemViewModel> Expenses
        {
            get { return this.expenses; }
            set { SetValue(ref this.expenses, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref filter, value);
                this.RefreshList();
            }
        }

        public decimal TotalExpenses
        {
            get { return this.totalExpenses; }
            set { SetValue(ref this.totalExpenses, value); }
        }

        #endregion

        #region Singleton
        private static ExpensesViewModel instance; // Atributo
        public static ExpensesViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new ExpensesViewModel();
            }
            return instance;
        }
        #endregion

        #region Constructors
        public ExpensesViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            LoadExpenses();
            //this.IsRefreshing = false;
        }
        public ExpensesViewModel(Request request)
        {
            instance = this;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.Request = request;
            RequestDescription = request.Description;
            LoadExpenses();
            //this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private async void LoadExpenses()
        {
            this.IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (connection.IsSuccess)
            {
                var answer = await this.LoadExpensesFromAPI();
                if (answer)
                {
                    this.SaveExpensesToDB();
                }
            }
            else
            {
                await this.LoadExpensesFromDB();
            }
            if (this.MyExpenses == null || this.MyExpenses.Count == 0)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoExpensesMessage, Languages.Accept);
                return;
            }
            this.RefreshList();
            this.IsRefreshing = false;
        }
        private async Task LoadExpensesFromDB()
        {

            this.MyExpensesLocal = await this.dataService.GetAllExpenses();

            this.MyExpenses = this.MyExpensesLocal.Select(p => new Expense
            {
                Amount = p.Amount,
                AmountIVA = p.AmountIVA,
                AmountPercepcion = p.AmountPercepcion,
                Comments = p.Comments,
                DocumentNumber = p.DocumentNumber,
                DocumentTypeId = p.DocumentTypeId,
                ExpenseDate = p.ExpenseDate,
                CurrencyId = p.CurrencyId,
                RequestId = p.RequestId,
                ExpenseId = p.ExpenseId,
                ExpenseTypeId = p.ExpenseTypeId,
                ImageArray = p.ImageArray,
                ImagePath = p.ImagePath,
                PaymentTypeId = p.PaymentTypeId,
                TotalAmount = p.TotalAmount,
                VendorId = p.VendorId,
                ExpenseType = MainViewModel.GetInstance().ExpenseTypes.MyExpenseTypes.Where(q => q.ExpenseTypeId == p.ExpenseTypeId).FirstOrDefault(),

            }).Where(p => p.RequestId == (this.Request!=null?this.Request.RequestId:p.RequestId)).ToList();

            //this.TotalExpenses = this.MyExpenses.Select(x => x.TotalAmount).Sum();

            //if (this.Request != null)
            //    this.MyExpenses = this.MyExpenses.Where(p => p.RequestId == this.Request.RequestId).ToList();
        }

        private async Task SaveExpensesToDB()
        {
            await this.dataService.DeleteAllExpenses();
            this.dataService.Insert(this.MyExpensesLocal);
        }

        private async Task<bool> LoadExpensesFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlExpensesController"].ToString(); // Obtengo el controlador del diccionario de recursos.

            //var response = await this.apiService.GetList<Expense>(url, prefix, controller);
            var response = await this.apiService.GetList<Expense>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyExpenses = (List<Expense>)response.Result; // hay que castearlo
            //if (this.Request != null)
            //    this.MyExpenses = this.MyExpenses.Where(p => p.RequestId == this.Request.RequestId).ToList();

            this.MyExpensesLocal = this.MyExpenses.Select(p => new ExpenseLocal
            {
                Amount = p.Amount,
                RequestId = p.RequestId,
                CurrencyId = p.CurrencyId,
                AmountIVA = p.AmountIVA,
                AmountPercepcion = p.AmountPercepcion,
                Comments = p.Comments,
                DocumentNumber = p.DocumentNumber,
                DocumentTypeId = p.DocumentTypeId,
                ExpenseDate = p.ExpenseDate,
                ExpenseId = p.ExpenseId,
                ExpenseTypeId = p.ExpenseTypeId,
                ImageArray = p.ImageArray,
                ImagePath = p.ImagePath,
                PaymentTypeId = p.PaymentTypeId,
                TotalAmount = p.TotalAmount,
                VendorId = p.VendorId,
            }).ToList();

            if (this.Request != null)
                this.MyExpenses = this.MyExpenses.Where(p => p.RequestId == this.Request.RequestId).ToList();

            //this.TotalExpenses = this.MyExpenses.Select(x => x.TotalAmount).Sum();
            return true;
        }

        public void RefreshList()
        {

            if (string.IsNullOrEmpty(this.Filter))
            {
                // Expresion Lamda (ALTA PERFORMANCE)
                var myListExpenseItemViewModel = this.MyExpenses.Select(p => new ExpenseItemViewModel
                {
                    VendorId = p.VendorId,
                    TotalAmount = p.TotalAmount,
                    PaymentTypeId = p.PaymentTypeId,
                    ImagePath = p.ImagePath,
                    ImageArray = p.ImageArray,
                    ExpenseTypeId = p.ExpenseTypeId,
                    ExpenseId = p.ExpenseId,
                    ExpenseDate = p.ExpenseDate,
                    DocumentTypeId = p.DocumentTypeId,
                    Amount = p.Amount,
                    AmountIVA = p.AmountIVA,
                    AmountPercepcion = p.AmountPercepcion,
                    Comments = p.Comments,
                    CurrencyId = p.CurrencyId,
                    RequestId = p.RequestId,
                    Currency = p.Currency,
                    Request = p.Request,
                    DocumentNumber = p.DocumentNumber,
                    DocumentType = p.DocumentType,
                    ExpenseType = p.ExpenseType,
                    PaymentType = p.PaymentType,
                    Vendor = p.Vendor,
                });

                this.Expenses = new ObservableCollection<ExpenseItemViewModel>(
                     myListExpenseItemViewModel.OrderBy(p => p.ExpenseDate));
            }
            else
            {
                var myListExpenseItemViewModel = this.MyExpenses.Select(p => new ExpenseItemViewModel
                {
                    VendorId = p.VendorId,
                    TotalAmount = p.TotalAmount,
                    PaymentTypeId = p.PaymentTypeId,
                    ImagePath = p.ImagePath,
                    ImageArray = p.ImageArray,
                    ExpenseTypeId = p.ExpenseTypeId,
                    ExpenseId = p.ExpenseId,
                    ExpenseDate = p.ExpenseDate,
                    DocumentTypeId = p.DocumentTypeId,
                    Amount = p.Amount,
                    AmountIVA = p.AmountIVA,
                    AmountPercepcion = p.AmountPercepcion,
                    Comments = p.Comments,
                    CurrencyId = p.CurrencyId,
                    RequestId = p.RequestId,
                    Currency = p.Currency,
                    Request = p.Request,
                    DocumentNumber = p.DocumentNumber,
                    DocumentType = p.DocumentType,
                    ExpenseType = p.ExpenseType,
                    PaymentType = p.PaymentType,
                    Vendor = p.Vendor,
                }).Where(p => p.ExpenseType.Description.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Expenses = new ObservableCollection<ExpenseItemViewModel>(
                    myListExpenseItemViewModel.OrderBy(p => p.ExpenseDate));
            }

            this.TotalExpenses = this.Expenses.Select(x => x.TotalAmount).Sum();
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadExpenses);
            }
        }


        public ICommand AddExpenseCommand
        {
            get
            {
                return new RelayCommand(AddExpense);
            }
        }

        private async void AddExpense()
        {

            MainViewModel.GetInstance().AddExpense = new AddExpenseViewModel(this.Request);
            await App.Navigator.PushAsync(new AddExpensePage());
        }
        #endregion
    }
}
