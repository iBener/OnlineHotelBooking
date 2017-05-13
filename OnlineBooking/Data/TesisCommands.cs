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

        public OtelFiyatViewModel GetFiyatListesi(int otelId, int konaklamaId)
        {
            var konaklama = FindWithId<KonaklamaTuru>(konaklamaId);
            var odatipleri = Query<OdaTipi>(null);
            var fiyatlar = Query<OtelFiyat>(new { OtelId = otelId, KonaklamaId = konaklamaId });

            var item = new OtelFiyatViewModel()
            {
                OtelId = otelId,
                KonaklamaId = konaklama.KonaklamaTuruId,
                Konaklama = konaklama.KonaklamaTuruAdi,
                Fiyatlar = new List<OtelFiyatItemViewModel>()
            };
            foreach (var odatipi in odatipleri.OrderBy(x => x.OdaTipiId))
            {
                var fiyat = fiyatlar.FirstOrDefault(x => x.OdaTipiId == odatipi.OdaTipiId);
                item.Fiyatlar.Add(new OtelFiyatItemViewModel()
                {
                    OtelFiyatId = fiyat?.OtelFiyatId,
                    OdaTipiId = odatipi.OdaTipiId,
                    OdaTipi = odatipi.OdaTipiAdi,
                    FiyatYetiskin = fiyat?.FiyatYetiskin,
                    FiyatCocuk = fiyat?.FiyatCocuk,
                });
            }
            return item;
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
