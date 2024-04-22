using Balance.BePaid.UseCases.Holds.Implementations;
using Balance.BePaid.UseCases.Holds.Interfaces;
using Balance.BePaid.UseCases.Payments.Implementations;
using Balance.BePaid.UseCases.Payments.Interfaces;
using Balance.BePaid.UseCases.PaymentSystemWidgets.Implementations;
using Balance.BePaid.UseCases.PaymentSystemWidgets.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Balance.BePaid.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IWidgetUseCases, WidgetUseCases>();
        serviceCollection.AddScoped<IPaymentUseCases, PaymentUseCases>();
        serviceCollection.AddScoped<IHoldUseCases, HoldUseCases>();
        
        return serviceCollection;
    }
}