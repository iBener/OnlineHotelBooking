using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table(name: "Tema")]
    class Tema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int TemaId { get; set; }

        [Display(Name = "Tema Adı")]
        public string TemaAdi { get; set; }
    }
}
