using OnlineBooking.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.Models
{
    public class DbModel
    {

        public DbModel(VeriTabani veriTabani)
        {
            Connection = veriTabani.GetConnection(open: true);

            
            
        }

        protected DbConnection Connection { get; }
        protected DbTransaction transaction;

       
    }
}
