using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class RezervasyonViewModel : Rezervasyon
    {

        public IEnumerable<Musteri> Musteriler { get; set; }

        public Musteri FaturaBilgileri { get; set; }

        public string KrediKartAdSoyad { get; set; }

        public string KrediKartNo { get; set; }

        public string KrediKartSonAy { get; set; }

        public string KrediKartSonYil { get; set; }

        public string KrediKartCvc { get; set; }

        public bool SozleymeOnay { get; set; }

        public bool KampanyaBildirimOnay { get; set; }

    }
}
