using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dnct.Infrastructure.Persistence.Configuration
{
    public class TableColConfigurationConfig : IEntityTypeConfiguration<TableColConfiguration>
    {
        public void Configure(EntityTypeBuilder<TableColConfiguration> builder)
        {
            // Table Mapping
            builder.ToTable("tbl_col_confgrtn", DbConst.SchemaDbo);

            // Primary Key
            builder.HasKey(t => t.TblColConfgrtnId);

            // Properties Mapping
            builder.Property(t => t.TblColConfgrtnId)
                .HasColumnName("tbl_col_confgrtn_id")
                .IsRequired();

            builder.Property(t => t.TblConfgrtnId)
                .HasColumnName("tbl_confgrtn_id")
                .IsRequired();

            builder.Property(t => t.ColmnName)
                .HasColumnName("colmn_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.DataType)
                .HasColumnName("data_type");

            builder.Property(t => t.ColmnTrnsfrmtnStep1)
                .HasColumnName("colmn_trnsfrmtn_step1");

            builder.Property(t => t.GenrtIdInd)
                .HasColumnName("genrt_id_ind")
                .HasColumnType("bpchar(1)");

            builder.Property(t => t.IdGenrtnStratgyId)
                .HasColumnName("id_genrtn_stratgy_id");

            builder.Property(t => t.Type2StartInd)
                .HasColumnName("type2_start_ind");

            builder.Property(t => t.Type2EndInd)
                .HasColumnName("type2_end_ind");

            builder.Property(t => t.CurrRowInd)
                .HasColumnName("curr_row_ind");

            builder.Property(t => t.Pattern1)
                .HasColumnName("pattern1")
                .HasMaxLength(1000);

            builder.Property(t => t.Pattern2)
                .HasColumnName("pattern2")
                .HasMaxLength(1000);

            builder.Property(t => t.Pattern3)
                .HasColumnName("pattern3")
                .HasColumnType("bpchar(1)");

            builder.Property(t => t.LadInd)
                .HasColumnName("lad_ind");

            builder.Property(t => t.JoinDupsInd)
                .HasColumnName("join_dups_ind");

            builder.Property(t => t.ConfgrtnEffStartTs)
                .HasColumnName("confgrtn_eff_start_ts");

            builder.Property(t => t.ConfgrtnEffEndTs)
                .HasColumnName("confgrtn_eff_end_ts")
                .IsRequired();
        }
    }
}
