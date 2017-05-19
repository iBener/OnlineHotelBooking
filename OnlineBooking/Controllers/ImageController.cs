using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using OnlineBooking.Helpers;
using OnlineBooking.Data;
using OnlineBooking.Models;
using ImageSharp;

namespace OnlineBooking.Controllers
{
    public class ImageController : BaseController
    {
        private readonly string wwwrootPath;

        public ImageController(IOptions<VeriTabani> ayarlar, IHostingEnvironment env) : base(ayarlar)
        {
            wwwrootPath = env.WebRootPath;
        }

        // Kullanımı:
        // <img src='<%= Url.Action( "show", "image", new { id = ViewData["imageID"] } ) %>' />
        // GET: /<controller>/
        public ActionResult Show(int id)
        {
            var imageData = new byte[] { }; //...get bytes from database...

            return File(imageData, "image/jpg");
        }



        [HttpPost("UploadFiles")]
        public IActionResult Post(List<IFormFile> files, int OtelId, OdaTipleri OdaTipiId)
        {
            long size = files.Sum(f => f.Length);

            using (var db = new DbModel(VeriTabani))
            {
                foreach (var formFile in files)
                {
                    var fileNameParcalari = formFile.FileName.Split('.');
                    if (formFile.Length > 0 && fileNameParcalari.Length > 1)
                    {
                        var uzanti = fileNameParcalari[fileNameParcalari.Length - 1];
                        var dosyaAdi = String.Join(".", Guid.NewGuid().ToString(), uzanti);

                        var resim = new OtelResim()
                        {
                            OtelResimId = 0,
                            OtelId = OtelId,
                            OdaTipiId = (int)OdaTipiId,
                            ImageUrl = String.Join("/", "/images", dosyaAdi)
                        };

                        db.Tesis.Insert(resim);

                        byte[] bytes = null; ;
                        using (var fileStream = formFile.OpenReadStream())
                        using (var ms = new MemoryStream())
                        {
                            fileStream.CopyTo(ms);
                            bytes = ms.ToArray();
                        }

                        var tamYol = Path.Combine(wwwrootPath, "images", dosyaAdi);
                        using (FileStream output = System.IO.File.OpenWrite(tamYol))
                        using (Image<Rgba32> image = Image.Load<Rgba32>(bytes))
                        {
                            image.Resize(480, 280)
                                 .Save(output);
                        }
                    }
                }
            }

            return RedirectToAction("Resim", "Tesis", new { id = OtelId });
        }
    }
}
