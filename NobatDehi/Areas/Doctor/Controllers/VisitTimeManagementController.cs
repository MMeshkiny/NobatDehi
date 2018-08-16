using NobatDehi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Data.Metadata.Edm;

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
                    Break = model.Break,
                    Duration = model.Duration,
                    State = VisitRecordState.Active

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
                        VisitRecordId = visitRecord.Id,
                        State = VisitTimeState.Active
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

        public ActionResult AddSurgery()
        {
            ViewBag.MedicalCenter = new SelectList(db.MedicalCenters, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSurgery([Bind(Include = "MedicalCenter,StartDateTime,Duration")]
            CreateVisitTimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var end = model.StartDateTime.AddMinutes(model.Duration);
                var confilictedVisitTimes = db.VisitTimes.Where(x =>
                    (x.Start.HasValue && x.VisitDurationMin.HasValue && DbFunctions.AddMinutes(x.Start, x.VisitDurationMin.Value) >= model.StartDateTime && x.Start<model.StartDateTime) 
                    || (x.Start.HasValue && x.Start <= end && DbFunctions.AddMinutes(x.Start, x.VisitDurationMin.Value)>end));
                model.ConfilictedVisitTimes = confilictedVisitTimes.ToList();
                //RedirectToAction("AddSurgeryConfirm",model);
                if (model.ConfilictedVisitTimes.Count > 0)
                {
                    return View("AddSurgeryConfirm", model);
                }

                return await AddSurgeryConfirm(model);
            }
            ViewBag.MedicalCenter = new SelectList(db.MedicalCenters, "Id", "Name");
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSurgeryConfirm([Bind(Include = "MedicalCenter,StartDateTime,Duration,ConfilictedVisitTimes")] CreateVisitTimeViewModel model)
        {
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                if (model.ConfilictedVisitTimes!=null && model.ConfilictedVisitTimes.Count>0)
                {
                    foreach (var visitTime in model.ConfilictedVisitTimes)
                    {
                        if (visitTime.State==VisitTimeState.Reserved)
                        {
                            Tools.NotifyToUser(visitTime.PatientUserId);
                        }
                        visitTime.State = VisitTimeState.DoctorCancel;
                    }
                }

                db.DoctorMedicalCenters.AddOrUpdate(x => new { x.DoctorId, x.MedicalCenterId }, new DoctorMedicalCenter
                {
                    MedicalCenterId = model.MedicalCenter,
                    DoctorId = userId
                });

                var visitRecord = db.VisitRecords.Add(new VisitRecord()
                {
                    CancelRate = 0,
                    Count = 1,
                    DoctorId = userId,
                    Fee = 0,
                    MedicalCenterId = model.MedicalCenter,
                    Removed = 0,
                    Reserved = 0,
                    Start = model.StartDateTime,
                    SpecialtyId = null,
                    Break = 0,
                    Duration = model.Duration,
                    State = VisitRecordState.Surgery
                });

                await db.SaveChangesAsync();
                var doctorMedicalCenter = db.DoctorMedicalCenters.First(x => x.DoctorId == userId && x.MedicalCenterId == model.MedicalCenter);

                db.VisitTimes.Add(new VisitTime
                {
                    CancelRate = 0,
                    Fee = 0,
                    Order = 1,
                    Start = model.StartDateTime,
                    VisitDurationMin = (Byte)model.Duration,
                    DoctorMedicalCenter = doctorMedicalCenter,
                    VisitRecordId = visitRecord.Id,
                    State = VisitTimeState.Surgery

                });


                //db.Entry(medicalCenter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Panel");
            }

            return View(model);
        }

    }
}