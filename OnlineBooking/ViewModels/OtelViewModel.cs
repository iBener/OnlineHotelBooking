using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class OtelViewModel : Otel
    {
        public int Fiyat { get; set; }

        public string Resim { get; set; }
    }
}
