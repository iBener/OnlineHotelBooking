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
            Aciklama = "";
            Adres = "";
            Degerlendirme = 90;
            IlAdi = "";
            IlceAdi = "";
            OtelAdi = "";
            OtelId = 0;
            Telefon = "";
            Yildiz = 0;
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
        [Required(ErrorMessage = "Lütfen bir otel adı belirtiniz.")]
        public string OtelAdi { get; set; }

        [Display(Name = "Açıklama")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Aciklama { get; set; }

        [Display(Name = "Bölge Adı")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BolgeAdi { get; set; }

        [Display(Name = "İli")]
        [Required(ErrorMessage = "Lütfen ilini belirtiniz.")]
        public string IlAdi { get; set; }

        [Display(Name = "İlçesi")]
        [Required(ErrorMessage = "Lütfen ilçesini belirtiniz.")]
        public string IlceAdi { get; set; }

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Lütfen otel adresini belirtiniz.")]
        public string Adres { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Lütfen otel telefon numarasını belirtiniz.")]
        public string Telefon { get; set; }

        [Display(Name = "Yıldız")]
        [Range(1, 5, ErrorMessage = "Lütfen 1-5 arası bir değer belirleyiniz.")]
        public int Yildiz { get; set; }

        [Display(Name = "Değerlendirme")]
        public byte Degerlendirme { get; set; }

    }
}
