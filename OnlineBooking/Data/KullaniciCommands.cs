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

        public void Kaydol(KullaniciViewModel model)
        {
            try
            {
                Transaction = Connection.BeginTransaction();

                model.Kullanici.Rol = "Musteri";
                model.Kullanici.KayitTarihi = DateTime.Now;
                model.Kullanici.Aktif = true;
                Insert<Kullanici>(model.Kullanici);

                model.Musteri.KullaniciId = model.Kullanici.KullaniciId;
                model.Musteri.EPosta = model.Kullanici.KullaniciAdi;
                if (model.Musteri.DogumTarihi == null || model.Musteri.DogumTarihi == DateTime.MinValue)
                {
                    model.Musteri.DogumTarihi = new DateTime(1900, 1, 1);
                }
                Insert<Musteri>(model.Musteri);

                Transaction.Commit();
            }
            catch (Exception)
            {
                Transaction.Rollback();
                throw;
            }
        }

        public void Guncelle(KullaniciViewModel model)
        {
            try
            {
                if (String.IsNullOrEmpty(model.Kullanici.Rol))
                {
                    var kullanici = FindWithId(model.Kullanici.KullaniciId);
                    model.Kullanici.Rol = kullanici.Rol;
                    model.Kullanici.Aktif = kullanici.Aktif;
                }
                Transaction = Connection.BeginTransaction();

                Update<Kullanici>(model.Kullanici);
                Update<Musteri>(model.Musteri);

                Transaction.Commit();
            }
            catch (Exception)
            {
                Transaction.Rollback();
                throw;
            }
        }

        public IEnumerable<KullaniciViewModel> GetKullaniciListesi()
        {
            var kullanicilar = Query(null);
            var sonuc = new List<KullaniciViewModel>();
            foreach (var item in kullanicilar)
            {
                sonuc.Add(new KullaniciViewModel()
                {
                    Kullanici = item,
                    Musteri = Query<Musteri>(new { KullaniciId = item.KullaniciId }).FirstOrDefault()
                });
            }
            return sonuc;
        }

        public KullaniciViewModel GetKullanici(int id)
        {
            var kullanici = FindWithId(id);
            var model = new KullaniciViewModel()
            {
                Kullanici = kullanici,
                Musteri = GetMusteriByKullaniciId(id),
                ParolaDogrula = kullanici.Parola,
            };
            return model;
        }

        public Musteri GetMusteriByKullaniciId(int id)
        {
            return Query<Musteri>(new { KullaniciId = id }).FirstOrDefault();
        }
    }
}
