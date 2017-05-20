using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineBooking.Data;
using OnlineBooking.Helpers;
using OnlineBooking.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBooking.Controllers
{
    public class RezervasyonController : BaseController
    {
        public RezervasyonController(IOptions<VeriTabani> ayarlar) : base(ayarlar)
        {
        }

        // GET: /<controller>/
        public IActionResult Index(int id, int otelId, int otelFiyatId, string giris, string cikis, int yetiskin, int cocuk)
        {
            if (otelId == 0)
            {
                return RedirectToAction("Index", "AnaSayfa");
            }
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Rezervasyon.GetRezervasyonViewModel(id, otelId, otelFiyatId, giris, cikis, yetiskin, cocuk));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RezervasyonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.HataMesaji = "Aşağıda belirtilen hataları düzelttikten sonra yeniden deneyiniz.";
                using (var db = new DbModel(VeriTabani))
                {
                    model.Otel = db.Otel.OtelModelOku(model.OtelId, "1900-01-01", "1900-01-01", 0, 0);
                    model.OtelFiyat = db.Rezervasyon.FiyatBilgisiOku(model.OtelFiyatId, model.Giris, model.Cikis, model.Yetiskin, model.Cocuk);
                }
                return View(model);
            }
            try
            {
                using (var db = new DbModel(VeriTabani))
                {
                    db.Rezervasyon.RezervasyonKaydet(model);
                }
                return RedirectToAction("Index", "AnaSayfa");
            }
            catch (Exception ex)
            {
                ViewBag.HataMesaji = ex.Message;
            }
            return View(model);
        }
    }
}
