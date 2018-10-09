namespace Expenses.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class DocumentsType
    {
        [Key]
        public int DocumentTypeId { get; set; }
        public string Description { get; set; }
        [StringLength(3)]
        public string DocumentCode { get; set; }
    }
}
