using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.Models
{
    internal interface ICommands<T>
    {
        IEnumerable<T> Query(object param);

        T FindWithId(int id);

        void Insert(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        bool DeleteWithId(int id);

        int Execute(string command, object param = null);
    }
}
