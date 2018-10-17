namespace Expenses.ViewModels
{
    using System;
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
    public class AddExpenseViewModel : BaseViewModel
    {
        #region Attributes
        private MediaFile file;
        private ImageSource imageSource;
        private bool isRunning;
        private bool isEnabled;
        private Currency currencySelected;
        private DocumentType documentTypeSelected;
        private PaymentType paymentTypeSelected;
        private ExpenseType expenseTypeSelected;
        private Vendor vendorSelected;
        private DateTime expenseDate;
        private string documentNumber;
        private string comments;
        private decimal amount;
        private decimal amountIVA;
        private decimal amountPercepcion;
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
        public Request Request { get; set; }

        public DateTime ExpenseDate
        {
            get { return this.expenseDate; }
            set { SetValue(ref this.expenseDate, value); }
        }

        public string DocumentNumber
        {
            get { return this.documentNumber; }
            set { SetValue(ref this.documentNumber, value); }
        }
        public string Comments
        {
            get { return this.comments; }
            set { SetValue(ref this.comments, value); }
        }
        public decimal Amount
        {
            get { return this.amount; }
            set { SetValue(ref this.amount, value); }
        }
        public decimal AmountIVA
        {
            get { return this.amountIVA; }
            set { SetValue(ref this.amountIVA, value); }
        }
        public decimal AmountPercepcion
        {
            get { return this.amountPercepcion; }
            set { SetValue(ref this.amountPercepcion, value); }
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
        public AddExpenseViewModel(Request request)
        {
            this.isEnabled = true;
            this.apiService = new ApiService();
            this.ImageSource = "noimage";
            this.ExpenseDate = DateTime.Now;
            this.Request = request;

            // Currencies
            this.MyCurrencies = MainViewModel.GetInstance().
                Currencies.MyCurrencies.Select(p => new Currency
                {
                    CurrencyId = p.CurrencyId,
                    Description = p.Description,
                    Symbol = p.Symbol,
                    Expenses = p.Expenses,
                }).ToList();
            this.Currencies = new ObservableCollection<Currency>(this.MyCurrencies);
            //this.CurrencySelected = this.MyCurrencies.Where(p => p.CurrencyId == expense.CurrencyId).FirstOrDefault();

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
            //this.DocumentTypeSelected = this.MyDocumentTypes.Where(p => p.DocumentTypeId == expense.DocumentTypeId).FirstOrDefault();

            // Payment Types
            this.MyPaymentTypes = MainViewModel.GetInstance().
                PaymentTypes.MyPaymentTypes.Select(p => new PaymentType
                {
                    PaymentTypeId = p.PaymentTypeId,
                    Description = p.Description,
                    Expenses = p.Expenses,
                }).ToList();
            this.PaymentTypes = new ObservableCollection<PaymentType>(this.MyPaymentTypes);
            //this.PaymentTypeSelected = this.MyPaymentTypes.Where(p => p.PaymentTypeId == expense.PaymentTypeId).FirstOrDefault();

            // Expense Types
            this.MyExpenseTypes = MainViewModel.GetInstance().
                ExpenseTypes.MyExpenseTypes.Select(p => new ExpenseType
                {
                    ExpenseTypeId = p.ExpenseTypeId,
                    Description = p.Description,
                }).ToList();
            this.ExpenseTypes = new ObservableCollection<ExpenseType>(this.MyExpenseTypes);
            //this.ExpenseTypeSelected = this.MyExpenseTypes.Where(p => p.ExpenseTypeId == expense.ExpenseTypeId).FirstOrDefault();

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
            //this.VendorSelected = this.MyVendors.Where(p => p.VendorId == expense.VendorId).FirstOrDefault();


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
            if (this.ExpenseDate > this.Request.ExpenseDateEnd ||
                this.ExpenseDate < this.Request.ExpenseDateStart)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DateRangeError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.DocumentNumber))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DocumentNumberError,
                    Languages.Accept);
                return;
            }

            if (this.Amount <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AmountInvalid,
                    Languages.Accept);
                return;
            }

            if (this.AmountIVA > this.Amount)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AmountIvaInvalid,
                    Languages.Accept);
                return;
            }

            if (this.AmountPercepcion > this.Amount)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AmountPercepcionInvalid,
                    Languages.Accept);
                return;
            }

            if (this.AmountIVA + this.AmountPercepcion > this.Amount)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AmountPercepcionInvalid,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true; // Esto muestra el activity indicator
            this.IsEnabled = false; // Desabilito el botón de SAVE para que el usuario no le pegue varias veces.

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }


            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var expense = new Expense();
            expense.ExpenseDate = this.ExpenseDate;
            expense.CurrencyId = this.CurrencySelected.CurrencyId;
            //expense.Currency = this.CurrencySelected;
            expense.DocumentTypeId = this.DocumentTypeSelected.DocumentTypeId;
            //expense.DocumentType = this.DocumentTypeSelected;
            expense.ExpenseTypeId = this.ExpenseTypeSelected.ExpenseTypeId;
            //expense.ExpenseType = this.ExpenseTypeSelected;
            expense.PaymentTypeId = this.PaymentTypeSelected.PaymentTypeId;
            //expense.PaymentType = this.PaymentTypeSelected;
            expense.VendorId = this.VendorSelected.VendorId;
            //expense.Vendor = this.VendorSelected;
            expense.RequestId = this.Request.RequestId;
            //expense.Request = this.Request;
            expense.Amount = this.Amount;
            expense.AmountIVA = this.AmountIVA;
            expense.AmountPercepcion = this.AmountPercepcion;
            expense.TotalAmount = expense.Amount + expense.AmountIVA + expense.AmountPercepcion;
            expense.DocumentNumber = this.DocumentNumber;
            expense.Comments = this.Comments;
            expense.ImageArray = imageArray;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlExpensesController"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, expense, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            var newExpense = (Expense)response.Result;
            var expensesViewModel = ExpensesViewModel.GetInstance();

            // Le agrego las clases
            newExpense.Currency = this.CurrencySelected;
            newExpense.DocumentType = this.DocumentTypeSelected;
            newExpense.ExpenseType = this.ExpenseTypeSelected;
            newExpense.PaymentType = this.PaymentTypeSelected;
            newExpense.Vendor = this.VendorSelected;
            newExpense.Request = this.Request;

            expensesViewModel.MyExpenses.Add(newExpense);
            expensesViewModel.RefreshList();

            this.IsRunning = false;
            this.IsEnabled = true;
            
            /*
            await Application.Current.MainPage.DisplayAlert(
                Languages.Atention,
                Languages.DataSaved,
                Languages.Accept);
            */

            await App.Navigator.PopAsync();
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

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }
        private async void Delete()
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

            /*
            this.IsRunning = true;
            this.isEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString(); // Obtengo la url del diccionario de recursos.
            var prefix = Application.Current.Resources["UrlPrefix"].ToString(); // Obtengo el prefijo del diccionario de recursos.
            var controller = Application.Current.Resources["UrlProductsController"].ToString(); // Obtengo el controlador del diccionario de recursos.

            var response = await this.apiService.Delete(url, prefix, controller, this.Product.ProductId, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var productsViewModel = ProductsViewModel.GetInstance();
            var deletedProduct = productsViewModel.MyProducts.Where(p => p.ProductId == this.Product.ProductId).FirstOrDefault(); // LinQ
            if (deletedProduct != null)
            {
                productsViewModel.MyProducts.Remove(deletedProduct); // con esto me lo debe refrescar automaticamente en la lista
            }
            productsViewModel.RefreshList();
            this.IsRunning = false;
            this.isEnabled = true;
            await App.Navigator.PopAsync();
            */

        }

        #endregion

    }
}
