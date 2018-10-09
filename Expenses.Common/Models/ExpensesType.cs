namespace Expenses.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class ExpensesType
    {
        [Key]
        public int ExpenseTypeId { get; set; }
        public string Description { get; set; }
    }
}
