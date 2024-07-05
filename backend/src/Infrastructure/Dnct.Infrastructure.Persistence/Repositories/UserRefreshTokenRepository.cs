using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Entities.User;
using Dnct.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Dnct.Infrastructure.Persistence.Repositories;

internal class UserRefreshTokenRepository : BaseAsyncRepository<UserRefreshToken>, IUserRefreshTokenRepository
{
    public UserRefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Guid> CreateToken(int userId)
    {
        try
        {
            var token = new UserRefreshToken { IsValid = true, UserId = userId };
            await base.AddAsync(token);
            return token.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<UserRefreshToken> GetTokenWithInvalidation(Guid id)
    {
        var token = await base.Table.Where(t => t.IsValid && t.Id.Equals(id)).FirstOrDefaultAsync();

        return token;
    }

    public async Task<User> GetUserByRefreshToken(Guid tokenId)
    {
        var user = await base.TableNoTracking.Include(t => t.User).Where(c => c.Id.Equals(tokenId))
            .Select(c => c.User).FirstOrDefaultAsync();

        return user;
    }

    public Task RemoveUserOldTokens(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}