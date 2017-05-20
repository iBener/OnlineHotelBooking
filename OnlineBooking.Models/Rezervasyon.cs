using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineBooking.Models
{
    [Table(name: "Rezervasyon")]
    public class Rezervasyon
    {
        public Rezervasyon() { }

        public Rezervasyon(Rezervasyon rezervasyon)
        {
            RezervasyonId = rezervasyon.RezervasyonId;
            OtelId = rezervasyon.OtelId;
            MusteriId = rezervasyon.MusteriId;
            Giris = rezervasyon.Giris;
            Cikis = rezervasyon.Cikis;
            Yetiskin = rezervasyon.Yetiskin;
            Cocuk = rezervasyon.Cocuk;
            OdaFiyati = rezervasyon.OdaFiyati;
            ToplamTutar = rezervasyon.ToplamTutar;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int RezervasyonId { get; set; }

        public int OtelId { get; set; }

        public int MusteriId { get; set; }

        public DateTime Giris { get; set; }

        public DateTime Cikis { get; set; }

        public int Yetiskin { get; set; }

        public int Cocuk { get; set; }

        public int OdaFiyati { get; set; }

        public int ToplamTutar { get; set; }

    }
}
