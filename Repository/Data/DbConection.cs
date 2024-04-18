using Npgsql;
using Npgsql.Replication.PgOutput.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace Repository.Data
{
    public class DbConection
    {
        public string connectionString = "Username = postgres; Password = lapoderosa123; Host = localhost; Port = 5432; Database = postgres";


        public DbConection(string _connectionString)
        {
            _connectionString = this.connectionString;
        }


        public IDbConnection dbConnection()
        {
            try
            {
                IDbConnection conexion = new NpgsqlConnection(connectionString);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {

                throw new NpgsqlException(ex.Message);
            }
        }
    }
}
