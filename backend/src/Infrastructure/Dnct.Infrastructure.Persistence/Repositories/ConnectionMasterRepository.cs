using Dapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Infrastructure.Persistence.Repositories
{
    public class ConnectionMasterRepository : IConnectionMasterRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ConnectionMasterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(DbConst.DbConnectionName);
        }

        public async Task<List<ConnectionsMaster>> GetAllServer()
        {
            var servers = new List<ConnectionsMaster>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                await connection.OpenAsync();
                servers = (await connection.QueryAsync<ConnectionsMaster>($"SELECT * FROM {DbConst.SchemaDbo}.contns_mstr")).ToList();
            }
            return servers;
        }
    }


}
