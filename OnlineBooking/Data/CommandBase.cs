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

namespace OnlineBooking.Data
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
            return GetTabloAdi<T>();
        }

        private string GetTabloAdi<TModel>()
        {
            //var att = (TableAttribute)Attribute.GetCustomAttribute(typeof(TModel), typeof(TableAttribute));
            var typInfo = typeof(TModel).GetTypeInfo();
            var att = typInfo.GetCustomAttribute<TableAttribute>();
            return att.Name;
        }

        private string GetKeyColumnName()
        {
            return GetKeyColumnName<T>();
        }

        private string GetKeyColumnName<TModel>()
        {
            foreach (var property in typeof(TModel).GetProperties())
            {
                var key = property.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault();
                if (key != null)
                {
                    return property.Name;
                }
            }
            throw new Exception("Key kolon adı bulunamadı.");
        }

        private int? GetKeyColumnValue(T item)
        {
            return GetKeyColumnValue<T>(item);
        }

        private int? GetKeyColumnValue<TModel>(TModel item)
        {
            foreach (var property in typeof(TModel).GetProperties())
            {
                var key = property.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault();
                if (key != null)
                {
                    return (int?)property.GetValue(item);
                }
            }
            throw new Exception("Key kolon değeri bulunamadı.");
        }

        public bool Delete(T entity)
        {
            return Connection.Delete<T>(entity); ;
        }

        public bool Delete<TModel>(TModel entity)
        {
            return Connection.Delete<TModel>(entity); ;
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
            return FindWithId<T>(id);
        }

        public TModel FindWithId<TModel>(int id)
        {
            var tabloAdi = GetTabloAdi<TModel>();
            var keyKolon = GetKeyColumnName<TModel>();
            var query = $"SELECT * FROM { tabloAdi } WHERE { keyKolon } = @id ";
            return Connection.QueryFirst<TModel>(query, new { id = id });
        }

        public void Insert(T entity)
        {
            Connection.Insert<T>(entity);
        }

        public void Insert<TModel>(TModel entity)
        {
            Connection.Insert<TModel>(entity);
        }

        public IEnumerable<T> Query(object param)
        {
            return Query<T>(param);
        }

        public IEnumerable<TModel> Query<TModel>(object param)
        {
            var query = BuildQuery<TModel>("SELECT *", param);
            return Connection.Query<TModel>(query, param);
        }

        public IEnumerable<dynamic> Query(string kolonlar, object param)
        {
            var query = BuildQuery($"SELECT { kolonlar }", param);
            return Connection.Query<dynamic>(query, param);
        }

        private string BuildQuery(string kolonlar, object param)
        {
            return BuildQuery<T>(kolonlar, param);
        }

        private string BuildQuery<TModel>(string kolonlar, object param)
        {
            var tabloAdi = GetTabloAdi<TModel>();
            var whereKosul = new List<string>();
            if (param != null)
            {
                foreach (var item in param.GetType().GetProperties())
                {
                    whereKosul.Add(String.Join(" = ", new string[] { item.Name, item.GetValue(param).ToString() }));
                }
            }
            var query = $"{ kolonlar } FROM { tabloAdi } \n";
            if (whereKosul.Count > 0)
            {
                query += $"WHERE { String.Join(" AND ", whereKosul) }\n";
            }
            return query;
        }

        public bool Update(T entity)
        {
            return Connection.Update<T>(entity);
        }

        public bool Update<TModel>(TModel entity)
        {
            return Connection.Update<TModel>(entity);
        }

        public bool InsertOrUpdate(T entity, int? id)
        {
            return InsertOrUpdate<T>(entity, id);
        }

        public bool InsertOrUpdate<TModel>(TModel entity, int? id)
        {
            if (id == null || id == 0)
            {
                Connection.Insert<TModel>(entity);
                return true;
            }
            return Connection.Update<TModel>(entity);
        }
    }
}
