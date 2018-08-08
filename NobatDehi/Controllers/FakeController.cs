using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NobatDehi.Models;

namespace NobatDehi.Controllers
{
    public class FakeController : Controller
    {
        // GET: Fake
        public ActionResult Pay(PayViewModel model)
        {
            return View(model);
        }
    }
}