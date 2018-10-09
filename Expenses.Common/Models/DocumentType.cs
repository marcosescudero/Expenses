namespace Expenses.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class DocumentType
    {
        [Key]
        public int DocumentTypeId { get; set; }
        public string Description { get; set; }
        [StringLength(3)]
        public string DocumentCode { get; set; }
        [JsonIgnore]
        public virtual ICollection<ExpenseDetail> ExpenseDetails { get; set; }
    }
}
