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
            var medicalCenterId = new SelectList(db.MedicalCenters, "Id", "Name");
            ViewBag.MedicalCenterId = medicalCenterId;
            ViewBag.SpetialtyId = new SelectList(db.Specialties, "Id", "DisplayTitle");
            ViewBag.DoctorId = new SelectList(db.Users.Where(x => x.Roles.Any(y => y.RoleId == "2")), "Id", "LastName");
            VisitTimeSearchViewModel model = new VisitTimeSearchViewModel();
            //model.Start = Tools.JalaliFromGorg(model.Start);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(VisitTimeSearchViewModel model)
        {

            model.Start = Tools.GorgFromJalali(model.Start);
            var end = model.Start.AddDays(model.Duration);

            //var result = db.VisitRecords
            //    .Where(x=>x.State.HasFlag(VisitRecordState.Active))
            //    .Where(x => model.DoctorId == null || model.DoctorId == x.DoctorId)
            //    .Where(x => model.MedicalCenterId == 0 || model.MedicalCenterId == x.MedicalCenterId)
            //    .Where(x => model.SpetialtyId == 0 || !x.SpecialtyId.HasValue || model.SpetialtyId == x.SpecialtyId)
            //    .Where(x => x.Start >= model.Start && x.Start >= DateTime.Now && x.Start <= end)
            //    .OrderBy(x => x.Start);
            var result = db.VisitRecords
                .Where(x => x.State.HasFlag(VisitRecordState.Active))
                .Where(x => model.DoctorId == null || model.DoctorId == x.DoctorId)
                .Where(x => model.MedicalCenterId == 0 || model.MedicalCenterId == x.MedicalCenterId)
                .Where(x => model.SpetialtyId == 0 || !x.SpecialtyId.HasValue || model.SpetialtyId == x.SpecialtyId)
                .Where(x => x.Start >= model.Start && x.Start <= end)
                .OrderBy(x => x.Start);
            model.ResultCount = result.Count();
            var resultList = result.Skip((model.Page - 1) * pageLenght)
                .Take(pageLenght).ToList();


            model.Results = resultList.Select(x => new VisitSearchResultViewModel() { visitRecord = x });
            ViewBag.MedicalCenterId = new SelectList(db.MedicalCenters, "Id", "Name");
            ViewBag.SpetialtyId = new SelectList(db.Specialties, "Id", "DisplayTitle");
            ViewBag.DoctorId = new SelectList(db.Users.Where(x => x.Roles.Any(y => y.RoleId == "2")), "Id", "LastName");
            //model.Start = Tools.JalaliFromGorg(model.Start);
            return View(model);

        }

        public ActionResult Select(int id)
        {
            VisitRecord visitRecord = db.VisitRecords.Find(id);
            if (visitRecord==null)
            {
                return HttpNotFound();
            }
            
            return View(visitRecord);
        }



    }
}