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

    public async Task<TableColConfigurationModel> GetById(int tblColConfigId,int tblConfigId)
    {
        var data = new TableColConfigurationModel();

        string sql = @"
                SELECT * 
                FROM codebotmstr.tbl_col_confgrtn
                WHERE tbl_confgrtn_id = @tblConfigId AND tbl_col_confgrtn_id = @tblColConfigId
                and confgrtn_eff_end_ts > current_timestamp(0)
                ORDER BY tbl_col_confgrtn_id 
                ";
        var parameters = new { tblConfigId = tblConfigId, tblColConfigId = tblColConfigId };

        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            data = (await conn.QueryAsync<TableColConfigurationModel>(sql, parameters)).FirstOrDefault();
        }
        return data;
    }

    
    public async Task Create(TableColConfiguration tblColConfig)
    {
        var maxIdSql = "SELECT MAX(tbl_confgrtn_id) from codebotmstr.tbl_col_confgrtn where tbl_confgrtn_id = 11 ;";

        string sql = @"
                INSERT INTO codebotmstr.tbl_col_confgrtn (
                    tbl_confgrtn_id,
                    colmn_name,
                    data_type,
                    colmn_trnsfrmtn_step1,
                    genrt_id_ind,
                    id_genrtn_stratgy_id,
                    type2_start_ind,
                    type2_end_ind,
                    curr_row_ind,
                    pattern1,
                    pattern2,
                    pattern3,
                    lad_ind,
                    join_dups_ind,
                    confgrtn_eff_start_ts,
                    confgrtn_eff_end_ts
                )
                VALUES (
                    @TblConfgrtnId,
                    @ColmnName,
                    @DataType,
                    @ColmnTrnsfrmtnStep1,
                    @GenrtIdInd,
                    @IdGenrtnStratgyId,
                    @Type2StartInd,
                    @Type2EndInd,
                    @CurrRowInd,
                    @Pattern1,
                    @Pattern2,
                    @Pattern3,
                    @LadInd,
                    @JoinDupsInd,
                    @ConfgrtnEffStartTs,
                    @ConfgrtnEffEndTs
                );
               ";
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            var maxId = (await conn.QueryAsync<int>(maxIdSql)).ToList();
            tblColConfig.TblColConfgrtnId = maxId[0] + 1;

            await conn.OpenAsync();
            await conn.ExecuteAsync(sql, tblColConfig);
        }
    }

    public async Task Update(TableColConfiguration tblColConfig)
    {
        string sql = @"
                UPDATE codebotmstr.tbl_col_confgrtn
                SET
                    colmn_name = @ColmnName,
                    data_type = @DataType,
                    colmn_trnsfrmtn_step1 = @ColmnTrnsfrmtnStep1,
                    genrt_id_ind = @GenrtIdInd,
                    id_genrtn_stratgy_id = @IdGenrtnStratgyId,
                    type2_start_ind = @Type2StartInd,
                    type2_end_ind = @Type2EndInd,
                    curr_row_ind = @CurrRowInd,
                    pattern1 = @Pattern1,
                    pattern2 = @Pattern2,
                    pattern3 = @Pattern3,
                    lad_ind = @LadInd,
                    join_dups_ind = @JoinDupsInd
                WHERE tbl_col_confgrtn_id = @TblColConfgrtnId AND tbl_confgrtn_id = 11";
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            await conn.ExecuteAsync(sql, tblColConfig);
        }
    }

    public async Task Delete(int tblColConfigId, int tblConfigId)
    {
        string sql = @"
            DELETE FROM codebotmstr.tbl_col_confgrtn
            WHERE tbl_col_confgrtn_id = @tblColConfigId
            AND tbl_confgrtn_id = @tblConfigId;
        ";

        var parameters = new { tblColConfigId = tblColConfigId, tblConfigId = tblConfigId };

        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            await conn.ExecuteAsync(sql, parameters);
        }
    }
}