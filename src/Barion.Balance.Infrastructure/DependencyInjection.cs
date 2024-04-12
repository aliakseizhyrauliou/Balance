using Ardalis.GuardClauses;
using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Services;
using Barion.Balance.Infrastructure.Data;
using Barion.Balance.Infrastructure.Data.Interceptors;
using Barion.Balance.Infrastructure.Data.Repositories;
using Barion.Balance.Infrastructure.External.BePaid;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IPaymentMethodRepository = Barion.Balance.Application.Common.Repositories.IPaymentMethodRepository;

namespace Barion.Balance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Npgsql");
        
        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<BalanceDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IBalanceDbContext>(provider => provider.GetRequiredService<BalanceDbContext>());
        services.AddScoped<IPaymentSystemConfigurationService, BePaidConfigurationService>();
        
        services.AddScoped<ApplicationDbContextInitialiser>();
        
        services.AddSingleton(TimeProvider.System);

        services.AddRepositories();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPaymentSystemConfigurationRepository, PaymentSystemConfigurationRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
    }
}