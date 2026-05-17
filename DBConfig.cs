using System.Configuration;
using System.Data.SqlClient;

namespace DocAtHome
{
    public static class DBConfig
    {
        public static string connString =>
            ConfigurationManager.ConnectionStrings["DocAtHome"].ConnectionString;

        public static SqlConnection GetConnection() => new SqlConnection(connString);
    }
}
