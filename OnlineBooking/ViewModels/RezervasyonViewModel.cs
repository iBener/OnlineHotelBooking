using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class RezervasyonViewModel : Rezervasyon
    {
        public Otel Otel { get; set; }

        public DateTime Giris { get; set; }

        public DateTime Cikis { get; set; }

        public int Yetiskin { get; set; }

        public int Cocuk { get; set; }

        public int OdaFiyati { get; set; }

        public int ToplamTutar { get; set; }

        public IEnumerable<Musteri> Musteriler { get; set; }

        public Musteri FaturaBilgileri { get; set; }

        public KrediKartiViewModel KrediKarti { get; set; }

        public bool SozleymeOnay { get; set; }

        public bool KampanyaBildirimOnay { get; set; }

    }
}
