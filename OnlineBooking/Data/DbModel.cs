using OnlineBooking.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooking.Data
{
    public class DbModel : IDisposable
    {

        public DbModel(VeriTabani veriTabani)
        {
            Connection = veriTabani.GetConnection(open: true);

            Tesis = new TesisCommands(this, Connection);
            Otel = new OtelBulCommands(this, Connection);
            Rezervasyon = new RezervasyonCommand(this, Connection);
            Kullanici = new KullaniciCommands(this, Connection);
        }

        protected DbConnection Connection { get; }
        protected DbTransaction transaction;

        public TesisCommands Tesis { get; }
        public OtelBulCommands Otel { get; }
        public RezervasyonCommand Rezervasyon { get; }
        public KullaniciCommands Kullanici { get; }

        public void Dispose()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
