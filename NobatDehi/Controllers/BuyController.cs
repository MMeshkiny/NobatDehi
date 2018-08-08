using NobatDehi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace NobatDehi.Controllers
{
    public class BuyController : UserController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult PreFactor(int id)
        {
            var visitTime = db.VisitTimes.Find(id);
            if (visitTime == null)
            {
                return new HttpNotFoundResult();
            }

            var trackingCode = Guid.NewGuid().ToString();
            db.Factors.Add(new Factor()
            {
                Fee = visitTime.Fee,
                PatientUserId = User.Identity.GetUserId(),
                PayedDateTime = null,
                PreFactorDateTime = DateTime.Now,
                VisitTimeId = id,
                TrackingCode = trackingCode
            });
            visitTime.PatientUserId = User.Identity.GetUserId();
            visitTime.ReserveDateTime = DateTime.Now;
            visitTime.VisitRecord.Reserved++;
            db.SaveChanges();
            PayViewModel viewModel = new PayViewModel()
            {
                Fee = visitTime.Fee,
                Tracking = trackingCode,
                PayStatus = null
            };
            return RedirectToAction("Pay", "Fake", viewModel);
        }


        public ActionResult BankCallBack(PayViewModel model)
        {
            //todo: maybe duplicated guid occure
            Factor factor = db.Factors.SingleOrDefault(x => x.TrackingCode == model.Tracking);
            if (factor == null)
            {
                return HttpNotFound();
            }
            factor.PayedDateTime = DateTime.Now;
            factor.VisitTime.PayDateTime = DateTime.Now;
            db.PayFactors.Add(new PayFactor
            {
                Fee = factor.Fee,
                PayDateTime = DateTime.Now,
                CreateDateTime = DateTime.Now,
                DoctorMedicalCenterId = factor.VisitTime.DoctorMedicalCenterId,
                TracingCode = factor.TrackingCode
            });
            db.SaveChanges();
            return View(model);
        }
    }
}