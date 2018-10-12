namespace Expenses.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        [StringLength(3)]
        public string Symbol { get; set; }

        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; }

    }
}
