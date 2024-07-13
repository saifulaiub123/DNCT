
using Dapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Dnct.Infrastructure.Persistence.Repositories
{

    public class DatabaseSourcesRepository : IDatabaseSourcesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DatabaseSourcesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(DbConst.DbConnectionName);
        }

        public async Task<List<DatabaseSources>> GetAllServer()
        {
            var servers = new List<DatabaseSources>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                await connection.OpenAsync();
                servers = (await connection.QueryAsync<DatabaseSources>($"SELECT * FROM {DbConst.SchemaDbo}.datbs_srcs")).ToList();
            }
            return servers;
        }
    }
    

}