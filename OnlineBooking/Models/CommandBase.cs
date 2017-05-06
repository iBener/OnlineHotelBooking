using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.FastCrud;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineBooking.Models
{
    public class CommandBase<T> : ICommands<T>
    {
        public CommandBase(DbModel model, DbConnection connection)
        {
            Model = model;
            Connection = connection;
        }

        public DbModel Model { get; set; }
        public DbConnection Connection { get; set; }

        private string GetTabloAdi()
        {
            //var att = (TableAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));
            var typInfo = typeof(T).GetTypeInfo();
            var att = typInfo.GetCustomAttribute<TableAttribute>();
            return att.Name;
        }

        private string GetKeyColumnName()
        {
            foreach (var item in typeof(T).GetProperties())
            {
                var key = item.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault();
                if (key != null)
                {
                    return item.Name;
                }
            }
            throw new Exception("Key kolon adı bulunamadı.");
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWithId(int id)
        {
            throw new NotImplementedException();
        }

        public int Execute(string command, object param = null)
        {
            throw new NotImplementedException();
        }

        public T FindWithId(int id)
        {
            var tabloAdi = GetTabloAdi();
            var keyKolon = GetKeyColumnName();
            var query = $"SELECT * FROM { tabloAdi } WHERE { keyKolon } = { id }";
            return Connection.QueryFirst<T>(query);
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query(object param)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
