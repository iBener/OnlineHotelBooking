using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineBooking.Models
{
    [Table(name: "OtelResim")]
    public class OtelResim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int OtelResimId { get; set; }

        public int OtelId { get; set; }

        public int OdaTipiId { get; set; }

        public string ImageUrl { get; set; }

    }
}
