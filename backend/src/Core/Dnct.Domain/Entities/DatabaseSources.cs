using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dnct.Domain.Entities
{
    [Table("datbs_srcs")]
    public class DatabaseSources
    {
        [Key]
        [Column("datbs_src_id")] 
        public int DatbsSrcId { get; set; }

        [Column("usrname")] 
        public string Username { get; set; }

        [Column("repstry_name")] 
        public string RepositoryName { get; set; }

        [Column("conctn_name")] 
        public string ConnectionName { get; set; }

        [Column("tbl_dbs_name")] 
        public string TableDbsName { get; set; }

        [Column("tbl_name")] 
        public string TableName { get; set; }

        [Column("tabl_kind")] 
        public string TableKind { get; set; }

        [Column("sql_to_use")] 
        public string SqlToUse { get; set; }

        [Column("queryband")] 
        public string QueryBand { get; set; }

        [Column("adtnl_wher_condtns")] 
        public string AdditionalWhereConditions { get; set; }

        [Column("trunct_tbl_befr_load")] 
        public char? TruncateTableBeforeLoad { get; set; }

        [Column("setl_setup_name")]
        public string SetupName { get; set; }

        [Column("objct_als")] 
        public string ObjectAlias { get; set; }

        [Column("dedup_by_colmns")] 
        public string DedupByColumns { get; set; }

        [Column("delt_row_idntfctn")] 
        public string DeltaRowIdentification { get; set; }

        [Column("estmtd_tbl_siz")] 
        public int? EstimatedTableSize { get; set; }

        [Column("type2_colmns")] 
        public string Type2Columns { get; set; }

        [Column("objct_natr")] 
        public string ObjectNature { get; set; }

        [Column("odbc_typ")] 
        public string OdbcType { get; set; }

        [Column("target_objc_con_name")]
        public string TargetObjectConnectionName { get; set; }

        [Column("partition_clause")]
        public string PartitionClause { get; set; }

        [Column("partition_colmns")]
        public string PartitionColumns { get; set; }

        [Column("sorc_targt_file_id")]
        public int? SourceTargetFileId { get; set; }

        [Column("dedup_logic")]
        public string DedupLogic { get; set; }

        [Column("pk_colmns")]
        public string PrimaryKeyColumns { get; set; }

        [Column("trunct_tbl_aftr_load")]
        public char? TruncateTableAfterLoad { get; set; }

        [Column("years_of_history")]
        public int? YearsOfHistory { get; set; }

        [Column("confgrtn_eff_start_ts")]
        public DateTime? ConfigurationEffectiveStartTime { get; set; }
        [Column("confgrtn_eff_end_ts")]
        public DateTime ConfigurationEffectiveEndTime { get; set; }
    }
}

