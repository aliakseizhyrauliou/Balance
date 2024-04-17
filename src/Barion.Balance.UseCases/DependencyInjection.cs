using Barion.Balance.UseCases.PaymentSystemWidgets.Implementations;
using Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Barion.Balance.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPaymentSystemWidgetUseCase, PaymentSystemWidgetUseCase>();
        
        return serviceCollection;
    }
}