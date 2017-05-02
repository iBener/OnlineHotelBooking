using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table(name: "Musteri")]
    class Musteri
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int MusteriId { get; set; }

        [Display(Name = "Adı")]
        public string Adi { get; set; }

        [Display(Name = "Soyadı")]
        public string Soyadi { get; set; }

        [Display(Name = "Adresi")]
        public string Adres { get; set; }

        [Display(Name = "Telefonu")]
        public string Telefon { get; set; }

        [Display(Name = "E-Posta")]
        public string EPosta { get; set; }
    }
}
