namespace Expenses.ViewModels
{
    using Common.Models;
    using Helpers;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    public class ExpenseItemViewModel : Expense
    {
        #region Attributes
        private ApiService apiService;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public ExpenseItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand EditExpenseCommand
        {
            get
            {
                return new RelayCommand(EditExpense);
            }
        }

        private async void EditExpense()
        {
            MainViewModel.GetInstance().EditExpense = new EditExpenseViewModel(this);
            await App.Navigator.PushAsync(new EditExpensePage());
        }
        #endregion

    }
}
