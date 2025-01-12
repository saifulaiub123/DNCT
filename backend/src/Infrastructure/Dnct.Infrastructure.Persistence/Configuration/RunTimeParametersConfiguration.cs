using Dnct.Domain.Constant;
using Dnct.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Dnct.Infrastructure.Persistence.Configuration
{
    public class RunTimeParametersConfiguration : IEntityTypeConfiguration<RunTimeParameters>
    {
        public void Configure(EntityTypeBuilder<RunTimeParameters> builder)
        {
            builder.ToTable("run_time_parmtrs", DbConst.Codebotmstr);
            builder.HasNoKey();
        }
    }
}
