
namespace Expenses.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vendors
    {
        [Key]
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        [StringLength(13)]
        public string Cuit { get; set; }
        public override string ToString()
        {
            return this.Alias;
        }
    }
}
