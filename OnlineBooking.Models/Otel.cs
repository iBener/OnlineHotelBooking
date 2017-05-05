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

        [Display(Name = "Alanı(m2)")]
        public int AlanM2 { get; set; }

        [Display(Name = "Oda Sayısı")]
        public int OdaSayisi { get; set; }

        [Display(Name = "Yıldız")]
        public int Yildiz { get; set; }

        [Display(Name = "Denize Mesafesi")]
        public int DenizMesafesi { get; set; }
    }
}
