namespace Expenses.ViewModels
{
    using Expenses.Services;
    using System.Collections.ObjectModel;

    public class ExpensesViewModel
    {
        #region Attributes
        private string filter;
        private bool isRefreshing;
        private bool isEnabled;

        private ObservableCollection<ExpensesViewModel> Expenses;
        #endregion

        #region Services
        private ApiService apiService;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Singleton

        #endregion

        #region Commands

        #endregion
    }
}
