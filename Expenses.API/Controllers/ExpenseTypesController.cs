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
    public class ExpenseTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ExpenseTypes
        public IQueryable<ExpenseType> GetExpenseTypes()
        {
            return db.ExpenseTypes;
        }

        // GET: api/ExpenseTypes/5
        [ResponseType(typeof(ExpenseType))]
        public async Task<IHttpActionResult> GetExpenseType(int id)
        {
            ExpenseType expenseType = await db.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return Ok(expenseType);
        }

        // PUT: api/ExpenseTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExpenseType(int id, ExpenseType expenseType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expenseType.ExpenseTypeId)
            {
                return BadRequest();
            }

            db.Entry(expenseType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseTypeExists(id))
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

        // POST: api/ExpenseTypes
        [ResponseType(typeof(ExpenseType))]
        public async Task<IHttpActionResult> PostExpenseType(ExpenseType expenseType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExpenseTypes.Add(expenseType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = expenseType.ExpenseTypeId }, expenseType);
        }

        // DELETE: api/ExpenseTypes/5
        [ResponseType(typeof(ExpenseType))]
        public async Task<IHttpActionResult> DeleteExpenseType(int id)
        {
            ExpenseType expenseType = await db.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }

            db.ExpenseTypes.Remove(expenseType);
            await db.SaveChangesAsync();

            return Ok(expenseType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExpenseTypeExists(int id)
        {
            return db.ExpenseTypes.Count(e => e.ExpenseTypeId == id) > 0;
        }
    }
}