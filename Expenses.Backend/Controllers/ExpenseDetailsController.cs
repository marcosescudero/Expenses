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
    public class ExpenseDetailsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ExpenseDetails
        public async Task<ActionResult> Index()
        {
            var expenseDetails = db.ExpenseDetails.Include(e => e.DocumentType).Include(e => e.Expense).Include(e => e.ExpenseType).Include(e => e.PaymentType).Include(e => e.Vendor);
            return View(await expenseDetails.ToListAsync());
        }

        // GET: ExpenseDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            if (expenseDetail == null)
            {
                return HttpNotFound();
            }
            return View(expenseDetail);
        }

        // GET: ExpenseDetails/Create
        public ActionResult Create()
        {
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description");
            ViewBag.ExpenseId = new SelectList(db.Expenses, "ExpenseId", "Description");
            ViewBag.ExpenseTypeId = new SelectList(db.ExpenseTypes, "ExpenseTypeId", "Description");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "Description");
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name");
            return View();
        }

        // POST: ExpenseDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExpenseDetailId,ExpenseId,ExpenseDate,VendorId,ExpenseTypeId,PaymentTypeId,DocumentTypeId,DocumentNumber,Amount,AmountIVA,AmountPercepcion,TotalAmount,Comments,ImagePath")] ExpenseDetail expenseDetail)
        {
            if (ModelState.IsValid)
            {
                db.ExpenseDetails.Add(expenseDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description", expenseDetail.DocumentTypeId);
            ViewBag.ExpenseId = new SelectList(db.Expenses, "ExpenseId", "Description", expenseDetail.ExpenseId);
            ViewBag.ExpenseTypeId = new SelectList(db.ExpenseTypes, "ExpenseTypeId", "Description", expenseDetail.ExpenseTypeId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "Description", expenseDetail.PaymentTypeId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", expenseDetail.VendorId);
            return View(expenseDetail);
        }

        // GET: ExpenseDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            if (expenseDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description", expenseDetail.DocumentTypeId);
            ViewBag.ExpenseId = new SelectList(db.Expenses, "ExpenseId", "Description", expenseDetail.ExpenseId);
            ViewBag.ExpenseTypeId = new SelectList(db.ExpenseTypes, "ExpenseTypeId", "Description", expenseDetail.ExpenseTypeId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "Description", expenseDetail.PaymentTypeId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", expenseDetail.VendorId);
            return View(expenseDetail);
        }

        // POST: ExpenseDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExpenseDetailId,ExpenseId,ExpenseDate,VendorId,ExpenseTypeId,PaymentTypeId,DocumentTypeId,DocumentNumber,Amount,AmountIVA,AmountPercepcion,TotalAmount,Comments,ImagePath")] ExpenseDetail expenseDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenseDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Description", expenseDetail.DocumentTypeId);
            ViewBag.ExpenseId = new SelectList(db.Expenses, "ExpenseId", "Description", expenseDetail.ExpenseId);
            ViewBag.ExpenseTypeId = new SelectList(db.ExpenseTypes, "ExpenseTypeId", "Description", expenseDetail.ExpenseTypeId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "PaymentTypeId", "Description", expenseDetail.PaymentTypeId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", expenseDetail.VendorId);
            return View(expenseDetail);
        }

        // GET: ExpenseDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            if (expenseDetail == null)
            {
                return HttpNotFound();
            }
            return View(expenseDetail);
        }

        // POST: ExpenseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            db.ExpenseDetails.Remove(expenseDetail);
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
