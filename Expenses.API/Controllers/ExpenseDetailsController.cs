using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Expenses.Common.Models;
using Expenses.Domain.Models;

namespace Expenses.API.Controllers
{
    public class ExpenseDetailsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ExpenseDetails
        public IQueryable<ExpenseDetail> GetExpenseDetails()
        {
            return db.ExpenseDetails
                .Include(i => i.Expense)
                .Include(i => i.Currency)
                .Include(i => i.DocumentType)
                .Include(i => i.ExpenseType)
                .Include(i => i.PaymentType)
                .Include(i => i.Vendor);
        }

        // GET: api/ExpenseDetails/5
        [ResponseType(typeof(ExpenseDetail))]
        public async Task<IHttpActionResult> GetExpenseDetail(int id)
        {
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            if (expenseDetail == null)
            {
                return NotFound();
            }

            return Ok(expenseDetail);
        }

        // PUT: api/ExpenseDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExpenseDetail(int id, ExpenseDetail expenseDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expenseDetail.ExpenseDetailId)
            {
                return BadRequest();
            }

            db.Entry(expenseDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ExpenseDetails
        [ResponseType(typeof(ExpenseDetail))]
        public async Task<IHttpActionResult> PostExpenseDetail(ExpenseDetail expenseDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExpenseDetails.Add(expenseDetail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = expenseDetail.ExpenseDetailId }, expenseDetail);
        }

        // DELETE: api/ExpenseDetails/5
        [ResponseType(typeof(ExpenseDetail))]
        public async Task<IHttpActionResult> DeleteExpenseDetail(int id)
        {
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            if (expenseDetail == null)
            {
                return NotFound();
            }

            db.ExpenseDetails.Remove(expenseDetail);
            await db.SaveChangesAsync();

            return Ok(expenseDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExpenseDetailExists(int id)
        {
            return db.ExpenseDetails.Count(e => e.ExpenseDetailId == id) > 0;
        }
    }
}