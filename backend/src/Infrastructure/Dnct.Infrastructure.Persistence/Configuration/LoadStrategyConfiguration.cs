using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dnct.Infrastructure.Persistence.Configuration
{
    public class LoadStrategyConfiguration : IEntityTypeConfiguration<LoadStrategy>
    {
        public void Configure(EntityTypeBuilder<LoadStrategy> builder)
        {
            // Table Mapping
            builder.ToTable("load_strategy", DbConst.Codebotmstr);
            builder.HasNoKey();

            // Properties Mapping
            builder.Property(t => t.LoadStrategyName)
                .HasColumnName("load_stratgy_name")
                .HasMaxLength(500);

            builder.Property(t => t.LoadStrategyDescription)
                .HasColumnName("load_stratgy_description")
                .HasMaxLength(900);
        }
    }
}
