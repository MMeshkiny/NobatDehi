using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NobatDehi.Models;
using EntityState = System.Data.Entity.EntityState;

namespace NobatDehi.Areas.Admin.Controllers
{
    public class MedicalCentersController : AdminController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/MedicalCenters
        public async Task<ActionResult> Index()
        {
            var medicalCenters = db.MedicalCenters.Include(m => m.MedicalCenterType);
            return View(await medicalCenters.ToListAsync());
        }

        // GET: Admin/MedicalCenters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalCenter medicalCenter = await db.MedicalCenters.FindAsync(id);
            if (medicalCenter == null)
            {
                return HttpNotFound();
            }
            return View(medicalCenter);
        }

        // GET: Admin/MedicalCenters/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.MedicalCenterTypes, "Id", "Title");
            return View();
        }

        // POST: Admin/MedicalCenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TypeId,Name,Address,Description,ImageUrl")] MedicalCenter medicalCenter)
        {
            if (ModelState.IsValid)
            {
                db.MedicalCenters.Add(medicalCenter);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.MedicalCenterTypes, "Id", "Title", medicalCenter.TypeId);
            return View(medicalCenter);
        }

        // GET: Admin/MedicalCenters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalCenter medicalCenter = await db.MedicalCenters.FindAsync(id);
            if (medicalCenter == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.MedicalCenterTypes, "Id", "Title", medicalCenter.TypeId);
            return View(medicalCenter);
        }

        // POST: Admin/MedicalCenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TypeId,Name,Address,Description,ImageUrl")] MedicalCenter medicalCenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalCenter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.MedicalCenterTypes, "Id", "Title", medicalCenter.TypeId);
            return View(medicalCenter);
        }

        // GET: Admin/MedicalCenters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalCenter medicalCenter = await db.MedicalCenters.FindAsync(id);
            if (medicalCenter == null)
            {
                return HttpNotFound();
            }
            return View(medicalCenter);
        }

        // POST: Admin/MedicalCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalCenter medicalCenter = await db.MedicalCenters.FindAsync(id);
            db.MedicalCenters.Remove(medicalCenter);
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
