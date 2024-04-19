using Barion.Balance.UseCases.Base;
using Barion.Balance.UseCases.Holds.Dtos;

namespace Barion.Balance.UseCases.Holds.Interfaces;

public interface IHoldUseCase : IBaseUseCase
{
    Task HoldWithSelectedPaymentMethod(HoldWithSelectedPaymentMethod dto, CancellationToken cancellationToken = default);
}