using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.FastCrud;
using OnlineBooking.Models;
using OnlineBooking.ViewModels;

namespace OnlineBooking.Data
{
    public class TesisCommands : CommandBase<Otel>
    {
        public TesisCommands(DbModel model, DbConnection connection) : base(model, connection)
        {
        }

        public OtelViewModel GetOtelViewModel(int id)
        {
            var otel = FindWithId(id);
            var konaklama = Query<KonaklamaTuru>(null);

            var otelModel = new OtelViewModel()
            {
                OtelId = otel.OtelId,
                OtelAdi = otel.OtelAdi,
                Aciklama = otel.Aciklama,
                Adres = otel.Adres,
                Telefon = otel.Telefon,
                IlAdi = otel.IlAdi,
                IlceAdi = otel.IlceAdi,
                Yildiz = otel.Yildiz,
                Degerlendirme = otel.Degerlendirme,
                Fiyatlar = new List<OtelFiyatItemViewModel>(),
            };
            foreach (var item in konaklama)
            {
                var fiyatlar = FiyatOlustur(id, item.KonaklamaTuruId);
                foreach (var fiyat in fiyatlar)
                {
                    otelModel.Fiyatlar.Add(fiyat);
                }
            }
            return otelModel;
        }

        public OtelFiyatViewModel GetFiyatListesi(int otelId, int konaklamaId)
        {
            var konaklama = FindWithId<KonaklamaTuru>(konaklamaId);

            var item = new OtelFiyatViewModel()
            {
                OtelId = otelId,
                KonaklamaId = konaklama.KonaklamaTuruId,
                Konaklama = konaklama.KonaklamaTuruAdi,
                Fiyatlar = FiyatOlustur(otelId, konaklamaId),
            };
            return item;
        }

        private List<OtelFiyatItemViewModel> FiyatOlustur(int otelId, int konaklamaId)
        {
            var odatipleri = Query<OdaTipi>(null);
            var fiyatlar = Query<OtelFiyat>(new { OtelId = otelId, KonaklamaId = konaklamaId });
            var odaFiyatlari = new List<OtelFiyatItemViewModel>();
            foreach (var odatipi in odatipleri.OrderBy(x => x.OdaTipiId))
            {
                var fiyat = fiyatlar.FirstOrDefault(x => x.OdaTipiId == odatipi.OdaTipiId);
                odaFiyatlari.Add(new OtelFiyatItemViewModel()
                {
                    KonaklamaId = fiyat?.KonaklamaId,
                    OtelFiyatId = fiyat?.OtelFiyatId,
                    OdaTipiId = odatipi.OdaTipiId,
                    OdaTipi = odatipi.OdaTipiAdi,
                    FiyatYetiskin = fiyat?.FiyatYetiskin,
                    FiyatCocuk = fiyat?.FiyatCocuk,
                });
            }
            return odaFiyatlari;
        }

        public void FiyatListesiKaydet(OtelFiyatViewModel fiyat)
        {
            foreach (var item in fiyat.Fiyatlar)
            {
                var oda = new OtelFiyat()
                {
                    OtelFiyatId = item.OtelFiyatId??0,
                    OtelId = fiyat.OtelId,
                    KonaklamaId = fiyat.KonaklamaId,
                    OdaTipiId = item.OdaTipiId,
                    FiyatYetiskin = item.FiyatYetiskin??0,
                    FiyatCocuk = item.FiyatCocuk??0,
                };
                InsertOrUpdate<OtelFiyat>(oda, oda.OtelFiyatId);
            }
        }
    }
}
