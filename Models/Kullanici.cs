using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table(name: "Kullanici")]
    public class Kullanici
    {
        [Key]
        [Display(Name = "Id #")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int KullaniciId { get; set; }

        [Display(Name = "Kullanıcı")]
        public string KullaniciAdi { get; set; }

        [DataType(DataType.Password)]
        public string Parola { get; set; }

        [Display(Name = "Rolü")]
        public string Rol { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        public DateTime KayitTarihi { get; set; }

        [Display(Name = "Aktif")]
        public bool Aktif { get; set; }
    }
}
