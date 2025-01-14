using Dapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Dnct.Infrastructure.Persistence.Repositories
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserQueryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(DbConst.DbConnectionName);
        }
        public async Task<List<UserQueryModel>> GetUserQuries()
        {
            var userQueries = new List<UserQueryModel>();

            var sql = @"
                        SELECT 
	                        usr_qry_id AS UserQueryId, 
	                        table_config_id AS TableConfigId, 
	                        usr_qry AS UserQuery, 
	                        base_query_ind AS BaseQueryIndicator, 
	                        qry_order_ind AS QueryOrderIndicator, 
	                        row_instr_ts AS RowInsertTimestamp
                        FROM codebotmstr.usr_queries
                        WHERE table_config_id = 100
                    ";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                userQueries = (await conn.QueryAsync<UserQueryModel>(sql)).ToList();
            }
            return userQueries;
        }
        public async Task<UserQueryModel> GetUserQueryByQueryId(int queryId)
        {
            var userQueries = new UserQueryModel();

            var sql = @"
                        SELECT 
	                        usr_qry_id AS UserQueryId, 
	                        table_config_id AS TableConfigId, 
	                        usr_qry AS UserQuery, 
	                        base_query_ind AS BaseQueryIndicator, 
	                        qry_order_ind AS QueryOrderIndicator, 
	                        row_instr_ts AS RowInsertTimestamp
                        FROM codebotmstr.usr_queries
                        WHERE table_config_id = 100
                        AND usr_qry_id = @queryId
                    ";
            var parameters = new { queryId = queryId };

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                userQueries = (await conn.QueryAsync<UserQueryModel>(sql, parameters)).FirstOrDefault();
            }
            return userQueries;
        }
        public async Task Create(UserQueryModel userQuery)
        {
            var maxIdSql = "SELECT MAX(usr_qry_id) from codebotmstr.usr_queries;";

            string sql = @"
                INSERT INTO codebotmstr.usr_queries
                (
                    usr_qry_id, 
                    table_config_id, 
                    usr_qry, 
                    base_query_ind, 
                    qry_order_ind, 
                    row_instr_ts
                )
                VALUES
                (
                    @UserQueryId, 
                    @TableConfigId, 
                    @UserQuery, 
                    @BaseQueryIndicator, 
                    @QueryOrderIndicator, 
                    @RowInsertTimestamp
                );
            ";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var maxId = (await conn.QueryAsync<int>(maxIdSql)).ToList();
                userQuery.UserQueryId = maxId[0] + 1;

                await conn.OpenAsync();
                await conn.ExecuteAsync(sql, userQuery);
            }

        }
        public async Task Update(UserQueryModel userQuery)
        {
            string sql = @"
                UPDATE codebotmstr.usr_queries
                SET
                    usr_qry = @UserQuery,
                    base_query_ind = @BaseQueryIndicator,
                    qry_order_ind = @QueryOrderIndicator,
                    row_instr_ts = @RowInsertTimestamp
                WHERE
                    usr_qry_id = @UserQueryId
                AND table_config_id = @TableConfigId
            ";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                await conn.ExecuteAsync(sql, userQuery);
            }

        }
        public async Task Delete(UserQueryModel userQuery)
        {
            string sql = @"
            DELETE FROM codebotmstr.usr_queries
            WHERE usr_qry_id = @UserQueryId
            AND table_config_id = @TableConfigId;
        ";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                await conn.ExecuteAsync(sql, userQuery);
            }

        }
    }
}
