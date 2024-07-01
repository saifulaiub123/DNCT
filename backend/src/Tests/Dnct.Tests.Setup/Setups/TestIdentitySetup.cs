using Dnct.Application.Contracts;
using Dnct.Application.Contracts.Identity;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Entities.User;
using Dnct.Infrastructure.Identity.Identity;
using Dnct.Infrastructure.Identity.Identity.Dtos;
using Dnct.Infrastructure.Identity.Identity.Extensions;
using Dnct.Infrastructure.Identity.Identity.Manager;
using Dnct.Infrastructure.Identity.Identity.Store;
using Dnct.Infrastructure.Identity.Jwt;
using Dnct.Infrastructure.Identity.UserManager;
using Dnct.Infrastructure.Persistence;
using Dnct.Infrastructure.Persistence.Repositories.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dnct.Tests.Setup.Setups;

public abstract class TestIdentitySetup
{
    protected IAppUserManager TestAppUserManager { get; }
    protected AppRoleManager TestAppRoleManager { get; }
    public AppSignInManager TestSignInManager { get; }
    public IAppUserManager User { get; }
    public IJwtService JwtService { get; }

    protected TestIdentitySetup()
    {
        var serviceCollection = new ServiceCollection();

        var connection = new SqliteConnection("DataSource=:memory:");

        serviceCollection.AddLogging();

        serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection));

        var context = serviceCollection.BuildServiceProvider().GetService<ApplicationDbContext>();
        context.Database.OpenConnection();
        context.Database.EnsureCreated();


        serviceCollection.AddIdentity<User, Role>(options =>
        {
            options.Stores.ProtectPersonalData = false;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireUppercase = false;

            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = true;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = false;
            options.User.RequireUniqueEmail = false;

        }).AddUserStore<AppUserStore>()
            .AddRoleStore<RoleStore>().
            AddUserManager<AppUserManager>().
            AddRoleManager<AppRoleManager>().
            AddDefaultTokenProviders().
            AddSignInManager<AppSignInManager>()
            .AddDefaultTokenProviders()
            .AddPasswordlessLoginTotpTokenProvider();

        serviceCollection.Configure<IdentitySettings>(settings =>
        {
            settings.Audience = "Dnct.Unit.Tests";
            settings.ExpirationMinutes = 5;
            settings.Issuer = "Dnct.Unit.Tests";
            settings.NotBeforeMinutes = 0;
            settings.SecretKey = "ShouldBe-LongerThan-16Char-SecretKey";
            settings.Encryptkey = "16CharEncryptKey";
        });

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<IJwtService, JwtService>();
        serviceCollection.AddScoped<IAppUserManager, AppUserManagerImplementation>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        TestAppUserManager = serviceProvider.GetRequiredService<IAppUserManager>();
        TestAppRoleManager = serviceProvider.GetRequiredService<AppRoleManager>();
        TestSignInManager = serviceProvider.GetRequiredService<AppSignInManager>();
        JwtService = serviceProvider.GetRequiredService<IJwtService>();
    }
}
