using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineBooking.Data;
using OnlineBooking.Helpers;
using OnlineBooking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using OnlineBooking.Models;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBooking.Controllers
{
    [Authorize]
    public class RezervasyonController : BaseController
    {
        public RezervasyonController(IOptions<VeriTabani> ayarlar) : base(ayarlar)
        {
        }

        // GET: /<controller>/
        public IActionResult Index(int otelId, int otelFiyatId, string giris, string cikis, int yetiskin, int cocuk)
        {
            if (otelId == 0)
            {
                return RedirectToAction("Index", "AnaSayfa");
            }
            using (var db = new DbModel(VeriTabani))
            {
                var model = db.Rezervasyon.GetRezervasyonViewModel(otelId, otelFiyatId, giris, cikis, yetiskin, cocuk);
                if (model.OtelFiyat == null)
                {
                    ViewBag.HataMesaji = "Otel fiyat bilgisi bulunamadı!";
                    ViewBag.KaydetEnable = false;
                }
                if (TempData["Musteri"] != null)
                {
                    var musteri = JsonConvert.DeserializeObject<Musteri>(TempData["Musteri"].ToString());
                    SetModelMusteri(model, musteri);
                }
                else if (User.Identity.IsAuthenticated)
                {
                    var userId = GetKullaniciId();
                    if (userId != 0)
                    {
                        var kullanici = db.Kullanici.GetKullanici(userId);
                        SetModelMusteri(model, kullanici.Musteri);
                    }
                }
                return View(model);
            }
        }

        public ActionResult Islemler()
        {
            var kullaniciId = GetKullaniciId();
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Rezervasyon.GetIslemListesi(kullaniciId));
            }
        }

        private void SetModelMusteri(RezervasyonViewModel model, Musteri musteri)
        {
            if (model.Musteriler.Count > 0)
            {
                model.Musteriler[0].Adi = musteri.Adi;
                model.Musteriler[0].Soyadi = musteri.Soyadi;
                model.Musteriler[0].EPosta = musteri.EPosta;
                model.Musteriler[0].Cinsiyeti = musteri.Cinsiyeti;
                model.Musteriler[0].DogumTarihi = musteri.DogumTarihi;
            }
            model.FaturaBilgileri.Adi = musteri.Adi;
            model.FaturaBilgileri.Soyadi = musteri.Soyadi;
            model.FaturaBilgileri.EPosta = musteri.EPosta;
            model.FaturaBilgileri.MusteriId = musteri.MusteriId;
            model.FaturaBilgileri.Adres = musteri.Adres;
            model.FaturaBilgileri.Telefon = musteri.Telefon;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RezervasyonViewModel model)
        {
            if (!ModelState.IsValid || model.OtelFiyat == null || model.OtelFiyat.OtelFiyatId == 0)
            {
                ViewBag.HataMesaji = "Aşağıda belirtilen hataları düzelttikten sonra yeniden deneyiniz.";
                using (var db = new DbModel(VeriTabani))
                {
                    model.Otel = db.Otel.OtelModelOku(model.OtelId, "1900-01-01", "1900-01-01", 0, 0);
                    model.OtelFiyat = db.Rezervasyon.FiyatBilgisiOku(model.OtelId, model.OtelFiyatId, model.Giris, model.Cikis, model.Yetiskin, model.Cocuk);
                    if (model.OtelFiyat == null)
                    {
                        ViewBag.HataMesaji += "\nOtel fiyat bilgisi bulunamadı!";
                        ViewBag.KaydetEnable = false;
                    }
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
