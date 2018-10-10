namespace Expenses.Models
{
    using SQLite;
    using System;
    [Table("ExpenseType")]
    public class ExpenseTypeLocal
    {
        public int ExpenseTypeId { get; set; }
        public string Description { get; set; }
    }
}
