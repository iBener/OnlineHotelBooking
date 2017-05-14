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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        // POST: Otel
        public ActionResult Index(IFormCollection collection)
        {
            var bolge = collection["bolge"].ToString();
            var giris = collection["giris"].ToString();
            var cikis = collection["cikis"].ToString();
            var yetiskin = collection["yetiskin"].ToString();
            var cocuk = collection["cocuk"].ToString();
            return View();
        }

        // GET: Otel/Detay/5
        public ActionResult Detay(int id)
        {
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Tesis.FindWithId(id));
            }
        }


    }
}