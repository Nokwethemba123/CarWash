using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarWash.Models;

namespace CarWash.Controllers
{
    public class WashesController : Controller
    {
        private WashDbContext db = new WashDbContext();

        // GET: Washes
        public ActionResult Index()
        {
            return View(db.Washes.ToList());
        }

        // GET: Washes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wash wash = db.Washes.Find(id);
            if (wash == null)
            {
                return HttpNotFound();
            }
            return View(wash);
        }

        // GET: Washes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Washes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WashId,WashName,Price")] Wash wash)
        {
            if (ModelState.IsValid)
            {
                db.Washes.Add(wash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wash);
        }

        // GET: Washes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wash wash = db.Washes.Find(id);
            if (wash == null)
            {
                return HttpNotFound();
            }
            return View(wash);
        }

        // POST: Washes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WashId,WashName,Price")] Wash wash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wash);
        }

        // GET: Washes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wash wash = db.Washes.Find(id);
            if (wash == null)
            {
                return HttpNotFound();
            }
            return View(wash);
        }

        // POST: Washes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wash wash = db.Washes.Find(id);
            db.Washes.Remove(wash);
            db.SaveChanges();
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
