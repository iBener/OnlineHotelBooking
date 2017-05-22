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

        public RezervasyonViewModel GetRezervasyonViewModel(int id, int otelId, int otelFiyatId, string giris, string cikis, int yetiskin, int cocuk)
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
            }
        }
    }
}
