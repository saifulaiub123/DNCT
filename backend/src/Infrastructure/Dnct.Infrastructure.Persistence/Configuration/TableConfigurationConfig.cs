using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Dnct.Infrastructure.Persistence.Configuration
{
    internal class TableConfigurationConfig : IEntityTypeConfiguration<TableConfiguration>
    {
        public void Configure(EntityTypeBuilder<TableConfiguration> builder)
        {
            builder.ToTable("tbl_confgrtn", "codebotmstr");
            builder.HasKey(x => new { x.TblConfgrtnId, x.ConfgrtnEffStartTs });
        }
    }
}
