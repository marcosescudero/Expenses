namespace Expenses.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
    using Xamarin.Forms;
    public class EditExpenseViewModel : BaseViewModel
    {
        #region Attributes
        private Expense expense;
        private MediaFile file;
        private ImageSource imageSource;
        private bool isRunning;
        private bool isEnabled;
        private Request requestSelected;
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
        public List<Request> MyRequests { get; set; }

        public ObservableCollection<Currency> Currencies { get; set; }
        public ObservableCollection<DocumentType> DocumentTypes { get; set; }
        public ObservableCollection<PaymentType> PaymentTypes { get; set; }
        public ObservableCollection<ExpenseType> ExpenseTypes { get; set; }
        public ObservableCollection<Vendor> Vendors { get; set; }
        public ObservableCollection<Request> Requests { get; set; }

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
        public Request RequestSelected
        {
            get { return this.requestSelected; }
            set { SetValue(ref this.requestSelected, value); }
        }
        #endregion

        #region Constructors
        public EditExpenseViewModel(Expense expense)
        {
            this.Expense = expense;
            this.isEnabled = true;
            this.apiService = new ApiService();
            //this.ImageSource = (expense.ImageFullPath!=null?expense.ImageFullPath:"noimage");
            this.ImageSource = "noimage";

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
            this.CurrencySelected = this.MyCurrencies.Where(p => p.CurrencyId == expense.CurrencyId).FirstOrDefault();

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
            this.DocumentTypeSelected = this.MyDocumentTypes.Where(p => p.DocumentTypeId == expense.DocumentTypeId).FirstOrDefault();

            // Payment Types
            this.MyPaymentTypes = MainViewModel.GetInstance().
                PaymentTypes.MyPaymentTypes.Select(p => new PaymentType
                {
                    PaymentTypeId = p.PaymentTypeId,
                    Description = p.Description,
                    Expenses = p.Expenses,
                }).ToList();
            this.PaymentTypes = new ObservableCollection<PaymentType>(this.MyPaymentTypes);
            this.PaymentTypeSelected = this.MyPaymentTypes.Where(p => p.PaymentTypeId == expense.PaymentTypeId).FirstOrDefault();

            // Expense Types
            this.MyExpenseTypes = MainViewModel.GetInstance().
                ExpenseTypes.MyExpenseTypes.Select(p => new ExpenseType
                {
                    ExpenseTypeId = p.ExpenseTypeId,
                    Description = p.Description,
                }).ToList();
            this.ExpenseTypes = new ObservableCollection<ExpenseType>(this.MyExpenseTypes);
            this.ExpenseTypeSelected = this.MyExpenseTypes.Where(p => p.ExpenseTypeId == expense.ExpenseTypeId).FirstOrDefault();

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
            this.VendorSelected = this.MyVendors.Where(p => p.VendorId == expense.VendorId).FirstOrDefault();

            //Request
            this.MyRequests = MainViewModel.GetInstance().
                Requests.MyRequests.Select(p => new Request
                {
                    RequestId = p.RequestId,
                    Approved = p.Approved,
                    Comments = p.Comments,
                    Description = p.Description,
                    ExpenseDateEnd = p.ExpenseDateEnd,
                    ExpenseDateStart = p.ExpenseDateStart,
                    Expenses = p.Expenses,
                    UserId = p.UserId,
                }).ToList();
            this.Requests = new ObservableCollection<Request>(this.MyRequests);
            this.RequestSelected = this.MyRequests.Where(p => p.RequestId == expense.RequestId).FirstOrDefault();
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
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }
        #endregion

    }
}
