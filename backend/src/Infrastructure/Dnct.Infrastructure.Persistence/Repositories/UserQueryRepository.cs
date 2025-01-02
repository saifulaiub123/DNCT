using Dapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    }
}
