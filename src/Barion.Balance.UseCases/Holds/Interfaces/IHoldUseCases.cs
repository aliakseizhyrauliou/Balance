using Barion.Balance.UseCases.Base;
using Barion.Balance.UseCases.Common;
using Barion.Balance.UseCases.Holds.Dtos;

namespace Barion.Balance.UseCases.Holds.Interfaces;

public interface IHoldUseCases : IBaseUseCases
{
    Task<CreatedEntityDto<int>> HoldWithSelectedPaymentMethod(HoldWithSelectedPaymentMethodDto dto, CancellationToken cancellationToken = default);
}