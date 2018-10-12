
namespace Expenses.Domain.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Common.Models;
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }
        public System.Data.Entity.DbSet<Expenses.Common.Models.Currency> Currencies { get; set; }

        public System.Data.Entity.DbSet<Expenses.Common.Models.ExpenseType> ExpenseTypes { get; set; }

        public System.Data.Entity.DbSet<Expenses.Common.Models.DocumentType> DocumentTypes { get; set; }

        public System.Data.Entity.DbSet<Expenses.Common.Models.PaymentType> PaymentTypes { get; set; }

        public System.Data.Entity.DbSet<Expenses.Common.Models.Vendor> Vendors { get; set; }

        public System.Data.Entity.DbSet<Expenses.Common.Models.Request> Requests { get; set; }

        public System.Data.Entity.DbSet<Expenses.Common.Models.Expense> Expenses { get; set; }

    }

}

