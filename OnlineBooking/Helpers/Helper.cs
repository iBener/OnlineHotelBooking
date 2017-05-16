using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
