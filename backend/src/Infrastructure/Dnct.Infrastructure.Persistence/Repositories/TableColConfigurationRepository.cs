using Dapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Dnct.Domain.Entities.Order;
using Dnct.Domain.Model;
using Dnct.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Dnct.Infrastructure.Persistence.Repositories;

public class TableColConfigurationRepository : ITableColConfigurationRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public TableColConfigurationRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString(DbConst.DbConnectionName);
    }

    public async Task<List<TableColConfigurationModel>> GetAll()
    {
        var data = new List<TableColConfigurationModel>();

        string sql = @"
                SELECT 
                    tbl_col_confgrtn_id AS TblColConfgrtnId,
                    tbl_confgrtn_id AS TblConfgrtnId,
                    colmn_name AS ColmnName,
                    data_type AS DataType,
                    colmn_trnsfrmtn_step1 AS ColmnTrnsfrmtnStep1,
                    genrt_id_ind AS GenrtIdInd,
                    id_genrtn_stratgy_id AS IdGenrtnStratgyId,
                    type2_start_ind AS Type2StartInd,
                    type2_end_ind AS Type2EndInd,
                    curr_row_ind AS CurrRowInd,
                    pattern1 AS Pattern1,
                    pattern2 AS Pattern2,
                    pattern3 AS Pattern3,
                    lad_ind AS LadInd,
                    join_dups_ind AS JoinDupsInd,
                    confgrtn_eff_start_ts AS ConfgrtnEffStartTs,
                    confgrtn_eff_end_ts AS ConfgrtnEffEndTs
                FROM codebotmstr.tbl_col_confgrtn
                WHERE tbl_confgrtn_id = 11
                and confgrtn_eff_end_ts > current_timestamp(0)
                ORDER BY tbl_col_confgrtn_id 
                ";
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            data = (await conn.QueryAsync<TableColConfigurationModel>(sql)).ToList();
        }
        return data;
    }
}