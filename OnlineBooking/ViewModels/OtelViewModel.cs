using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class OtelViewModel :  Otel
    {
        public IEnumerable<OtelFiyatItemViewModel> Fiyatlar { get; set; }

        public IEnumerable<OtelResim> Resimler { get; set; }
    }
}
