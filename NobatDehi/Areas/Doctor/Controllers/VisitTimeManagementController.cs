using NobatDehi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace NobatDehi.Areas.Doctor.Controllers
{
    public class VisitTimeManagementController : DoctorController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Doctor/VisitTimeManagement
        public ActionResult Add()
        {
            ViewBag.MedicalCenter = new SelectList(db.MedicalCenters, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "MedicalCenter,StartDateTime,Duration,Break,Count,Fee,CancelRate")] CreateVisitTimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                db.DoctorMedicalCenters.AddOrUpdate(x => new { x.DoctorId, x.MedicalCenterId }, new DoctorMedicalCenter
                {
                    MedicalCenterId = model.MedicalCenter,
                    DoctorId = userId
                });
                await db.SaveChangesAsync();
                var doctorMedicalCenter = db.DoctorMedicalCenters.First(x => x.DoctorId == userId && x.MedicalCenterId == model.MedicalCenter);

                for (int i = 0; i < model.Count; i++)
                {
                    db.VisitTimes.Add(new VisitTime
                    {
                        CancelRate = model.CancelRate,
                        Fee = model.Fee,
                        Order = i + 1,
                        Start = model.StartDateTime.AddMinutes(i * (model.Duration + model.Break)),
                        VisitDurationMin = (Byte)model.Duration,
                        DoctorMedicalCenter = doctorMedicalCenter
                    });
                }


                //db.Entry(medicalCenter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Panel");
            }
            ViewBag.MedicalCenter = new SelectList(db.MedicalCenters, "Id", "Name");
            return View(model);
        }
    }
}