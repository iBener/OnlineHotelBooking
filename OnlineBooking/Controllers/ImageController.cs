using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBooking.Controllers
{
    public class ImageController : Controller
    {
        // GET: /<controller>/
        public ActionResult Show(int id)
        {
            var imageData = new byte[] { }; //...get bytes from database...

        return File(imageData, "image/jpg");
        }

        // Kullanımı:
        // <img src='<%= Url.Action( "show", "image", new { id = ViewData["imageID"] } ) %>' />
    }
}
