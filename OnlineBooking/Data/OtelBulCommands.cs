using OnlineBooking.Models;
using OnlineBooking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using OnlineBooking.Helpers;
using OnlineBooking.ViewModels;
using Dapper;

namespace OnlineBooking.Data
{
    public class OtelBulCommands : CommandBase<Otel>
    {
        public OtelBulCommands(DbModel model, DbConnection connection) : base(model, connection)
        {
        }

        public IEnumerable<OtelViewModel> Bul(string bolge, string giris, string cikis, int yetiskin, int cocuk, List<string> fiyat, List<KonaklamaTipleri> konaklama)
        {
            if (String.IsNullOrEmpty(bolge))
            {
                bolge = String.Empty;
            }
            var w_konaklama = "";
            if (konaklama.Count > 0)
            {
                w_konaklama = " and f.KonaklamaId in(" + String.Join(",", konaklama) + ")";
            }
            var query =
                "select distinct o.* from Otel o \n" +
                "inner join OtelFiyat f on f.OtelId = o.OtelId \n" +
                "where (o.BolgeAdi like '%'+@bolge+'%' or o.IlAdi like '%'+@bolge+'%' or o.IlceAdi like '%'+@bolge+'%') \n" +
                "and f.FiyatYetiskin<>0 " + w_konaklama;

            var oteller = Connection.Query<OtelViewModel>(query, new { bolge });

            foreach (var otel in oteller)
            {
                var resim = Execute("select top 1 Path from OtelResim where OtelId = @otelId and OdaTipiId = 0", new { otelId = otel.OtelId });
                var otelfiyati = Connection.QuerySingleOrDefault<dynamic>("select top 1 OtelFiyatId, FiyatYetiskin Yetiskin, FiyatCocuk Cocuk from OtelFiyat f where f.OtelId = @otelId and f.FiyatYetiskin <> 0 " + w_konaklama, new { otelId = otel.OtelId });

                if (resim != null)
                {
                    otel.Resim = resim.ToString();
                }
                otel.Fiyat = yetiskin * (int)otelfiyati.Yetiskin + cocuk * (int)otelfiyati.Cocuk;
            }

            return oteller;
        }

        public OtelOdalarViewModel OtelOda(int id, string giris, string cikis, int yetiskin, int cocuk)
        {
            var otel = FindWithId(id);
            var model = new OtelOdalarViewModel(otel);
            var query =
                "select o.OdaTipiAdi,k.KonaklamaTuruAdi,f.FiyatYetiskin,f.FiyatCocuk,0 ToplamFiyat,r.Path ImageUrl \n" +
                "from OdaTipi o join OtelFiyat f on f.OdaTipiId = o.OdaTipiId \n" +
                "join KonaklamaTuru k on k.KonaklamaTuruId = f.KonaklamaId \n" +
                "left join OtelResim r on r.OdaTipiId = f.OdaTipiId \n" +
                "where f.OtelId=1 and f.FiyatYetiskin<>0 order by f.KonaklamaId,f.OdaTipiId \n";

            model.Odalar = Connection.Query<OtelOdaTipleri>(query);

            return model;
        }
    }
}
