using Microsoft.AspNet.Identity.Owin;
using NobatDehi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NobatDehi.Areas.Admin.Controllers
{
    public class DoctorManagementController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Admin/DoctorManagement
        public ActionResult Index()
        {
            var doctors = db.Users.Where(x => x.Roles.Any(y => y.RoleId == "2"));
            return View(doctors);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string userName)
        {
            var doctor = db.Users.FirstOrDefault(x => x.Email.ToLower() == userName.ToLower());
            if (doctor==null)
            {
                ModelState.AddModelError("UserName", "کاربری با این ایمیل وجود ندارد");
                return View();
            }
            await UserManager.AddToRoleAsync(doctor.Id, "Doctor");
            return RedirectToAction("Index");

        }


        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await UserManager.RemoveFromRoleAsync(id, "Doctor");
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Spetialties = db.Specialties.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(user);
        }

        public async Task<ActionResult> AddSpecialty(AddDoctorSpetialtyViewModel model)
        {
            db.DoctorSpecialties.AddOrUpdate(x => new { x.SpecialtyId, x.DoctorId },
                new DoctorSpecialty()
                {
                    DoctorId = model.Id,
                    SpecialtyId = model.Spetialty
                });
            await db.SaveChangesAsync();

            return RedirectToAction("Details", new { id = model.Id });
        }

        public async Task<ActionResult> DeleteSpecialty(int id)
        {
            var doctorSpecialty = db.DoctorSpecialties.FirstOrDefault(x => x.Id == id);
            if (doctorSpecialty == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = doctorSpecialty.DoctorId;
            db.DoctorSpecialties.Remove(doctorSpecialty);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = userId });
        }
    }
}