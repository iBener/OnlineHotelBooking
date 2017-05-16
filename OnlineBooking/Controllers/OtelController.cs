using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBooking.Models;
using OnlineBooking.Helpers;
using Microsoft.Extensions.Options;
using OnlineBooking.Data;

namespace OnlineBooking.Controllers
{
    public class OtelController : BaseController
    {
        public OtelController(IOptions<VeriTabani> ayarlar) : base(ayarlar)
        {
        }

        // GET: Otel
        public ActionResult Index(string bolge, string giris, string cikis, int yetiskin, 
                                  int cocuk, List<string> fiyat, List<KonaklamaTipleri> konaklama)
        {
            ViewBag.Bolge = bolge;
            ViewBag.Giris = giris;
            ViewBag.Cikis = cikis;
            ViewBag.Yetiskin = yetiskin;
            ViewBag.Cocuk = cocuk;
            ViewBag.Fiyatlar = new Dictionary<string, string>()
            {
                { "1", fiyat.Contains("0-249") ? "checked" : "" },
                { "2", fiyat.Contains("250-499") ? "checked" : "" },
                { "3", fiyat.Contains("500+") ? "checked" : "" }
            };
            ViewBag.Konaklamalar = new Dictionary<string, string>()
            {
                { "1", konaklama.Contains(KonaklamaTipleri.YarimPansiyon) ? "checked" : "" },
                { "2", konaklama.Contains(KonaklamaTipleri.TamPansiyon) ? "checked" : "" },
                { "3", konaklama.Contains(KonaklamaTipleri.HerseyDahil) ? "checked" : "" },
                { "4", konaklama.Contains(KonaklamaTipleri.Ultra) ? "checked" : "" },
            };
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Otel.Query(null));
            }
        }

        // GET: Otel/Detay/5
        public ActionResult Detay(int id, string giris, string cikis, int yetiskin, int cocuk)
        {
            ViewBag.Giris = giris;
            ViewBag.Cikis = cikis;
            ViewBag.Yetiskin = yetiskin;
            ViewBag.Cocuk = cocuk;
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Otel.OtelOda(id, giris, cikis, yetiskin, cocuk));
            }
        }


    }
}