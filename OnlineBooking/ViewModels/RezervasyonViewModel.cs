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

        public Otel Otel { get; set; }

        public List<Musteri> Musteriler { get; set; }

        public Musteri FaturaBilgileri { get; set; }

        public KrediKartiViewModel KrediKarti { get; set; }

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
            }

            if (!SozleymeOnay)
            {
                yield return new ValidationResult("Lütfen sözleşmeyi onaylayınız!", new[] { "SozleymeOnay" });
            }
        }
    }
}
