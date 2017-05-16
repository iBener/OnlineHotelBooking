using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class OtelOdalarViewModel : Otel
    {
        public IEnumerable<OtelOdaTipleri> Odalar { get; set; }

        public OtelOdalarViewModel()
        {

        }

        public OtelOdalarViewModel(Otel otel) : base(otel)
        {
        }
    }

    public class OtelOdaTipleri
    {
        public string OdaTipiAdi { get; set; }

        public string KonaklamaTuruAdi { get; set; }

        public string ImageUrl { get; set; }

        public int FiyatYetiskin { get; set; }

        public int FiyatCocuk { get; set; }

        public int ToplamFiyat { get; set; }
    }
}
