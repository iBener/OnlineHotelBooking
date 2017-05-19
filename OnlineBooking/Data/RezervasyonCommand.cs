using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using OnlineBooking.ViewModels;

namespace OnlineBooking.Data
{
    public class RezervasyonCommand : CommandBase<Rezervasyon>
    {
        public RezervasyonCommand(DbModel model, DbConnection connection) : base(model, connection)
        {
        }

        public RezervasyonViewModel GetRezervasyonViewModel(int id, int otelId, int otelFiyatId, string giris, string cikis, int yetiskin, int cocuk)
        {
            var otel = FindWithId<Otel>(otelId);
            var model = new RezervasyonViewModel(yetiskin)
            {
                Otel = otel,
                FaturaBilgileri = new Musteri(),
                KrediKarti = new KrediKartiViewModel(),
                SozleymeOnay = false,
            };

            return model;
        }

        public string RezervasyonKaydet(RezervasyonViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
