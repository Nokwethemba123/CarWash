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
    public class ServicesController : Controller
    {
        private WashDbContext db = new WashDbContext();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Vehicle).Include(s => s.Wash);
            return View(services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "VehicleName");
            ViewBag.WashId = new SelectList(db.Washes, "WashId", "WashName");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,CustomerName,LicenseExpDate,WashId,VehicleId,WashTypeCost,VehicleTypeCost,TotalCost")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.WashTypeCost = service.pullWashTypeCost();
                service.VehicleTypeCost = service.pullVehicleTypeCost();
                service.TotalCost = service.calcTotalCost();
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "VehicleName", service.VehicleId);
            ViewBag.WashId = new SelectList(db.Washes, "WashId", "WashName", service.WashId);
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "VehicleName", service.VehicleId);
            ViewBag.WashId = new SelectList(db.Washes, "WashId", "WashName", service.WashId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,CustomerName,LicenseExpDate,WashId,VehicleId,WashTypeCost,VehicleTypeCost,TotalCost")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "VehicleName", service.VehicleId);
            ViewBag.WashId = new SelectList(db.Washes, "WashId", "WashName", service.WashId);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
