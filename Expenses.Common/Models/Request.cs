namespace Expenses.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [Display(Name = "Date Start")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDateStart { get; set; }

        [Display(Name = "Date End")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDateEnd { get; set; }

        [Display(Name = "User")]
        public int UserId { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }

        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; }

        public override string ToString()
        {
            return this.Description;
        }

    }
}
