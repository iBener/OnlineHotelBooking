using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.ViewModels
{
    public class RezervasyonViewModel : Rezervasyon, IValidatableObject
    {
        public RezervasyonViewModel() { }

        public RezervasyonViewModel(int yetiskin) 
            : this()
        {
            Musteriler = new List<Musteri>();
            for (int i = 0; i < yetiskin; i++)
            {
                Musteriler.Add(new Musteri());
            }
        }

        public RezervasyonViewModel(Rezervasyon rezervasyon) : base(rezervasyon) { }

        public OtelViewModel Otel { get; set; }

        [Required]
        public List<Musteri> Musteriler { get; set; }

        [Required]
        public Musteri FaturaBilgileri { get; set; }

        [Required]
        public int OtelFiyatId { get; set; }

        [Required]
        public OtelFiyatViewModel OtelFiyat { get; set; }

        [Required]
        public KrediKartiViewModel KrediKarti { get; set; }

        [Required]
        public bool SozleymeOnay { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            for (int i = 0; i < Musteriler.Count; i++)
            {
                var musteri = Musteriler[i];
                if (musteri.Cinsiyeti == "Cinsiyeti")
                {
                    yield return new ValidationResult("Lütfen bir cinsiyet seçiniz!", new[] { "Musteriler[" + i + "].Cinsiyeti" });
                }
                if (String.IsNullOrWhiteSpace(musteri.Adi))
                {
                    yield return new ValidationResult("Lütfen müşteri adını belirtiniz!", new[] { "Musteriler[" + i + "].Adi" });
                }
                if (String.IsNullOrWhiteSpace(musteri.Soyadi))
                {
                    yield return new ValidationResult("Lütfen müşteri soyadını belirtiniz!", new[] { "Musteriler[" + i + "].Soyadi" });
                }
                if (musteri.DogumTarihi == null || musteri.DogumTarihi == DateTime.MinValue)
                {
                    yield return new ValidationResult("Lütfen müşteri doğum tarihini belirtiniz!", new[] { "Musteriler[" + i + "].DogumTarihi" });
                }
            }

            if (String.IsNullOrWhiteSpace(FaturaBilgileri.Adi))
            {
                yield return new ValidationResult("Lütfen bir ad belirtiniz!", new[] { "FaturaBilgileri.Adi" });
            }
            if (String.IsNullOrWhiteSpace(FaturaBilgileri.Soyadi))
            {
                yield return new ValidationResult("Lütfen bir soyad belirtiniz!", new[] { "FaturaBilgileri.Soyadi" });
            }
            if (String.IsNullOrWhiteSpace(FaturaBilgileri.EPosta))
            {
                yield return new ValidationResult("Lütfen bir e-postası belirtiniz!", new[] { "FaturaBilgileri.EPostasi" });
            }
            var emailattrb = new EmailAddressAttribute();
            if (!emailattrb.IsValid(FaturaBilgileri.EPosta))
            {
                yield return new ValidationResult("Lütfen geçerli bir e-posta adresi belirtiniz!", new[] { "FaturaBilgileri.EPostasi" });
            }
            if (String.IsNullOrWhiteSpace(FaturaBilgileri.Adres))
            {
                yield return new ValidationResult("Lütfen bir adres belirtiniz!", new[] { "FaturaBilgileri.Adresi" });
            }
            if (String.IsNullOrWhiteSpace(FaturaBilgileri.Telefon))
            {
                yield return new ValidationResult("Lütfen bir telefon numarası belirtiniz!", new[] { "FaturaBilgileri.Telefonu" });
            }

            if (String.IsNullOrWhiteSpace(KrediKarti.AdSoyad))
            {
                yield return new ValidationResult("Lütfen kredi kartı sahibini belirtiniz!", new[] { "KrediKarti.AdSoyad" });
            }
            var attrb = new CreditCardAttribute();
            if (attrb.IsValid(KrediKarti.KartNo))
            {
                yield return new ValidationResult("Girdiğiniz kart numarası geçerli bir kredi kartı numarası değildir!", new[] { "KrediKarti.KartNo" });
            }
            if (String.IsNullOrWhiteSpace(KrediKarti.GecerlilikTarihi) || KrediKarti.GecerlilikTarihi.Split('/').Length != 2)
            {
                yield return new ValidationResult("Lütfen kredi kartı geçerlilik tarihini belirtiniz!", new[] { "KrediKarti.GecerlilikTarihi" });
            }
            if (String.IsNullOrWhiteSpace(KrediKarti.Cvc))
            {
                yield return new ValidationResult("Lütfen kredi kartı CVC numarasını belirtiniz!", new[] { "KrediKarti.Cvc" });
            }

            if (!SozleymeOnay)
            {
                yield return new ValidationResult("Lütfen sözleşmeyi onaylayınız!", new[] { "SozleymeOnay" });
            }
        }
    }
}
