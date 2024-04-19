using Barion.Balance.UseCases.Base;
using Barion.Balance.UseCases.Payments.Dtos;
using Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;

namespace Barion.Balance.UseCases.Payments.Interfaces;

public interface IPaymentUseCases : IBaseUseCase
{
    Task PaymentWithSelectedPaymentMethod(PaymentWithSelectedPaymentMethodDto dto,
        CancellationToken cancellationToken = default);
}