using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineBooking.Models
{
    [Table(name: "Tema")]
    public class Tema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int TemaId { get; set; }

        [Display(Name = "Tema Adı")]
        public string TemaAdi { get; set; }
    }
}
