
namespace Expenses.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        [StringLength(13)]
        public string Cuit { get; set; }
        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; }
        public override string ToString()
        {
            return this.Alias;
        }
    }
}