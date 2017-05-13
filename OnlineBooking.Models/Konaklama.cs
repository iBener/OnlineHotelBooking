using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBooking.Models
{
    [Table(name: "KonaklamaTuru")]
    public class KonaklamaTuru
    {
        [Key]
        [Display(Name = "Id #")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int KonaklamaTuruId { get; set; }

        [Display(Name = "Konaklama Türü")]
        public string KonaklamaTuruAdi { get; set; }
    }
}
