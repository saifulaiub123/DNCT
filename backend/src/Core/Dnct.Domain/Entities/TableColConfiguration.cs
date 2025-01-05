using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Dnct.Domain.Entities
{
    [Table("tbl_col_confgrtn")]
    public class TableColConfiguration
    {
        [Key]
        [Column("tbl_col_confgrtn_id")]
        public int TblColConfgrtnId { get; set; }

        [Required]
        [Column("tbl_confgrtn_id")]
        public int TblConfgrtnId { get; set; }

        [Required]
        [Column("colmn_name")]
        [MaxLength(100)]
        public string ColmnName { get; set; }

        [Column("data_type")]
        public string DataType { get; set; }

        [Column("colmn_trnsfrmtn_step1")]
        public string ColmnTrnsfrmtnStep1 { get; set; }

        [Column("genrt_id_ind", TypeName = "bpchar(1)")]
        public char? GenrtIdInd { get; set; }

        [Column("id_genrtn_stratgy_id")]
        public short? IdGenrtnStratgyId { get; set; }

        [Column("type2_start_ind")]
        public short? Type2StartInd { get; set; }

        [Column("type2_end_ind")]
        public short? Type2EndInd { get; set; }

        [Column("curr_row_ind")]
        public short? CurrRowInd { get; set; }

        [Column("pattern1")]
        [MaxLength(1000)]
        public string Pattern1 { get; set; }

        [Column("pattern2")]
        [MaxLength(1000)]
        public string Pattern2 { get; set; }

        [Column("pattern3", TypeName = "bpchar(1)")]
        public char? Pattern3 { get; set; }

        [Column("lad_ind")]
        public short? LadInd { get; set; }

        [Column("join_dups_ind")]
        public short? JoinDupsInd { get; set; }

        [Column("confgrtn_eff_start_ts")]
        public DateTime? ConfgrtnEffStartTs { get; set; }

        [Column("confgrtn_eff_end_ts")]
        public DateTime ConfgrtnEffEndTs { get; set; }
    }
}
