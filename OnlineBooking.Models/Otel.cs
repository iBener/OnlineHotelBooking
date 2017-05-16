using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineBooking.Models
{
    [Table(name: "Otel")]
    public class Otel
    {
        public Otel()
        {
        }

        public Otel(Otel other)
        {
            Aciklama = other.Aciklama;
            Adres = other.Adres;
            Degerlendirme = other.Degerlendirme;
            IlAdi = other.IlAdi;
            IlceAdi = other.IlceAdi;
            OtelAdi = other.OtelAdi;
            OtelId = other.OtelId;
            Telefon = other.Telefon;
            Yildiz = other.Yildiz;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int OtelId { get; set; }

        [Display(Name = "Otel Adı")]
        public string OtelAdi { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "İli")]
        public string IlAdi { get; set; }

        [Display(Name = "İlçesi")]
        public string IlceAdi { get; set; }

        [Display(Name = "Adres")]
        public string Adres { get; set; }

        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Yıldız")]
        public int Yildiz { get; set; }

        [Display(Name = "Değerlendirme")]
        public byte Degerlendirme { get; set; }

    }
}
