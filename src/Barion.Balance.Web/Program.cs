using System.Reflection;
using Barion.Balance.Application;
using Barion.Balance.Infrastructure;
using Barion.Balance.Infrastructure.Data;
using Barion.Balance.UseCases;
using Barion.Balance.Web;
using Barion.Balance.Web.Infrastructure;

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