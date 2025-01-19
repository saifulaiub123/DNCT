using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;


namespace Dnct.Infrastructure.Persistence.Repositories
{
    public class RunTimeParametersMasterRepository : IRunTimeParametersMasterRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public RunTimeParametersMasterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(DbConst.DbConnectionName);
        }

        public async Task<List<RunTimeParametersMasterModel>> GetAll(int tableConfigId)
        {
            var loadStrategies = new List<RunTimeParametersMasterModel>();

            var sql = @"
                        select rtp.rtm_parmtrs_mstr_id AS RuntimeParametersMasterId ,rtpm.parmtr_key AS ParameterKey, rtp.parmtr_val AS ParameterValue, rtp.table_config_id AS TableConfigId
                        from  codebotmstr.run_time_parmtrs_mstr rtpm 
                        left outer join  codebotmstr.run_time_parmtrs rtp
                        on rtp.rtm_parmtrs_mstr_id  = rtpm.rtm_parmtrs_mstr_id 
                        and rtp.confgrtn_eff_end_ts > current_timestamp(0)
                        and rtp.table_config_id = @tableConfigId
                        order by rtpm.rtm_parmtrs_mstr_id
                    ";
            var parameters = new { tableConfigId = tableConfigId };
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                loadStrategies = (await conn.QueryAsync<RunTimeParametersMasterModel>(sql, parameters)).ToList();
            }

            return loadStrategies;
        }


        public async Task Create(List<RunTimeParametersMasterModel> models)

        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                foreach (var param in models)
                {
                    param.ConfigurationEffectiveStartTimestamp = DateTime.UtcNow;
                    param.ConfigurationEffectiveEndTimestamp = new DateTime(9999, 1, 1, 1, 1, 1, DateTimeKind.Utc);

                    // Check if the combination of TableConfigId and RuntimeParametersMasterId already exists
                    var existingData = await conn.QuerySingleAsync<RunTimeParametersModel>(@"
                            SELECT 
                                table_config_id AS TableConfigId, 
                                rtm_parmtrs_mstr_id AS RuntimeParametersMasterId, 
                                parmtr_val AS ParameterValue, 
                                confgrtn_eff_start_ts AS ConfigurationEffectiveStartTimestamp, 
                                confgrtn_eff_end_ts AS ConfigurationEffectiveEndTimestamp  
                            FROM codebotmstr.run_time_parmtrs
                            WHERE table_config_id = @TableConfigId AND rtm_parmtrs_mstr_id = @RuntimeParametersMasterId LIMIT 1", param);

                    if (existingData is not null)
                    {
                        if (existingData.ParameterValue != param.ParameterValue)
                        {
                            // If exists, update the row
                            await conn.ExecuteAsync(@"
                            UPDATE codebotmstr.run_time_parmtrs
                            SET
                                parmtr_val = @ParameterValue,
                                confgrtn_eff_start_ts = @ConfigurationEffectiveStartTimestamp,
                                confgrtn_eff_end_ts = @ConfigurationEffectiveEndTimestamp
                            WHERE table_config_id = @TableConfigId AND rtm_parmtrs_mstr_id = @RuntimeParametersMasterId", param);

                            await conn.ExecuteAsync(@"
                            INSERT INTO codebotmstr.run_time_parmtrs (
                                table_config_id, 
                                rtm_parmtrs_mstr_id, 
                                parmtr_val, 
                                confgrtn_eff_start_ts, 
                                confgrtn_eff_end_ts
                            )
                            VALUES (
                                @TableConfigId, 
                                @RuntimeParametersMasterId, 
                                @ParameterValue, 
                                @ConfigurationEffectiveStartTimestamp, 
                                @ConfigurationEffectiveEndTimestamp
                            )", param);
                        }
                    }
                    else
                    {
                        // If does not exist, insert the row
                        await conn.ExecuteAsync(@"
                            INSERT INTO codebotmstr.run_time_parmtrs (
                                table_config_id, 
                                rtm_parmtrs_mstr_id, 
                                parmtr_val, 
                                confgrtn_eff_start_ts, 
                                confgrtn_eff_end_ts
                            )
                            VALUES (
                                @TableConfigId, 
                                @RuntimeParametersMasterId, 
                                @ParameterValue, 
                                @ConfigurationEffectiveStartTimestamp, 
                                @ConfigurationEffectiveEndTimestamp
                            )", param);
                    }
                }
            }

        }
    }
}


