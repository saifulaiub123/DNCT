using Dnct.Domain.Entities.User;
using Dnct.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dnct.Infrastructure.Identity.Identity.Store;

public class RoleStore:RoleStore<Role,ApplicationDbContext,int,UserRole,RoleClaim>
{
    public RoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
    {
    }
}