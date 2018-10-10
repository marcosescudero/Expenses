namespace Expenses.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class ExpenseType
    {
        [Key]
        public int ExpenseTypeId { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<ExpenseDetail> ExpenseDetails { get; set; }
    }
}
