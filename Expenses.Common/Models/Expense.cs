namespace Expenses.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Display(Name = "Request")]
        public int RequestId { get; set; }

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

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        [Display(Name = "Neto")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        [Display(Name = "IVA")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal AmountIVA { get; set; }

        [Display(Name = "Percepciones")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal AmountPercepcion { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal TotalAmount { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        // Cuando tengo atributos que forman parte del modelo, PERO que no formen parte de la base de datos, se coloca [NotMapped]
        [NotMapped]
        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noimage";
                }
                return $"http://200.55.241.235/ExpensesAPI{this.ImagePath.Substring(1)}"; // el substring es para quitarle el ñuflo
            }
        }

        public virtual Request Request { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual Currency Currency { get; set; }

        public override string ToString()
        {
            return this.Comments;
        }


    }
}
