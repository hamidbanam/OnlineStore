using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Generators
{
    public static class ConvertDate
    {

        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar persian = new PersianCalendar();
            string persianDate = persian.GetYear(date) + "/"
                + persian.GetMonth(date).ToString("00") + "/"
                + persian.GetDayOfMonth(date).ToString("00");
            return persianDate;
        }

        public static int ToShamsiDay(this DateTime date)
        {
            PersianCalendar persian = new PersianCalendar();
            int persianDate = persian.GetDayOfMonth(date);
            return persianDate;
        } 
        public static int ToShamsiYear(this DateTime date)
        {
            PersianCalendar persian = new PersianCalendar();
            int persianDate = persian.GetYear(date);
            return persianDate;
        }  
        public static int ToShamsiMonth(this DateTime date)
        {
            PersianCalendar persian = new PersianCalendar();
            int persianDate = persian.GetMonth(date);
   
            return persianDate;
        }

        public static string GetPersianMonthName(this int month)
        {
            CultureInfo persianCulture = new CultureInfo("fa-IR");
            return persianCulture.DateTimeFormat.GetMonthName(month);
        }

    }
}
