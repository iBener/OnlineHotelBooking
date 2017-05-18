using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBooking.Models.Base
{
    public abstract class Kisi
    {
        [Display(Name = "Ad")]
        public string Adi { get; set; }

        [Display(Name = "Soyad")]
        public string Soyadi { get; set; }

        [Display(Name = "Adres")]
        public string Adresi { get; set; }

        [Display(Name = "Telefon")]
        public string Telefonu { get; set; }

        [Display(Name = "E-Posta")]
        public string EPostasi { get; set; }

        [Display(Name = "Cinsiyeti")]
        public string Cinsiyeti { get; set; }
    }
}
