﻿namespace Expenses.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class MainViewModel
    {
        #region Attributes
        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public CurrenciesViewModel Currencies { get; set; }
        public DocumentTypesViewModel DocumentTypes { get; set; }
        public PaymentTypesViewModel PaymentTypes { get; set; }
        public ExpenseTypesViewModel ExpenseTypes { get; set; }
        public VendorsViewModel Vendors { get; set; }
        public RequestsViewModel Requests { get; set; }
        public ExpensesViewModel Expenses { get; set; }
        public EditExpenseViewModel EditExpense { get; set; }
        public AddExpenseViewModel AddExpense { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.LoadMenu();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance; // Atributo
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_info",
                PageName = "AboutPage",
                Title = Languages.About,
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_phonelink_setup",
                PageName = "SetupPage",
                Title = Languages.Setup,
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = Languages.Exit,
            });
        }
        #endregion


    }
}
