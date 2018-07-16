using NobatDehi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace NobatDehi.Areas.Doctor.Controllers
{
    public class PanelController : DoctorController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Doctor/Panel
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var visitTimes = db.VisitTimes.Where(x => x.DoctorMedicalCenter.DoctorId == userId && x.Start.Value>=DateTime.Today).OrderBy(x => x.Start).ToList() ;
            return View(visitTimes);
        }
    }
}