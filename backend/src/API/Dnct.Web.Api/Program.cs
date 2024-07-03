using System.Diagnostics;
using Carter;
using Dnct.Application.ServiceConfiguration;
using Dnct.Domain.Entities.User;
using Dnct.Infrastructure.CrossCutting.Logging;
using Dnct.Infrastructure.Identity.Identity.Dtos;
using Dnct.Infrastructure.Identity.Jwt;
using Dnct.Infrastructure.Identity.ServiceConfiguration;
using Dnct.Infrastructure.Persistence.ServiceConfiguration;
using Dnct.SharedKernel.Extensions;
using Dnct.Web.Api.Controllers.V1.IdentityManagement;
using Dnct.Web.Plugins.Grpc;
using Dnct.WebFramework.Filters;
using Dnct.WebFramework.Middlewares;
using Dnct.WebFramework.ServiceConfiguration;
using Dnct.WebFramework.Swagger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(LoggingConfiguration.ConfigureLogger);

var configuration = builder.Configuration;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

builder.Services.Configure<IdentitySettings>(configuration.GetSection(nameof(IdentitySettings)));

var identitySettings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(OkResultAttribute));
    options.Filters.Add(typeof(NotFoundResultAttribute));
    options.Filters.Add(typeof(ContentResultFilterAttribute));
    options.Filters.Add(typeof(ModelStateValidationAttribute));
    options.Filters.Add(typeof(BadRequestResultFilterAttribute));

}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressMapClientErrors = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddCarter(configurator: configurator => { configurator.WithEmptyValidators();});

builder.Services.AddApplicationServices()
    .RegisterIdentityServices(identitySettings)
    .AddPersistenceServices(configuration)
    .AddWebFrameworkServices();

builder.Services.RegisterValidatorsAsServices();
builder.Services.AddExceptionHandler<ExceptionHandler>();


#region Plugin Services Configuration

builder.Services.ConfigureGrpcPluginServices();

#endregion

builder.Services.AddAutoMapper(expression =>
{
    expression.AddMaps(typeof(User), typeof(JwtService), typeof(IdentityController));
});

var app = builder.Build();


await app.ApplyMigrationsAsync();
await app.SeedDefaultUsersAsync();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(_=>{});
app.UseSwaggerAndUI();

app.MapCarter();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.ConfigureGrpcPipeline();

await app.RunAsync();



