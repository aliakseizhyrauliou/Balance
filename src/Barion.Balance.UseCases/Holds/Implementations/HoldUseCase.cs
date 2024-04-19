using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Holds.Commands;
using Barion.Balance.Application.PaymentMethods.Queries;
using Barion.Balance.UseCases.Base;
using Barion.Balance.UseCases.Holds.Dtos;
using Barion.Balance.UseCases.Holds.Interfaces;
using MediatR;

namespace Barion.Balance.UseCases.Holds.Implementations;

public class HoldUseCase(IMediator mediator, IUser currentUser) : BaseUseCase(mediator, currentUser), IHoldUseCase
{
    public async Task HoldWithSelectedPaymentMethod(HoldWithSelectedPaymentMethod dto, CancellationToken cancellationToken = default)
    {
        var selectedMethod = await mediator.Send(new GetSelectedPaymentMethodByUserIdQuery
        {
            UserId = currentUser.Id
        }, cancellationToken);

        await mediator.Send(new MakeHoldCommand
        {
            UserId = currentUser.Id,
            PaidResourceId = dto.PaidResourceId,
            OperatorId = dto.OperatorId,
            Amount = dto.Amount,
            PaidResourceTypeId = dto.PaidResourceTypeId,
            PaymentMethodId = selectedMethod.Id,
            AdditionalData = dto.AdditionalData
        }, cancellationToken);
    }
}