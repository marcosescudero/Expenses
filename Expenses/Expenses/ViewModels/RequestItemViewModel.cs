namespace Expenses.ViewModels
{
    using Common.Models;
    using Helpers;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;

    public class RequestItemViewModel : Request
    {
        #region Attributes
        private ApiService apiService;
        #endregion

        #region Constructors
        public RequestItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand GotoExpensesCommand
        {
            get
            {
                return new RelayCommand(GotoExpenses);
            }
        }

        private async void GotoExpenses()
        {
            // Aqui hay que abrir una pagina con la lista de gastos del expense seleccionado

            //MainViewModel.GetInstance().EditExpense = new EditExpenseViewModel(this);
            //await App.Navigator.PushAsync(new EditExpensePage());

            MainViewModel.GetInstance().Expenses = new ExpensesViewModel(this);
            await App.Navigator.PushAsync(new ExpensesPage());
        }

        #endregion
    }
}
