using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.FastCrud;
using System.Data.Common;

namespace OnlineBooking.Helpers
{
    public class VeriTabani
    {

        public string ProviderName { get; set; }

        public string ConnectionString { get; set; }

        public DbConnection GetConnection(bool open = false)
        {
            DbConnection connection = null;

            switch (ProviderName)
            {
                case "MsSql":
                    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                    break;
                case "MySql":
                    break;
                case "PostgreSql":
                    break;
                case "SqLite":
                    break;
                default:
                    break;
            }

            if (open)
            {
                connection.Open();
            }

            return connection;
        }

    }
}
