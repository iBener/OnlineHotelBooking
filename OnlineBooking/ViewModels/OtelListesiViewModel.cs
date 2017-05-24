using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class OtelListesiViewModel
    {
        public int OtelId { get; set; }

        public string OtelAdi { get; set; }

        public int Yildiz { get; set; }

        public string IlAdi { get; set; }

        public string IlceAdi { get; set; }

        public string KullaniciAdi { get; set; }

    }
}
