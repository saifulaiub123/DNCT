using Dnct.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dnct.Domain.Constant;

namespace Dnct.Infrastructure.Persistence.Configuration
{
    public class TblLoadStrategyConfiguration : IEntityTypeConfiguration<TblLoadStrategy>
    {
        public void Configure(EntityTypeBuilder<TblLoadStrategy> builder)
        {
            // Map to the table
            builder.ToTable("tbl_load_strategy", DbConst.Codebotmstr);
            builder.HasNoKey();

            // Configure properties
            builder.Property(e => e.TableConfigId)
                .HasColumnName("table_config_id");

            builder.Property(e => e.LoadStrategyId)
                .HasColumnName("load_stratgy_id");

            builder.Property(e => e.ConfigurationEffectiveStartTimestamp)
                .HasColumnName("confgrtn_eff_start_ts")
                .HasColumnType("timestamp");

            builder.Property(e => e.ConfigurationEffectiveEndTimestamp)
                .HasColumnName("confgrtn_eff_end_ts")
                .HasColumnType("timestamp")
                .IsRequired(); // NOT NULL constraint
        }
    }
}