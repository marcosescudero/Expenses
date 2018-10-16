
namespace Expenses.API.Controllers
{
    using Expenses.Common.Models;
    using Expenses.Domain.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [Authorize]
    public class ExpensesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Expenses
        public IQueryable<Expense> GetExpenses()
        {
            return db.Expenses
                .Include(p => p.Currency)
                .Include(p => p.PaymentType)
                .Include(p => p.DocumentType)
                .Include(p => p.ExpenseType)
                .Include(p => p.Vendor)
                .Include(p => p.Request);
        }

        // GET: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public async Task<IHttpActionResult> GetExpense(int id)
        {
            Expense expense = await db.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        // PUT: api/Expenses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExpense(int id, Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            if (expense.ImageArray != null && expense.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(expense.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/images";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    expense.ImagePath = fullPath;
                }
            }


            db.Entry(expense).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok(expense); // Devuelve el expense tal cual quedó en la base de datos.!!
        }

        // POST: api/Expenses
        [ResponseType(typeof(Expense))]
        public async Task<IHttpActionResult> PostExpense(Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (expense.ImageArray != null && expense.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(expense.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/images";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    expense.ImagePath = fullPath;
                }
            }


            db.Expenses.Add(expense);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = expense.ExpenseId }, expense);
        }

        // DELETE: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public async Task<IHttpActionResult> DeleteExpense(int id)
        {
            Expense expense = await db.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            db.Expenses.Remove(expense);
            await db.SaveChangesAsync();

            return Ok(expense); // Devuelve el expense tal cual quedó en la base de datos.!!
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExpenseExists(int id)
        {
            return db.Expenses.Count(e => e.ExpenseId == id) > 0;
        }
    }
}