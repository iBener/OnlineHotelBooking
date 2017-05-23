using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineBooking.Helpers;
using OnlineBooking.Models;
using System.Security.Claims;
using OnlineBooking.Data;
using OnlineBooking.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBooking.Controllers
{
    public class KullaniciController : BaseController
    {
        public KullaniciController(IOptions<VeriTabani> ayarlar) : base(ayarlar)
        {
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Kullanici.GetKullaniciListesi());
            }
        }

        public IActionResult Duzenle(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userId != null && id != null && userId.Value != id.Value.ToString())
                {
                    return RedirectToHataMesaji("Bu işlemi yapmak için yetkili değilsiniz.");
                }
                id = Int32.Parse(userId.Value);
            }
            using (var db = new DbModel(VeriTabani))
            {
                return View(db.Kullanici.GetKullanici(id ?? 0));
            }
        }

        [HttpPost]
        public IActionResult Duzenle(KullaniciViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using (var db = new DbModel(VeriTabani))
                {
                    db.Kullanici.Guncelle(model);
                }
                return RedirectToAction("Index", "AnaSayfa");
            }
            catch (Exception ex)
            {
                ViewBag.HataMesaji = ex.ToString();
            }
            return View(model);
        }

        public IActionResult Giris(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            if (!ViewData.ContainsKey("Kaydol"))
            {
                ViewData["Kaydol"] = false;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Giris(KullaniciViewModel model, string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            ViewData["Kaydol"] = false;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (var db = new DbModel(VeriTabani))
                {

                    var item = db.Kullanici.GirisKontrol(model.Kullanici.KullaniciAdi, model.Kullanici.Parola);

                    if (item != null)
                    {
                        var musteri = db.Kullanici.GetMusteriByKullaniciId(item.KullaniciId);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, item.KullaniciId.ToString(), ClaimValueTypes.Integer32),
                            new Claim(ClaimTypes.Name, item.KullaniciAdi, ClaimValueTypes.String),
                            new Claim("AdiSoyadi", musteri == null ? "" : musteri.Adi + " " + musteri.Soyadi, ClaimValueTypes.String),
                            //new Claim(ClaimTypes.Email, item.EPosta, ClaimValueTypes.String),
                            //new Claim("AdiSoyadi", item.AdiSoyadi, ClaimValueTypes.String),
                        };

                        if (!String.IsNullOrWhiteSpace(item.Rol))
                        {
                            foreach (var role in item.Rol.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String));
                            }
                        }

                        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Ohb"));

                        await HttpContext.Authentication.SignInAsync("Ohb", principal);

                        if (ReturnUrl != null)
                        {
                            return Redirect(ReturnUrl);
                        }
                        return RedirectToAction("Index", "AnaSayfa");
                    }
                    ViewBag.HataMesaji = "Giriş başarısız! Kullanıcı adı veya parola hatalı.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.HataMesaji = ex.ToString();
            }
            return View(model);
        }

        public IActionResult Izin()
        {
            return RedirectToHataMesaji("Bu işlemi yapmak için yetkili değilsiniz!");
        }

        public async Task<ActionResult> Kaydol(KullaniciViewModel model, string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            ViewData["Kaydol"] = true;
            if (!ModelState.IsValid)
            {
                return View("Giris", model);
            }
            try
            {
                using (var db = new DbModel(VeriTabani))
                {
                    db.Kullanici.Kaydol(model);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.Kullanici.KullaniciId.ToString(), ClaimValueTypes.Integer32),
                        new Claim(ClaimTypes.Name, model.Kullanici.KullaniciAdi, ClaimValueTypes.String),
                        //new Claim(ClaimTypes.Email, item.EPosta, ClaimValueTypes.String),
                        //new Claim("AdiSoyadi", item.AdiSoyadi, ClaimValueTypes.String),
                    };

                    if (!String.IsNullOrWhiteSpace(model.Kullanici.Rol))
                    {
                        foreach (var role in model.Kullanici.Rol.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String));
                        }
                    }

                    var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Ohb"));

                    await HttpContext.Authentication.SignInAsync("Ohb", principal);

                    if (ReturnUrl != null)
                    {
                        if (ReturnUrl.StartsWith("/Rezervasyon"))
                        {
                            TempData["Musteri"] = JsonConvert.SerializeObject(model.Musteri);
                        }
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "AnaSayfa");
                }
            }
            catch (Exception ex)
            {
                ViewBag.HataMesaji = ex.ToString();
            }
            return View("Giris", model);
        }

        public async Task<IActionResult> Cikis()
        {
            await HttpContext.Authentication.SignOutAsync("Ohb");

            return RedirectToAction("Index", "AnaSayfa");
        }
    }
}
