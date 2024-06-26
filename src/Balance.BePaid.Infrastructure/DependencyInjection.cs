using Ardalis.GuardClauses;
using Balance.BePaid.Application.Common.Interfaces;
using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Services;
using Balance.BePaid.Infrastructure.Data;
using Balance.BePaid.Infrastructure.Data.Interceptors;
using Balance.BePaid.Infrastructure.Data.Repositories;
using Balance.BePaid.Infrastructure.External.BePaid.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BePaidService = Balance.BePaid.Infrastructure.External.BePaid.Services.BePaidService;
using IPaymentMethodRepository = Balance.BePaid.Application.Common.Repositories.IPaymentMethodRepository;

namespace Balance.BePaid.Infrastructure;

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
        services.AddServices();

        return services;
    }


    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentSystemService, BePaidService>();
        services.AddScoped<IPaymentSystemAuthorizationService, BePaidAuthorizationService>();
        services.AddScoped<IPaymentSystemConfigurationService, BePaidConfigurationService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IHoldRepository, HoldRepository>();
        services.AddScoped<IReceiptRepository, ReceiptRepository>();
        services.AddScoped<IPaidResourceTypeRepository, PaidResourceTypeRepository>();
        services.AddScoped<IPaymentSystemWidgetGenerationRepository, PaymentSystemWidgetGenerationRepository>();
        services.AddScoped<IPaymentSystemConfigurationRepository, PaymentSystemConfigurationRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IDebtorRepository, DebtorRepository>();
    }
}