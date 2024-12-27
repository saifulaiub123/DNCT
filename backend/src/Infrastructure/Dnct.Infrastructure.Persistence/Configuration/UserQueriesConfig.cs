using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dnct.Domain.Entities;

namespace Dnct.Infrastructure.Persistence.Configuration
{
    public class UserQueriesConfig : IEntityTypeConfiguration<UserQueries>
    {
        public void Configure(EntityTypeBuilder<UserQueries> builder)
        {
            builder.ToTable("usr_queries", "codebotmstr");
            builder.HasKey(x => new { x.UserQueryId });
        }
    }
}
