
namespace Expenses.Domain.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Common.Models;
    public class DataContext : DbContext
    {
        public DataContext() :base("DefaultConnection")
        {
        }
    }
}
