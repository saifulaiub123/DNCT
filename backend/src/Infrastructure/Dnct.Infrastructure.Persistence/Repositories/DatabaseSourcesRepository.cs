
using Dapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Dnct.Domain.Model;
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

        public async Task CrateTable(DatabaseSources dbSrcModel)
        {
            var maxIdSql = "SELECT MAX(datbs_src_id) from codebotmstr.datbs_srcs;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                await connection.OpenAsync();
                var maxId = (await connection.QueryAsync<int>(maxIdSql)).ToList();
                dbSrcModel.DatbsSrcId = maxId[0] + 1;

                var insertSql = "INSERT INTO codebotmstr.datbs_srcs (datbs_src_id, conctn_name, tbl_dbs_name, tbl_name, confgrtn_eff_end_ts) VALUES (@DatbsSrcId, @ConctnName, @TblDbsName, @TblName, @ConfgrtnEffEndTs)";
                var rowsAffected = await connection.ExecuteAsync(insertSql, dbSrcModel);
            }

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
        public async Task<List<DatabaseSourceModel>> GetDatabasesByServerIds(List<int> ids)
        {
            var databases = new List<DatabaseSourceModel>();
            var sql = @"SELECT DISTINCT
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
                        AND conmstr.contn_id = ANY (@Ids);";

            var parameters = new { Ids = ids };

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                await connection.OpenAsync();
                databases = (await connection.QueryAsync<DatabaseSourceModel>(sql, parameters)).ToList();
            }
            return databases;
        }

        public async Task<List<DatabaseSourceModel>> GetTableInstance(string connection, string databaseName, string tableName)
        {
            var databases = new List<DatabaseSourceModel>();

            var sql = @"
                        SELECT *
                        FROM 
                            codebotmstr.datbs_srcs dsrc  
                        WHERE 
                            conctn_name = @connection
                            AND tbl_dbs_name = @databaseName
                            AND tbl_name = @tableName
                    ";
            var parameters = new { connection = connection, databaseName = databaseName, tableName = tableName };
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                databases = (await conn.QueryAsync<DatabaseSourceModel>(sql, parameters)).ToList();
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