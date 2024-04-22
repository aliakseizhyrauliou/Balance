using Balance.BePaid.UseCases.Base;
using Balance.BePaid.UseCases.Common;
using Balance.BePaid.UseCases.Holds.Dtos;

namespace Balance.BePaid.UseCases.Holds.Interfaces;

public interface IHoldUseCases : IBaseUseCases
{
    Task<CreatedEntityDto<int>> HoldWithSelectedPaymentMethod(HoldWithSelectedPaymentMethodDto dto, CancellationToken cancellationToken = default);
}