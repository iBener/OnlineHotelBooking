using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBooking.Models
{
    [Table(name: "OtelOdaTipi")]
    public class OtelOdaTipi
    {
        public int OtelId { get; set;}

        public int OdaTipiId { get; set; }

    }
}
