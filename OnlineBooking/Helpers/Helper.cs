using Microsoft.AspNetCore.Mvc;
using OnlineBooking.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBooking.Helpers
{
    public class Helper
    {
        public static DateTime GetSonrakiGun(DayOfWeek day, DateTime baslangic)
        {
            int daysToAdd = ((int)day - (int)baslangic.DayOfWeek + 7) % 7;
            return baslangic.AddDays(daysToAdd);
        }

        public static string ToBase64(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public static string FromBase64(string base64)
        {
            if (base64 == null)
            {
                return String.Empty;
            }
            var bytes = Convert.FromBase64String(base64);
            var text = Encoding.UTF8.GetString(bytes);
            return text;
        }
    }
}
