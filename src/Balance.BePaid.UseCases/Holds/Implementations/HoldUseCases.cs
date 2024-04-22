using Balance.BePaid.Application.Common.Interfaces;
using Balance.BePaid.Application.Holds.Commands;
using Balance.BePaid.Application.PaymentMethods.Queries;
using Balance.BePaid.UseCases.Base;
using Balance.BePaid.UseCases.Common;
using Balance.BePaid.UseCases.Holds.Dtos;
using Balance.BePaid.UseCases.Holds.Interfaces;
using MediatR;

namespace Balance.BePaid.UseCases.Holds.Implementations;

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