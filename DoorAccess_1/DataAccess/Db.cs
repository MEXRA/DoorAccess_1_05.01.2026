using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DoorAccess_1.DataAccess
{
    public static class Db
    {
        public static string ConnectionString { get; set; }

        public static SqlConnection CreateConnection()
            => new SqlConnection(ConnectionString);
    }
}
