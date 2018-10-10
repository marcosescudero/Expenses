namespace Expenses.Models
{
    using Common.Models;
    using SQLite;
    using System;

    [Table("Expense")]
    public class ExpenseLocal
    {
        public int ExpenseId { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public DateTime ExpenseDateStart { get; set; }
        public DateTime ExpenseDateEnd { get; set; }
        public int UserId { get; set; }
        public bool Approved { get; set; }
    }
}
