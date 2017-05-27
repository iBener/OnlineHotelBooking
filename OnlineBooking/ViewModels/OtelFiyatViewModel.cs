using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class OtelFiyatViewModel : OtelFiyat
    {

        public string OdaTipiAdi { get; set; }

        public string KonaklamaTuruAdi { get; set; }

        public int ToplamFiyat { get; private set; }

        private void ToplamFiyatHesapla()
        {
            ToplamFiyat = gece * FiyatYetiskin * yetiskin + FiyatCocuk * cocuk;
        }

        private int gece;
        public int Gece
        {
            get
            {
                return gece;
            }
            set
            {
                gece = value;
                ToplamFiyatHesapla();
            }
        }

        private int yetiskin;
        public int Yetiskin
        {
            get { return yetiskin; }
            set
            {
                yetiskin = value;
                ToplamFiyatHesapla();
            }
        }

        private int cocuk;
        public int Cocuk
        {
            get { return cocuk; }
            set
            {
                cocuk = value;
                ToplamFiyatHesapla();
            }
        }

        public string ImageUrl { get; set; }

    }
}
