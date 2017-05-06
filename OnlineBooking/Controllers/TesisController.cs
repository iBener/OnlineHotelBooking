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

        // GET: Tesis
        public ActionResult Index()
        {
            using (var db = new DbModel(VeriTabani))
            {
                var item = db.Tesis.FindWithId(1);
                return View();
            }
        }

        /// <summary>
        /// Tesis detaylarýný görüntüleme metodu.
        /// </summary>
        /// <param name="id">Tesis id'si.</param>
        // GET: Tesis/Detay/5
        public ActionResult Detay(int id)
        {
            return View();
        }

        /// <summary>
        /// Yeni tesis tanýmlama metodu.
        /// </summary>
        // GET: Tesis/Yeni
        public ActionResult Yeni()
        {
            return View();
        }

        /// <summary>
        /// Yeni tesis kaydetme metodu
        /// </summary>
        // POST: Tesis/Yeni
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tesis/Duzelt/5
        public ActionResult Duzelt(int id)
        {
            return View();
        }

        // POST: Tesis/Duzelt/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzelt(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tesis/Sil/5
        public ActionResult Sil(int id)
        {
            return View();
        }

        // POST: Tesis/Sil/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}