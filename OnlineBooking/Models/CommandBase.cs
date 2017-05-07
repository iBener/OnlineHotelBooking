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

        protected DbModel Model { get; set; }
        protected DbConnection Connection { get; set; }

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
            return Connection.Delete<T>(entity); ;
        }

        public bool DeleteWithId(int id)
        {
            var tabloAdi = GetTabloAdi();
            var keyKolon = GetKeyColumnName();
            var query = $"DELETE FROM { tabloAdi } WHERE { keyKolon } = @id ";
            return Connection.Execute(query) > 0;
        }

        public object Execute(string command, object param = null)
        {
            return Connection.ExecuteScalar(command, param);
        }

        public T FindWithId(int id)
        {
            var tabloAdi = GetTabloAdi();
            var keyKolon = GetKeyColumnName();
            var query = $"SELECT * FROM { tabloAdi } WHERE { keyKolon } = @id ";
            return Connection.QueryFirst<T>(query, new { id = id });
        }

        public void Insert(T entity)
        {
            Connection.Insert<T>(entity);
        }

        public IEnumerable<T> Query(object param)
        {
            var query = BuildQuery("SELECT *", param);
            return Connection.Query<T>(query, param);
        }

        public IEnumerable<dynamic> Query(string kolonlar, object param)
        {
            var query = BuildQuery($"SELECT { kolonlar }", param);
            return Connection.Query<dynamic>(query, param);
        }

        private string BuildQuery(string kolonlar, object param)
        {
            var tabloAdi = GetTabloAdi();
            var keyKolon = GetKeyColumnName();
            var whereKosul = new List<string>();
            foreach (var item in param.GetType().GetProperties())
            {
                whereKosul.Add(String.Join(" = ", new string[] { item.Name, item.GetValue(param).ToString() }));
            }  
            var query = $"{ kolonlar } FROM { tabloAdi } \n" +
                        $"WHERE { String.Join(" AND ", whereKosul) }\n";
            return query;
        }

        public bool Update(T entity)
        {
            return Connection.Update<T>(entity);
        }
    }
}
