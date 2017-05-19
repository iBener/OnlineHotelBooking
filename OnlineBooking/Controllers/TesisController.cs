using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineBooking.Helpers;
using OnlineBooking.Models;
using OnlineBooking.Data;
using OnlineBooking.ViewModels;

namespace OnlineBooking.Controllers
{
    /// <summary>
    /// Tesis tanýmlama class'ý. Bu class ile otel/tesis yetkileri kendi tesislerini sisteme 
    /// tanýmlayabilirler.
    /// </summary>
    public class TesisController : BaseController
    {
        public TesisController(IOptions<VeriTabani> ayarlar) : base(ayarlar)
        {
        }

        private OtelViewModel GetOtelById(int id)
        {
            using (var db = new DbModel(VeriTabani))
            {
                return db.Tesis.GetOtelViewModel(id);
            }
        }

        // GET: Tesis
        public ActionResult Index(int id)
        {
            return View(GetOtelById(id));
        }

        public ActionResult Detay(int id)
        {
            ViewBag.Islem = "Düzelt";
            return View(GetOtelById(id));
        }

        // POST: Tesis/Detay/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detay(Otel tesis)
        {
            if (!ModelState.IsValid)
            {
                return View(tesis);
            }
            try
            {
                using (var db = new DbModel(VeriTabani))
                {
                    if (tesis.OtelId == 0)
                        db.Tesis.Insert(tesis);
                    else
                        db.Tesis.Update(tesis);
                }
                return RedirectToAction("Index", new { id = tesis.OtelId });
            }
            catch (Exception ex)
            {
                ViewBag.HataMesaji = ex.Message;
            }
            return View(tesis);
        }

        public ActionResult Fiyat(int id, int konaklamaId)
        {
            ViewBag.OtelId = id;
            ViewBag.KonaklamaId = konaklamaId;
            if (id == 0)
            {
                ViewBag.HataMesaji = "Lütfen fiyatlarýný girmek istediðiniz oteli belirtiniz!";
                return View();
            }
            if (konaklamaId == 0)
            {
                ViewBag.HataMesaji = "Lütfen fiyatlarýný girmek istediðiniz otelin konaklama tipini belirtiniz!";
                return View();
            }
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Tesis.GetFiyatListesi(id, konaklamaId));
            }
        }

        // POST: Tesis/Detay/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Fiyat(int otelId, int konaklamaId, IEnumerable<OtelFiyatViewModel> fiyat)
        {
            ViewBag.OtelId = otelId;
            ViewBag.KonaklamaId = konaklamaId;
            if (!ModelState.IsValid)
            {
                return View(fiyat);
            }
            try
            {
                using (var db = new DbModel(VeriTabani))
                {
                    db.Tesis.FiyatListesiKaydet(fiyat);
                }
                return RedirectToAction("Index", new { id = otelId });
            }
            catch (Exception ex)
            {
                ViewBag.HataMesaji = ex.Message;
            }
            return View(fiyat);
        }

        public ActionResult Resim(int id)
        {
            ViewBag.OtelId = id;
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Tesis.GetOtelResimleri(id));
            }
        }

        public ActionResult ResimEkle(int id, OdaTipleri odatipi)
        {
            ViewBag.OtelId = id;
            ViewBag.OdaTipiId = odatipi;
            if (odatipi == OdaTipleri.JuniorOda)
            {
                ViewBag.OdaTipi = "Junior Oda Resmi";
            }
            else if (odatipi == OdaTipleri.StandartOda)
            {
                ViewBag.OdaTipi = "Standar Oda Resmi";
            }
            else if (odatipi == OdaTipleri.AileOdasi)
            {
                ViewBag.OdaTipi = "Aile Odasý Resmi";
            }
            else if (odatipi == OdaTipleri.SuitOda)
            {
                ViewBag.OdaTipi = "Suit Oda Resmi";
            }
            else
            {
                ViewBag.OdaTipi = "Otel Tesis Resmi";
            }
            return View();
        }

        // GET: Tesis/Sil/5
        public ActionResult Sil(int id)
        {
            return View(GetOtelById(id));
        }

        // POST: Tesis/Sil/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int id, IFormCollection collection)
        {
            try
            {
                using (var db = new DbModel(VeriTabani))
                {
                    db.Tesis.DeleteWithId(id);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.HataMesaji = ex.Message;
            }
            return View();
        }

        public JsonResult OdaTipiAcKapa(int otelId, int id, bool acKapa)
        {

            return Json("ok");
        }

        public JsonResult OzellikAcKapa(int otelId, int id, bool acKapa)
        {

            return Json("ok");
        }
    }
}