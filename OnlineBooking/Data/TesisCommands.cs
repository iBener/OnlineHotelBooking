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
                Fiyatlar = OtelFiyatlariOlustur(id),
                Resimler = GetOtelResimleri(id),
            };
            return otelModel;
        }

        private IEnumerable<OtelFiyatItemViewModel> OtelFiyatlariOlustur(int id)
        {
            var konaklama = Query<KonaklamaTuru>(null);
            var fiyatlar = new List<OtelFiyatItemViewModel>();
            foreach (var item in konaklama)
            {
                var konaklamaFiyatlari = KonaklamaFiyatOlustur(id, item.KonaklamaTuruId);
                foreach (var fiyat in konaklamaFiyatlari)
                {
                    fiyatlar.Add(fiyat);
                }
            }
            return fiyatlar;
        }

        public OtelFiyatViewModel GetFiyatListesi(int otelId, int konaklamaId)
        {
            var konaklama = FindWithId<KonaklamaTuru>(konaklamaId);

            var item = new OtelFiyatViewModel()
            {
                OtelId = otelId,
                KonaklamaId = konaklama.KonaklamaTuruId,
                Konaklama = konaklama.KonaklamaTuruAdi,
                Fiyatlar = KonaklamaFiyatOlustur(otelId, konaklamaId),
            };
            return item;
        }

        private List<OtelFiyatItemViewModel> KonaklamaFiyatOlustur(int otelId, int konaklamaId)
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

        public IEnumerable<OtelResim> GetOtelResimleri(int id)
        {
            return Query<OtelResim>(new { OtelId = id });
        }
    }
}
