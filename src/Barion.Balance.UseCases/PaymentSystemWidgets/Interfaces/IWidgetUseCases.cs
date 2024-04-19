using Barion.Balance.Application.PaymentSystemWidgets.Queries;
using Barion.Balance.UseCases.Base;
using Barion.Balance.UseCases.PaymentSystemWidgets.Dtos;

namespace Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;

public interface IWidgetUseCases : IBaseUseCases
{
    Task<CheckoutDto> GenerateWidgetForCreatePaymentMethodAsync(GeneratePaymentMethodWidgetDto dto, 
        CancellationToken cancellationToken = default);
    Task<CheckoutDto> GenerateWidgetForPayment(GeneratePaymentWidgetDto command, 
        CancellationToken cancellationToken = default);
}