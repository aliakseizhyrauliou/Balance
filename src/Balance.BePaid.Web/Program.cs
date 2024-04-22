using System.Reflection;
using Balance.BePaid.Application;
using Balance.BePaid.Infrastructure;
using Balance.BePaid.Infrastructure.Data;
using Balance.BePaid.UseCases;
using Balance.BePaid.Web;
using Balance.BePaid.Web.Infrastructure;
using Barion.Balance.Infrastructure;
using Barion.Balance.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddUseCases()
    .AddWebServices()
    .AddControllers();

builder.AddAuth();

var app = builder.Build();

await app.InitialiseDatabaseAsync();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();