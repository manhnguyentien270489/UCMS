using System;
using System.Data;
using System.Linq;
using Npgsql;

namespace UCMS.Core
{
    public class PgSqlHelper
    {
        public static TResult ExecuteReader<TResult>(string connectionString, CommandType commandType, string commandText, NpgsqlParameter[] parameters, Func<IDataReader, TResult> func)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var comm = CreateCommand(conn, commandType, commandText, parameters))
                {
                    var dr = comm.ExecuteReader();
                    var result = func(dr);
                    return result;
                }
            }
        }

        public static TResult ExecuteScalar<TResult>(string connectionString, CommandType commandType, string commandText, NpgsqlParameter[] parameters)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var comm = CreateCommand(conn, commandType, commandText, parameters))
                {
                    var result = comm.ExecuteScalar();
                    return (TResult)result;
                }
            }
        }

        public static void ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, NpgsqlParameter[] parameters)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var comm = CreateCommand(conn, commandType, commandText, parameters))
                {
                    comm.ExecuteNonQuery();
                }
            }
        }

        public static void ExecuteNonQuery(string storedName, NpgsqlParameter[] parameters)
        {
            ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, storedName, parameters);
        }

        public static TResult ExecuteReader<TResult>(string connectionString, CommandType commandType, string commandText, NpgsqlParameter[] parameters, Func<IDataReader, NpgsqlCommand, TResult> func)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var comm = CreateCommand(conn, commandType, commandText, parameters))
                {
                    var dr = comm.ExecuteReader();
                    var result = func(dr, comm);
                    return result;
                }
            }
        }

        public static TResult ExecuteReader<TResult>(string storedName, NpgsqlParameter[] parameters,
            Func<IDataReader, TResult> func)
        {
            return ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, storedName, parameters, func);
        }

        public static void ExecuteCommand(NpgsqlConnection connection, NpgsqlTransaction transaction, CommandType commandType,
            string commandText, NpgsqlParameter[] parameters)
        {
            using (var command = CreateCommand(connection, commandType, commandText, parameters))
            {
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
        }

        private static NpgsqlCommand CreateCommand(NpgsqlConnection conn, CommandType commandType, string commandText, NpgsqlParameter[] parameters, int timeoutSeconds = 60)
        {
            var comm = new NpgsqlCommand(commandText);
            comm.Parameters.Clear();
            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    comm.Parameters.Add(param);
                }
            }
            comm.CommandType = commandType;
            comm.CommandTimeout = timeoutSeconds;
            comm.Connection = conn;
            comm.CommandText = commandText;
            return comm;
        }

        public static void StartTransaction(string connectionString, Action<NpgsqlConnection, NpgsqlTransaction> action)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    action.Invoke(conn, transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}
