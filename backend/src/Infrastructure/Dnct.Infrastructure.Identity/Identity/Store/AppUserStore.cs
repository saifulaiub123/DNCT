using Dnct.Domain.Entities.User;
using Dnct.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dnct.Infrastructure.Identity.Identity.Store;

public class AppUserStore:UserStore<User,Role,ApplicationDbContext,int,UserClaim,UserRole,UserLogin,UserToken,RoleClaim>
{
    public AppUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
    {
    }
}