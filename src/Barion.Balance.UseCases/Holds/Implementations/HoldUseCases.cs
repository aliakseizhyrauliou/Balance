using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Holds.Commands;
using Barion.Balance.Application.PaymentMethods.Queries;
using Barion.Balance.UseCases.Base;
using Barion.Balance.UseCases.Common;
using Barion.Balance.UseCases.Holds.Dtos;
using Barion.Balance.UseCases.Holds.Interfaces;
using MediatR;

namespace Barion.Balance.UseCases.Holds.Implementations;

public class HoldUseCases(IMediator mediator, IUser currentUser) : BaseUseCases(mediator, currentUser), IHoldUseCases
{
    public async Task<CreatedEntityDto<int>> HoldWithSelectedPaymentMethod(HoldWithSelectedPaymentMethodDto dto, CancellationToken cancellationToken = default)
    {
        var selectedMethod = await mediator.Send(new GetSelectedPaymentMethodByUserIdQuery
        {
            UserId = currentUser.Id
        }, cancellationToken);

        var createdHold = await mediator.Send(new MakeHoldCommand
        {
            UserId = currentUser.Id,
            PaidResourceId = dto.PaidResourceId,
            OperatorId = dto.OperatorId,
            Amount = dto.Amount,
            PaidResourceTypeId = dto.PaidResourceTypeId,
            PaymentMethodId = selectedMethod.Id,
            AdditionalData = dto.AdditionalData,
            PaymentSystemConfigurationId = dto.PaymentSystemConfigurationId
        }, cancellationToken);

        return new CreatedEntityDto<int>(createdHold);
    }
}