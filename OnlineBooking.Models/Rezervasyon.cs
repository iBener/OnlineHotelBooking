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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int RezervasyonId { get; set; }

        public string OtelId { get; set; }

        public string MusteriId { get; set; }
    }
}
