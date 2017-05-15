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
        public ActionResult Index(string bolge, string giris, string cikis, int yetiskin, int cocuk)
        {
            ViewBag.Bolge = bolge;
            ViewBag.Giris = giris;
            ViewBag.Cikis = cikis;
            ViewBag.Yetiskin = yetiskin;
            ViewBag.Cocuk = cocuk;
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Otel.Query(null));
            }
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