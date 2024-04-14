using System.Reflection;
using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Infrastructure.Data;
using Barion.Balance.Web.Infrastructure;
using Barion.Balance.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        
        services.AddHealthChecks()
            .AddDbContextCheck<BalanceDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();
        
        services.AddScoped<IUser, CurrentUserMock>();
        services.AddHttpClient();
        
        services.AddHttpContextAccessor();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            var xmlFilename = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
            var xmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), xmlFilename);

            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}