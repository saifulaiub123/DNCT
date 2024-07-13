using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Constant;
using Dnct.Infrastructure.Persistence.Repositories;
using Dnct.Infrastructure.Persistence.Repositories.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dnct.Infrastructure.Persistence.ServiceConfiguration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDatabaseSourcesRepository, DatabaseSourcesRepository>();
        services.AddScoped<IConnectionMasterRepository, ConnectionMasterRepository>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString(DbConst.DbConnectionName), x => x.MigrationsHistoryTable("__EFMigrationsHistory", "dbo"));
            
        });

        return services;
    }

    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        //await using var scope = app.Services.CreateAsyncScope();
        //var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        //if (context is null)
        //    throw new Exception("Database Context Not Found");

        //await context.Database.MigrateAsync();
    }
}