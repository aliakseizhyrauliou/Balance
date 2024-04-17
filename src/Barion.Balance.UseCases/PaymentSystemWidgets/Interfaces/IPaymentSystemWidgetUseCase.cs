using Barion.Balance.Application.PaymentSystemWidgetGenerations.Queries;

namespace Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;

public interface IPaymentSystemWidgetUseCase : IBaseUseCase
{
    public Task<CheckoutDto> GeneratePaymentSystemWidget(CancellationToken cancellationToken = default);
}