using Barion.Balance.Application;
using Barion.Balance.Infrastructure;
using Barion.Balance.Infrastructure.Data;
using Barion.Balance.Web;
using Barion.Balance.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
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