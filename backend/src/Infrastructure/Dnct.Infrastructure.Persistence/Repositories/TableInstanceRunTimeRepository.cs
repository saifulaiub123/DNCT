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
using Dapper;

namespace Dnct.Infrastructure.Persistence.Repositories
{
    public class TableInstanceRunTimeRepository : ITableInstanceRunTimeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public TableInstanceRunTimeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(DbConst.DbConnectionName);
        }

        public async Task<List<TableInstanceRunTimeModel>> GetAll(int tableConfigId)
        {
            var loadStrategies = new List<TableInstanceRunTimeModel>();

            var sql = @"

                        select tc.tbl_confgrtn_id AS TableConfigId,tc.instnc_name As InstanceName, tirt.instnc_ordr as InstanceOrder ,tirt.ovride_ind  as OverrideInd
                        from  codebotmstr.tbl_confgrtn tc
                        left outer join  codebotmstr.tbl_instnc_run_time  tirt 
                        on tirt.tbl_confgrtn_id  = tc.tbl_confgrtn_id 
                        and  tirt.confgrtn_eff_end_ts > current_timestamp(0)
                        where tc.datbs_src_id in (select datbs_src_id from  codebotmstr.tbl_confgrtn 
						                        where confgrtn_eff_end_ts > current_timestamp(0)
						                        and tbl_confgrtn_id = @tableConfigId )
                        and  tc.confgrtn_eff_end_ts > current_timestamp(0)
                    ";
            var parameters = new { tableConfigId = tableConfigId };
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                loadStrategies = (await conn.QueryAsync<TableInstanceRunTimeModel>(sql, parameters)).ToList();
            }

            return loadStrategies;
        }


        public async Task Create(List<TableInstanceRunTimeModel> models)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var data = models.FirstOrDefault();
                await conn.ExecuteAsync(@"
                           delete from codebotmstr.tbl_instnc_run_time where tbl_confgrtn_id = @TableConfigId", models.FirstOrDefault());
                
                foreach (var param in models)
                {
                    await conn.ExecuteAsync(@"
                        INSERT INTO codebotmstr.tbl_instnc_run_time(
	                        tbl_confgrtn_id, 
                            reltd_tbl_confgrtn_id, 
                            instnc_ordr, 
                            ovride_ind, 
                            confgrtn_eff_start_ts, 
                            confgrtn_eff_end_ts
                        ) VALUES (
                            @TableConfigId, 
                            @RelatedTableConfigId, 
                            @InstanceOrder, 
                            @OverrideInd, 
                            @ConfigurationEffectiveStartTimestamp, 
                            @ConfigurationEffectiveEndTimestamp)", param);
                    
                }
            }

        }
    }
}


