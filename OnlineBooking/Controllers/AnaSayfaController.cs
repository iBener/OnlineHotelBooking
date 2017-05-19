using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OnlineBooking.Helpers;

namespace OnlineBooking.Controllers
{
    public class AnaSayfaController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Hakkinda()
        {
            ViewData["Message"] = "Online Hotel Booking";

            return View();
        }

        public IActionResult Iletisim()
        {
            ViewData["Message"] = "İletişim bilgileri:";

            return View();
        }

        public IActionResult Hata(string m)
        {
            ViewBag.Mesaj = Helper.FromBase64(m);
            return View();
        }
    }
}
