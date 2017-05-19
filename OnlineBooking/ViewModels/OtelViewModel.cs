using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class OtelViewModel : Otel
    {
        public OtelViewModel() { }

        public OtelViewModel(Otel otel) : base(otel) { }

        public IEnumerable<OtelFiyatViewModel> Fiyat { get; set; }

        public IEnumerable<OtelResim> Resim { get; set; }
    }
}
