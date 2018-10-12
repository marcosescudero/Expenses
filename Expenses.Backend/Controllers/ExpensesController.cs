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
    public class ExpensesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Expenses
        public async Task<ActionResult> Index()
        {
            var expenses = db.Expenses.Include(e => e.DocumentType).Include(e => e.ExpenseType).Include(e => e.PaymentType).Include(e => e.Request).Include(e => e.Vendor);
            return View(await expenses.ToListAsync());
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
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description");
            ViewBag.ExpenseTypeId = new SelectList(db.ExpenseTypes, "ExpenseTypeId", "Description");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "Description");
            ViewBag.RequestId = new SelectList(db.Requests, "RequestId", "Description");
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExpenseId,RequestId,ExpenseDate,VendorId,ExpenseTypeId,PaymentTypeId,DocumentTypeId,DocumentNumber,Amount,AmountIVA,AmountPercepcion,TotalAmount,Comments,ImagePath")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description", expense.DocumentTypeId);
            ViewBag.ExpenseTypeId = new SelectList(db.ExpenseTypes, "ExpenseTypeId", "Description", expense.ExpenseTypeId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "Description", expense.PaymentTypeId);
            ViewBag.RequestId = new SelectList(db.Requests, "RequestId", "Description", expense.RequestId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", expense.VendorId);
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
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description", expense.DocumentTypeId);
            ViewBag.ExpenseTypeId = new SelectList(db.ExpenseTypes, "ExpenseTypeId", "Description", expense.ExpenseTypeId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "Description", expense.PaymentTypeId);
            ViewBag.RequestId = new SelectList(db.Requests, "RequestId", "Description", expense.RequestId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", expense.VendorId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExpenseId,RequestId,ExpenseDate,VendorId,ExpenseTypeId,PaymentTypeId,DocumentTypeId,DocumentNumber,Amount,AmountIVA,AmountPercepcion,TotalAmount,Comments,ImagePath")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description", expense.DocumentTypeId);
            ViewBag.ExpenseTypeId = new SelectList(db.ExpenseTypes, "ExpenseTypeId", "Description", expense.ExpenseTypeId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "Description", expense.PaymentTypeId);
            ViewBag.RequestId = new SelectList(db.Requests, "RequestId", "Description", expense.RequestId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", expense.VendorId);
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
