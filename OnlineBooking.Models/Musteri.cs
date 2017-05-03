using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineBooking.Models
{
    [Table(name: "Musteri")]
    class Musteri : Base.Kisi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int MusteriId { get; set; }
    }
}
