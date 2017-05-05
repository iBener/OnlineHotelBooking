using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.FastCrud;

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
            throw new NotImplementedException();
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
