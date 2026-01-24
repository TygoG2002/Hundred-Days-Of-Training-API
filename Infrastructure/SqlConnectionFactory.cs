using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace Infrastructure
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default") 
                ?? throw new InvalidOperationException("Connection string 'Default' not found.");
        }

        public IDbConnection CreateConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open(); 
            return connection;
        }
    }
}
