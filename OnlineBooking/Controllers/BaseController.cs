using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineBooking.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineBooking.Controllers
{
    public class BaseController : Controller
    {
        public VeriTabani VeriTabani { get; private set; }

        public BaseController(IOptions<VeriTabani> ayarlar)
        {
            VeriTabani = ayarlar.Value;
        }

        public IActionResult RedirectToHataMesaji(string mesaj)
        {
            var m = Helper.ToBase64(mesaj);
            return RedirectToAction("Hata", "AnaSayfa", new { m });
        }

        public IActionResult RedirectToHataMesajiAnasayfa(string mesaj)
        {
            var m = Helper.ToBase64(mesaj);
            return RedirectToAction("Index", "AnaSayfa", new { m });
        }

        public int GetKullaniciId()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userId != null && Int32.TryParse(userId.Value, out int kullaniciid))
            {
                return kullaniciid;
            }
            return 0;
        }
    }
}
