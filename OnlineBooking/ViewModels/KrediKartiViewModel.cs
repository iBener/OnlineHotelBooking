using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class KrediKartiViewModel
    {
        public string AdSoyad { get; set; }

        public string KartNo { get; set; }

        public string GecerlilikTarihi { get; set; }

        public string Cvc { get; set; }
    }
}
