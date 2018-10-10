namespace Expenses.Models
{
    using SQLite;

    [Table("Currency")]
    public class CurrencyLocal
    {
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        public string Symbol { get; set; }
    }
}
