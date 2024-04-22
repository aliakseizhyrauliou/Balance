using Balance.BePaid.Application.PaymentSystemWidgets.Queries;
using Balance.BePaid.UseCases.Base;
using Balance.BePaid.UseCases.PaymentSystemWidgets.Dtos;

namespace Balance.BePaid.UseCases.PaymentSystemWidgets.Interfaces;

public interface IWidgetUseCases : IBaseUseCases
{
    Task<CheckoutDto> GenerateWidgetForCreatePaymentMethodAsync(GeneratePaymentMethodWidgetDto dto, 
        CancellationToken cancellationToken = default);
    Task<CheckoutDto> GenerateWidgetForPayment(GeneratePaymentWidgetDto command, 
        CancellationToken cancellationToken = default);
}