using System.Web.Mvc;

namespace NobatDehi.Areas.Doctor
{
    public class DoctorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Doctor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Doctor_default",
                "Doctor/{controller}/{action}/{id}",
                new { controller="Panel", action = "Index", id = UrlParameter.Optional }
            );
        }
        
    }
    
}