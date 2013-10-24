using System.Data;
using System.Data.SqlClient;
using System.Security;

namespace Turn5.DataAccess
{
    public static class SqlBuilder
    {
        public static SqlDataReader ExecuteStoredProcedure(string connectionString, string storedProcName)
        {
            var command = SqlCommandBuilder.BuildCommand(GetConnection(connectionString), storedProcName, CommandType.StoredProcedure);
            return command.ExecuteReader();
        }

        public static SqlDataReader ExecuteInlineSql(string connectionString, string sql)
        {
            var command = SqlCommandBuilder.BuildCommand(GetConnection(connectionString), sql, CommandType.Text);
            return command.ExecuteReader();
        }

        public static SqlDataReader ExecuteTableDirect(string connectionString, string sql)
        {
            var command = SqlCommandBuilder.BuildCommand(GetConnection(connectionString), sql, CommandType.TableDirect);
            return command.ExecuteReader();
        }

        public static SqlDataReader ExecuteStoredProcedureWithCredentials(string connectionString, string storedProcName, string userId, SecureString password)
        {
            var command = SqlCommandBuilder.BuildCommand(GetConnectionWithCredentials(connectionString, userId, password), storedProcName, CommandType.StoredProcedure);
            return command.ExecuteReader();
        }

        public static SqlDataReader ExecuteInlineSqlWithCredentials(string connectionString, string sql, string userId, SecureString password)
        {
            var command = SqlCommandBuilder.BuildCommand(GetConnectionWithCredentials(connectionString, userId, password), sql, CommandType.Text);
            return command.ExecuteReader();
        }

        public static SqlDataReader ExecuteTableDirectWithCredentials(string connectionString, string sql, string userId, SecureString password)
        {
            var command = SqlCommandBuilder.BuildCommand(GetConnectionWithCredentials(connectionString, userId, password), sql, CommandType.TableDirect);
            return command.ExecuteReader();
        }

        private static SqlConnection GetConnection(string connectionString)
        {
            var connection = SqlConnectionBuilder.BuildConnection(connectionString);
            return connection;
        }

        private static SqlConnection GetConnectionWithCredentials(string connectionString, string userId, SecureString password)
        {
            var credentials = new SqlCredential(userId, password);
            var connection = SqlConnectionBuilder.BuildConnection(connectionString, credentials);
            return connection;
        }
    }
}
