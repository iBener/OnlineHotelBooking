using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBooking.Controllers
{
    public class OtelController : Controller
    {
        // GET: Otel
        public ActionResult Index()
        {
            return View();
        }

        // GET: Otel/Detay/5
        public ActionResult Detay(int id)
        {
            
            return View();
        }

        // GET: Otel/Yeni
        public ActionResult Yeni()
        {
            return View();
        }

        // POST: Otel/Yeni
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

        // GET: Otel/Duzelt/5
        public ActionResult Duzelt(int id)
        {
            return View();
        }

        // POST: Otel/Duzelt/5
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

        // GET: Otel/Sil/5
        public ActionResult Sil(int id)
        {
            return View();
        }

        // POST: Otel/Sil/5
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