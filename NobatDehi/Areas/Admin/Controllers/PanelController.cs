using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NobatDehi.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        
        // GET: Admin/Panel
        public ActionResult Index()
        {
            return View();
        }
    }
}