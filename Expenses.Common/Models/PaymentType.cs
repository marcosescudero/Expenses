namespace Expenses.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }
        public string Description { get; set; }

    }
}
