using Barion.Balance.UseCases.Holds.Implementations;
using Barion.Balance.UseCases.Holds.Interfaces;
using Barion.Balance.UseCases.Payments.Implementations;
using Barion.Balance.UseCases.Payments.Interfaces;
using Barion.Balance.UseCases.PaymentSystemWidgets.Implementations;
using Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Barion.Balance.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IWidgetUseCases, WidgetUseCases>();
        serviceCollection.AddScoped<IPaymentUseCases, PaymentUseCases>();
        serviceCollection.AddScoped<IHoldUseCase, HoldUseCase>();
        
        return serviceCollection;
    }
}