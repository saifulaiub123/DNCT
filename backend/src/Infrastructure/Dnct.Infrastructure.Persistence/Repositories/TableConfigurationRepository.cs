using Dapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Infrastructure.Persistence.Repositories
{
    public class TableConfigurationRepository : ITableConfigurationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public TableConfigurationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(DbConst.DbConnectionName);
        }

        public async Task<List<TableConfigurationModel>> GetTableInstanceByDatabaseSourceId(int databaseSourceId)
        {
            var instances = new List<TableConfigurationModel>();

            var sql = @"
                        SELECT 
                            tbl.tbl_confgrtn_id AS TableConfigId,
                            tbl.datbs_src_id AS DatabaseSourceId,
                            tbl.instnc_name AS InstanceName
                        FROM 
                            codebotmstr.tbl_confgrtn tbl
                        WHERE 
                            tbl.confgrtn_eff_end_ts > current_timestamp(0)
                            AND tbl.datbs_src_id = @id

                       ";
            var parameters = new { id = databaseSourceId };
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                await connection.OpenAsync();
                instances = (await connection.QueryAsync<TableConfigurationModel>(sql, parameters)).ToList();
            }
            return instances;
        }
    }
}
