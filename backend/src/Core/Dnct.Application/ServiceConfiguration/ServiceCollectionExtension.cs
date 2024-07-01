using System.Reflection;
using Dnct.Application.Common;
using Dnct.SharedKernel.Extensions;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Dnct.Application.ServiceConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.Namespace = "Dnct.Application.Mediator";
        });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
        //services.AddMediator(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(expression =>
        {
            expression.AddMaps(Assembly.GetExecutingAssembly());
        });


        return services;
    }

   
}