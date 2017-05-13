using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.FastCrud;
using OnlineBooking.Models;

namespace OnlineBooking.Data
{
    public class TesisCommands : CommandBase<Otel>
    {
        public TesisCommands(DbModel model, DbConnection connection) : base(model, connection)
        {
        }

    }
}
