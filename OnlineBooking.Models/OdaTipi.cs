using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineBooking.Models
{
    [Table(name: "OdaTipi")]
    public class OdaTipi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int OdaTipiId { get; set; }

        [Column("OdaTipi")]
        [Display(Name = "Oda Tipi")]
        public string OdaTipiAdi { get; set; }
    }
}
