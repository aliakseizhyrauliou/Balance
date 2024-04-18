using Barion.Balance.Application.PaymentSystemWidgetGenerations.Queries;
using Barion.Balance.UseCases.PaymentSystemWidgets.Dtos;

namespace Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;

public interface IWidgetUseCase : IBaseUseCase
{
    Task<CheckoutDto> GenerateWidgetForCreatePaymentMethodAsync(GeneratePaymentMethodWidgetDto dto, 
        CancellationToken cancellationToken = default);
    Task<CheckoutDto> GenerateWidgetForPayment(GeneratePaymentWidgetDto command, 
        CancellationToken cancellationToken = default);
}