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
    public class ExpenseTypesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ExpenseTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.ExpenseTypes.ToListAsync());
        }

        // GET: ExpenseTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseType expenseType = await db.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return HttpNotFound();
            }
            return View(expenseType);
        }

        // GET: ExpenseTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExpenseTypeId,Description")] ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                db.ExpenseTypes.Add(expenseType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(expenseType);
        }

        // GET: ExpenseTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseType expenseType = await db.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return HttpNotFound();
            }
            return View(expenseType);
        }

        // POST: ExpenseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExpenseTypeId,Description")] ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenseType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(expenseType);
        }

        // GET: ExpenseTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseType expenseType = await db.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return HttpNotFound();
            }
            return View(expenseType);
        }

        // POST: ExpenseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ExpenseType expenseType = await db.ExpenseTypes.FindAsync(id);
            db.ExpenseTypes.Remove(expenseType);
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
