using System.Data.SqlClient;

namespace Turn5.DataAccess
{
    public static class SqlConnectionBuilder
    {
        public static SqlConnection BuildConnection(string connectionString)
        {
            return BuildConnection(connectionString, null);
        }

        public static SqlConnection BuildConnection(string connectionString, SqlCredential credential)
        {
            var connection = new SqlConnection(connectionString, credential);
            return connection;
        }
    }
}
