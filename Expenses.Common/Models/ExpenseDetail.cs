namespace Expenses.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ExpenseDetails
    {
        [Key]
        public int ExpenseId { get; set; }

        [Display(Name = "Line")]
        public int LineNumber { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }

        [Display(Name = "Vendor")]
        public int VendorId { get; set; }

        [Display(Name = "Expense Type")]
        public int ExpenseTypeId { get; set; }

        [Display(Name = "Payment Type")]
        public int PaymentTypeId { get; set; }

        [Display(Name = "Document Type")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Document Number")]
        public string DocumentNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal AmountIIBB { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal AmountIVA { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal AmountGanancias { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal AmountPercepcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal TotalAmount { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [NotMapped] // Cuando tengo atributos que forman parte del modelo, PERO que no formen parte de la base de datos, se coloca [NotMapped]
        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noproduct";
                }
                //return $"http://200.55.241.235/ExpensesBackend{this.ImagePath.Substring(1)}"; // el substring es para quitarle el ñuflo
                return $"http://200.55.241.235/ExpensesAPI{this.ImagePath.Substring(1)}"; // el substring es para quitarle el ñuflo
            }
        }

        public override string ToString()
        {
            return this.Comments;
        }


    }
}
