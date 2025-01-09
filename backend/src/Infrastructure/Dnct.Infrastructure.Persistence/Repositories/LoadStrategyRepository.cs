using Dapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Dnct.Domain.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Dnct.Infrastructure.Persistence.Repositories
{
    public class LoadStrategyRepository : ILoadStrategyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LoadStrategyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(DbConst.DbConnectionName);
        }

        public async Task<List<LoadStrategyModel>> GetAll()
        {
            var loadStrategies = new List<LoadStrategyModel>();

            var sql = @"
                        SELECT 
                            ls.load_stratgy_id AS LoadStrategyId, 
                            ls.load_stratgy_name AS LoadStrategyName, 
                            CASE 
                                WHEN tls.load_stratgy_id IS NOT NULL THEN 'Selected' 
                                ELSE 'Not Selected' 
                            END AS status
                        FROM 
                            codebotmstr.load_strategy ls
                        LEFT OUTER JOIN 
                            codebotmstr.tbl_load_strategy tls
                        ON 
                            tls.load_stratgy_id = ls.load_stratgy_id 
                            AND tls.confgrtn_eff_end_ts > CURRENT_TIMESTAMP(0)
                            AND tls.table_config_id = 118
                            ORDER BY ls.load_stratgy_id 
                            ;
                    ";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                loadStrategies = (await conn.QueryAsync<LoadStrategyModel>(sql)).ToList();
            }

            return loadStrategies;
        }

        public async Task<LoadStrategyModel> Get(int tableConfigId, int loadStrategyId)
        {
            var loadStrategies = new List<LoadStrategyModel>();

            var sql = @"
                        SELECT * FROM codebotmstr.tbl_load_strategy t 
                        WHERE t.table_config_id = @tableConfigId AND load_stratgy_id = @loadStrategyId;
                    ";

            var parameters = new { tableConfigId = tableConfigId, loadStrategyId = loadStrategyId };

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                loadStrategies = (await conn.QueryAsync<LoadStrategyModel>(sql,parameters)).ToList();
            }

            return loadStrategies.FirstOrDefault();
        }

        public async Task Create(TblLoadStrategyModel model)
        {
            string sql = @"
                BEGIN;
                    update  codebotmstr.tbl_load_strategy
                    set confgrtn_eff_end_ts = current_timestamp(0)
                    where table_config_id = 200
                    and confgrtn_eff_end_ts >  current_timestamp(0);

                   INSERT INTO codebotmstr.tbl_load_strategy (
                        table_config_id,
                        load_stratgy_id,
                        confgrtn_eff_start_ts,
                        confgrtn_eff_end_ts
                    ) Values(@TableConfigId, @LoadStrategyId, @ConfigurationEffectiveStartTimestamp, @ConfigurationEffectiveEndTimestamp);
               COMMIT;
            ";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                await conn.ExecuteAsync(sql,model);
            }
        }
    }
}

