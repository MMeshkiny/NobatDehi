﻿using System.Web.Mvc;

namespace NobatDehi.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller="Panel", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
    [Authorize(Roles = "Admin")]
    public abstract class AdminController : Controller { }
}