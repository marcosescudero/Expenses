namespace Expenses.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    //using Plugin.Media;
    //using Plugin.Media.Abstractions;
    using Helpers;
    using Services;
    using Xamarin.Forms;
    public class EditExpenseViewModel : BaseViewModel
    {
        #region Attributes
        private Expense expense;
        //private MediaFile file;
        private ImageSource imageSource;
        private bool isRunning;
        private bool isEnabled;
        private Currency currencySelected;
        private DocumentType documentTypeSelected;
        #endregion

        #region Services
        private ApiService apiService;
        #endregion

        #region Properties
        public List<Currency> MyCurrencies { get; set; }
        public List<DocumentType> MyDocumentTypes { get; set; }
        public ObservableCollection<Currency> Currencies { get; set; }
        public ObservableCollection<DocumentType> DocumentTypes { get; set; }
        public Expense Expense
        {
            get { return this.expense; }
            set { SetValue(ref expense, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public EditExpenseViewModel(Expense expense)
        {
            this.Expense = expense;
            this.isEnabled = true;
            this.apiService = new ApiService();
            this.ImageSource = expense.ImageFullPath;

            // Currencies
            this.MyCurrencies = MainViewModel.GetInstance().
                Currencies.MyCurrencies.Select(p => new Currency
                {
                    CurrencyId =p.CurrencyId,
                    Description = p.Description,
                    Symbol = p.Symbol,
                    Expenses = p.Expenses,
                }).ToList();
            this.Currencies = new ObservableCollection<Currency>(this.MyCurrencies);

            // Document Types
            this.MyDocumentTypes = MainViewModel.GetInstance().
                DocumentTypes.MyDocumentTypes.Select(p => new DocumentType
                {
                    DocumentTypeId = p.DocumentTypeId,
                    Description = p.Description,
                    DocumentCode = p.DocumentCode,
                    Expenses = p.Expenses,
                }).ToList();
            this.DocumentTypes = new ObservableCollection<DocumentType>(this.MyDocumentTypes);
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {

            await Application.Current.MainPage.DisplayAlert(
                Languages.Atention,
                "DSFDSFDSFDSF",
                Languages.Accept);
            return;
  
        }
        #endregion

    }
}
