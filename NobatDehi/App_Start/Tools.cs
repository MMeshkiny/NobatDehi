using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NobatDehi
{
    public static class Tools
    {

        static public DateTime GorgFromJalali(DateTime d)
        {
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            return calendar.ToDateTime(d.Year, d.Month, d.Day, 0, 0, 0, 0);
        }

        static public DateTime JalaliFromGorg(DateTime d)
        {
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            var year = calendar.GetYear(d);
            var month = calendar.GetMonth(d);
            var day = calendar.GetDayOfMonth(d);
            return new DateTime(year, month, day, d.Hour, d.Minute, d.Second);
        }

        static public string TimeOnly(DateTime d)
        {
            return d.ToShortTimeString();
        }

        public static void NotifyToUser(string UserId)
        { }
    }
    [Authorize()]
    public abstract class UserController : Controller { }
    [Authorize(Roles = "Admin")]
    public abstract class AdminController : Controller { }
    [Authorize(Roles = "Doctor")]
    public abstract class DoctorController : Controller { }
}