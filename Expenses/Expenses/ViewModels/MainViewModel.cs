namespace Expenses.ViewModels
{
    using Helpers;
    using System.Collections.ObjectModel;
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
        public RequestsViewModel Requests { get; set; }
        public ExpensesViewModel Expenses { get; set; }

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
