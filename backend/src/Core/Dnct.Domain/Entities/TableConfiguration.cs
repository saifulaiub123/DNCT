using System.ComponentModel.DataAnnotations.Schema;

[Table("tbl_confgrtn")]
public class TableConfiguration
{
    [Column("tbl_confgrtn_id")]
    public int? TblConfgrtnId { get; set; }

    [Column("instnc_name")]
    public string InstncName { get; set; }

    [Column("instnc_use_type")]
    public string InstncUseType { get; set; }

    [Column("datbs_src_id")]
    public int? DatbsSrcId { get; set; }

    [Column("queryband")]
    public string Queryband { get; set; }

    [Column("adtnl_wher_condtns")]
    public string AdtnlWherCondtns { get; set; }

    [Column("sql_to_use_for_sel")]
    public string SqlToUseForSel { get; set; }

    [Column("dml_action_type")]
    public string DmlActionType { get; set; }

    [Column("trunct_tbl_befr_load")]
    public char? TrunctTblBefrLoad { get; set; }

    [Column("setl_setup_name")]
    public string SetlSetupName { get; set; }

    [Column("dedup_by_colmns")]
    public string DedupByColmns { get; set; }

    [Column("objct_als")]
    public string ObjctAls { get; set; }

    [Column("type2_colmns")]
    public string Type2Colmns { get; set; }

    [Column("delt_row_idntfctn")]
    public string DeltRowIdntfctn { get; set; }

    [Column("estmtd_tbl_siz")]
    public int? EstmtdTblSiz { get; set; }

    [Column("objct_natr")]
    public string ObjctNatr { get; set; }

    [Column("partition_clause")]
    public string PartitionClause { get; set; }

    [Column("partition_colmns")]
    public string PartitionColmns { get; set; }

    [Column("target_objc_con_name")]
    public string TargetObjcConName { get; set; }

    [Column("dedup_logic")]
    public string DedupLogic { get; set; }

    [Column("pk_colmns")]
    public string PkColmns { get; set; }

    [Column("trunct_tbl_aftr_load")]
    public char? TrunctTblAftrLoad { get; set; }

    [Column("years_of_history")]
    public int? YearsOfHistory { get; set; }

    [Column("src_patrn_id")]
    public int? SrcPatrnId { get; set; }

    [Column("confgrtn_eff_start_ts")]
    public DateTime? ConfgrtnEffStartTs { get; set; }

    [Column("confgrtn_eff_end_ts")]
    public DateTime? ConfgrtnEffEndTs { get; set; }
}
