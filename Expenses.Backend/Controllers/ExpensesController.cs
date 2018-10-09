using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Expenses.Backend.Models;
using Expenses.Common.Models;

namespace Expenses.Backend.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Expenses
        public async Task<ActionResult> Index()
        {
            return View(await db.Expenses.ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = await db.Expenses.FindAsync(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExpenseId,Description,Comments,ExpenseDateStart,ExpenseDateEnd,UserId,Approved")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = await db.Expenses.FindAsync(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExpenseId,Description,Comments,ExpenseDateStart,ExpenseDateEnd,UserId,Approved")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = await db.Expenses.FindAsync(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Expense expense = await db.Expenses.FindAsync(id);
            db.Expenses.Remove(expense);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
