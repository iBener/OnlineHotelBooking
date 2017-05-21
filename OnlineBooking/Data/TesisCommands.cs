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
        public TesisCommands(DbModel model, DbConnection connection, DbTransaction transaction) : base(model, connection, transaction)
        {
        }

        public OtelViewModel GetOtelViewModel(int id)
        {
            var otel = FindWithId(id);
            if (otel == null)
            {
                return null;
            }
            var model = new OtelViewModel(otel)
            {
                Fiyat = OtelFiyatlariOlustur(id),
                Resim = GetOtelResimleri(id)
            };
            return model;
        }

        private IEnumerable<OtelFiyatViewModel> OtelFiyatlariOlustur(int id)
        {
            var konaklama = Query<KonaklamaTuru>(null);
            var fiyatlar = new List<OtelFiyatViewModel>();
            foreach (var item in konaklama)
            {
                var konaklamaFiyatlari = FiyatListesiOlustur(id, item.KonaklamaTuruId);
                foreach (var fiyat in konaklamaFiyatlari)
                {
                    fiyatlar.Add(fiyat);
                }
            }
            return fiyatlar;
        }

        public IEnumerable<OtelResim> GetOtelResimleri(int otelId, int odaTipi = -1)
        {
            if (odaTipi >= 0)
            {
                return Query<OtelResim>(new { OtelId = otelId, OdaTipiId = odaTipi });
            }
            return Query<OtelResim>(new { OtelId = otelId});
        }

        public IEnumerable<OtelFiyatViewModel> GetFiyatListesi(int otelId, int konaklamaId)
        {
            return FiyatListesiOlustur(otelId, konaklamaId);
        }

        public List<OtelFiyatViewModel> FiyatListesiOlustur(int otelId, int konaklamaId)
        {
            var odatipleri = Query<OdaTipi>(null);
            var fiyatlar = Query<OtelFiyat>(new { OtelId = otelId, KonaklamaId = konaklamaId });
            var odaFiyatlari = new List<OtelFiyatViewModel>();
            foreach (var odatipi in odatipleri.OrderBy(x => x.OdaTipiId))
            {
                var fiyat = fiyatlar.FirstOrDefault(x => x.OdaTipiId == odatipi.OdaTipiId);
                odaFiyatlari.Add(new OtelFiyatViewModel()
                {
                    OtelId = otelId,
                    KonaklamaId = konaklamaId,
                    OdaTipiId = odatipi.OdaTipiId,
                    OdaTipiAdi = odatipi.OdaTipiAdi,
                    OtelFiyatId = fiyat != null ? fiyat.OtelFiyatId : 0,
                    FiyatYetiskin = fiyat != null ? fiyat.FiyatYetiskin : 0,
                    FiyatCocuk = fiyat != null ? fiyat.FiyatCocuk : 0,
                });
            }
            return odaFiyatlari;
        }

        public void FiyatListesiKaydet(IEnumerable<OtelFiyatViewModel> fiyat)
        {
            foreach (var item in fiyat)
            {
                if (item.FiyatYetiskin != 0 || item.FiyatCocuk != 0)
                {
                    var oda = new OtelFiyat()
                    {
                        OtelFiyatId = item.OtelFiyatId,
                        OtelId = item.OtelId,
                        KonaklamaId = item.KonaklamaId,
                        OdaTipiId = item.OdaTipiId,
                        FiyatYetiskin = item.FiyatYetiskin,
                        FiyatCocuk = item.FiyatCocuk,
                    };
                    InsertOrUpdate<OtelFiyat>(oda, oda.OtelFiyatId);
                }
                else
                {
                    DeleteWithId<OtelFiyat>(item.OtelFiyatId);
                }
            }
        }
    }
}
