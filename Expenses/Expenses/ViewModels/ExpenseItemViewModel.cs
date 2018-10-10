

namespace Expenses.ViewModels
{
    using Common.Models;
    using Helpers;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ExpenseItemViewModel : Expense
    {
        #region Attributes
        private ApiService apiService;
        #endregion

        #region Constructors
        public ExpenseItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand EditProductCommand
        {
            get
            {
                return new RelayCommand(EditProduct);
            }
        }

        private async void EditProduct()
        {
            //MainViewModel.GetInstance().EditExpense = new EditExpenseViewModel(this);
            //await App.Navigator.PushAsync(new EditExpensePage());
        }

        public ICommand DeleteProductCommand
        {
            get
            {
                return new RelayCommand(DeleteProduct);
            }
        }

        private async void DeleteProduct()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.DeleteConfirmation,
                Languages.Yes,
                Languages.No);

            if (!answer)
            {
                return;
            }

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlExpensesController"].ToString(); // Obtengo el controlador del diccionario de recursos.

            /*
            var response = await this.apiService.Delete(url, prefix, controller, this.ProductId, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var productsViewModel = ProductsViewModel.GetInstance();
            var deletedProduct = productsViewModel.MyProducts.Where(p => p.ProductId == this.ProductId).FirstOrDefault(); // LinQ
            if (deletedProduct != null)
            {
                productsViewModel.MyProducts.Remove(deletedProduct); // con esto me lo debe refrescar automaticamente en la lista
            }
            productsViewModel.RefreshList();
            */
        }
        #endregion
    }
}
