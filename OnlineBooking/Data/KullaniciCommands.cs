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
        public KullaniciCommands(DbModel model, DbConnection connection, DbTransaction transaction) : base(model, connection, transaction)
        {
        }

        public Kullanici GirisKontrol(string KullaniciAdi, string Parola)
        {
            var model = Query(new { KullaniciAdi, Parola }).FirstOrDefault();
            
            return model;
        }

        public void Kaydet(KullaniciViewModel model)
        {
            model.Kullanici.Rol = "Musteri";
            model.Kullanici.KayitTarihi = DateTime.Now;
            model.Kullanici.Aktif = true;
            Insert(model.Kullanici);

            model.Musteri.KullaniciId = model.Kullanici.KullaniciId;
            model.Musteri.EPostasi = model.Kullanici.KullaniciAdi;
            model.Musteri.DogumTarihi = new DateTime(1900, 1, 1);
            Insert(model.Musteri);
        }
    }
}
