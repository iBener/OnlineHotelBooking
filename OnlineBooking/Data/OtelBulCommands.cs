using OnlineBooking.Models;
using OnlineBooking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using OnlineBooking.Helpers;
using Dapper;

namespace OnlineBooking.Data
{
    public class OtelBulCommands : CommandBase<Otel>
    {
        public OtelBulCommands(DbModel model, DbConnection connection) : base(model, connection)
        {
        }

        public IEnumerable<OtelViewModel> OtelListesiOku(string bolge, string giris, string cikis, int yetiskin, int cocuk, List<string> fiyat, List<KonaklamaTipleri> konaklama)
        {
            if (String.IsNullOrEmpty(bolge))
            {
                bolge = String.Empty;
            }
            var w_konaklama = "";
            if (konaklama != null && konaklama.Count > 0)
            {
                int[] sonuc = konaklama.Select(x => (int)x).ToArray();
                w_konaklama = " and f.KonaklamaId in(" + String.Join(",", sonuc) + ")";
            }
            var query =
                "select distinct o.* from Otel o \n" +
                "inner join OtelFiyat f on f.OtelId = o.OtelId \n" +
                "where (o.BolgeAdi like '%'+@bolge+'%' or o.IlAdi like '%'+@bolge+'%' or o.IlceAdi like '%'+@bolge+'%') \n" +
                "and f.FiyatYetiskin<>0 " + w_konaklama;

            var oteller = Connection.Query<OtelViewModel>(query, new { bolge });
            var tgiris = Convert.ToDateTime(giris);
            var tcikis = Convert.ToDateTime(cikis);
            var gece = (int)(tcikis - tgiris).TotalDays;
            var tesis = new TesisCommands(Model, Connection);

            foreach (var otel in oteller)
            {
                var resim = tesis.GetOtelResimleri(otel.OtelId, (int)OdaTipleri.Yok).FirstOrDefault();
                otel.Resim = new List<OtelResim> { resim };

                var enUcuzOdafiyati = Connection.QuerySingleOrDefault<OtelFiyatViewModel>(
                    "select top 1 f.* from OtelFiyat f where f.OtelId = @otelId and f.FiyatYetiskin <> 0 " + 
                    w_konaklama + " order by f.FiyatYetiskin, f.FiyatCocuk", new { otelId = otel.OtelId });
                enUcuzOdafiyati.Gece = gece;
                enUcuzOdafiyati.Yetiskin = yetiskin;
                enUcuzOdafiyati.Cocuk = cocuk;
                otel.Fiyat = new List<OtelFiyatViewModel> { enUcuzOdafiyati };
            }

            return oteller;
        }

        public OtelViewModel OtelModelOku(int otelId, string giris, string cikis, int yetiskin, int cocuk)
        {
            var otel = FindWithId(otelId);
            var model = new OtelViewModel(otel);
            var query =
                "select f.*, o.OdaTipiAdi, k.KonaklamaTuruAdi, '' ImageUrl \n" +
                "from OdaTipi o join OtelFiyat f on f.OdaTipiId = o.OdaTipiId \n" +
                "join KonaklamaTuru k on k.KonaklamaTuruId = f.KonaklamaId \n" +
                "--left join OtelResim r on r.OtelId = @OtelId and r.OdaTipiId = f.OdaTipiId \n" +
                "where f.OtelId=@OtelId and f.FiyatYetiskin<>0 order by f.KonaklamaId,f.OdaTipiId \n";

            model.Fiyat = Connection.Query<OtelFiyatViewModel>(query, new { OtelId = otelId });
            var tgiris = Convert.ToDateTime(giris);
            var tcikis = Convert.ToDateTime(cikis);
            var gece = (int)(tcikis - tgiris).TotalDays;

            foreach (var fiyat in model.Fiyat)
            {
                fiyat.Gece = gece;
                fiyat.Yetiskin = yetiskin;
                fiyat.Cocuk = cocuk;
                fiyat.ImageUrl = Connection.ExecuteScalar<string>(
                    "select top 1 ImageUrl from OtelResim where OtelId = @otelId and OdaTipiId = @odaTipiId ",
                    new { otelId, odaTipiId = fiyat.OdaTipiId });
            }
            var tesis = new TesisCommands(Model, Connection);
            model.Resim = tesis.GetOtelResimleri(otelId);

            return model;
        }
    }
}
