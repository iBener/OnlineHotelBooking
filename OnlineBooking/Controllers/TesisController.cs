using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineBooking.Helpers;
using OnlineBooking.Models;

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

        private Otel GetOtelById(int id)
        {
            using (var db = new DbModel(VeriTabani))
            {
                return db.Tesis.FindWithId(id);
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

        // POST: Tesis/Kayit/5
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

        public JsonResult OdaTipiAcKapa(int otelId, string odaTipi, bool acKapa)
        {

            return Json("ok");
        }
    }
}