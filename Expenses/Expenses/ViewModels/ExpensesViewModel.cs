using Expenses.Common.Models;
using Expenses.Models;
using Expenses.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Expenses.ViewModels
{
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



    }
}
