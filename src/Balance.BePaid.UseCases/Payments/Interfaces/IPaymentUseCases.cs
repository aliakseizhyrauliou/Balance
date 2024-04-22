using Balance.BePaid.UseCases.Base;
using Balance.BePaid.UseCases.Common;
using Balance.BePaid.UseCases.Payments.Dtos;

namespace Balance.BePaid.UseCases.Payments.Interfaces;

public interface IPaymentUseCases : IBaseUseCases
{
    Task<PaymentResponseDto> PaymentWithSelectedPaymentMethod(PaymentWithSelectedPaymentMethodDto dto,
        CancellationToken cancellationToken = default);
}