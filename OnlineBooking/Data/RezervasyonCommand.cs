using OnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;

namespace OnlineBooking.Data
{
    public class RezervasyonCommand : CommandBase<Rezervasyon>
    {
        public RezervasyonCommand(DbModel model, DbConnection connection) : base(model, connection)
        {
        }
    }
}
