using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.Models
{
    internal interface ICommands<T>
    {
        IEnumerable<T> Query(object param);

        IEnumerable<dynamic> Query(string kolonlar, object param);

        T FindWithId(int id);

        void Insert(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        bool DeleteWithId(int id);

        object Execute(string command, object param = null);
    }
}
