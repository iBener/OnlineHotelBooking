using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineBooking.Models
{
    [Table(name: "Musteri")]
    public class Musteri
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int MusteriId { get; set; }

        public int KullaniciId { get; set; }

        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Lütfen adınızı belirtiniz.")]
        public string Adi { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Lütfen soyadınızı belirtiniz.")]
        public string Soyadi { get; set; }

        [Display(Name = "Adres")]
        public string Adresi { get; set; }

        [Display(Name = "Telefon")]
        public string Telefonu { get; set; }

        [Display(Name = "E-Posta")]
        [DataType(DataType.EmailAddress)]
        public string EPostasi { get; set; }

        [Display(Name = "Cinsiyeti")]
        public string Cinsiyeti { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime DogumTarihi { get; set; }

        public bool KampanyaBildirimOnay { get; set; }
    }
}
