namespace Expenses.Models
{
    using SQLite;
    using System;

    [Table("Expense")]
    public class ExpenseLocal
    {
        public int ExpenseDetailId { get; set; }

        public int ExpenseId { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int VendorId { get; set; }

        public int ExpenseTypeId { get; set; }

        public int PaymentTypeId { get; set; }

        public int DocumentTypeId { get; set; }

        public string DocumentNumber { get; set; }

        public decimal Amount { get; set; }

        public decimal AmountIVA { get; set; }

        public decimal AmountPercepcion { get; set; }

        public decimal TotalAmount { get; set; }

        public string Comments { get; set; }

        public string ImagePath { get; set; }

        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noproduct";
                }
                return $"http://200.55.241.235/ExpensesAPI{this.ImagePath.Substring(1)}"; // el substring es para quitarle el ñuflo
            }
        }
    }
}
