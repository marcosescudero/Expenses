namespace Expenses.Models
{
    using SQLite;

    [Table("DocumentType")]
    public class DocumentTypeLocal
    {
        public int DocumentTypeId { get; set; }
        public string Description { get; set; }
        public string DocumentCode { get; set; }
    }
}
