using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.Models
{
    public class BaseBI
    {
        public BaseBI(DbModel model, DbConnection connection)
        {
            Model = model;
            Connection = connection;
        }

        public DbModel Model { get; set; }
        public DbConnection Connection { get; set; }
    }
}
