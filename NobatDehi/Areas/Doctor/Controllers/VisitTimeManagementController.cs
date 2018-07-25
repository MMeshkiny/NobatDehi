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
            var userId = User.Identity.GetUserId();
            var user = db.Users.First(x => x.Id == userId);
            ViewBag.MedicalCenter = new SelectList(db.MedicalCenters, "Id", "Name");
            var specialties = new SelectList(user.DoctorSpecialties.Select(x => x.Specialty), "Id", "Title").ToList();
            specialties.Insert(0, new SelectListItem() { Text = "همه موارد", Value = "0" });
            ViewBag.Spetiality = specialties;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "MedicalCenter,StartDateTime,Duration,Break,Count,Fee,CancelRate,Spetiality")] CreateVisitTimeViewModel model)
        {
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.DoctorMedicalCenters.AddOrUpdate(x => new { x.DoctorId, x.MedicalCenterId }, new DoctorMedicalCenter
                {
                    MedicalCenterId = model.MedicalCenter,
                    DoctorId = userId
                });

                var visitRecord = db.VisitRecords.Add(new VisitRecord()
                {
                    CancelRate = model.CancelRate,
                    Count = model.Count,
                    DoctorId = userId,
                    Fee = model.Fee,
                    MedicalCenterId = model.MedicalCenter,
                    Removed = 0,
                    Reserved = 0,
                    Start = model.StartDateTime,
                    SpecialtyId = model.Spetiality == 0 ? null : model.Spetiality,
                    Break=model.Break,
                    Duration=model.Duration
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
                        DoctorMedicalCenter = doctorMedicalCenter,
                        VisitRecordId = visitRecord.Id
                    });
                }


                //db.Entry(medicalCenter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Panel");
            }
            var user = db.Users.First(x => x.Id == userId);
            ViewBag.MedicalCenter = new SelectList(db.MedicalCenters, "Id", "Name");
            var specialties = new SelectList(user.DoctorSpecialties.Select(x => x.Specialty), "Id", "Title").ToList();
            specialties.Insert(0, new SelectListItem() { Text = "همه موارد", Value = "0" });
            ViewBag.Spetiality = specialties;
            return View(model);
        }
    }
}