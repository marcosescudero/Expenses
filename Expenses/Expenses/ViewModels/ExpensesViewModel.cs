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

    public class ExpensesViewModel : BaseViewModel
    {
        #region Attributes
        private bool isRefreshing;
        private bool isEnabled;
        private ObservableCollection<ExpenseItemViewModel> expenses;
        #endregion

        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Properties
        public List<Expense> MyExpenses { get; set; }
        public List<ExpenseLocal> MyExpensesLocal { get; set; }

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
            this.IsRefreshing = false;
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
                Approved = p.Approved,
                Comments = p.Comments,
                Description = p.Description,
                ExpenseDateEnd = p.ExpenseDateEnd,
                ExpenseDateStart = p.ExpenseDateStart,
                ExpenseId = p.ExpenseId,
                UserId = p.UserId,
            }).ToList();
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

            var response = await this.apiService.GetList<Expense>(url, prefix, controller);
            //var response = await this.apiService.GetList<Expense>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);


            if (!response.IsSuccess)
            {
                return false;
            }
            this.MyExpenses = (List<Expense>)response.Result; // hay que castearlo
            this.MyExpensesLocal = this.MyExpenses.Select(p => new ExpenseLocal
            {
                Approved = p.Approved,
                Comments = p.Comments,
                Description = p.Description,
                ExpenseDateEnd = p.ExpenseDateEnd,
                ExpenseDateStart = p.ExpenseDateStart,
                ExpenseId = p.ExpenseId,
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
            var myListExpenseItemViewModel = this.MyExpenses.Select(p => new ExpenseItemViewModel
            {
                Approved = p.Approved,
                Comments = p.Comments,
                Description = p.Description,
                ExpenseDateEnd = p.ExpenseDateEnd,
                ExpenseDateStart = p.ExpenseDateStart,
                ExpenseDetails = p.ExpenseDetails,
                ExpenseId = p.ExpenseId,
                UserId = p.UserId,
            });
            this.Expenses = new ObservableCollection<ExpenseItemViewModel>(
                myListExpenseItemViewModel.OrderBy(p => p.Description));

        }


        #endregion

        #region Commands

        #endregion


    }
}