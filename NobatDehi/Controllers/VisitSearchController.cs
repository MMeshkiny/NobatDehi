using NobatDehi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace NobatDehi.Controllers
{
    public class VisitSearchController : Controller
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
        int pageLenght = 10;
        // GET: VisitSearch
        public ActionResult Index()
        {
            ViewBag.MedicalCenterId = new SelectList(db.MedicalCenters, "Id", "Name");
            ViewBag.SpetialtyId = new SelectList(db.Specialties, "Id", "Title");
            ViewBag.DoctorId = new SelectList(db.Users.Where(x => x.Roles.Any(y => y.RoleId == "2")), "Id", "LastName");
            VisitTimeSearchViewModel model = new VisitTimeSearchViewModel();
            model.Start = Tools.JalaliFromGorg(model.Start);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(VisitTimeSearchViewModel model)
        {

            model.Start = Tools.GorgFromJalali(model.Start);
            var end = model.Start.AddDays(model.Duration);

            var z = db.VisitRecords
                .Where(x => model.DoctorId == null || model.DoctorId == x.DoctorId)
                .Where(x => model.MedicalCenterId == 0 || model.MedicalCenterId == x.MedicalCenterId)
                .Where(x => model.SpetialtyId == 0 || !x.SpecialtyId.HasValue || model.SpetialtyId == x.SpecialtyId)
                .ToList();

            var result = db.VisitRecords
                .Where(x => model.DoctorId == null || model.DoctorId == x.DoctorId)
                .Where(x => model.MedicalCenterId == 0 || model.MedicalCenterId == x.MedicalCenterId)
                .Where(x => model.SpetialtyId == 0 || !x.SpecialtyId.HasValue || model.SpetialtyId == x.SpecialtyId)
                .Where(x => x.Start >= model.Start && x.Start >= DateTime.Now && x.Start <= end)
                .OrderBy(x => x.Start).Skip((model.Page - 1) * pageLenght)
                .Take(pageLenght).ToList();


            model.Results = result.Select(x => new VisitSearchResultViewModel() { visitRecord = x });
            ViewBag.MedicalCenterId = new SelectList(db.MedicalCenters, "Id", "Name");
            ViewBag.SpetialtyId = new SelectList(db.Specialties, "Id", "Title");
            ViewBag.DoctorId = new SelectList(db.Users.Where(x => x.Roles.Any(y => y.RoleId == "2")), "Id", "LastName");
            model.Start = Tools.JalaliFromGorg(model.Start);
            return View(model);

        }

        public ActionResult Select(int id)
        {
            return View();
        }

        

    }
}