namespace Expenses.Models
{
    using SQLite;
    using System;

    [Table("PaymentType")]
    public class PaymentTypeLocal
    {
        public int PaymentTypeId { get; set; }
        public string Description { get; set; }
    }
}
