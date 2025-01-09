using Dnct.Domain.Constant;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dnct.Domain.Entities;

namespace Dnct.Infrastructure.Persistence.Configuration
{
    public class DatabaseSourcesConfig : IEntityTypeConfiguration<DatabaseSources>
    {
        public void Configure(EntityTypeBuilder<DatabaseSources> builder)
        {
            builder.ToTable("datbs_srcs", DbConst.Codebotmstr);
            builder.HasKey(x => new { x.DatbsSrcId, x.ConfgrtnEffEndTs });
        }
    }
}
