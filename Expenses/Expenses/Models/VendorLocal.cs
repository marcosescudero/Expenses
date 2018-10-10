namespace Expenses.Models
{
    using SQLite;

    [Table("Vendor")]
    public class VendorLocal
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Cuit { get; set; }
    }
}
