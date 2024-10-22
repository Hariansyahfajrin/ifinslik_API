using Npgsql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DotNetEnv;

namespace DAL.Helper
{
    public class Database
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public Database()
        {
            Env.Load(); // ini untuk mendapatkan variabel di file .env
            _connectionString = GetConnectionString();
        }
        public IDbTransaction BeginTransaction()
        {
            var connection = Connection;
            return connection.BeginTransaction();
        }
        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = GetConnection();
                    _connection.Open();
                }

                return _connection;
            }
        }

        private IDbConnection GetConnection()
        {
            var databaseType = Env.GetString("DATABASE_TYPE");
            return databaseType switch
            {
                "PostgreSQL" => new NpgsqlConnection(_connectionString),
                "SqlServer" => new SqlConnection(_connectionString),
                "MySql" => new MySqlConnection(_connectionString),
                "Oracle" => new OracleConnection(_connectionString),
                _ => throw new InvalidOperationException("Unsupported database type.")
            };
        }

        private string GetConnectionString()
        {
            return Env.GetString("DATABASE_CONNECTION_STRING");
        }

        public string Type()
        {
            return Env.GetString("DATABASE_TYPE");
        }

        public string Symbol()
        {
            var databaseType = Env.GetString("DATABASE_TYPE");

            switch (databaseType)
            {
                case "Oracle":
                    return ":";
                default:
                    return "@";
            }
        }
    }
}
