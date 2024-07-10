

using Dnct.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dnct.Infrastructure.Persistence.Configuration
{
    public class ConnectionsMasterConfig : IEntityTypeConfiguration<ConnectionsMaster>
    {
        public void Configure(EntityTypeBuilder<ConnectionsMaster> builder)
        {
            builder.ToTable("contns_mstr", "public");
            builder.HasKey(x => x.ContnId);
        }
    }
}

