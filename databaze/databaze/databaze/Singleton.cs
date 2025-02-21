using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    internal class Singleton
    {

        private static string connection;

        static Singleton()
        {
            connection = ConfigurationManager.ConnectionStrings["WinAu"].ConnectionString;
        }

        /// <summary>
        /// Připojení do databáze
        /// </summary>
        /// <returns>připojení do databáze</returns>
        public static SqlConnection Connect()
        {
            return new SqlConnection(connection);
        }
    }

}
