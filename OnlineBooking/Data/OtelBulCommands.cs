using OnlineBooking.Models;
using OnlineBooking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using Dapper;
using Dapper.FastCrud;

namespace OnlineBooking.Data
{
    public class OtelBulCommands : CommandBase<Otel>
    {
        public OtelBulCommands(DbModel model, DbConnection connection) : base(model, connection)
        {
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
