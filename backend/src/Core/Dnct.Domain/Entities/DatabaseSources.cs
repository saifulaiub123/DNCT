using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dnct.Domain.Entities
{
    public class DatabaseSources
    {
        [Key]
        [Column("datbs_src_id")]
        public int DatbsSrcId { get; set; }

        [Column("usrname")]
        public string Usrname { get; set; }

        [Column("repstry_name")]
        public string RepstryName { get; set; }

        [Column("conctn_name")]
        public string ConctnName { get; set; }

        [Column("tbl_dbs_name")]
        public string TblDbsName { get; set; }

        [Column("tbl_name")]
        public string TblName { get; set; }

        [Column("tabl_kind")]
        public string TablKind { get; set; }

        [Column("sql_to_use")]
        public string SqlToUse { get; set; }

        [Column("queryband")]
        public string Queryband { get; set; }

        [Column("adtnl_wher_condtns")]
        public string AdtnlWherCondtns { get; set; }

        [Column("trunct_tbl_befr_load")]
        public char? TrunctTblBefrLoad { get; set; }

        [Column("setl_setup_name")]
        public string SetlSetupName { get; set; }

        [Column("objct_als")]
        public string ObjctAls { get; set; }

        [Column("dedup_by_colmns")]
        public string DedupByColmns { get; set; }

        [Column("delt_row_idntfctn")]
        public string DeltRowIdntfctn { get; set; }

        [Column("estmtd_tbl_siz")]
        public int? EstmtdTblSiz { get; set; }

        [Column("type2_colmns")]
        public string Type2Colmns { get; set; }

        [Column("objct_natr")]
        public string ObjctNatr { get; set; }

        [Column("odbc_typ")]
        public string OdbcTyp { get; set; }

        [Column("target_objc_con_name")]
        public string TargetObjcConName { get; set; }

        [Column("partition_clause")]
        public string PartitionClause { get; set; }

        [Column("partition_colmns")]
        public string PartitionColmns { get; set; }

        [Column("sorc_targt_file_id")]
        public int? SorcTargtFileId { get; set; }

        [Column("dedup_logic")]
        public string DedupLogic { get; set; }

        [Column("pk_colmns")]
        public string PkColmns { get; set; }

        [Column("trunct_tbl_aftr_load")]
        public char? TrunctTblAftrLoad { get; set; }

        [Column("years_of_history")]
        public int? YearsOfHistory { get; set; }

        [Column("confgrtn_eff_start_ts")]
        public DateTime? ConfgrtnEffStartTs { get; set; }

        [Column("confgrtn_eff_end_ts")]
        public DateTime ConfgrtnEffEndTs { get; set; }
    }
}

