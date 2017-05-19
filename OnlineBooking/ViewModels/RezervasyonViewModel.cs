using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class RezervasyonViewModel : Rezervasyon
    {
        public RezervasyonViewModel() { }

        public RezervasyonViewModel(int yetiskin) 
            : this()
        {
            Musteriler = new List<Musteri>();
            for (int i = 0; i < yetiskin; i++)
            {
                Musteriler.Add(new Musteri());
            }
        }

        public RezervasyonViewModel(Rezervasyon rezervasyon) : base(rezervasyon) { }

        public Otel Otel { get; set; }

        public List<Musteri> Musteriler { get; set; }

        public Musteri FaturaBilgileri { get; set; }

        public KrediKartiViewModel KrediKarti { get; set; }

        public bool SozleymeOnay { get; set; }

    }
}
