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
    public class CurrenciesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Currencies
        public async Task<ActionResult> Index()
        {
            return View(await db.Currencies.ToListAsync());
        }

        // GET: Currencies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await db.Currencies.FindAsync(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // GET: Currencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CurrencyId,Description,Symbol")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                db.Currencies.Add(currency);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await db.Currencies.FindAsync(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CurrencyId,Description,Symbol")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currency).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await db.Currencies.FindAsync(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Currency currency = await db.Currencies.FindAsync(id);
            db.Currencies.Remove(currency);
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
