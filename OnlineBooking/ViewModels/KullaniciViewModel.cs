using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class KullaniciViewModel : IValidatableObject
    {
        public Kullanici Kullanici { get; set; }

        public string ParolaDogrula { get; set; }

        public Musteri Musteri { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ParolaDogrula != null && Kullanici.Parola != ParolaDogrula)
            {
                yield return new ValidationResult("Parolanız eşleşmiyor.", new[] { "ParolaDogrula" });
            }

            if (Musteri != null)
            {
                if (String.IsNullOrWhiteSpace(Musteri.Adres))
                {
                    Musteri.Adres = String.Empty;
                }

                if (String.IsNullOrWhiteSpace(Musteri.Telefon))
                {
                    Musteri.Telefon = String.Empty;
                }

                if (String.IsNullOrWhiteSpace(Musteri.Cinsiyeti) || Musteri.Cinsiyeti == "Cinsiyeti")
                {
                    Musteri.Cinsiyeti = "";
                }

                if (Musteri.DogumTarihi == null || Musteri.DogumTarihi == DateTime.MinValue)
                {
                    Musteri.DogumTarihi = new DateTime(1900, 1, 1);
                }

                Musteri.KullaniciId = Kullanici.KullaniciId;
                Musteri.EPosta = Kullanici.KullaniciAdi; 
            }
        }
    }
}
