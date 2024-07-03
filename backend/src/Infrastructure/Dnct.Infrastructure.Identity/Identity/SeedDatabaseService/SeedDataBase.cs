using Dnct.Domain.Entities.User;
using Dnct.Infrastructure.Identity.Identity.Manager;
using Microsoft.EntityFrameworkCore;

namespace Dnct.Infrastructure.Identity.Identity.SeedDatabaseService;

public interface ISeedDataBase
{
    Task Seed();
}

public class SeedDataBase : ISeedDataBase
{
    private readonly AppUserManager _userManager;
    private readonly AppRoleManager _roleManager;

    public SeedDataBase(AppUserManager userManager, AppRoleManager roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Seed()
    {
        if (!_roleManager.Roles.AsNoTracking().Any(r => r.Name.Equals("admin")))
        {
            var role=new Role
            {
                Name = "admin",
            };
            await _roleManager.CreateAsync(role);
            role = new Role
            {
                Name = "user",
            };
            await _roleManager.CreateAsync(role);

        }

        if (!_userManager.Users.AsNoTracking().Any(u => u.UserName.Equals("admin")))
        {
            var user = new User
            {
                UserName = "admin@site.com",
                Email = "admin@site.com",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true
            };

            await  _userManager.CreateAsync(user, "Pass@123");
            await _userManager.AddToRoleAsync(user,"admin");
        }
    }
}