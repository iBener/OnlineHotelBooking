using OnlineBooking.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.Models
{
    public class DbModel : IDisposable
    {
        private bool transactionFlag;

        public DbModel(VeriTabani veriTabani)
        {
            Connection = veriTabani.GetConnection(open: true);

            
            
        }

        protected DbConnection Connection { get; }
        protected DbTransaction transaction;

        public void BeginTransaction()
        {
            transaction = Connection.BeginTransaction();
            transactionFlag = false;
        }

        public void RollbackTransaction()
        {
            if (!transactionFlag && transaction != null)
            {
                transaction.Rollback();
                transactionFlag = true;
            }
        }

        public void CommitTransaction()
        {
            if (!transactionFlag && transaction != null)
            {
                transaction.Commit();
                transactionFlag = true;
            }
        }

        public void Dispose()
        {
            RollbackTransaction();
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
