
using Dapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Dnct.Domain.Model;
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

        public async Task<List<DatabaseSourceModel>> GetDatabasesByServerId(int id)
        {
            var databases = new List<DatabaseSourceModel>();

            var sql = @"
                        SELECT DISTINCT
                        conmstr.contn_id AS ServerId,
                        conmstr.host_ip AS HostIp,
                        dsrc.datbs_src_id AS DatabaseSourceId,
                        dsrc.tbl_dbs_name AS DataBaseName,
                        dsrc.tbl_name AS TableName
                    FROM 
                        codebotmstr.datbs_srcs dsrc  
                        LEFT OUTER JOIN codebotmstr.contns_mstr conmstr  
                            ON conmstr.contn_name = dsrc.target_objc_con_name  
                    WHERE 
                        dsrc.confgrtn_eff_end_ts > current_timestamp(0)
                        AND conmstr.contn_id = @id
                ";
            var parameters = new { id = id };
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                await connection.OpenAsync();
                databases = (await connection.QueryAsync<DatabaseSourceModel>(sql,parameters)).ToList();
            }
            return databases;
        }

        public async Task<List<DatabaseSourceModel>> GetTablesByDatabaseSourceId(int databaseSourceId)
        {
            var databases = new List<DatabaseSourceModel>();

            var sql = @"
                        SELECT DISTINCT
                        conmstr.contn_id AS ServerId,
                        conmstr.host_ip AS HostIp,
                        dsrc.datbs_src_id AS DatabaseSourceId,
                        dsrc.tbl_dbs_name AS DataBaseName,
                        dsrc.tbl_name AS TableName
                    FROM 
                        codebotmstr.datbs_srcs dsrc  
                        LEFT OUTER JOIN codebotmstr.contns_mstr conmstr  
                            ON conmstr.contn_name = dsrc.target_objc_con_name  
                    WHERE 
                        dsrc.confgrtn_eff_end_ts > current_timestamp(0)
                        AND dsrc.datbs_src_id = @databaseSourceId
                ";
            var parameters = new { databaseSourceId = databaseSourceId };
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                await connection.OpenAsync();
                databases = (await connection.QueryAsync<DatabaseSourceModel>(sql, parameters)).ToList();
            }
            return databases;
        }
    }
    

}