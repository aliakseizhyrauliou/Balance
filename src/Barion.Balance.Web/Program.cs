using System.Reflection;
using Barion.Balance.Application;
using Barion.Balance.Infrastructure;
using Barion.Balance.Infrastructure.Data;
using Barion.Balance.Web;
using Barion.Balance.Web.Infrastructure;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddWebServices()
    .AddControllers();
        /*.AddNewtonsoftJson(options =>
           options.SerializerSettings.ContractResolver =
              new CamelCasePropertyNamesContractResolver());*/

builder.AddAuth();

var app = builder.Build();

await app.InitialiseDatabaseAsync();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();