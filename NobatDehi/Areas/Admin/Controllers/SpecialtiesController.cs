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
    public class SpecialtiesController : AdminController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Specialties
        public async Task<ActionResult> Index()
        {
            return View(await db.Specialties.ToListAsync());
        }

        // GET: Admin/Specialties/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = await db.Specialties.FindAsync(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // GET: Admin/Specialties/Create
        public ActionResult Create()
        {
            var specialties = new SelectList(db.Specialties.Where(x=>x.ParentId==null), "Id", "Title").ToList();
            specialties.Insert(0, new SelectListItem() { Text = "هیچکدام", Value = "0" });
            ViewBag.ParentId = specialties;
            return View();
        }

        // POST: Admin/Specialties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,ParentId,Description")] Specialty specialty)
        {

            if (specialty.ParentId == 0)
            {
                specialty.ParentId = null;
            }
            if (ModelState.IsValid)
            {
                db.Specialties.Add(specialty);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var specialties = new SelectList(db.Specialties.Where(x => x.ParentId == null), "Id", "Title").ToList();
            specialties.Insert(0, new SelectListItem() { Text = "هیچکدام", Value = "0" });
            ViewBag.ParentId = specialties;
            return View(specialty);
        }

        // GET: Admin/Specialties/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = await db.Specialties.FindAsync(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            var specialties = new SelectList(db.Specialties.Where(x => x.ParentId == null), "Id", "Title").ToList();
            specialties.Insert(0, new SelectListItem() { Text = "هیچکدام", Value = "0" });
            if (specialty?.ParentId != null)
                specialties.First(x => x.Value == specialty.ParentId.ToString()).Selected = true;
            ViewBag.ParentId = specialties;
            return View(specialty);
        }

        // POST: Admin/Specialties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,ParentId,Description")] Specialty specialty)
        {
            if (specialty.ParentId==0)
            {
                specialty.ParentId = null;
            }
            if (ModelState.IsValid)
            {
                db.Entry(specialty).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var specialties = new SelectList(db.Specialties.Where(x => x.ParentId == null), "Id", "Title").ToList();
            specialties.Insert(0, new SelectListItem() { Text = "هیچکدام", Value = "0" });
            if (specialty?.ParentId != null)
                specialties.First(x => x.Value == specialty.ParentId.ToString()).Selected = true;
            ViewBag.ParentId = specialties;
            return View(specialty);
        }

        // GET: Admin/Specialties/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = await db.Specialties.FindAsync(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Admin/Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Specialty specialty = await db.Specialties.FindAsync(id);
            db.Specialties.Remove(specialty);
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
