using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dnct.Infrastructure.Persistence.Configuration
{
    public class RunTimeParametersMasterConfiguration : IEntityTypeConfiguration<RunTimeParametersMaster>
    {
        public void Configure(EntityTypeBuilder<RunTimeParametersMaster> builder)
        {
            builder.ToTable("run_time_parmtrs_mstr", DbConst.Codebotmstr);
            builder.HasNoKey();
            builder.Property(e => e.ParameterKey)
                .HasMaxLength(1000);
        }
    }
}
