﻿
namespace Expenses.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Expenses
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Display(Name = "Start")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDateStart { get; set; }

        [Display(Name = "End")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDateEnd { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }

               


        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; } // Siempre conbiene utilizar "decimal" para manejar valores monetarios en C#

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Publish On")]
        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }

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
                //return $"http://200.55.241.235/SalesBackend{this.ImagePath.Substring(1)}"; // el substring es para quitarle el ñuflo
                return $"http://200.55.241.235/SalesAPI{this.ImagePath.Substring(1)}"; // el substring es para quitarle el ñuflo
            }
        }

        public override string ToString()
        {
            return this.Description;
        }

    }
}