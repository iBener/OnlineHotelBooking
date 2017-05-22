using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using OnlineBooking.ViewModels;

namespace OnlineBooking.Data
{
    public class KullaniciCommands : CommandBase<Kullanici>
    {
        public KullaniciCommands(DbModel model, DbConnection connection) : base(model, connection)
        {
        }

        public Kullanici GirisKontrol(string KullaniciAdi, string Parola)
        {
            var model = Query(new { KullaniciAdi, Parola }).FirstOrDefault();
            
            return model;
        }

        public void Kaydet(KullaniciViewModel model)
        {
            try
            {
                Transaction = Connection.BeginTransaction();

                model.Kullanici.Rol = "Musteri";
                model.Kullanici.KayitTarihi = DateTime.Now;
                model.Kullanici.Aktif = true;
                Insert(model.Kullanici);

                model.Musteri.KullaniciId = model.Kullanici.KullaniciId;
                model.Musteri.EPosta = model.Kullanici.KullaniciAdi;
                Insert(model.Musteri);

                Transaction.Commit();
            }
            catch (Exception)
            {
                Transaction.Rollback();
                throw;
            }
        }
    }
}
