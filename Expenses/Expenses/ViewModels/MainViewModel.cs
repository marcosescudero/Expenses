namespace Expenses.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        #endregion

        #region Properties

        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public ExpensesViewModel Expenses { get; set; }
        public EditExpenseViewModel EditExpense { get; set; }
        public CurrenciesViewModel Currencies { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
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

    }
}
