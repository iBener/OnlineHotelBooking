using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using OnlineBooking.ViewModels;
using Dapper;

namespace OnlineBooking.Data
{
    public class RezervasyonCommand : CommandBase<Rezervasyon>
    {
        public RezervasyonCommand(DbModel model, DbConnection connection) : base(model, connection)
        {
        }

        public RezervasyonViewModel GetRezervasyonViewModel(int otelId, int otelFiyatId, string giris, string cikis, int yetiskin, int cocuk)
        {
            var tgiris = Convert.ToDateTime(giris);
            var tcikis = Convert.ToDateTime(cikis);
            var model = new RezervasyonViewModel(yetiskin)
            {
                Otel = Model.Otel.OtelModelOku(otelId, "1900-01-01", "1900-01-01", 0, 0),
                FaturaBilgileri = new Musteri(),
                KrediKarti = new KrediKartiViewModel(),
                SozleymeOnay = false,
                OtelFiyat = FiyatBilgisiOku(otelId, otelFiyatId, tgiris, tcikis, yetiskin, cocuk),
                Giris = tgiris,
                Cikis = tcikis,
                Yetiskin = yetiskin,
                Cocuk = cocuk,
            };

            return model;
        }

        public OtelFiyatViewModel FiyatBilgisiOku(int otelId, int otelFiyatId, DateTime giris, DateTime cikis, int yetiskin, int cocuk)
        {
            var query = "select f.*, o.OdaTipiAdi, k.KonaklamaTuruAdi, r.ImageUrl \n" +
                "from OdaTipi o join OtelFiyat f on f.OdaTipiId = o.OdaTipiId \n" +
                "join KonaklamaTuru k on k.KonaklamaTuruId = f.KonaklamaId \n" +
                "left join OtelResim r on r.OtelId = f.OtelId and r.OdaTipiId = f.OdaTipiId \n" +
                "where f.OtelFiyatId = @otelFiyatId and f.OtelId = @otelId \n";
            var fiyat = Connection.Query<OtelFiyatViewModel>(query, new { otelId, otelFiyatId }).FirstOrDefault();
            var gece = (int)(cikis - giris).TotalDays;
            if (fiyat != null)
            {
                fiyat.Gece = gece;
                fiyat.Yetiskin = yetiskin;
                fiyat.Cocuk = cocuk;
            }
            return fiyat;
        }

        public void RezervasyonKaydet(RezervasyonViewModel model)
        {
            var rez = (Rezervasyon)model;
            rez.MusteriId = model.FaturaBilgileri.MusteriId;
            rez.OtelFiyatId = model.OtelFiyat.OtelFiyatId;
            InsertOrUpdate(rez, rez.RezervasyonId);

            foreach (var musteri in model.Musteriler)
            {
                var misafir = new RezervasyonMisafir()
                {
                    RezervasyonId = rez.RezervasyonId,
                    AdiSoyadi = $"{ musteri.Adi } { musteri.Soyadi }",
                    Cinsiyeti = musteri.Cinsiyeti,
                    DogumTarihi = musteri.DogumTarihi,
                };
                Insert(misafir);

                if (model.FaturaBilgileri.MusteriId != 0 && model.FaturaBilgileri.Adi == musteri.Adi && model.FaturaBilgileri.Soyadi == musteri.Soyadi)
                {
                    if ((model.FaturaBilgileri.DogumTarihi == null || model.FaturaBilgileri.DogumTarihi == DateTime.MinValue || model.FaturaBilgileri.DogumTarihi == new DateTime(1900, 1, 1)))
                    {
                        Connection.Execute("update Musteri set DogumTarihi = @DogumTarihi where MusteriId = @MusteriId",
                                           new { DogumTarihi = musteri.DogumTarihi, MusteriId = model.FaturaBilgileri.MusteriId });
                    }
                    if (String.IsNullOrEmpty(model.FaturaBilgileri.Cinsiyeti))
                    {
                        Connection.Execute("update Musteri set Cinsiyeti = @Cinsiyeti where MusteriId = @MusteriId",
                                           new { Cinsiyeti = musteri.Cinsiyeti, MusteriId = model.FaturaBilgileri.MusteriId });
                    }
                    Connection.Execute("update Musteri set Adres = @Adres, Telefon = @Telefon where MusteriId = @MusteriId",
                                        new { Adres = model.FaturaBilgileri.Adres, Telefon = model.FaturaBilgileri.Telefon, MusteriId = model.FaturaBilgileri.MusteriId });
                }
            }
        }

        public IEnumerable<RezervasyonViewModel> GetIslemListesi(int kullaniciId)
        {
            var tesis = new TesisCommands(Model, Connection);
            var query =
                "select r.* from Rezervasyon r \n" +
                "join Musteri m on m.MusteriId = r.MusteriId \n" +
                "where m.KullaniciId = @kullaniciId \n";
            var rezervasyonlar = Connection.Query<Rezervasyon>(query, new { kullaniciId });
            var sonuc = new List<RezervasyonViewModel>();
            foreach (var rezervasyon in rezervasyonlar)
            {
                var item = new RezervasyonViewModel(rezervasyon)
                {
                    Otel = tesis.GetOtelViewModel(rezervasyon.OtelId),
                    Musteriler = new List<Musteri>(),
                    FaturaBilgileri = FindWithId<Musteri>(rezervasyon.MusteriId),
                };
                var musteriler = Connection.Query<dynamic>(
                    "select * from RezervasyonMisafir where RezervasyonId = @id ", new { id = rezervasyon.RezervasyonId });
                foreach (var musteri in musteriler)
                {
                    item.Musteriler.Add(new Musteri()
                    {
                        Adi = musteri.AdiSoyadi,
                        DogumTarihi = musteri.DogumTarihi,
                        Cinsiyeti = musteri.Cinsiyeti,
                    });
                }
                sonuc.Add(item);
            }
            return sonuc;
        }

        public RezervasyonViewModel GetRezervasyonViewModel(int rezervasyonId)
        {
            var tesis = new TesisCommands(Model, Connection);
            var rezervasyon = FindWithId(rezervasyonId);
            var model = new RezervasyonViewModel(rezervasyon)
            {
                Otel = tesis.GetOtelViewModel(rezervasyon.OtelId),
                Musteriler = new List<Musteri>(),
                FaturaBilgileri = FindWithId<Musteri>(rezervasyon.MusteriId),
                OtelFiyat = FiyatBilgisiOku(rezervasyon.OtelId, rezervasyon.OtelFiyatId, rezervasyon.Giris, rezervasyon.Cikis, rezervasyon.Yetiskin, rezervasyon.Cocuk),
            };
            var musteriler = Connection.Query<dynamic>(
                "select * from RezervasyonMisafir where RezervasyonId = @id ", new { id = rezervasyon.RezervasyonId });
            foreach (var musteri in musteriler)
            {
                model.Musteriler.Add(new Musteri()
                {
                    Adi = musteri.AdiSoyadi,
                    DogumTarihi = musteri.DogumTarihi,
                    Cinsiyeti = musteri.Cinsiyeti,
                });
            }
            return model;
        }
    }
}
