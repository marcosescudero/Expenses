﻿
namespace Expenses.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }
        public static string Products
        {
            get { return Resource.Products; }
        }
        public static string TurnOnInternet
        {
            get { return Resource.TurnOnInternet; }
        }
        public static string AddProduct
        {
            get { return Resource.AddProduct; }
        }
        public static string Description
        {
            get { return Resource.Description; }
        }
        public static string DescriptionPlaceholder
        {
            get { return Resource.DescriptionPlaceholder; }
        }
        public static string Price
        {
            get { return Resource.Price; }
        }
        public static string PricePlaceholder
        {
            get { return Resource.PricePlaceholder; }
        }
        public static string Remarks
        {
            get { return Resource.Remarks; }
        }
        public static string Save
        {
            get { return Resource.Save; }
        }
        public static string ChangeImage
        {
            get { return Resource.ChangeImage; }
        }
        public static string DescriptionError
        {
            get { return Resource.DescriptionError; }
        }
        public static string PriceError
        {
            get { return Resource.PriceError; }
        }
        public static string ImageSource
        {
            get { return Resource.ImageSource; }
        }

        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }

        public static string NewPicture
        {
            get { return Resource.NewPicture; }
        }

        public static string Cancel
        {
            get { return Resource.Cancel; }
        }
        public static string Delete
        {
            get { return Resource.Delete; }
        }

        public static string Edit
        {
            get { return Resource.Edit; }
        }

        public static string DeleteConfirmation
        {
            get { return Resource.DeleteConfirmation; }
        }

        public static string Yes
        {
            get { return Resource.Yes; }
        }

        public static string No
        {
            get { return Resource.No; }
        }
        public static string Confirm
        {
            get { return Resource.Confirm; }
        }
        public static string EditExpense
        {
            get { return Resource.EditExpense; }
        }
        public static string IsAvailable
        {
            get { return Resource.IsAvailable; }
        }
        public static string Search
        {
            get { return Resource.Search; }
        }

        public static string Login
        {
            get { return Resource.Login; }
        }

        public static string EMail
        {
            get { return Resource.EMail; }
        }

        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }

        public static string Password
        {
            get { return Resource.Password; }
        }

        public static string PasswordPlaceHolder
        {
            get { return Resource.PasswordPlaceHolder; }
        }

        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }

        public static string Forgot
        {
            get { return Resource.Forgot; }
        }

        public static string Register
        {
            get { return Resource.Register; }
        }

        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }

        public static string PasswordValidation
        {
            get { return Resource.PasswordValidation; }
        }

        public static string SomethingWrong
        {
            get { return Resource.SomethingWrong; }
        }

        public static string Menu
        {
            get { return Resource.Menu; }
        }

        public static string Setup
        {
            get { return Resource.Setup; }
        }

        public static string About
        {
            get { return Resource.About; }
        }

        public static string Exit
        {
            get { return Resource.Exit; }
        }
        public static string NoExpensesMessage
        {
            get { return Resource.NoExpensesMessage; }
        }

        public static string FirstName
        {
            get { return Resource.FirstName; }
        }

        public static string FirstNamePlaceholder
        {
            get { return Resource.FirstNamePlaceholder; }
        }

        public static string LastName
        {
            get { return Resource.LastName; }
        }

        public static string LastNamePlaceholder
        {
            get { return Resource.LastNamePlaceholder; }
        }

        public static string Phone
        {
            get { return Resource.Phone; }
        }

        public static string PhonePlaceHolder
        {
            get { return Resource.PhonePlaceHolder; }
        }

        public static string PasswordConfirm
        {
            get { return Resource.PasswordConfirm; }
        }

        public static string PasswordConfirmPlaceHolder
        {
            get { return Resource.PasswordConfirmPlaceHolder; }
        }

        public static string Address
        {
            get { return Resource.Address; }
        }

        public static string AddressPlaceHolder
        {
            get { return Resource.AddressPlaceHolder; }
        }

        public static string FirstNameError
        {
            get { return Resource.FirstNameError; }
        }

        public static string LastNameError
        {
            get { return Resource.LastNameError; }
        }

        public static string EMailError
        {
            get { return Resource.EMailError; }
        }

        public static string PhoneError
        {
            get { return Resource.PhoneError; }
        }

        public static string PasswordError
        {
            get { return Resource.PasswordError; }
        }

        public static string PasswordConfirmError
        {
            get { return Resource.PasswordConfirmError; }
        }

        public static string PasswordsNoMatch
        {
            get { return Resource.PasswordsNoMatch; }
        }

        public static string RegisterConfirmation
        {
            get { return Resource.RegisterConfirmation; }
        }

        public static string NoExpenseTypesMessage
        {
            get { return Resource.NoExpenseTypesMessage; }
        }

        public static string NoDocumentTypesMessage
        {
            get { return Resource.NoDocumentTypesMessage; }
        }

        public static string NoCurrenciesMessage
        {
            get { return Resource.NoCurrenciesMessage; }
        }

        public static string NoVendorsMessage
        {
            get { return Resource.NoVendorsMessage; }
        }

        public static string NoPaymentTypesMessage
        {
            get { return Resource.NoPaymentTypesMessage; }
        }
        public static string NotImplemented
        {
            get { return Resource.NotImplemented; }
        }
        public static string Atention
        {
            get { return Resource.Atention; }
        }
        public static string ExpensesRequests
        {
            get { return Resource.ExpensesRequests; }
        }
        public static string ExpenseDate
        {
            get { return Resource.ExpenseDate; }
        }
        public static string Sync
        {
            get { return Resource.Sync; }
        }
        public static string NoRequestMessage
        {
            get { return Resource.NoRequestMessage; }
        }
        public static string Select
        {
            get { return Resource.Select; }
        }
        public static string DateRangeError
        {
            get { return Resource.DateRangeError; }
        }
        public static string DocumentNumberError
        {
            get { return Resource.DocumentNumberError; }
        }
        public static string AmountInvalid
        {
            get { return Resource.AmountInvalid; }
        }
        public static string AmountIvaInvalid
        {
            get { return Resource.AmountIvaInvalid; }
        }
        public static string AmountPercepcionInvalid
        {
            get { return Resource.AmountPercepcionInvalid; }
        }
        public static string DataSaved
        {
            get { return Resource.DataSaved; }
        }
        public static string ExpenseType
        {
            get { return Resource.ExpenseType; }
        }
        public static string Vendor
        {
            get { return Resource.Vendor; }
        }
        public static string DocumentType
        {
            get { return Resource.DocumentType; }
        }
        public static string DocumentNumber
        {
            get { return Resource.DocumentNumber; }
        }
        public static string PaymentType
        {
            get { return Resource.PaymentType; }
        }
        public static string Currency
        {
            get { return Resource.Currency; }
        }
        public static string Amount
        {
            get { return Resource.Amount; }
        }
        public static string IVA
        {
            get { return Resource.IVA; }
        }
        public static string Perception
        {
            get { return Resource.Perception; }
        }
        public static string TotalAmount
        {
            get { return Resource.TotalAmount; }
        }
        public static string Comments
        {
            get { return Resource.Comments; }
        }
        public static string AddExpense
        {
            get { return Resource.AddExpense; }
        }

    }
}
