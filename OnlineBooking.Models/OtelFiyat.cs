using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBooking.Models
{
    [Table(name: "OtelFiyat")]
    public class OtelFiyat
    {
        [Key]
        [Display(Name = "Id #")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int OtelFiyatId { get; set; }

        [Display(Name = "Otel Id#")]
        public int OtelId { get; set; }

        [Display(Name = "Konaklama Id#")]
        public int KonaklamaId { get; set; }

        [Display(Name = "Oda Tipi Id#")]
        public int OdaTipiId { get; set; }

        [Display(Name = "Yetiştin Fiyat")]
        public int FiyatYetiskin { get; set; }

        [Display(Name = "9-14 Yaş Fiyat")]
        public int FiyatCocuk { get; set; }
    }
}
