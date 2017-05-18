using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class OtelFiyatItemViewModel
    {
        public int OtelId { get; set; }

        public int KonaklamaId { get; set; }

        public int? OtelFiyatId { get; set; }

        public int OdaTipiId { get; set; }

        public string OdaTipi { get; set; }

        public int? FiyatYetiskin { get; set; }

        public int? FiyatCocuk { get; set; }
    }
}
