using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBooking.Controllers
{
    public class TesisController : Controller
    {
        // GET: Tesis
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tesis/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tesis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tesis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: Tesis/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tesis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: Tesis/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tesis/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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