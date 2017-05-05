using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineBooking.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.Controllers
{
    public class BaseController : Controller
    {
        public VeriTabani VeriTabani { get; private set; }

        public BaseController(IOptions<VeriTabani> ayarlar)
        {
            VeriTabani = ayarlar.Value;
        }

    }
}
