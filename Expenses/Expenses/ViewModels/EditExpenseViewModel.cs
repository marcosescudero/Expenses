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
        private PaymentType paymentTypeSelected;
        private ExpenseType expenseTypeSelected;
        private Vendor vendorSelected;
        #endregion

        #region Services
        private ApiService apiService;
        #endregion

        #region Properties
        public List<Currency> MyCurrencies { get; set; }
        public List<DocumentType> MyDocumentTypes { get; set; }
        public List<PaymentType> MyPaymentTypes { get; set; }
        public List<ExpenseType> MyExpenseTypes { get; set; }
        public List<Vendor> MyVendors { get; set; }

        public ObservableCollection<Currency> Currencies { get; set; }
        public ObservableCollection<DocumentType> DocumentTypes { get; set; }
        public ObservableCollection<PaymentType> PaymentTypes { get; set; }
        public ObservableCollection<ExpenseType> ExpenseTypes { get; set; }
        public ObservableCollection<Vendor> Vendors { get; set; }

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
        public Currency CurrencySelected
        {
            get { return this.currencySelected; }
            set { SetValue(ref this.currencySelected, value); }
        }
        public DocumentType DocumentTypeSelected
        {
            get { return this.documentTypeSelected; }
            set { SetValue(ref this.documentTypeSelected, value); }
        }
        public PaymentType PaymentTypeSelected
        {
            get { return this.paymentTypeSelected; }
            set { SetValue(ref this.paymentTypeSelected, value); }
        }
        public ExpenseType ExpenseTypeSelected
        {
            get { return this.expenseTypeSelected; }
            set { SetValue(ref this.expenseTypeSelected, value); }
        }
        public Vendor VendorSelected
        {
            get { return this.vendorSelected; }
            set { SetValue(ref this.vendorSelected, value); }
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
            this.CurrencySelected = expense.Currency;

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
            this.DocumentTypeSelected = expense.DocumentType;

            // Payment Types
            this.MyPaymentTypes = MainViewModel.GetInstance().
                PaymentTypes.MyPaymentTypes.Select(p => new PaymentType
                {
                    PaymentTypeId = p.PaymentTypeId,
                    Description = p.Description,
                    Expenses = p.Expenses,
                }).ToList();
            this.PaymentTypes = new ObservableCollection<PaymentType>(this.MyPaymentTypes);
            this.PaymentTypeSelected = expense.PaymentType;

            // Expense Types
            this.MyExpenseTypes = MainViewModel.GetInstance().
                ExpenseTypes.MyExpenseTypes.Select(p => new ExpenseType
                {
                    ExpenseTypeId = p.ExpenseTypeId,
                    Description = p.Description,
                }).ToList();
            this.ExpenseTypes = new ObservableCollection<ExpenseType>(this.MyExpenseTypes);
            this.ExpenseTypeSelected = expense.ExpenseType;

            // Vendors
            this.MyVendors = MainViewModel.GetInstance().
                Vendors.MyVendors.Select(p => new Vendor
                {
                    VendorId = p.VendorId,
                    Name = p.Name,
                    Alias = p.Alias,
                    Cuit = p.Cuit,
                    Expenses = p.Expenses,
                }).ToList();
            this.Vendors = new ObservableCollection<Vendor>(this.MyVendors);
            this.VendorSelected = expense.Vendor;
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
                "Datos Guardados",
                Languages.Accept);
            return;
          }
        #endregion

    }
}
