using System.Data;
using System.Data.SqlClient;

namespace MyCompany.DataAccess
{
    public static class SqlCommandBuilder
    {
        private static SqlCommand _sqlCommand;

        public static SqlCommand BuildCommand(SqlConnection connection, string commandText, CommandType commandType)
        {
            return BuildCommand(connection, commandText, commandType, null);
        }

        public static SqlCommand BuildCommand(SqlConnection connection, string commandText, CommandType commandType, SqlTransaction transaction)
        {
            _sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandText = commandText,
                    CommandType = commandType,
                    Transaction = transaction
                };
            return _sqlCommand;
        }
    }
}
