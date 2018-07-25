using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}