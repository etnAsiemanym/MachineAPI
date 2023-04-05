using Npgsql;
using System.Data;

namespace MachineAPI.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("PostgresqlConnection");
        }
        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}
