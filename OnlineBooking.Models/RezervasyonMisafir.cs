using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineBooking.Models
{
    [Table("RezervasyonMisafir")]
    public class RezervasyonMisafir
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int RezervasyonMisafirId { get; set; }

        public int RezervasyonId { get; set; }

        public string Cinsiyeti { get; set; }

        public string AdiSoyadi { get; set; }

        public DateTime DogumTarihi { get; set; }
    }
}
