using Barion.Balance.UseCases.Base;
using Barion.Balance.UseCases.Common;
using Barion.Balance.UseCases.Payments.Dtos;

namespace Barion.Balance.UseCases.Payments.Interfaces;

public interface IPaymentUseCases : IBaseUseCases
{
    Task<CreatedEntityDto<int>> PaymentWithSelectedPaymentMethod(PaymentWithSelectedPaymentMethodDto dto,
        CancellationToken cancellationToken = default);
}