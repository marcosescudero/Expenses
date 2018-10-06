namespace Expenses.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        #endregion

        #region Properties
        public ExpensesViewModel Expenses { get; set; }
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
